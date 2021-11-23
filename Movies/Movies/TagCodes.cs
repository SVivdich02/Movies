using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class TagCodes
    {
        public static ConcurrentDictionary<int, Tag> dictionary = new ConcurrentDictionary<int, Tag>();

        public static void ReadAndGet()
        {
            var output = new BlockingCollection<string>();
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\TagCodes_MovieLens.csv", output);
            Parse(output, dictionary);
        }
        public static void Parse(BlockingCollection<string> output, ConcurrentDictionary<int, Tag> dictionary)
        {
            foreach (string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split(",");
                dictionary.AddOrUpdate(Convert.ToInt32(array[0]), new Tag(array[1]), (x, y) => y);
            };
        }
    }
}
