using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class TagCodes // ключ tagId значение объект Tag
    {
        public static BlockingCollection<string> output = new BlockingCollection<string>();
        public static ConcurrentDictionary<int, Tag> dictionary = new ConcurrentDictionary<int, Tag>();

        public static void ReadAndGet()
        {
            Task task1 = Loader.LoadContentAsync(@"D:\ml-latest\TagCodes_MovieLens.csv", output);
            Parse();
        }
        public static void Parse()
        {
            foreach (string str in output.GetConsumingEnumerable())
            {
                string[] array = str.Split(",");
                dictionary.AddOrUpdate(Convert.ToInt32(array[0]), new Tag(array[1]), (x, y) => y);
            }
        }
    }
}
