using Newtonsoft.Json;

namespace CreatioDataService.DTO
{
    [JsonObject]
    public class AuthRequest
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("UserPassword")]
        public string UserPassword { get; set; }

    }
}
