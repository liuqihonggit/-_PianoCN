//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Drawing;

namespace PiaNO
{
    /// <summary>
    /// 打印样式
    /// </summary>
    public class PlotStyle : PiaNode
    {
        #region Properties

        private const string USE_OBJECT_COLOR = "-1006632961";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return GetValue("name_str"); }
            set { SetValue("name_str", value); }
        }
        /// <summary>
        /// 本地化名称
        /// </summary>
        public string LocalizedName
        {
            get { return GetValue("localized_name_str"); }
            set { SetValue("localized_name_str", value); }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return GetValue("description_str"); }
            set { SetValue("description_str", value); }
        }
        /// <summary>
        /// 颜色
        /// </summary>
        public Color? Color
        {
            get
            {
                var color = GetValue("color");
                return color != null ? GetColor(color) : null;
            }
            set { SetValue("color", value == null ? USE_OBJECT_COLOR : value.ToString()); }
        }
        /// <summary>
        /// 模型彩色
        /// </summary>
        public Color? ModeColor
        {
            get
            {
                var color = GetValue("mode_color");
                return color != null ? GetColor(color) : null;
            }
            set { SetValue("mode_color", value == null ? USE_OBJECT_COLOR : value.ToString()); }
        }
        /// <summary>
        /// 色彩政策
        /// </summary>
        public short ColorPolicy
        {
            get { return short.Parse(GetValue("color_policy")); }
            set { SetValue("color_policy", value.ToString()); }
        }
        /// <summary>
        /// 启用抖动
        /// </summary>
        public bool EnableDithering
        {
            get { return ColorPolicy % 2 == 1; } //很确定这是按位的

        }
        /// <summary>
        /// 转换灰度
        /// </summary>
        public bool ConvertoToGrayscale
        {
            get { return ColorPolicy == 2 || ColorPolicy == 3; } //很确定这是按位的
        }
        /// <summary>
        /// 物理序列号
        /// </summary>
        public short PhysicalPenNumber
        {
            get { return short.Parse(GetValue("physical_pen_number")); }
            set { SetValue("physical_pen_number", value.ToString()); }
        }
        /// <summary>
        /// 虚拟数字
        /// </summary>
        public short VirtualPenNumber
        {
            get { return short.Parse(GetValue("virtual_pen_number")); }
            set { SetValue("virtual_pen_number", value.ToString()); }
        }
        /// <summary>
        /// 屏幕
        /// </summary>
        public short Screen
        {
            get { return short.Parse(GetValue("screen")); }
            set { SetValue("screen", value.ToString()); }
        }
        /// <summary>
        /// 直线尺寸
        /// </summary>
        public double LinePatternSize
        {
            get { return double.Parse(GetValue("linepattern_size")); }
            set { SetValue("linepattern_size", value.ToString()); }
        }
        /// <summary>
        /// 线型
        /// </summary>
        public PlotLinetype Linetype
        {
            get { return (PlotLinetype)Enum.Parse(typeof(PlotLinetype), GetValue("linetype")); }
            set { SetValue("linetype", ((int)value).ToString()); }
        }
        /// <summary>
        /// 自适应线型
        /// </summary>
        public bool AdaptiveLinetype
        {
            get { return bool.Parse(GetValue("adaptive_linetype")); }
            set { SetValue("adaptive_linetype", value.ToString().ToUpper()); }
        }
        /// <summary>
        /// 线宽
        /// </summary>
        public short LineWeight
        {
            get { return short.Parse(GetValue("lineweight")); }
            set { SetValue("lineweight", value.ToString()); }
        }
        /// <summary>
        /// 填充状态
        /// </summary>
        public FillStyle FillStyle
        {
            get { return (FillStyle)Enum.Parse(typeof(FillStyle), GetValue("fill_style")); }
            set { SetValue("fill_style", ((int)value).ToString()); }
        }
        /// <summary>
        /// 结束样式
        /// </summary>
        public EndStyle EndStyle
        {
            get { return (EndStyle)Enum.Parse(typeof(EndStyle), GetValue("end_style")); }
            set { SetValue("end_style", ((int)value).ToString()); }
        }
        /// <summary>
        /// 连接样式
        /// </summary>
        public JoinStyle JoinStyle
        {
            get { return (JoinStyle)Enum.Parse(typeof(JoinStyle), GetValue("join_style")); }
            set { SetValue("join_style", ((int)value).ToString()); }
        }

        #endregion

        #region Constructors 构造函数

        /// <summary>
        /// 打印样式
        /// </summary>
        public PlotStyle()
            : base()
        {
            Name = "New style"; // 需要基于现有节点进行迭代
            LocalizedName = string.Empty;
            Description = string.Empty;
            Color = null;
            ModeColor = null;
            ColorPolicy = 1;
            PhysicalPenNumber = 0;
            VirtualPenNumber = 0;
            Screen = 100;
            LinePatternSize = 0.5;
            Linetype = PlotLinetype.FromObject;
            AdaptiveLinetype = true;
            LineWeight = 0;
            FillStyle = FillStyle.FromObject;
            EndStyle = EndStyle.FromObject;
            JoinStyle = JoinStyle.FromObject;
        }
        protected internal PlotStyle(string innerData)
            : base(innerData) { }

        internal PlotStyle(PiaNode baseNode)
        {
            NodeName = baseNode.NodeName;
            Parent = baseNode.Parent;
            Owner = baseNode.Owner;
            Values = baseNode.Values;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
