using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteMultipart : NoteBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rights_restriction")]
        public RightsRestriction RightsRestriction { get; set; }

        [JsonProperty("subnotes")]
        public ICollection<NoteItemBase> Subnotes { get; set; } //once again, we have a collection of different types that we can't deserialize into base without losing detail
    }
}