using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class MovieLinks // MovieID --- IMDB_ID
    {
        public static BlockingCollection<string> output = new BlockingCollection<string>();
        public static ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();
        public static void ReadAndGet()
        {
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\links_IMDB_MovieLens.csv", output);
            Parse();
        }
        public static void Parse()
        {
            foreach (string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split(',');
                dictionary.AddOrUpdate(array[0], "tt" + array[1], (x, y) => y);
            }
        }
    }
}
