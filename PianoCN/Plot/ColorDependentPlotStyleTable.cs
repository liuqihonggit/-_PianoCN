//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Collections.Generic;

namespace PiaNO
{
    /// <summary>
    /// 颜色依赖的打印样式表 
    /// </summary>
    public class ColorDependentPlotStyleTable : PlotStyleTable
    {
        #region Properties 性质

        public IDictionary<string, string> AciTable
        {
            get { return _getAciTable(); }
        }

        #endregion

        #region Constructors 构造函数

        /// <summary>
        /// 颜色相关绘图样式表
        /// </summary>
        public ColorDependentPlotStyleTable() : base()
        {
            AciTableAvailable = true;
        }
        public ColorDependentPlotStyleTable(string innerData)
            : base(innerData) { }

        #endregion

        #region Methods 函数 
        private IDictionary<string, string> _getAciTable()
        {
            if (!HasChildNodes)
                return null;

            var aciNode = this["aci_table"];
            if (aciNode == null)
                return null;

            return aciNode.Values;
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="style"></param>
        public override void AddStyle(PlotStyle style)
        {
            throw new NotSupportedException("Cannot add styles to .ctb files.");
        }

        #endregion
    }
}
