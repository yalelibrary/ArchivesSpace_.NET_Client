using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceTopContainerManager : ArchivesSpaceObjectManagerBase
    {
        public ArchivesSpaceTopContainerManager(ArchivesSpaceService activeService)
            : base(activeService)
        {
            //This sets the ArchivesSpaceService field in the base class
        }

        public virtual async Task<List<int>> GetAllTopContainerIdsAsync()
        {
            return await GetAllTopContainerIdsActionAsync();
        }

        public virtual async Task<TopContainer> GetTopContainerByIdAsync(int id)
        {
            return await GetTopContainerByIdActionAsync(id);
        }

        public virtual async Task<ICollection<TopContainer>> GetTopContainersByIdsAsync(ICollection<int> idCollection)
        {
            return await GetTopContainersByIdsActionAsync(idCollection);
        }

        public virtual async Task<SearchResultTopContainer> GetTopContainerSeriesSearchAsync(string archivalObjectUri, bool allResults) //ToDo: Change Signature to bool
        {
            return await GetTopContainerSeriesSearchActionAsync(archivalObjectUri, allResults);
        }

        public virtual async Task<SearchResultTopContainer> GetTopContainerResourceSearchAsync(string resourceObjectUri, bool allResults) //ToDo: Change Signature to bool
        {
            return await GetTopContainerResourceSearchActionAsync(resourceObjectUri, allResults);
        }

        public virtual async Task<TopContainer> GetTopContainerByBarcodeAsync(string barcode)
        {
            return await GetTopContainerByBarcodeActionAsync(barcode);
        }

        //ToDo: Refactor the resource and series search, avoid copied code
        private async Task<SearchResultTopContainer> GetTopContainerSeriesSearchActionAsync(string archivalObjectUri, bool allResults) //ToDo: Change signature to bool
        {
            var searchEngine = new ArchivesSpaceSearch(ArchivesSpaceService);
            var searchText = String.Format("series_uri_u_sstr:(\"{0}\")", archivalObjectUri);
            var searchQuery = new SearchOptions {Query = searchText};
            var searchResult = await searchEngine.TopContainerSearchAsync(searchQuery, allResults);
            return searchResult;
            //var searchQuery = String.Format("series_uri_u_sstr:(\"{0}\")", archivalObjectUri);
            //var searchResults = await GetTopContainerSearchActionAsync(searchQuery, page);
            //return searchResults;
        }

        private async Task<SearchResultTopContainer> GetTopContainerResourceSearchActionAsync(string resourceObjectUri, bool allResults) //ToDo: Change signature to bool
        {
            var searchText = String.Format("collection_uri_u_sstr:(\"{0}\")", resourceObjectUri);
            return await GetTopContainerSearchActionAsync(searchText, allResults);
        }

        private async Task<SearchResultTopContainer> GetTopContainerSearchActionAsync(string searchText, bool allResults)
        {
            var searchEngine = new ArchivesSpaceSearch(ArchivesSpaceService);
            var searchQuery = new SearchOptions { Query = searchText };
            var searchResult = await searchEngine.TopContainerSearchAsync(searchQuery, allResults);
            return searchResult;
        }

        private async Task<ICollection<TopContainer>> GetTopContainersByIdsActionAsync(ICollection<int> idCollection)
        {
            idCollection = idCollection.Distinct().ToList();
            var containerBag = new ConcurrentBag<TopContainer>();
            //reading only so I'm not worrying about the source object being a regular collection rather than concurrent collection
            await idCollection.ForEachAsync(Environment.ProcessorCount, async x =>
            {
                containerBag.Add(await GetTopContainerByIdActionAsync(x));
            });
            return containerBag.ToList();
        }

        private async Task<List<int>> GetAllTopContainerIdsActionAsync()
        {
            var uriSegment = "/top_containers?all_ids=true";
            var allTcsString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var alTcs = JsonConvert.DeserializeObject<List<int>>(allTcsString);
            return alTcs;
        }

        private async Task<TopContainer> GetTopContainerByIdActionAsync(int id)
        {
            var uriSegment = String.Format("/top_containers/{0}", id);
            var tcString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var tc = JsonConvert.DeserializeObject<TopContainer>(tcString, new JsonAspaceNoteConverter(), new JsonAspaceNoteItemConverter());
            return tc;
        }

        private async Task<TopContainer> GetTopContainerByBarcodeActionAsync(string barcode)
        {
            var searchText = String.Format("barcode_u_sstr:(\"{0}\")", barcode);
            var searchResult = await GetTopContainerSearchActionAsync(searchText, true);
            if (searchResult.TotalHits < 1)
            {
                return null;
            }
            //barcodes are unique across top containers (or at least that's my understanding) so there should only ever be one hit
            return searchResult.Results.First().ParsedJson;
        }
    }
}