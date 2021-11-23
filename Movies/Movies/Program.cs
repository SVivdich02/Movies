using System;
using System.Threading.Tasks;

namespace Movies
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task1 = Task.Run(() => TagCodes.ReadAndGet());
            task1.Wait();
            var q = TagCodes.dictionary;
            Task task2 = Task.Run(() => TagScores.ReadAndGet());
            task2.Wait();
            var q2 = TagScores.dict;
            int a = 45;
        }
    }
}
