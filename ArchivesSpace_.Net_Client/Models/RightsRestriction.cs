using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RightsRestriction
    {
        [JsonProperty("begin")]
        public string Begin { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("local_access_restriction_type")]
        public ICollection<string> LocalAccessRestrictionType { get; set; }

        [JsonProperty("linked_records")]
        public dynamic LinkedRecords { get; set; } //ref

        [JsonProperty("restriction_note_type")]
        public string RestrictionNoteType { get; set; }
    }
}