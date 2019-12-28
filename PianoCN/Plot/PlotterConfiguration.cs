//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
namespace PiaNO
{
    /// <summary>
    /// pmp的解析,meta内容
    /// </summary>
    public class PlotterConfiguration : PiaFile
    {
        #region Properties 解析meta内容

        const string meta = "meta";
        /// <summary>
        /// 规范家庭名称
        /// </summary>
        public string CanonicalFamily
        {
            get { return _getValue(meta, "canonical_family_name_str"); }
            set { _setValue(meta, "canonical_family_name_str", value); }
        }

        /// <summary>
        /// 规范模型名称
        /// </summary>
        public string CanonicalModel
        {
            get { return _getValue(meta, "canonical_model_name_str"); }
            set { _setValue(meta, "canonical_model_name_str", value); }
        }

        /// <summary>
        /// 驱动程序路径
        /// </summary>
        public string DriverPath
        {
            get { return _getValue(meta, "driver_pathname_str"); }
            set { _setValue(meta, "driver_pathname_str", value); }
        }

        /// <summary>
        /// 驱动标记线
        /// </summary>
        public string DriverTagline
        {
            get { return _getValue(meta, "driver_tag_line_str"); }
            set { _setValue(meta, "driver_tag_line_str", value); }
        }

        /// <summary>
        /// 驱动程序类型
        /// </summary>
        public int DriverType
        {
            get { return int.Parse(_getValue(meta, "driver_type")); }
            set { _setValue(meta, "driver_type", value.ToString()); }
        }

        /// <summary>
        /// 驱动程序版本
        /// </summary>
        public string DriverVersion
        {
            get { return _getValue(meta, "driver_version_str"); }
            set { _setValue(meta, "driver_version_str", value); }
        }

        /// <summary>
        /// 本地家庭名称
        /// </summary>
        public string LocalizedFamily
        {
            get { return _getValue(meta, "localized_family_name_str"); }
            set { _setValue(meta, "localized_family_name_str", value); }
        }

        /// <summary>
        /// 本地模型名
        /// </summary>
        public string LocalizedModel
        {
            get { return _getValue(meta, "localized_model_name_str"); }
            set { _setValue(meta, "localized_model_name_str", value); }
        }

        /// <summary>
        /// 模型库
        /// </summary>
        public string ModelBase
        {
            get { return _getValue(meta, "user_defined_model_basename_str"); }
            set { _setValue(meta, "user_defined_model_basename_str", value); }
        }

        /// <summary>
        /// 模型路径
        /// </summary>
        public string ModelPath
        {
            get { return _getValue(meta, "user_defined_model_pathname_str"); }
            set { _setValue(meta, "user_defined_model_pathname_str", value); }
        }

        /// <summary>
        /// 打印到文件
        /// </summary>
        public bool PlotToFile
        {
            get { return bool.Parse(_getValue(meta, "file_only")); }
            set { _setValue(meta, "file_only", value.ToString().ToUpper()); }
        }

        /// <summary>
        /// 显示自定义
        /// </summary>
        public bool ShowCustomFirst
        {
            get { return bool.Parse(_getValue(meta, "show_custom_first")); }
            set { _setValue(meta, "show_custom_first", value.ToString().ToUpper()); }
        }

        /// <summary>
        /// 工具包版本
        /// </summary>
        public int ToolkitVersion
        {
            get { return int.Parse(_getValue(meta, "toolkit_version")); }
            set { _setValue(meta, "toolkit_version", value.ToString()); }
        }

        /// <summary>
        /// 文本类型填充
        /// </summary>
        public bool TruetypeAsText
        {
            get { return bool.Parse(_getValue(meta, "truetype_as_text")); }
            set { _setValue(meta, "truetype_as_text", value.ToString().ToUpper()); }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// 绘图仪配置
        /// </summary>
        public PlotterConfiguration() : base() { }

        /// <summary>
        /// 绘图仪配置
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public PlotterConfiguration(string fileName) : base(fileName) { }
         
        #endregion Constructors

        #region Methods

        public object GetCustomValue(string name)
        {
            return GetCustomValue<object>(name);
        }
        public T GetCustomValue<T>(string key)
        {
            if (!HasChildNodes)
                throw new InvalidOperationException(NodeName + " has no child nodes");

            var node = this["custom"];
            if (node == null)
                throw new InvalidOperationException(NodeName + " has no custom node");

            foreach (var child in node)
            {
                string name;
                string valueString;

                if (!child.Values.TryGetValue("name_str", out name) ||
                    !child.Values.TryGetValue("value", out valueString))
                    continue;

                if (!name.Equals(key, StringComparison.OrdinalIgnoreCase))
                    continue;

                object value = null;
                if (valueString.Equals("TRUE", StringComparison.OrdinalIgnoreCase) ||
                    valueString.Equals("FALSE", StringComparison.OrdinalIgnoreCase))
                {
                    value = bool.Parse(valueString);
                }
                else if (valueString.All(char.IsDigit))
                {
                    value = int.Parse(valueString);
                }
                else
                {
                    double numValue;
                    if (double.TryParse(valueString, out numValue))
                    {
                        value = numValue;
                    }
                    else
                    {
                        value = valueString;  // Default to string
                    }
                }

                return (T)(value ?? default(T));
            }

            return default(T);
        }

        public void SetCustomValue(string key, object value)
        {
            if (!HasChildNodes)
                throw new InvalidOperationException(NodeName + " has no child nodes");

            var node = this["custom"];
            if (node == null)
                throw new InvalidOperationException(NodeName + " has no custom node");

            foreach (var child in node)
            {
                string name;
                string valueString;

                if (!child.Values.TryGetValue("name_str", out name) ||
                    !child.Values.TryGetValue("value", out valueString))
                    continue;

                if (!name.Equals(key, StringComparison.OrdinalIgnoreCase))
                    continue;

                child.SetValue("value", value.ToString());
            }
        }

        private string _getValue(string nodeName, string name)
        {
            if (!HasChildNodes)
                return null;

            var node = this[nodeName];
            if (node == null)
                return null;

            if (!node.Values.ContainsKey(name))
                return null;

            return node.Values[name];
        }
        private void _setValue(string nodeName, string name, string value)
        {
            if (!HasChildNodes)
                return;

            var node = this[nodeName];
            if (node == null)
                return;

            node.Values[name] = value;
        }
        private IDictionary<string, object> _getCustomDictionary()
        {
            if (!HasChildNodes)
                return null;

            var node = this["custom"];
            if (node == null)
                return null;

            var customDictionary = new Dictionary<string, object>(node.ChildNodes.Count, StringComparer.OrdinalIgnoreCase);
            foreach (var child in node)
            {
                string name;
                string valueString;

                if (!child.Values.TryGetValue("name_str", out name) ||
                    !child.Values.TryGetValue("value", out valueString))
                    return null;

                object value = null;
                if (valueString.Equals("TRUE", StringComparison.OrdinalIgnoreCase) ||
                    valueString.Equals("FALSE", StringComparison.OrdinalIgnoreCase))
                {
                    value = bool.Parse(valueString);
                }
                else if (valueString.All(char.IsDigit))
                {
                    value = int.Parse(valueString);
                }
                else
                {
                    double numValue;
                    if (double.TryParse(valueString, out numValue))
                    {
                        value = numValue;
                    }
                    else
                    {
                        value = valueString;  // Default to string
                    }
                }

                customDictionary.Add(name, value);
            }

            return customDictionary;
        }

        #endregion Methods
    }
}