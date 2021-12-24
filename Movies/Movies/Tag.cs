using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Movies
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public HashSet<Movie> Movies { get; set; }

        public Tag() { }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
