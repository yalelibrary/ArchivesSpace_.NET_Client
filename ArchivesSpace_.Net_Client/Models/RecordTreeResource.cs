using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RecordTreeResource : RecordTreeBase
    {
        [JsonProperty("finding_aid_filing_title")]
        public string FindingAidFilingTitle { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("instance_types")]
        public ICollection<string> InstanceTypes { get; set; }

        [JsonProperty("containers")]
        public ICollection<dynamic> Containers { get; set; } //jsonmodel type is specified as "object" can't find examples

        [JsonProperty("children")]
        public ICollection<RecordTreeResource> Children { get; set; }
      
    }
}