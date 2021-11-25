using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class MovieCodes //key  --- IMDB_ID; value --- title movie 
    {
        public static ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();

        public static void ReadAndGetData() 
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\MovieCodes_IMDB.tsv", output);
            Parse(output, dictionary);
        }
        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<string, string> dictionary)
        {
            foreach (string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split("\t");
                if (array[4] == "en")
                {
                    dictionary.AddOrUpdate(array[0], array[2], (x, y) => y);
                }
            }
        }
    }
}
