using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class MovieCodes
    {
        public static ConcurrentDictionary<string, string> dictionary;// = new ConcurrentDictionary<string, string>();

        public static void ReadAndGetData() // why static?
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\MovieCodes_IMDB.tsv", output);
            dictionary = new ConcurrentDictionary<string, string>();
            ParseData(output, dictionary).Wait();
        }
        public static Task ParseData(BlockingCollection<string> output, ConcurrentDictionary<string, string> dictionary)
        {
            return Task.Factory.StartNew(() => 
            {
                foreach (string str in output.GetConsumingEnumerable())
                {
                    string[] array = str.Split("\t");
                    if (array[4] == "en")
                    {
                        dictionary.AddOrUpdate(array[0], array[2], (x, y) => y);
                    }
                }
            });
        }
    }
}
