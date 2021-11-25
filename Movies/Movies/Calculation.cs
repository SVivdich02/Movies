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
            Task task_Ratings = Task.Run(() => Ratings.ReadAndGet());
            Task task_ActorNames = Task.Run(() => ActorDirectorNames.ReadAndGetData());
            Task task_Links = Task.Run(() => MovieLinks.ReadAndGet());

            Task task_ActorCodes = task_ActorNames.ContinueWith(x => ActorDirectorCodes.ReadAndGetData());
            Task task_TagCodes = Task.Run(() => TagCodes.ReadAndGet());

            Task task_TagScores = Task.WhenAll(task_Links, task_TagCodes).ContinueWith(x => TagScores.ReadAndGet());

            Task task_MovieCodes = Task.WhenAll(task_Ratings, task_ActorCodes, task_TagScores).ContinueWith(x => MovieCodes.ReadAndGetData());
        }
    }
}
