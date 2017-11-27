using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;

namespace ArchivesSpace_.Net_Client
{
    public static class Extensions
    {
        public static void SetArchivesSpaceHeader(this HttpClient client, string sessionKey)
        {
            var archivesSpaceHeaderName = "X-ArchivesSpace-Session";
            if (client.DefaultRequestHeaders.Contains(archivesSpaceHeaderName))
            {
                AsLogger.LogDebug("[SetArchivesSpaceHeader] Existing header found, removing from request");
                client.DefaultRequestHeaders.Remove(archivesSpaceHeaderName);
            }
            AsLogger.LogDebug(String.Format("[SetArchivesSpaceHeader] Setting ArchivesSpace Header with name [ {0} ] to value [ {1} ]", archivesSpaceHeaderName, sessionKey)); 
            client.DefaultRequestHeaders.Add(archivesSpaceHeaderName, sessionKey);
        }

        //http://stackoverflow.com/a/25877042
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(dop)
                select Task.Run(async delegate
                {
                    using (partition)
                        while (partition.MoveNext())
                            await body(partition.Current).ContinueWith(t =>
                            {
                                //observe exceptions
                            });

                }));
        }

        //http://stackoverflow.com/questions/7062882/searching-a-tree-using-linq
        public static IEnumerable<SmallTree> Descendants(this SmallTree root)
        {
            var tree = new Stack<SmallTree>(new[] { root });
            while (tree.Any())
            {
                var node = tree.Pop();
                yield return node;
                foreach (var n in node.Children) tree.Push(n);
            }
        }
    }
}