using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Location
    {
        [JsonIgnore]
        public virtual int Id
        {
            get { return int.Parse(Uri.Split('/').Last()); }
        }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("external_ids")]
        public ICollection<ExternalId> ExternalIds { get; set; }

        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("floor")]
        public string Floor { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("classification")]
        public string Classification { get; set; }

        [JsonProperty("coordinate_1_label")]
        public string Coordinate1Label { get; set; }

        [JsonProperty("coordinate_1_indicator")]
        public string Coordinate1Indicator { get; set; }

        [JsonProperty("coordinate_2_label")]
        public string Coordinate2Label { get; set; }

        [JsonProperty("coordinate_2_indicator")]
        public string Coordinate2Indicator { get; set; }

        [JsonProperty("coordinate_3_label")]
        public string Coordinate3Label { get; set; }

        [JsonProperty("coordinate_3_indicator")]
        public string Coordinate3Indicator { get; set; }

        [JsonProperty("temporary")]
        public string Temporary { get; set; }

        [JsonProperty("location_profile")]
        public RefLocationProfile LocationProfile { get; set; }

        [JsonProperty("owner_repo")]
        public RefRepository OwnerRepo { get; set; }

    }
}