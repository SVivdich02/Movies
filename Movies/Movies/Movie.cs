using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Movies
{
    public class Movie
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public HashSet<Actor> Actors { get; set; }
        public Director Director { get; set; }
        public HashSet<Tag> Tags { get; set; }
        public double Rating { get; set; }
        public string Similar { get; set; }

        public Movie() { }
        public Movie(string id, string name, HashSet<Actor> actors, Director director, HashSet<Tag> tags, double rating)
        {
            ID = id;
            Name = name;
            Actors = actors;
            Director = director;
            Tags = tags;
            Rating = rating;
        }

        public void GetSimilar()
        {
            Dictionary<string, double> dictCompare = new Dictionary<string, double>();
            foreach (var movie in Calculation.movieDictionary)
            {
                var result = Count(movie.Value);
                dictCompare.Add(movie.Key, result);
            }

            var sorted = dictCompare.OrderByDescending(item => item.Value).Take(10).Select(item => item.Key + " ").ToList<string>();

            foreach (var movieID in sorted)
            {
                Similar += movieID;
            }
        }

        public double Count(Movie movie)
        {
            double result = 0;
            var minTags = (this.Tags.Count <= movie.Tags.Count) ? this.Tags : movie.Tags;
            var maxTags = (minTags == this.Tags) ? movie.Tags : this.Tags;

            int countTag = 0;

            foreach (var tag in minTags)
            {
                if (maxTags.Contains(tag))
                {
                    countTag++;
                }
            }

            double coefTag = (countTag / minTags.Count) * 0.3;

            var minActors = (this.Actors.Count <= movie.Actors.Count) ? this.Actors : movie.Actors;
            var maxActors = (minActors == this.Actors) ? movie.Actors : this.Actors;

            int countActor = 0;

            foreach (var actor in minActors)
            {
                if (maxActors.Contains(actor))
                {
                    countActor++;
                }
            }

            double coefActor = (countActor / minActors.Count) * 0.1;

            double coefDirector = 0;

            if (this.Director == movie.Director)
            {
                coefDirector = 0.1;
            }

            result = coefDirector + coefTag + coefActor;
            return result;
        }

    }
}

