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
            Regex regexActor = new Regex("actor|actress");
            Regex regexDirector = new Regex("director");

            foreach (string str in output.GetConsumingEnumerable())
            {
                if (regexActor.IsMatch(str) || regexDirector.IsMatch(str))
                {
                    int index1 = str.IndexOf("\t");
                    string personID = str.Substring(0, index1);

                    int index2 = str.IndexOf("\t", index1 + 1);
                    string personName = str.Substring(index1 + 1, index2 - index1 - 1);

                    if (regexActor.IsMatch(str))
                    {
                        dictionaryActor.AddOrUpdate(personID, new Actor(personID, personName), (x, y) => y);
                    }

                    if (regexDirector.IsMatch(str))
                    {
                        dictionaryDirector.AddOrUpdate(personID, new Director(personID, personName), (x, y) => y);
                    }
                }
            }
        }
    }
}
