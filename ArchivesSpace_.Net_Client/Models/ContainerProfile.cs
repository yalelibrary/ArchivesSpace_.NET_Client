using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class ContainerProfile
    {
        [JsonIgnore]
        public virtual int Id
        {
            get { return int.Parse(Uri.Split('/').Last()); }
        }

        [JsonProperty("lock_version")]
        public int LockVersion { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("dimension_units")]
        public string DimensionUnits { get; set; }

        [JsonProperty("extent_dimension")]
        public string ExtentDimension { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("depth")]
        public string Depth { get; set; }

        [JsonProperty("stacking_limit")]
        public string StackingLimit { get; set; }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }
    }
}