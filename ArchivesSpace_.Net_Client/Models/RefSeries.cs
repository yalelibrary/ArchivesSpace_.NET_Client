using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RefSeries : RefBase
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }

        [JsonProperty("level_display_string")]
        public string LevelDisplayString { get; set; }
    }
}
