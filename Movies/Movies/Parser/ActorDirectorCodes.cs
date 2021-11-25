using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class ActorDirectorCodes // ключ - айдишник фильма значение - множество актеров
    {
        public static ConcurrentDictionary<string, HashSet<Actor>> dictionary = new ConcurrentDictionary<string, HashSet<Actor>>();
        public static void ReadAndGetData()
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\ActorsDirectorsCodes_IMDB.tsv", output);
            Parse(output, dictionary);
        }
        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<string, HashSet<Actor>> dictionary)
        {
            foreach(string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split("\t");
                if (array[3] == "actor" || array[3] == "actress")
                {
                    dictionary.AddOrUpdate(array[0], new HashSet<Actor>(new Actor[] { ActorDirectorNames.dictionary[array[2]] }),
                    (x, y) =>
                    {
                        y.Add(ActorDirectorNames.dictionary[array[2]]);
                        return y;
                    });
                }
            }
        }
    }
}
