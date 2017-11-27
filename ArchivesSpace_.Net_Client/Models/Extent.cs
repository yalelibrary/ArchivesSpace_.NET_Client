using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Extent
    {
        [JsonProperty("portion")]
        public string Portion { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("extent_type")]
        public string ExtentType { get; set; }

        [JsonProperty("container_summary")]
        public string ContainerSummary { get; set; }

        [JsonProperty("physical_details")]
        public string PhysicalDetails { get; set; }

        [JsonProperty("dimensions")]
        public string Dimensions { get; set; }
    }
}
