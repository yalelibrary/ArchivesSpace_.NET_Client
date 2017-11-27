using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    //ASpace defines an abstract class and then extends it with a class of the same name (classification extends abstract_classification). I wasn't sure of the rationale and skipped that.
    class Classification
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("path_from_root")]
        public ICollection<dynamic> PathFromRoot { get; set; }

        [JsonProperty("linked_records")]
        public ICollection<dynamic> LinkedRecords { get; set; } //It wants both accession and resource types here, but they don't share an ancestor or interface

        [JsonProperty("creator")]
        public dynamic Creator { get; set; } //This is a "ref" entry that contains the URI property for any of four agent types for creator. I'm leaving dynamic but it could be replaced with a "ref" object with a string for URI
    }
}
