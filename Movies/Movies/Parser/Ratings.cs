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
        public static BlockingCollection<string> output = new BlockingCollection<string>();
        public static ConcurrentDictionary<string, Double> dictionary = new ConcurrentDictionary<string, Double>();

        public static void ReadAndGet()
        {
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\Ratings_IMDB.tsv", output);
            Parse();
        } 

        public static void Parse()
        {
            foreach(string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split("\t");
                dictionary.AddOrUpdate(array[0], Convert.ToDouble(array[1].Replace('.', ',')), (x, y) => y); 
            }
        }
    }
}
