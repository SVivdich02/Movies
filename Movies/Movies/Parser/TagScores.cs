using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class TagScores // ключ IMDB_ID значение HashSet<Tag>>
    {
        public static ConcurrentDictionary<string, HashSet<Tag>> dictionary = new ConcurrentDictionary<string, HashSet<Tag>>();
        
        public static void ReadAndGet()
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\TagScores_MovieLens.csv", output);
            Parse(output, dictionary);
        }
        
        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<string, HashSet<Tag>> dictionary)
        {
            foreach (string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split(",");
                if (Convert.ToDouble(array[2].Replace('.', ',')) >= 0.5)
                {

                    dictionary.AddOrUpdate(MovieLinks.dictionary[array[0]], new HashSet<Tag>(new Tag[] { TagCodes.dictionary[Convert.ToInt32(array[1])] }),
                    (x, y) =>
                    {
                        y.Add(TagCodes.dictionary[Convert.ToInt32(array[1])]);
                        return y;
                    });
                    
                }
            } 
        }
    }
}
