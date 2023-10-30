namespace AoC.src.Puzzles._2015.Day14
{
    public class Puzzle
    {
        private readonly List<Reindeer> _reindeers;

            public Puzzle(string input)
        {
            _reindeers = ParseReindeers(Util.readLines(input));
            Race(2503);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private static List<Reindeer> ParseReindeers(List<string> list)
        {
            var deers = new List<Reindeer> ();
            foreach(string d in list)
            {
                var split = d.Split(' ');
                deers.Add(new Reindeer(split[1], 
                    int.Parse(split[3]), 
                    int.Parse(split[6]), 
                    int.Parse(split[13])));
            }
            return deers;
        }

        private void Race(int totalDistance)
        {
            for (int i = 0; i < totalDistance; i++)
            {
                foreach (var deer in _reindeers)
                {
                    deer.Tick();
                }

                int maxDistance = _reindeers.Max(r => r.Distance);
                _reindeers.Where(r => r.Distance == maxDistance).ToList().ForEach(reindeer => reindeer.Points++);
            }
        }

        public int SolveTask1() => _reindeers.Max(r => r.Distance);

        public int SolveTask2() => _reindeers.Max(r => r.Points);
        
    }
}
