using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies
{
    class TagScores //  IMDB_ID --- HashSet<Tag>>
    {
        public static BlockingCollection<string> output = new BlockingCollection<string>();
        public static ConcurrentDictionary<string, HashSet<Tag>> dictionary = new ConcurrentDictionary<string, HashSet<Tag>>();
        
        public static void ReadAndGet()
        {
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\TagScores_MovieLens.csv", output);
            Parse();
        }
        
        public static void Parse()
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
