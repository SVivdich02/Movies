using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies
{
    class Calculation
    {
        public static void Method()
        {
            Task taskActorNames = Task.Run(() => ActorDirectorNames.ReadAndGetData());
            Task taskRatings = Task.Run(() => Ratings.ReadAndGet());
            Task taskTagCodes = Task.Run(() => TagCodes.ReadAndGet());

            Task taskActorCodes = taskActorNames.ContinueWith(x => ActorDirectorCodes.ReadAndGetData());
            Task taskLinks = taskTagCodes.ContinueWith(x => MovieLinks.ReadAndGet());

            Task taskTagScores = Task.WhenAll(taskLinks, taskTagCodes).ContinueWith(x => TagScores.ReadAndGet());

            Task taskMovieCodesDictionary = Task.WhenAll(taskRatings, taskActorCodes, taskTagScores).ContinueWith(x => MovieCodes.ReadAndGetData());
            taskMovieCodesDictionary.Wait();

            var movieDictionary = MovieCodes.dictionary;
            int a = 5;

            var actorsDictionary = new ConcurrentDictionary<Actor, HashSet<Movie>>();
            var directorsDictionary = new ConcurrentDictionary<Director, HashSet<Movie>>();
            var tagsDictionary = new ConcurrentDictionary<Tag, HashSet<Movie>>();

            Task taskActorsDictionary = Task.Run(() => 
            {
                foreach (var movie in movieDictionary)
                {
                    var actors = movie.Value.Actors;
                    foreach (var actor in actors)
                    {
                        actorsDictionary.AddOrUpdate(actor,
                            new HashSet<Movie>(new Movie[] { movie.Value }),
                            (x, y) =>
                            {
                                y.Add(movie.Value);
                                return y;
                            });
                    }
                }
            });

            Task taskDirectorsDictionary = Task.Run(() =>
            {
                foreach (var movie in movieDictionary)
                {
                    var director = movie.Value.Director;
                    directorsDictionary.AddOrUpdate(director,
                        new HashSet<Movie>(new Movie[] { movie.Value }),
                        (x, y) =>
                        {
                            y.Add(movie.Value);
                            return y;
                        });
                }
            });
            
            Task taskTagsDictionary = Task.Run(() =>
            {
                foreach (var movie in movieDictionary)
                {
                    var tags = movie.Value.Tags;
                    foreach (var tag in tags)
                    {
                        tagsDictionary.AddOrUpdate(tag,
                            new HashSet<Movie>(new Movie[] { movie.Value }),
                            (x, y) =>
                            {
                                y.Add(movie.Value);
                                return y;
                            });
                    }
                }
            });

            Task.WaitAll(taskActorsDictionary, taskDirectorsDictionary, taskTagsDictionary);

            using (MovieContext db = new MovieContext())
            {
                foreach (var movie in movieDictionary)
                {
                    db.Movies.Add(movie.Value);
                }

                db.SaveChanges();
            }
        }
    }
}
