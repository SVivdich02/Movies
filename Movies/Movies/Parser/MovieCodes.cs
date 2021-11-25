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
        public static ConcurrentDictionary<string, Movie> dictionary = new ConcurrentDictionary<string, Movie>();

        public static void ReadAndGetData() 
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\MovieCodes_IMDB.tsv", output);
            Parse(output, dictionary);
        }
        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<string, Movie> dictionary)
        {
            Regex regex = new Regex("\ten|ru\t");
            Regex regex_en = new Regex("\ten\t");

            foreach (string str in output.GetConsumingEnumerable())
            {
                if (regex.IsMatch(str))
                {
                    int index1 = str.IndexOf("\t");
                    string id = str.Substring(0, index1);

                    int index2 = str.IndexOf("\t", index1 + 1);
                    int index3 = str.IndexOf("\t", index2 + 1);
                    string movieName = str.Substring(index2 + 1, index3 - index2 - 1);

                    if (Ratings.dictionary.ContainsKey(id)
                        && ActorDirectorCodes.dictionary.ContainsKey(id)
                        && TagScores.dictionary.ContainsKey(id))
                    {
                        var id_lang = id + (regex_en.IsMatch(str) ? "en" : "ru");
                        dictionary.AddOrUpdate(id_lang,
                        new Movie(id_lang, movieName, ActorDirectorCodes.dictionary[id], TagScores.dictionary[id], Ratings.dictionary[id]),
                        (x, y) => y);
                    }
                }
            }
        }
    }
}
