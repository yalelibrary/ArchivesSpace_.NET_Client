using System;
using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RefBase
    {
        [JsonIgnore]
        public virtual int RefStrippedId
        {
            get { return int.Parse(Ref.Split('/').Last()); }
        }

        [JsonProperty("ref")]
        public string Ref { get; set; }
    }
}