using System;
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


            Task taskMovieCodes = Task.WhenAll(taskRatings, taskActorCodes, taskTagScores).ContinueWith(x => MovieCodes.ReadAndGetData());
            taskMovieCodes.Wait();
            var q = MovieCodes.dictionary;
            int a = 5;
        }
    }
}
