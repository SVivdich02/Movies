using System;
using System.Threading.Tasks;

namespace Movies
{
    class Program
    {
        static void Main(string[] args)
        {
            TagScores.ReadAndGet().Wait();
            var q = TagScores.dict;
            int a = 45;
        }
    }
}
