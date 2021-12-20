using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class Director
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public Director(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
