using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace Movies
{
    class Loader
    {
        public static async Task LoadContentAsync(string path, BlockingCollection<string> output) 
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var reader = new StreamReader(stream);
                var firstLine = reader.ReadLine();
                string line = null;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    output.Add(line);
                }
            }
            output.CompleteAdding();
        }
    }
}
