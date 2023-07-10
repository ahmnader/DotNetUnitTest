using System;
using System.Runtime.Serialization;
using DevOpsWebApp.Models;

namespace DevOpsWebApp.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public string Code { get; set; }

        public ApiMessageType Level  { get; set; }

        
        public BusinessException(ApiMessageType level, string code, string message) : base(message)
        {
            this.Code = code;
            this.Level = level;
        }
        public BusinessException(ApiMessageType level, string code, string message, Exception exception) : base(message, exception)
        {
            this.Code = code;
            this.Level = level;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
