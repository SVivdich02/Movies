using System;
using System.Threading.Tasks;

namespace Movies
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task1 = Task.Run(() => MovieLinks.ReadAndGet());
            Task task2 = Task.Run(() => TagCodes.ReadAndGet());
            Task.WaitAll(task1, task2);

            Task task3 = Task.Run(() => TagScores.ReadAndGet());
            task3.Wait();

            var q = TagScores.dictionary;
            int a = 10;
        }
    }
}
