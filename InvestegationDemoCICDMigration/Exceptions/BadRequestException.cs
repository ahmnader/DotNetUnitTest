using System;
using System.Runtime.Serialization;

namespace DevOpsWebApp.Exceptions
{
    [Serializable]
    public class BadRequestException : BusinessException
    {
        public BadRequestException(string message) : base(Models.ApiMessageType.Error, CommonHelpersMessageCode.ValidationBehaviourError, message)
        {
        }
        public BadRequestException(string message, Exception exception) : base(Models.ApiMessageType.Error, CommonHelpersMessageCode.ValidationBehaviourError, message, exception)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
