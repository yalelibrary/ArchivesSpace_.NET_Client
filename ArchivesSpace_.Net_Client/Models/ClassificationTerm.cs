using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    class ClassificationTerm : Classification
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("parent")]
        public dynamic Parent { get; set; }

        [JsonProperty("classification")]
        public dynamic Classification { get; set; }//this is kind of weird, seems to be a ref object pointing at a classification object uri
    }
}
