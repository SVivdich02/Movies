using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class Actor
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public Actor(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
