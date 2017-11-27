using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceSearch
    {
        protected readonly ArchivesSpaceService ArchivesSpaceService;

        public ArchivesSpaceSearch(ArchivesSpaceService activeService)
        {
            ArchivesSpaceService = activeService;
        }

        public async Task<SearchResultTopContainer> TopContainerSearchAsync(SearchOptions search, bool allPages = false)
        {
            search.Type = new List<string> { "top_container" }; //want to make sure that the right type is requested I think a new is faster than a null-check + search
            var querystring = QueryStringBuilder(search);
            var initialResultText = await RunSearchAsync(querystring, 1);
            var initialResult = JsonConvert.DeserializeObject<SearchResultTopContainer>(initialResultText);
            if (allPages && initialResult.LastPage > 1)
            {
                var additionalResults = new ConcurrentBag<SearchResultEntryTopContainer>();
                var pageRange = new List<int>(Enumerable.Range(2, initialResult.LastPage));
                await pageRange.ForEachAsync(Environment.ProcessorCount, async x =>
                {
                    var pageResultText = await RunSearchAsync(querystring, x);
                    var pageResult = JsonConvert.DeserializeObject<SearchResultTopContainer>(pageResultText);
                    foreach (var searchResultEntryTopContainer in pageResult.Results)
                    {
                        additionalResults.Add(searchResultEntryTopContainer);
                    }
                });
                foreach (var result in additionalResults.ToList())
                {
                    initialResult.Results.Add(result);
                }
            }
            return initialResult;
        }

        public async Task<SearchResultResource> ResourceSearchAsync(SearchOptions search, bool allPages = false)
        {
            search.Type = new List<string> {"resource"}; //same drill
            var querystring = QueryStringBuilder(search);
            var initialResultText = await RunSearchAsync(querystring, 1);
            var initialResult = JsonConvert.DeserializeObject<SearchResultResource>(initialResultText);
            if (allPages && initialResult.LastPage > 1)
            {
                var additionalResults = new ConcurrentBag<SearchResultEntryResource>();
                var pageRange = new List<int>(Enumerable.Range(2, initialResult.LastPage));
                await pageRange.ForEachAsync(Environment.ProcessorCount, async x =>
                {
                    var pageResultText = await RunSearchAsync(querystring, x);
                    var pageResult = JsonConvert.DeserializeObject<SearchResultResource>(pageResultText);
                    foreach (var searchResultResource in pageResult.Results)
                    {
                        additionalResults.Add(searchResultResource);
                    }
                });
                foreach (var result in additionalResults.ToList())
                {
                    initialResult.Results.Add(result);
                }
            }
            return initialResult;
        }

        public async Task<SearchResultArchivalObject> ArchivalObjectSearchAsync(SearchOptions search, bool allPages = false)
        {
            search.Type = new List<string> { "archival_object" }; //same drill
            var querystring = QueryStringBuilder(search);
            var initialResultText = await RunSearchAsync(querystring, 1);
            var initialResult = JsonConvert.DeserializeObject<SearchResultArchivalObject>(initialResultText);
            if (allPages && initialResult.LastPage > 1)
            {
                var additionalResults = new ConcurrentBag<SearchResultEntryArchivalObject>();
                var pageRange = new List<int>(Enumerable.Range(2, initialResult.LastPage));
                await pageRange.ForEachAsync(Environment.ProcessorCount, async x =>
                {
                    var pageResultText = await RunSearchAsync(querystring, x);
                    var pageResult = JsonConvert.DeserializeObject<SearchResultArchivalObject>(pageResultText);
                    foreach (var searchResultEntryArchivalObject in pageResult.Results)
                    {
                        additionalResults.Add(searchResultEntryArchivalObject);
                    }
                });
                foreach (var result in additionalResults.ToList())
                {
                    initialResult.Results.Add(result);
                }
            }
            return initialResult;
        }

        private async Task<string> RunSearchAsync(string formattedQuery, int page)
        {
            var requestUrl = String.Format("search?page={0}&{1}", page, formattedQuery);
            var searchResultText = await ArchivesSpaceService.GetApiDataAsync(requestUrl);
            return searchResultText;
        }

        private string QueryStringBuilder(SearchOptions searchOptions)
        {
            //string advancedQueryParam;
            //I'm not yet sure how to handle advanced queries
            var queryParam = !String.IsNullOrEmpty(searchOptions.Query) ? String.Format("q={0}", Uri.EscapeDataString(searchOptions.Query)) : String.Empty;
            var sortParam = !String.IsNullOrEmpty(searchOptions.Sort) ? String.Format("sort={0}", Uri.EscapeDataString(searchOptions.Sort)) : String.Empty; ;
            var rootRecordParam = !String.IsNullOrEmpty(searchOptions.RootRecord) ? String.Format("root_record={0}", Uri.EscapeDataString(searchOptions.RootRecord)) : String.Empty; ;
            var returnTypeParam = !String.IsNullOrEmpty(searchOptions.Dt) ? String.Format("dt={0}", Uri.EscapeDataString(searchOptions.Dt)) : String.Empty; ;
            var highlightParam = searchOptions.Highlight ? String.Format("hl={0}", searchOptions.Highlight) : String.Empty; ;


            var typeParam = BuildArrayParam("type", searchOptions.Type);
            var facetParam = BuildArrayParam("facet", searchOptions.Facet);
            var filterTermParam = BuildArrayParam("filter_term", searchOptions.FilterTerm);
            var simpleFilterParam = BuildArrayParam("simple_filter", searchOptions.SimpleFilter);
            var excludeParam = BuildArrayParam("exclude", searchOptions.Exclude);

            string[] queryStringArray =
            {
                typeParam, facetParam, filterTermParam, simpleFilterParam, excludeParam,
                sortParam, rootRecordParam, returnTypeParam, highlightParam, queryParam
            };

            var formattedQueryString = String.Join("&", queryStringArray.Where(x => !String.IsNullOrEmpty(x)));
            return formattedQueryString;

        }

        private string BuildArrayParam(string parameterName, ICollection<string> parameterValues)
        {
            if (parameterValues != null && parameterValues.Count > 0)
            {
                var i = 0;
                var typeSb = new StringBuilder();
                foreach (var value in parameterValues)
                {
                    var joiner = i > 0 ? "&" : String.Empty;
                    typeSb.Append(String.Format("{0}{1}[]={2}", joiner, parameterName, Uri.EscapeDataString(value)));
                    i++;
                }
                var builtParam = typeSb.ToString();
                return builtParam;
            }
            return String.Empty;
        }
    }
}