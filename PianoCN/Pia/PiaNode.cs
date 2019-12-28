//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PiaNO
{
    /// <summary>
    /// 压缩文件信息
    /// </summary>
    public class PiaNode : ICloneable, IEquatable<PiaNode>, IEnumerable<PiaNode>
    {
        #region Properties 性质

        /// <summary>
        /// 子节点
        /// </summary>
        private IList<PiaNode> _childNodes;

        /// <summary>
        /// 子节点
        /// </summary>
        protected internal IList<PiaNode> ChildNodes
        {
            get { return _childNodes ?? (_childNodes = new List<PiaNode>()); }
            set { _childNodes = value; }
        }

        /// <summary>
        /// 有子节点
        /// </summary>
        protected internal bool HasChildNodes
        {
            get { return ChildNodes != null && ChildNodes.Count > 0; }
        }

        /// <summary>
        /// 设置节点数据
        /// </summary>
        /// <param name="piaNode">节点</param>
        /// <param name="vla">数据</param>
        void SetInnerData(PiaNode piaNode, string vla)
        {
            piaNode.DeserializeNode(vla); //反序列节点      
        }

        /// <summary>
        /// 内部数据
        /// </summary>
        public string InnerData//这个是不是需要隐藏?
        {
            get { return PiaSerializer.SerializeNode(this); } //序列化节点
            set { SetInnerData(PiaSerializer.PiaFile, value); }
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 拥有者
        /// </summary>
        protected internal PiaFile Owner { get; set; }

        /// <summary>
        /// 起源
        /// </summary>
        protected internal PiaNode Parent { get; set; }

        /// <summary>
        /// 节点的属性,用键值对呈现
        /// </summary>
        public Dictionary<string, string> Values        
        {
            get { return _values ?? (_values = new Dictionary<string, string>()); }
            set { _values = value; }
        }

        /// <summary>
        /// 修改节点属性值
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">属性值</param>
        public void ValueChang(string key, string value)
        {
            var newValue = new Dictionary<string, string>(); //总的
            foreach (var item in Values)
            {
                if (item.Key == key)
                {
                    newValue.Add(key, value);
                }
                else
                {
                    newValue.Add(item.Key, item.Value);
                }
            }
            Values = newValue;
        }

        private Dictionary<string, string> _values;

        /// <summary>
        /// 清空所有节点数据
        /// </summary>
        public void Clear()
        {
            Values.Clear();
            ChildNodes.Clear();//主要清空这个
        }

        #endregion

        #region Constructors 构造函数

        /// <summary>
        /// 节点
        /// </summary>
       // protected internal PiaNode()
        public PiaNode()
        {
            ChildNodes = new List<PiaNode>();//子节点
            Values = new Dictionary<string, string>();
        }

        /// <summary>
        /// 内部数据
        /// </summary>
        /// <param name="innerData">内部数据</param>
        public PiaNode(string innerData)      
        {
            ChildNodes = new List<PiaNode>();//子节点
            Values = new Dictionary<string, string>(); 
            this.DeserializeNode(innerData);
        }

        #endregion

        #region Methods 函数

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected internal string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (!Values.ContainsKey(key))
                Values.Add(key, string.Empty);

            return Values[key];
        }

        /// <summary>
        /// 设置节点值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected internal void SetValue(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (!Values.ContainsKey(key))
                Values.Add(key, value);
            else
                Values[key] = value;
        }

        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected internal static Color? GetColor(string input)
        {
            var colorVal = int.Parse(input);
            if (colorVal == -1)
                return null;

            return Color.FromArgb(colorVal);
        }

        /// <summary>
        /// 虚函数 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual PiaNode this[string name]
        {
            get
            {
                if (ChildNodes.Count == 0)
                    return null;

                return ChildNodes.FirstOrDefault(n => n.NodeName.Equals(name,
                       StringComparison.InvariantCultureIgnoreCase));

            }
        }

        public override string ToString()
        {
            return NodeName;
        }

        #endregion
         
        /// <summary>
        /// ICloneable 可克隆的,浅克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// IEquatable 可等值的 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PiaNode other)
        {
            if (this == null && other == null)
                return true;

            if (this == null || other == null)
                return false;

            return this.NodeName.Equals(other.NodeName, StringComparison.InvariantCultureIgnoreCase);
        }
         
        #region IEnumerable 可枚举的

        public IEnumerator<PiaNode> GetEnumerator()
        {
            return ChildNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
