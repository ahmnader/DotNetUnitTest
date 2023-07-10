using System;
using System.Runtime.Serialization;
using CommonHelpers.Models;
using DevOpsWebApp.Models;

namespace DevOpsWebApp.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public string? Code { get; set; }

        public ApiMessageType Level  { get; set; }
    }
}
