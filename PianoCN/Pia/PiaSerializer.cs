//
//  Copyright © 2015 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PiaNO //串行化
{
    /// <summary>
    /// 串行器
    /// </summary>
    public static class PiaSerializer
    {
        /// <summary>
        /// 当前文件的信息,修改整段内容的时候用到
        /// </summary>
        public static PiaFile PiaFile;

        /// <summary>
        /// 反序列化解压
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="piaFile"></param>
        public static void Deserialize(this PiaFile piaFile, Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("Stream");

            if (piaFile == null)
                throw new ArgumentNullException("PiaFile");

            try
            {
                //头信息
                var headerBytes = new byte[48]; // Ignore 12 byte checksum 有12个是校验信息,所以不解压
                stream.Read(headerBytes, 0, headerBytes.Length);//二进制读取这些信息
                var headerString = Encoding.Default.GetString(headerBytes);
                piaFile.Header = new PiaHeader(headerString); //头信息写到这里干什么??
                //解压 
                stream.Seek(60, SeekOrigin.Begin);//从60字节位置开始解压

                string inflatedString;
                using (var zStream = new InflaterInputStream(stream))//压缩流
                {
                    var sr = new StreamReader(zStream, Encoding.Default);//自动编码读取压缩流
                    inflatedString = sr.ReadToEnd();//读到最后
                }

                //拥有者
                piaFile.Owner = piaFile;
                PiaFile = piaFile;
                DeserializeNode(piaFile, inflatedString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 反序列化解压节点,加入InnerData的方法
        /// </summary>
        /// <param name="parent">文件主体</param>
        /// <param name="nodeString">注释</param>
        public static void DeserializeNode(this PiaNode parent, string nodeString)
        {
            if (parent == null && !(parent is PiaFile))
                throw new ArgumentNullException("parent"); //参数空异常

            if (nodeString == null)
                throw new ArgumentNullException("nodeString"); //参数空异常

            var dataLines = nodeString.Split
                (new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//删除空条目

            for (int i = 0; i < dataLines.Length; i++)
            {
                var curLine = dataLines[i];
                if (curLine.Contains('='))//有等号就获取属性
                {
                    var value = DeserializeValue(curLine);

                    if (!parent.Values.ContainsKey(value.Key))
                        parent.Values.Add(value.Key, value.Value);
                    else
                        parent.Values[value.Key] = value.Value;
                }
                else if (curLine.Contains('{'))// "meta{"
                {
                    var nodeBuilder = GetNodeInnerData(dataLines, i, out int n);

                    //添加内部数据
                    var childNode = new PiaNode(nodeBuilder)
                    {
                        NodeName = curLine.Trim().TrimEnd('{'),//名称
                        Parent = parent,    //来源
                        Owner = parent.Owner//拥有者
                    };

                    parent.ChildNodes.Add(childNode); //每一个大括号内是一组

                    i = n - 1;
                }
            }
        }

        /// <summary>
        /// 提取节点数据
        /// </summary>
        /// <param name="nodeString">数据</param> 
        /// <returns></returns>
        public static string GetNodeInnerData(string nodeString)
        {
            var dataLines = nodeString.Split
            (new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);//删除空条目
            return GetNodeInnerData(dataLines, 0, out _);
        }

        /// <summary>
        /// 提取节点数据
        /// </summary>
        /// <param name="dataLines"></param>
        /// <param name="i"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string GetNodeInnerData(string[] dataLines, int i, out int n)
        {
            var bracketCount = 1;
            var nodeBuilder = new StringBuilder();
            n = i + 1;
            while (bracketCount != 0)
            {
                string subLine = dataLines[n++];
                bracketCount += subLine.Contains('{') ? 1 : subLine.Contains('}') ? -1 : 0;
                if (bracketCount != 0)
                    nodeBuilder.AppendLine(subLine);
            }
            return nodeBuilder.ToString();
        }

        /// <summary>
        /// 序列化值,切割等号获取值
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        private static KeyValuePair<string, string> DeserializeValue(string valueString)
        {
            var prop = valueString.TrimEnd(new char[] { '\r', '\n' }).Split('=');
            if (prop[1].StartsWith("\""))
            {
                prop[0] += "_str";
                prop[1] = prop[1].TrimStart('\"');
            }
            var sb = new StringBuilder();

            for (int i = 1; i < prop.Length; i++)//误切的等号要在这里加回去
            {
                sb.Append('=' + prop[i]);
            }
            //剔除第一个等号,就是每组=后面的值
            var str = sb.ToString().Substring(1, sb.Length - 1).Trim();

            return new KeyValuePair<string, string>(prop[0].Trim(), str);
        }

        /// <summary>
        /// 写压缩文件 串行化
        /// </summary>
        /// <param name="piaFile">被反序列化解压的压缩文件信息</param>
        /// <param name="stream">流文件</param> 
        /// <param name="IsPlotOrTxt">true保存为打印,false保存为txt</param>
        public static void Serialize(this PiaFile piaFile, Stream stream, bool IsPlotOrTxt = true)
        {
            if (piaFile == null)
                throw new ArgumentNullException("PiaFile");

            if (stream == null)
                throw new ArgumentNullException("Stream");

            if (IsPlotOrTxt)
            {
                //头信息
                var headerString = piaFile.Header.ToString();
                var headerBytes = Encoding.Default.GetBytes(headerString);
                stream.Write(headerBytes, 0, headerBytes.Length);//写入 
            }

            //节点
            var nodeString = piaFile.SerializeNode();
            var nodeBytes = Encoding.Default.GetBytes(nodeString);

            if (IsPlotOrTxt)
            {
                //压缩节点
                byte[] deflatedBytes;
                var deflater = new Deflater(Deflater.DEFAULT_COMPRESSION);//默认压缩系数
                using (var ms = new MemoryStream())//内存流
                {
                    var deflateStream = new DeflaterOutputStream(ms, deflater);//压缩流生成
                    deflateStream.Write(nodeBytes, 0, nodeBytes.Length);
                    deflateStream.Finish();

                    deflatedBytes = ms.ToArray();
                }

                //校验和
                var checkSum = new byte[12];
                BitConverter.GetBytes(deflater.Adler).CopyTo(checkSum, 0);       // Adler
                BitConverter.GetBytes(nodeBytes.Length).CopyTo(checkSum, 4);     // 压缩前字节数
                BitConverter.GetBytes(deflatedBytes.Length).CopyTo(checkSum, 8); // 压缩后字节数
                stream.Write(checkSum, 0, checkSum.Length);//写入

                // 最终写入
                stream.Write(deflatedBytes, 0, deflatedBytes.Length);
            }
            else
            {
                stream.Write(nodeBytes, 0, nodeBytes.Length);//写出txt测试
            }
            stream.Write(Encoding.Default.GetBytes("\0"), 0, 1);
        }

        /// <summary>
        /// 序列化节点(压缩)
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="level">节点数</param>
        /// <returns></returns>
        internal static string SerializeNode(this PiaNode node, int level = 0)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            var nodeBuilder = new StringBuilder();
            var whiteSpace = new string(' ', level);

            foreach (var value in node.Values)
            {
                nodeBuilder.AppendFormat("{0}{1}\n", whiteSpace, SerializeValue(value));
            }

            foreach (var child in node.ChildNodes)
            {
                nodeBuilder.AppendFormat("{0}{1}{2}\n", whiteSpace, child.NodeName, "{");
                nodeBuilder.Append(SerializeNode(child, level + 1));
                nodeBuilder.AppendFormat("{0}{1}\n", whiteSpace, "}");
            }

            return nodeBuilder.ToString();
        }

        /// <summary>
        /// 序列化值
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns></returns>
        private static string SerializeValue(KeyValuePair<string, string> value)
        {
            var valueString = $"{value.Key}={value.Value}";
            valueString = valueString.Replace("_str=", "=\"");

            return valueString;
        }

        /// <summary>
        /// 在节点上添加新节点
        /// </summary>
        /// <param name="parent">数据节点</param>
        /// <param name="nodeString">要添加的信息</param>
        public static PiaNode Add(this PiaNode parent, string name, string nodeString = null)
        {
            var childNode = new PiaNode
            {
                NodeName = name,     //名称 
                Parent = parent,     //来源
                Owner = parent.Owner //拥有者 
            };

            if (!string.IsNullOrEmpty(nodeString))
            {
                //添加内部数据
                var getNodeInnerData = GetNodeInnerData(nodeString);
                childNode.DeserializeNode(getNodeInnerData); //加入InnerData的方法
            }
            parent.ChildNodes.Add(childNode);
            return childNode;
        }

        /// <summary>
        /// 在外节点上移除内节点
        /// </summary>
        /// <param name="parent">外节点</param>
        /// <param name="name">要移除内节点名称</param>
        //public static void Remove(this PiaNode parent, string name)
        //{
        //    //读取原本节点      
        //    foreach (var nt in parent.ChildNodes)
        //    { 
        //        if (nt.NodeName == name)
        //        {
        //            parent.ChildNodes.Remove(nt);
        //            break;
        //        }
        //    }
        //}

        /// <summary>
        /// 移除纸张
        /// </summary>
        /// <param name="parent">外节点</param>
        /// <param name="name">要移除内节点名称</param>
        public static void RemoveChildNodes(this PiaNode parent, string[] name)
        {
            //读取原本节点      
            var pia = new List<PiaNode>();
            foreach (var nt in parent.ChildNodes)
            {
                foreach (var va in nt.Values)
                {
                    if (name.Contains(va.Value))
                    {
                        pia.Add(nt);
                        break;
                    }
                }
            }
            if (pia.Count != 0)
            {
                foreach (var item in pia)
                {
                    parent.ChildNodes.Remove(item);
                }
            }
        }
    }
}
