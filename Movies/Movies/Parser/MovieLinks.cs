using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class MovieLinks // ключ MovieID значение IMDB_ID
    {
        public static ConcurrentDictionary<string, string> dictionary = new ConcurrentDictionary<string, string>();
        public static void ReadAndGet()
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\links_IMDB_MovieLens.csv", output);
            Parse(output, dictionary);
        }
        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<string, string> dictionary)
        {
            foreach (string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split(',');
                dictionary.AddOrUpdate(array[0], "tt" + array[1], (x, y) => y);
            }
        }
    }
}
