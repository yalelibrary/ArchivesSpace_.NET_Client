using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class CollectionManagement
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("external_ids")]
        public ICollection<ExternalId> ExternalIds { get; set; }

        [JsonProperty("processing_hours_per_foot_estimate")]
        public string ProcessingHoursPerFootEstimate { get; set; }
        
        [JsonProperty("processing_total_extent")]
        public string ProcessingTotalExtent { get; set; }

        [JsonProperty("processing_total_extent_type")]
        public string ProcessingTotalExtentType { get; set; }

        [JsonProperty("processing_hours_total")]
        public string ProcessingHoursTotal { get; set; }

        [JsonProperty("processing_plan")]
        public string ProcessingPlan { get; set; }

        [JsonProperty("processing_priority")]
        public string ProcessingPriority { get; set; }

        [JsonProperty("processing_funding_source")]
        public string ProcessingFundingSource { get; set; }

        [JsonProperty("processors")]
        public string Processors { get; set; }

        [JsonProperty("rights_determined")]
        public bool RightsDetermined { get; set; }

        [JsonProperty("processing_status")]
        public string ProcessingStatus { get; set; }
    }
}