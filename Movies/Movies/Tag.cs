using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Movies
{
    class Tag
    {
        [Key]
        public string Name { get; set; }

        public Tag() { }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
