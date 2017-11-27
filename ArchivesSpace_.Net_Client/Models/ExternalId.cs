using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class ExternalId
    {
        [JsonProperty("external_id")]
        public string ExternalIdText { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}
