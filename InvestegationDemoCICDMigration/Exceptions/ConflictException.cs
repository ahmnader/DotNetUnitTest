using System;
using System.Runtime.Serialization;

namespace DevOpsWebApp.Exceptions
{
    [Serializable]
    public class ConflictException : BusinessException
    {
        public ConflictException(string message) : base(Models.ApiMessageType.Error, CommonHelpersMessageCode.DatabaseConflict, message)
        {
        }
        public ConflictException(string message, Exception exception) : base(Models.ApiMessageType.Error, CommonHelpersMessageCode.DatabaseConflict, message, exception)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
