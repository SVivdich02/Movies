using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class Movie
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public HashSet<Actor> Actors { get; set; }
        public Director Director { get; set; }
        public HashSet<Tag> Tags { get; set; }
        public double Rating { get; set; }
        public Movie(string id, string name, HashSet<Actor> actors, Director director, HashSet<Tag> tags, double rating)
        {
            ID = id;
            Name = name;
            Actors = actors;
            Director = director;
            Tags = tags;
            Rating = rating;
        }

    }
}

