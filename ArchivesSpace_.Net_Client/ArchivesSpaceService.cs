using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{

    //This service is used to establish a connection to ArchivesSpace, set a repository, and to perform administrative functions that are
    //not related to archival management (e.g., creating user accounts or setting permissions). Functionality to be implemented as needed.
    public class ArchivesSpaceService
    {
        private readonly ArchivesSpaceConnectionHandler _connectionHandler;
        private readonly ArchivesSpaceLocationManager _locManager;
        private readonly ArchivesSpaceContainerProfileManager _cpManager;
        private int _activeRepository;
        private ICollection<Repository> _lastKnownRepositoryList;
        private Dictionary<int, Location> _locations = new Dictionary<int, Location>();
        private Dictionary<int, ContainerProfile> _containerProfiles = new Dictionary<int, ContainerProfile>();

        public ArchivesSpaceService()
        {
            var cred = new ArchivesSpaceCredential
            {
                Username = Constants.ArchivesSpaceUsername,
                Password = Constants.ArchivesSpacePassword
            };
            _connectionHandler = new ArchivesSpaceConnectionHandler(cred);
            _locManager = new ArchivesSpaceLocationManager(this);
            _cpManager = new ArchivesSpaceContainerProfileManager(this);
        }

        public ArchivesSpaceService(ArchivesSpaceCredential cred)
        {
            _connectionHandler = new ArchivesSpaceConnectionHandler(cred);
            _locManager = new ArchivesSpaceLocationManager(this);
            _cpManager = new ArchivesSpaceContainerProfileManager(this);
        }

        public ArchivesSpaceService(ArchivesSpaceConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
            _locManager = new ArchivesSpaceLocationManager(this);
            _cpManager = new ArchivesSpaceContainerProfileManager(this);
        }

        public async Task<ContainerProfile> GetContainerProfileAsync(int cpId)
        {
            ContainerProfile cp;
            if (!_containerProfiles.TryGetValue(cpId, out cp))
            {
                cp = await _cpManager.GetContainerProfileByIdAsync(cpId);
                _containerProfiles.Add(cpId, cp);
            }
            return cp;
        }

        public async Task<Location> GetLocationAsync(int locId)
        {
            Location loc;
            if (!_locations.TryGetValue(locId, out loc))
            {
                loc = await _locManager.GetLocationByIdAsync(locId);
                _locations.Add(locId, loc);
            }
            return loc;
        }

        public Repository ActiveRepository
        {
            get
            {
                return IsRepositorySet() ? _lastKnownRepositoryList.First(x => x.Id == _activeRepository) : null;
            }
        }

        //ToDo: evaluate whether this should be removed
        public async Task<ICollection<Repository>> GetAllRepositoriesAsync()
        {
            return await _connectionHandler.GetAllRepositoriesAsync();
        }
        
        //It's possible that this should be done at initialization and be a read-only property. Intended use is for repository transitions never to be attempted during async processing since that could yield
        //unintended results. This could be guaranteed by not allowing repository switching. On the other hand, there may be situations where you need to switch repositories often and don't want the overhead
        //of multiple objects each with their own connection pool or to create/destroy frequently. Therefore I'm going to leave it like this for now with an open mind to switching to a readonly repository in
        //the future.
        public async Task SetActiveRepositoryAsync(string code)
        {
            var allRepos = await _connectionHandler.GetAllRepositoriesAsync();
            var activeRepo = allRepos.Where(x => x.RepoCode == code);
            if (!activeRepo.Any())
            {
                throw new ArgumentException(String.Format("invalid repository code: [ {0} ]", code), "code");
            }
            _lastKnownRepositoryList = allRepos;
            _activeRepository = activeRepo.First().Id;
        }

        public async Task SetActiveRepositoryAsync(int repoId)
        {
            var allRepos = await _connectionHandler.GetAllRepositoriesAsync();
            var activeRepo = allRepos.Where(x => x.Id == repoId);
            if (!activeRepo.Any())
            {
                throw new ArgumentException(String.Format("invalid repository code: [ {0} ]", repoId), "repoId");
            }
            _lastKnownRepositoryList = allRepos;
            _activeRepository = activeRepo.First().Id;            
        }

        public void SetActiveRepository(string code)
        {
            var setRepoTask = SetActiveRepositoryAsync(code);
            Task.WaitAll(setRepoTask);
        }

        public void SetActiveRepository(int repoId)
        {
            var setRepoTask = SetActiveRepositoryAsync(repoId);
            Task.WaitAll(setRepoTask);
        }

        public async Task<ICollection<Location>> GetAllLocationsAsync()
        {
            //I originally set up a somewhat complicated process to get this as a property that could be stored but it takes about 5
            //seconds to populate the location list so it was regularly finishing after other processes were complete. It also generated
            //a bunch of http connections which were better left free for other requests. so we'll get this as needed.
            return await GetAllLocationsActionAsync();
        }

        public async Task<string> GetApiDataAsync(string relativeUri)
        {
            if (!IsRepositorySet())
            {
                throw new InvalidOperationException("active repository has not been set");
            }
            if (relativeUri.StartsWith(@"/"))
            {
                relativeUri = relativeUri.TrimStart('/');
            }
            var uriWithRepository = String.Format("/repositories/{0}/{1}", ActiveRepository.Id, relativeUri);
            return await _connectionHandler.GetAsync(uriWithRepository);
        }

        public async Task<string> GetApiDataNoRepositoryAsync(string relativeUri)
        {
            if (relativeUri.StartsWith(@"/"))
            {
                relativeUri = relativeUri.TrimStart('/');
            }
            var uriWithRepository = String.Format("/{0}", relativeUri);
            return await _connectionHandler.GetAsync(uriWithRepository);
        }

        private async Task<ICollection<Location>> GetAllLocationsActionAsync()
        {
            var locIds = await _locManager.GetAllLocationIdsAsync();
            var locList = await _locManager.GetLocationsByIdsAsync(locIds);
            return locList;
        }

        private bool IsRepositorySet()
        {
            if (_activeRepository.Equals(0))
            {
                return false;
            }
            return true;
        }

    }
}
