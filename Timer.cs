using System.Diagnostics;

namespace TimeManagerTest
{
    internal class Timer
    {
        /*public static List<Stopwatch> listOfWatches = new List<Stopwatch>();
        private List<string> acts = new List<string>();
        public void Activity(User user, string yourActivity)
        {
            var index = RepeatChecking();
            if (index == int.MaxValue)
            {
                var stopwatch = new Stopwatch();
                listOfWatches.Add(stopwatch);
                index = 0;
            }
            else if (index == listOfWatches.Count)
            {
                var stopwatch = new Stopwatch();
                listOfWatches.Add(stopwatch);
            }
            listOfWatches[index].Start();
            int timerLoopCheck = 1;
            Console.WriteLine(
                "1 - end of this activity, 2 - pause timer, enter - check how much time did you spend by doing this activity so far.");
            while (timerLoopCheck != 0)
            {
                Console.WriteLine("{0:hh\\:mm\\:ss\\.ff} ", listOfWatches[index].Elapsed);
                int.TryParse(Console.ReadLine(), out int pauseResult);
                if (pauseResult == 1)
                {
                    listOfWatches[index].Stop();
                    timerLoopCheck = 0;
                }
                else if (pauseResult == 2)
                {
                    int continueTimerResult = 0;
                    listOfWatches[index].Stop();
                    while (continueTimerResult != 1)
                    {
                        Console.WriteLine("1 - continue timer");
                        var continueTimer = Console.ReadLine();
                        int.TryParse(continueTimer, out continueTimerResult);
                        if (continueTimerResult == 1)
                        {
                            listOfWatches[index].Start();
                        }
                    }
                }
            }
            int RepeatChecking()
            {
                int indexer = int.MaxValue;
                for (int i = 0; i < acts.Count; i++)
                {
                    if (acts[i] == yourActivity)
                    {
                        indexer = i;
                        break;
                    }
                    else
                    {
                        indexer = listOfWatches.Count;
                    }
                }
                if (indexer == listOfWatches.Count || indexer == int.MaxValue)
                {
                    acts.Add(yourActivity);
                }
                return indexer;

            }
        }*/
    }
}
