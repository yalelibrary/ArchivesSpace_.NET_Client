using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArchivesSpace_.Net_Client_Demo
{
    class Program
    {
        private static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            //Console.WriteLine("Press 'enter' to begin");
            //Console.ReadLine();
            var resultsList = new List<Tuple<string, long>>();
            var sw = new Stopwatch();
            sw.Start();
            var aspaceService = new ArchivesSpaceService();

            aspaceService.SetActiveRepository("MSSA");
            //aspaceService.SetActiveRepository(19);

            //await TestRepositories(aspaceService);
            //await TestAccessions(aspaceService);
            //await TestResources(aspaceService);
            //await TestResources(aspaceService, 5240);

            //resultsList.AddRange(await TestArchivalObjects(aspaceService, true, 0));

            var targetId = 2087993; //A series-level KSS object resource 5240
            //var targetId = 1954935; //Lindbergh MS325 - correspondence 1911-1974 is a large series resource 4817
            //var targetId = 1173259; //series-level object from sizer papers MS 453, no id
            //var targetId = 2312889; //Test archival object for restrictions
            //var targetId = 2088139; //a bottom level KSS object
            //var targetId = 1204274; //MS1797 inventory, tiny

            //var targetId = 2312904; //added by Mark to test container searching resource 5749 repo 19 - has ID
            //var targetId = 2312903; //added by Mark to test container searching resource 5749 repo 19 - no ID
            resultsList.AddRange(await TestArchivalObjects(aspaceService, false, targetId));
            //resultsList.AddRange(await PrintLocations(aspaceService));
        
            sw.Stop();
            foreach (var resultEntry in resultsList)
            {
                Console.WriteLine("Task [ {0} ] completed in [ {1} ] ms", resultEntry.Item1, resultEntry.Item2);
            }
            Console.WriteLine("Finished, total execution: [ {0} ] ms", sw.ElapsedMilliseconds);
            Console.ReadLine();
        }

        public static async Task<List<Tuple<string, long>>> PrintLocations(ArchivesSpaceService aspaceService)
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Getting all locations:");
            Console.WriteLine(Serialize(await aspaceService.GetAllLocationsAsync()));
            sw.Stop();
            var res = new Tuple<string,long>("Retrieve all locations", sw.ElapsedMilliseconds);
            var resultList = new List<Tuple<string, long>> {res};
            return resultList;
        }

        public static async Task TestResources(ArchivesSpaceService aspaceService)
        {
            await TestResources(aspaceService, 0);
        }

        public static async Task TestResources(ArchivesSpaceService aspaceService, int resourceId)
        {
            var randIntGen = new Random();
            Console.WriteLine("Getting all resources:");
            var resourceManager = new ArchivesSpaceResourceManager(aspaceService);

            
            if (resourceId == 0)
            {
                Console.WriteLine("No resource specified, fetching random from list of all resources: ");
                var resourceList = await resourceManager.GetAllResourceIdsAsync();
                Console.WriteLine(Serialize(resourceList));

                var randInt = randIntGen.Next(0, resourceList.Count - 1);
            
                resourceId = resourceList[randInt];
                Console.WriteLine("ID [ {0} ] selected at random", resourceId);
            }
            Console.WriteLine("Getting resource with ID [ {0} ]", resourceId);
            var singleResource = await resourceManager.GetResourceByIdAsync(resourceId);
            Console.WriteLine(Serialize(singleResource));

            Console.WriteLine("Get the top level series archival objects for this resource:");
            var seriesCollection = await resourceManager.GetTopLevelSeriesArchivalObjects(resourceId);
            Console.WriteLine(Serialize(seriesCollection));
            

            Console.WriteLine("Fetch the small tree of this resource: ");
            var smallTree = await resourceManager.GetSmallResourceTreeAsync(resourceId);
            Console.WriteLine(Serialize(smallTree));

        }

        public static async Task TestRepositories(ArchivesSpaceService aspaceService)
        {
            Console.WriteLine("All repositories: ");
            var allRepos = await aspaceService.GetAllRepositoriesAsync();
            Console.WriteLine(Serialize(allRepos));

            Console.WriteLine("Set repository to BRBL by name");
            aspaceService.SetActiveRepository("BRBL");
            Console.WriteLine(Serialize(aspaceService.ActiveRepository));

            Console.WriteLine("Now set repository to MSSA by ID");
            aspaceService.SetActiveRepository(12);
            Console.WriteLine(Serialize(aspaceService.ActiveRepository));
        }

        public static async Task TestAccessions(ArchivesSpaceService aspaceService)
        {

            Console.WriteLine("Getting all accessions:");
            var accessionManager = new ArchivesSpaceAccessionManager(aspaceService);
            var accessionList = await accessionManager.GetAllAccessionIdsAsync();
            Console.WriteLine(Serialize(accessionList));

            Console.WriteLine("Get a random accession from the list:");
            var randIntGen = new Random();
            var randInt = randIntGen.Next(0, accessionList.Count - 1);
            Console.WriteLine("Getting accession with ID [ {0} ]", accessionList[randInt]);
            var singleAccession = await accessionManager.GetAccessionByIdAsync(accessionList[randInt]);
            Console.WriteLine(Serialize(singleAccession));
        }

        public static async Task<List<Tuple<string, long>>> TestArchivalObjects(ArchivesSpaceService aspaceService, bool getAllObjects, int targetId = 0)
        {
            var randIntGen = new Random();
            var sw = new Stopwatch();
            sw.Start();
            var returnList = new List<Tuple<string, long>>();

            if (targetId == 0 && !getAllObjects)
            {
                throw new InvalidOperationException("you must get all objects in order to select a random one");
            }
            
            var archivalObjectManager = new ArchivesSpaceArchivalObjectManager(aspaceService);

            if (getAllObjects)
            {
                sw.Restart();
                Console.WriteLine("Getting all archival objects:");
                var archivalObjectList = await archivalObjectManager.GetAllArchivalObjectIdsAsync();
                returnList.Add(new Tuple<string, long>(String.Format("Retrieve all [ {0} ] archival objects", archivalObjectList.Count), sw.ElapsedMilliseconds));
                Console.WriteLine(Serialize(archivalObjectList));
                if (targetId == 0)
                {
                    Console.WriteLine("Get a random archival object from the list:");
                    var randInt = randIntGen.Next(0, archivalObjectList.Count - 1);
                    targetId = archivalObjectList[randInt];
                }
            }
            /*
            sw.Restart();
            var singleArchivalObject = await archivalObjectManager.GetArchivalObjectByIdAsync(targetId);
            returnList.Add(new Tuple<string, long>(String.Format("Retrieve single archival object with ID [ {0} ]", targetId), sw.ElapsedMilliseconds));
            Console.WriteLine(Serialize(singleArchivalObject));

            sw.Restart();
            Console.WriteLine("Get object's children:");
            var archivalObjectChildren = await archivalObjectManager.GetArchivalObjectChildrenAsync(targetId);
            returnList.Add(new Tuple<string, long>(String.Format("Get immediate children of archival object [ {0} ]", targetId), sw.ElapsedMilliseconds));
            Console.WriteLine(Serialize(archivalObjectChildren));

            sw.Restart();
            Console.WriteLine("And get all instances associated with that object:");
            var archivalObjectInstances = await archivalObjectManager.GetAllInstancesForIdAsync(targetId);
            returnList.Add(new Tuple<string, long>(String.Format("Get all [ {0} ] instances assocaited with archival object [ {1} ]", archivalObjectInstances.Count, targetId), sw.ElapsedMilliseconds));
            Console.WriteLine(Serialize(archivalObjectInstances));

            sw.Restart();
            Console.WriteLine("And get all containers associated with that object:");
            var archivalObjectContainers = await archivalObjectManager.GetAllContainersForIdAsync(targetId);
            sw.Stop();
            int subContainerCount = 0;
            foreach (var archivalObjectContainer in archivalObjectContainers)
            {
                subContainerCount = subContainerCount + archivalObjectContainer.SubContainers.Count;
            }
            returnList.Add(new Tuple<string, long>(String.Format("Get all [ {0} ] containers and [ {1} ] subcontainers associated with object [ {2} ]", archivalObjectContainers.Count, subContainerCount, targetId), sw.ElapsedMilliseconds));
            Console.WriteLine(Serialize(archivalObjectContainers));
            */
            sw.Restart();
            Console.WriteLine("And get all top containers associated with that object:");
            var archivalObjectTopContainers = await archivalObjectManager.GetAllTopContainersForIdAsync(targetId);
            returnList.Add(new Tuple<string, long>(String.Format("Get all [ {0} ] top containers associated with object [ {1} ]", archivalObjectTopContainers.Count, targetId), sw.ElapsedMilliseconds));
            Console.WriteLine(Serialize(archivalObjectTopContainers));

            return returnList;
        }

        public static string Serialize(object inputObject)
        {
            return JsonConvert.SerializeObject(inputObject, Formatting.Indented);
        }
    }
}
