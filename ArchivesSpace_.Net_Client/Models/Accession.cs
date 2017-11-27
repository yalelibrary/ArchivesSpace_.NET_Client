using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Accession
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("external_ids")]
        public ICollection<ExternalId> ExternalIds { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }

        [JsonProperty("id_0")]
        public string Id0 { get; set; }

        [JsonProperty("id_1")]
        public string Id1 { get; set; }

        [JsonProperty("id_2")]
        public string Id2 { get; set; }

        [JsonProperty("id_3")]
        public string Id3 { get; set; }

        [JsonProperty("content_description")]
        public string ContentDescription { get; set; }

        [JsonProperty("condition_description")]
        public string ConditionDescription { get; set; }

        [JsonProperty("disposition")]
        public string Disposition { get; set; }

        [JsonProperty("inventory")]
        public string Inventory { get; set; }

        [JsonProperty("provenance")]
        public string Provenance { get; set; }

        [JsonProperty("related_accessions")]
        public ICollection<dynamic> RelatedAccessions { get; set; } //Unfortunately these aren't full accession records but rather two different subtypes (accession_parts_relationship, accession_sibling_relationship). again, I can't expect the deserializer to choose the correct one

        [JsonProperty("accession_date")]
        public DateTimeOffset AccessionDate { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("classifications")]
        public ICollection<dynamic> Classifications { get; set; } //Yet another "ref" value for URLs to classifications

        [JsonProperty("subjects")]
        public ICollection<dynamic> Subjects { get; set; } //And another "ref" to urls for subjects

        [JsonProperty("linked_events")]
        public ICollection<dynamic> LinkedEvents { get; set; } //ref

        [JsonProperty("extents")]
        public ICollection<Extent> Extents { get; set; }

        [JsonProperty("dates")]
        public ICollection<Date> Dates { get; set; }

        [JsonProperty("external_documents")]
        public ICollection<ExternalDocument> ExternalDocuments { get; set; }

        [JsonProperty("rights_statements")]
        public ICollection<RightsStatement> RightsStatements { get; set; }

        [JsonProperty("deaccessions")]
        public ICollection<Deaccession> Deaccessions { get; set; }

        [JsonProperty("collection_management")]
        public CollectionManagement CollectionManagement { get; set; }

        [JsonProperty("user_defined")]
        public dynamic UserDefined { get; set; } //Since this can be customized I'm not going to model it.

        [JsonProperty("related_resources")]
        public ICollection<dynamic> RelatedResources { get; set; } //ref

        [JsonProperty("suppressed")]
        public bool Suppressed { get; set; }

        [JsonProperty("acquisition_type")]
        public string AcquisitionType { get; set; }

        [JsonProperty("resource_type")]
        public string ResourceType { get; set; }

        [JsonProperty("restrictions_apply")]
        public bool RestrictionsApply { get; set; }

        [JsonProperty("retention_rule")]
        public string RetentionRule { get; set; }

        [JsonProperty("general_note")]
        public string GeneralNote { get; set; }

        [JsonProperty("access_restrictions")]
        public bool AccessRestrictions { get; set; }

        [JsonProperty("access_restrictions_note")]
        public string AccessRestrictionsNote { get; set; }

        [JsonProperty("use_restrictions")]
        public bool UseRestrictions { get; set; }

        [JsonProperty("use_restrictions_note")]
        public string UseRestrictionsNote { get; set; }

        [JsonProperty("linked_agents")]
        public ICollection<dynamic> LinkedAgents { get; set; }

        [JsonProperty("instances")]
        public ICollection<Instance> Instances { get; set; }

    }
}
