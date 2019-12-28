//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Globalization;

namespace PiaNO
{
    /// <summary>
    /// 解压版本和类型
    /// </summary>
    public class PiaHeader
    {
        private readonly string _headerData;

        /// <summary>
        /// 解压文件版本
        /// </summary>
        public double PiaFileVersion { get; private set; }
        /// <summary>
        /// 解压类型版本
        /// </summary>
        public short TypeVersion { get; private set; }
        /// <summary>
        /// 解压类型
        /// </summary>
        public EnumDecompressionType PiaType { get; private set; }

        /// <summary>
        /// 头信息处理
        /// </summary>
        /// <param name="headerString">pc3的头信息</param>
        public PiaHeader(string headerString)
        {
            _headerData = headerString;

            var firstLine = headerString.Split()[0];
            var headerArray = headerString.Split(new char[] { ',', '_'});
            if (headerArray.Length < 4)
                throw new ArgumentOutOfRangeException();

            NumberFormatInfo wNFI = new NumberFormatInfo //数字格式信息
            {
                CurrencyDecimalSeparator = "." //货币小数分隔符
            };
            PiaFileVersion = double.Parse(headerArray[1], wNFI);

            var typeString = headerArray[2].Substring(0, 3);
            PiaType = (EnumDecompressionType)Enum.Parse(typeof(EnumDecompressionType), typeString);

            var versionString = headerArray[2].Substring(3).ToUpper().Replace("VER", string.Empty);
            TypeVersion = short.Parse(versionString);
        }

        public override string ToString()
        {
            return _headerData;
            //var fileversionString = string.Format("{0:0.0}", Math.Truncate(PiaFileVersion * 10) / 10);
            //return string.Format(PIA_HEADER_FORMAT, fileversionString, PiaType, TypeVersion);
        }
    }
}
