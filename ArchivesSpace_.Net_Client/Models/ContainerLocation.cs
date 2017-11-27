using System;
using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class ContainerLocation
    {
        [JsonIgnore]
        public virtual int RefStrippedId
        {
            get { return int.Parse(Ref.Split('/').Last()); }
        }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; } //not offset since no time, date text only

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("ref")]
        public string Ref { get; set; } //Somewhat unusual, this is actually returned as a string.
    }
}