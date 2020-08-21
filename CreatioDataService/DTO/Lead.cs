using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioDataService.DTO
{
    [JsonObject]
    public class Lead
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Contact")]
        public string Contact { get; set; }

        [JsonProperty("Account")]
        public string Account { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("LeadType")]
        public Guid LeadType { get; set; }

        [JsonProperty("Owner")]
        public Guid Owner { get; set; }     

        [JsonProperty("Notes")]
        public String Notes { get; set; }
        
        [JsonProperty("Commentary")]
        public string Commentary { get; set; }

    }
}
