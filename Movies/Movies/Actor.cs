using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Movies
{
    class Actor
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public HashSet<Movie> Movies { get; set; }

        public Actor() { }

        public Actor(string id, string name)
        {
            ID = id;
            Name = name;
        }

    }
}
