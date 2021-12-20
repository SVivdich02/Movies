using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies
{
    class ActorDirectorNames // id actor / director --- Actor / Director
    {
        public static BlockingCollection<string> output = new BlockingCollection<string>();
        public static ConcurrentDictionary<string, Actor> dictionaryActor = new ConcurrentDictionary<string, Actor>();
        public static ConcurrentDictionary<string, Director> dictionaryDirector = new ConcurrentDictionary<string, Director>();

        public static void ReadAndGetData()
        {
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\ActorsDirectorsNames_IMDB.txt", output);
            Parse();
        }
        public static void Parse()
        {
            Regex regex = new Regex("actor|actress|director");
            
            foreach (string str in output.GetConsumingEnumerable())
            {
                if (regex.IsMatch(str))
                {
                    int index1 = str.IndexOf("\t");
                    string actorID = str.Substring(0, index1);

                    int index2 = str.IndexOf("\t", index1 + 1);
                    string actorName = str.Substring(index1 + 1, index2 - index1 - 1);

                    dictionaryActor.AddOrUpdate(actorID, new Actor(actorID, actorName), (x, y) => y);
                    dictionaryDirector.AddOrUpdate(actorID, new Director(actorID, actorName), (x, y) => y);
                }
            }
        }
    }
}
