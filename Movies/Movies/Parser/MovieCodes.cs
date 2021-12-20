using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies
{
    class MovieCodes //key  --- IMDB_ID; value --- title movie 
    {
        public static BlockingCollection<string> output = new BlockingCollection<string>();
        public static ConcurrentDictionary<string, Movie> dictionary = new ConcurrentDictionary<string, Movie>();

        public static void ReadAndGetData() 
        {
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\MovieCodes_IMDB.tsv", output);
            Parse();
        }
        public static void Parse()
        {
            Regex regex = new Regex("\ten|ru\t");

            foreach (string str in output.GetConsumingEnumerable())
            {
                if (regex.IsMatch(str))
                {
                    int index1 = str.IndexOf("\t");
                    string IMDB_ID = str.Substring(0, index1);

                    int index2 = str.IndexOf("\t", index1 + 1);
                    int index3 = str.IndexOf("\t", index2 + 1);
                    string movieName = str.Substring(index2 + 1, index3 - index2 - 1);

                    if (Ratings.dictionary.ContainsKey(IMDB_ID)
                        && ActorDirectorCodes.dictionaryActor.ContainsKey(IMDB_ID)
                        && TagScores.dictionary.ContainsKey(IMDB_ID))
                    {
                        var movieDirector = ActorDirectorCodes.dictionaryDirector.ContainsKey(IMDB_ID) ? ActorDirectorCodes.dictionaryDirector[IMDB_ID] : null;

                        dictionary.AddOrUpdate(IMDB_ID,
                        new Movie(IMDB_ID, 
                        movieName, 
                        ActorDirectorCodes.dictionaryActor[IMDB_ID],
                        movieDirector, 
                        TagScores.dictionary[IMDB_ID], 
                        Ratings.dictionary[IMDB_ID]),
                        (x, y) => y);
                    }
                }
            }
        }
    }
}
