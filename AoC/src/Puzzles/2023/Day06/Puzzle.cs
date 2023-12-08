namespace AoC.src.Puzzles._2023.Day06
{
    public class Puzzle
    {
        record Race(long Time, long Distance);
        private readonly List<Race> _races;

        public Puzzle(string input)
        {
            var lines = input.Split('\n');
            var times = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse);
            var dist = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse);
            _races = times.Zip(dist, (t, d) => new Race(t, d)).ToList();

            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public long SolveTask1() => _races.Aggregate(1L, (res, race) => res * CalcWins(race));

        public static long SolveTask2() => CalcWins(new Race(58996469, 478223210191071));

        private static long CalcWins(Race race)
        {
            var wins = 0;
            for (long i = 1; i < race.Time - 1; i++)
            {
                if (CalcDistance(i, race.Time) > race.Distance) wins++;
            }
            return wins;
        }

        private static long CalcDistance(long press, long total) => (total - press) * press;
    }
}