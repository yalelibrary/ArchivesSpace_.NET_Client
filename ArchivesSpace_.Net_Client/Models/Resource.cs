using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    
    public class Resource : ArchivalObjectBase
    {

        [JsonProperty("id_0")]
        public string Id0 { get; set; }

        [JsonProperty("id_1")]
        public string Id1 { get; set; }

        [JsonProperty("id_2")]
        public string Id2 { get; set; }

        [JsonProperty("id_3")]
        public string Id3 { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("other_level")]
        public string OtherLevel { get; set; }

        [JsonProperty("resource_type")]
        public string ResourceType { get; set; }

        [JsonProperty("tree")]
        public dynamic Tree { get; set; } //ref

        [JsonProperty("restrictions")]
        public bool Restrictions { get; set; }

        [JsonProperty("repository_processing_note")]
        public string RepositoryProcessingNote { get; set; }

        [JsonProperty("ead_id")]
        public string EadId { get; set; }

        [JsonProperty("ead_location")]
        public string EadLocation { get; set; }

        [JsonProperty("finding_aid_title")]
        public string FindingAidTitle { get; set; }

        [JsonProperty("finding_aid_subtitle")]
        public string FindingAidSubtitle { get; set; }

        [JsonProperty("finding_aid_filing_title")]
        public string FindingAidFilingTitle { get; set; }

        [JsonProperty("finding_aid_date")]
        public string FindingAidDate { get; set; }

        [JsonProperty("finding_aid_author")]
        public string FindingAidAuthor { get; set; }

        [JsonProperty("finding_aid_description_rules")]
        public string FindingAidDescriptionRules { get; set; }

        [JsonProperty("finding_aid_language")]
        public string FindingAidLanguage { get; set; }

        [JsonProperty("finding_aid_sponsor")]
        public string FindingAidSponsor { get; set; }

        [JsonProperty("finding_aid_edition_statement")]
        public string FindingAidEditionStatement { get; set; }

        [JsonProperty("finding_aid_series_statement")]
        public string FindingAidSeriesStatement { get;set; }

        [JsonProperty("finding_aid_status")]
        public string FindingAidStatus { get; set; }

        [JsonProperty("finding_aid_note")]
        public string FindingAidNote { get; set; }

        [JsonProperty("extents")]
        public override ICollection<Extent> Extents { get; set; }

        [JsonProperty("revision_statements")]
        public ICollection<RevisionStatement> Revisionstatements { get; set; }

        [JsonProperty("dates")]
        public override ICollection<Date> Dates { get; set; }

        [JsonProperty("instances")]
        public ICollection<Instance> Instances { get; set; }

        [JsonProperty("deaccessions")]
        public ICollection<Deaccession> Deaccessions { get; set; }

        [JsonProperty("collection_management")]
        public CollectionManagement CollectionManagement { get; set; }

        [JsonProperty("user_defined")]
        public dynamic UserDefined { get; set; } //again, user defined so not defining

        [JsonProperty("related_accessions")]
        public ICollection<dynamic> RelatedAccessions { get; set; } //ref

        [JsonProperty("classifications")]
        public ICollection<dynamic> Classifications { get; set; } //ref

        //The converter is provided at the time of deserialization. Specification of converter in attribute doesn't work when extending JSON.net's CustomCreationConverter.
        [JsonProperty("notes")]
        public ICollection<NoteBase> Notes { get; set; }

        [JsonProperty("representative_image")]
        public FileVersion RepresentativeImage { get; set; }

    }
}