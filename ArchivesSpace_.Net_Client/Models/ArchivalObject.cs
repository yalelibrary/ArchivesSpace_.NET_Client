using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class ArchivalObject : ArchivalObjectBase
    {
        [JsonProperty("ref_id")]
        public string RefId { get; set; }

        [JsonProperty("component_id")]
        public string ComponentId { get; set; }

        [JsonProperty("level")]
        public string Level { get;set; }

        [JsonProperty("other_level")]
        public string OtherLevel { get; set; }
        //public string Title { get; set; } No need to override title from base since I'm not adding validation

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }

        [JsonProperty("restrictions_apply")]
        public bool RestrictionsApply { get; set; }

        [JsonProperty("repository_processing_note")]
        public string RepositoryProcessingNote { get; set; }

        [JsonProperty("parent")]
        public RefBase Parent { get; set; } //ref - Even though the derived class has no added properties I'm leaving at base unless additional props materialize

        [JsonProperty("resource")]
        public RefBase Resource { get; set; } //ref - even though it's a resource the additional info isn't returned

        [JsonProperty("series")]
        public RefBase Series { get; set; } //ref - ditto

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("instances")]
        public ICollection<Instance> Instances { get; set; }

        [JsonProperty("notes")]
        public ICollection<NoteBase> Notes { get; set; }

        [JsonProperty("has_unpublished_ancestor")]
        public bool HasUnpublishedAncestor { get; set; }

        [JsonProperty("representative_image")]
        public FileVersion RepresentativeImage { get;set; }
    }

}