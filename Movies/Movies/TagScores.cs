using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class TagScores
    {
        public static ConcurrentDictionary<int, HashSet<Tag>> dict = new ConcurrentDictionary<int, HashSet<Tag>>();
        
        public static void ReadAndGet()
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\TagScores_MovieLens.csv", output);
            Parse(output, dict);
        }
        
        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<int, HashSet<Tag>> dict)
        {
            foreach (string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split(",");
                if (Convert.ToDouble(array[2].Replace('.', ',')) >= 0.5)
                {
                    dict.AddOrUpdate(Convert.ToInt32(array[0]), new HashSet<Tag>(new Tag[] { TagCodes.dictionary[Convert.ToInt32(array[1])] }),
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
