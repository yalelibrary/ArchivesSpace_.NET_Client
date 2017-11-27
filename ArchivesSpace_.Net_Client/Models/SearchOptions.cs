using System.Collections.Generic;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchOptions
    {
        //Used to construct a search query string. Never passed to or from API so no json serialization
        public string Query { get; set; } //A search query string. Uses Lucene 4.0 syntax: http://lucene.apache.org/core/4_0_0/queryparser/org/apache/lucene/queryparser/classic/package-summary.html Search index structure can be found in solr/schema.xml
        public SearchAdvancedQueryBase AdvancedQuery { get; set; } //A json string containing the advanced query
        public ICollection<string> Type { get; set; } //The record type to search (defaults to all types if not specified)
        public string Sort { get; set; } //The attribute to sort and the direction e.g. &sort=title desc&...
        public ICollection<string> Facet { get; set; } //The list of the fields to produce facets for
        public ICollection<string> FilterTerm { get; set; } //A json string containing the term/value pairs to be applied as filters. Of the form: {"fieldname": "fieldvalue"}.
        public ICollection<string> SimpleFilter { get; set; } //A simple direct filter to be applied as a filter. Of the form 'primary_type:accession OR primary_type:agent_person'.
        public ICollection<string> Exclude { get; set; } //A list of document IDs that should be excluded from results
        public bool Highlight { get; set; } //Whether to use highlighting
        public string RootRecord { get; set; } // Search within a collection of records (defined by the record at the root of the tree)
        public string Dt { get; set; } // Format to return (JSON default)
    }
}