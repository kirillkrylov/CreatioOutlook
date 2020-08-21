using Newtonsoft.Json;

namespace CreatioDataService.DTO
{
    [JsonObject]
    public class AuthResponseDTO
    {
        [JsonProperty("Code")]
        public int Code { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Exception")]
        public Exception Exception { get; set; }

        [JsonProperty("PasswordChangeUrl")]
        public string PasswordChangeUrl { get; set; }

        [JsonProperty("RedirectUrl")]
        public string RedirectUrl { get; set; }
    }

    public class Exception
    {
        [JsonProperty("HelpLink")]
        public string HelpLink { get; set; }

        [JsonProperty("InnerException")]
        public string InnerException { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }


        [JsonProperty("StackTrace")]
        public string StackTrace { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

    }
}
