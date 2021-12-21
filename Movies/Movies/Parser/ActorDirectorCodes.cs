using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies
{
    class ActorDirectorCodes // id movie --- HashSet<Actor>
    {
        public static BlockingCollection<string> output = new BlockingCollection<string>();
        public static ConcurrentDictionary<string, HashSet<Actor>> dictionaryActor = new ConcurrentDictionary<string, HashSet<Actor>>();
        public static ConcurrentDictionary<string, Director> dictionaryDirector = new ConcurrentDictionary<string, Director>();
        public static void ReadAndGetData()
        {
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\ActorsDirectorsCodes_IMDB.tsv", output);
            Parse();
        }
        public static void Parse()
        {
            Regex regex = new Regex("actor|actress");
            Regex regexDirector = new Regex("director");

            foreach (string str in output.GetConsumingEnumerable())
            {
                if (regex.IsMatch(str))
                {
                    int index1 = str.IndexOf("\t");
                    int index2 = str.IndexOf("\t", index1 + 1);
                    int index3 = str.IndexOf("\t", index2 + 1);

                    string id = str.Substring(0, index1);
                    string idActor = str.Substring(index2 + 1, index3 - index2 - 1);

                    if (ActorDirectorNames.dictionaryActor.ContainsKey(idActor))
                    {
                        dictionaryActor.AddOrUpdate(id, new HashSet<Actor>(new Actor[] {ActorDirectorNames.dictionaryActor[idActor]}),
                        (x, y) =>
                        {
                            y.Add(ActorDirectorNames.dictionaryActor[idActor]);
                            return y;
                        });
                    }
                }
                else if (regexDirector.IsMatch(str))
                {
                    int index1 = str.IndexOf("\t");
                    int index2 = str.IndexOf("\t", index1 + 1);
                    int index3 = str.IndexOf("\t", index2 + 1);

                    string IMDB_ID = str.Substring(0, index1);
                    string idDirector = str.Substring(index2 + 1, index3 - index2 - 1);

                    if (ActorDirectorNames.dictionaryActor.ContainsKey(idDirector))
                    {
                        dictionaryDirector.AddOrUpdate(IMDB_ID, ActorDirectorNames.dictionaryDirector[idDirector], (x, y) => y);
                    }
                }
            }
        }
    }
}
