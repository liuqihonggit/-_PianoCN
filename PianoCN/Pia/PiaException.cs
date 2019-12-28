//
//  Copyright © 2014 Parrish Husband (parrish.husband@gmail.com)
//  The MIT License (MIT) - See LICENSE.txt for further details.
//

using System;
using System.Runtime.Serialization;

namespace PiaNO
{
    /// <summary>
    /// 异常信息处理
    /// </summary>
    [Serializable] //可串行化的 
    public class PiaException : ApplicationException//应用程序异常
    {
        // 受保护的  异常信息处理
        protected PiaException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public PiaException()
        { }

        public PiaException(string message)
            : base(message) { }
 
        public PiaException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
