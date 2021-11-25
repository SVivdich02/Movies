using System;
using System.Threading.Tasks;

namespace Movies
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "tt0000004	5	A Good Beer	N	N	N	N	0";
            int index1 = str.IndexOf("\t");
            string id = str.Substring(0, index1);

            int index2 = str.IndexOf("\t", index1 + 1);
            int index3 = str.IndexOf("\t", index2 + 1);
            string movieName = str.Substring(index2 + 1, index3 - index2 - 1);
            int a = 5;
        }
    }
}
