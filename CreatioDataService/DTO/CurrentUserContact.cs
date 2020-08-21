using Newtonsoft.Json;
using System;

namespace CreatioDataService.DTO
{
    [JsonObject]
    public class CurrentUserContact
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email{ get; set; }

        [JsonProperty("Contact.Id")]
        public Guid ContactId { get; set; }
    }
}
