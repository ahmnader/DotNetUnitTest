using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace DevOpsWebApp.Models
{
    public class ApiResult<T> 
    {
        [Required]
        public Result Status { get; set; }

        public T? Model { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public ApiMessage Message
        {
            get => Messages!.FirstOrDefault()!;
            set
            {
                Messages = new ApiMessage[] { value };
            }
        }

        public IEnumerable<ApiMessage>? Messages { get; set; }
    }

    public class InquiryResult<T>
    {
        public InquiryResult(IEnumerable<T> list, int totalCount)
        {
            List = list;
            TotalCount = totalCount;
        }

        public IEnumerable<T> List { get; set; }

        public int? TotalCount { get; set; }
    }

    public class ApiMessage
    {
        [Required]
        public ApiMessageType Level  { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Message { get; set; } 
        public string? Suggestion { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))] 
    public enum ApiMessageType
    {
        Info,
        Warning,
        Error
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Result
    {
        Success = 200,
        Fail = 400,
        ServerError = 500
    }
}
