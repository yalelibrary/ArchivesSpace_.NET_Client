using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class TopContainer
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

        [JsonProperty("indicator")]
        public string Indicator { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }

        [JsonProperty("long_display_string")]
        public string LongDisplayString { get; set; }

        [JsonProperty("ils_holding_id")]
        public string IlsHoldingId { get; set; }

        [JsonProperty("ils_item_id")]
        public string IlsItemId { get; set; }

        [JsonProperty("exported_to_ils")]
        public string ExportedToIls { get; set; }

        [JsonProperty("restricted")]
        public bool Restricted { get; protected set; }
        
        [JsonProperty("active_restrictions")]
        public ICollection<RightsRestriction> ActiveRestrictions { get; set; }

        [JsonProperty("container_locations")]
        public ICollection<ContainerLocation> ContainerLocations { get; set; }

        [JsonProperty("container_profile")]
        public RefContainerProfile ContainerProfile { get; set; } //ref

        [JsonProperty("series")]
        public ICollection<RefSeries> Series { get; set; } //ref

        [JsonProperty("collection")]
        public ICollection<RefResource> Collection { get; set; } //ref

    }
}