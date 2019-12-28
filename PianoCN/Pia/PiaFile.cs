//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//


using System;
using System.IO;

namespace PiaNO
{
    /// <summary>
    /// 解压类型文件
    /// </summary>
    public abstract class PiaFile : PiaNode
    {
        #region Properties

        /// <summary>
        /// 头信息
        /// </summary>
        public PiaHeader Header { get; internal set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string PiaFileName { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string PiaPath { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// Pia文件
        /// </summary>
        protected internal PiaFile() : base() { }

        /// <summary>
        /// Pia文件读取
        /// </summary>
        /// <param name="filePath">文件路径</param>
        protected PiaFile(string path) : base()
        {
            PiaPath = path;
            Read();
        }

        #endregion

        #region 函数

        /// <summary>
        /// 读取 初始化
        /// </summary>
        public void Read(string path)
        {
            PiaFileName = path;
            Read();
        }

        /// <summary>
        /// 读文件
        /// </summary> 
        void Read()
        {
            if (string.IsNullOrEmpty(PiaPath))
                throw new ArgumentNullException("path");

            if (!File.Exists(PiaPath))
                throw new FileNotFoundException("Plot file not found path", PiaPath);

            try
            {
                PiaFileName = Path.GetFileName(PiaPath);//获取 文件名
                using (var inStream = new FileStream(PiaPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))//文件流
                {
                    this.Deserialize(inStream);//反序列化解压 解压执行
                    inStream.Close();//关闭流
                }
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("读文件失败:" + e.Message);
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">保存的路径</param>
        /// <param name="IsPlotOrTxt">true保存为打印机/false保存为文本</param>
        public void Saves(string path = null, bool IsPlotOrTxt = true)
        {
            if (!string.IsNullOrEmpty(path))
            {
                PiaPath = path;
            }
            if (string.IsNullOrEmpty(PiaPath))
            {
                throw new ArgumentNullException("PiaPath");
            }

            using (var outStream = new FileStream(PiaPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))//流文件
            {
                this.Serialize(outStream, IsPlotOrTxt);//串行器 串行化
                outStream.Close();//关闭流
            }
        }

        public override string ToString()
        {
            return Path.GetFileName(PiaFileName);
        }

        #endregion
    }
}
