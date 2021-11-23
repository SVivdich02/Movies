using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class ActorDirectorNames
    {
        public static ConcurrentDictionary<string, Actor> dictionary = new ConcurrentDictionary<string, Actor>();

        public static void ReadAndGetData()
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\ActorsDirectorsNames_IMDB.txt", output);
            Task.Run(() => Parse(output, dictionary));
        }
        public static Task Parse(BlockingCollection<string> output, ConcurrentDictionary<string, Actor> dictionary)
        {
            return new Task(() =>
            {
                foreach (string str in output.GetConsumingEnumerable())
                {
                    string[] array = str.Split("\t");
                    dictionary.AddOrUpdate(array[0], new Actor(array[1]), (x, y) => y);
                }
            });
        }
    }
}
