using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public abstract class SearchAdvancedQueryBase
    {
        [JsonProperty("query")]
        public virtual SearchAdvancedQueryBase Query { get; set; }
    }
}