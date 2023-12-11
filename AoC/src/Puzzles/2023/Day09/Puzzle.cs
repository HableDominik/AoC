namespace AoC.src.Puzzles._2023.Day09
{
    public class Puzzle
    {
        private readonly List<List<long>> _histories;

        public Puzzle(string input)
        {
            _histories = Util.readLines(input)
                .Select(line => line.Split(' ').Select(long.Parse).ToList())
                .ToList();
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public long SolveTask1() => _histories.Sum(history => Forecast(history, list => list.Last()));

        public long SolveTask2() => _histories.Sum(history => Forecast(history, list => list.First(), -1));

        private static long Forecast(
            List<long> histories, 
            Func<List<long>, long> firstLast, 
            int toggleValue = 1)
        {
            long sum = 0;
            long sign = 1;
            var oldEntries = histories.ToList();
            var newEntries = new List<long>();
            do
            {
                newEntries.Clear();
                for (var i = 0; i < oldEntries.Count - 1; i++)
                {
                    newEntries.Add(oldEntries[i + 1] - oldEntries[i]);
                }
                sum += firstLast(oldEntries) * sign;
                sign *= toggleValue;
                oldEntries = newEntries.ToList();
            } while (newEntries.Sum() != 0);
            return sum;
        }
    }
}
