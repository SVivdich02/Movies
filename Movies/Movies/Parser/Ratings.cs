using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class Ratings
    {
        //IMDB_ID
        public static ConcurrentDictionary<string, Double> dictionary = new ConcurrentDictionary<string, Double>();

        public static void ReadAndGet()
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\Ratings_IMDB.tsv", output);
            Parse(output, dictionary);
        } 

        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<string, Double> dictionary)
        {
            foreach(string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split("\t");
                dictionary.AddOrUpdate(array[0], Convert.ToDouble(array[1].Replace('.', ',')), (x, y) => y); 
            }
        }
    }
}
