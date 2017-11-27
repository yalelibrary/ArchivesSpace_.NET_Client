using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SmallTree
    {
        [JsonIgnore]
        public virtual int Id
        {
            get { return int.Parse(Uri.Split('/').Last()); }
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("ref_id")]
        public string RefId { get; set; }
        
        [JsonProperty("container")]
        public string Container { get; set; }
        
        [JsonProperty("has_children")]
        public bool HasChildren { get; set; }
        
        [JsonProperty("children")]
        public ICollection<SmallTree> Children { get; set; }
    }
}