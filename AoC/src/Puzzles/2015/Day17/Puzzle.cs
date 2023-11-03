using System.Collections.Generic;

namespace AoC.src.Puzzles._2015.Day17
{
    public class Puzzle
    {
        private readonly List<List<int>> _validCombinations;
        private readonly int _total = 150;

        public Puzzle(string input)
        {
            _validCombinations = GetValidCombinations(ParseContainer(input), 150);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private static List<int> ParseContainer(string input)
        {
            var lines = Util.readLines(input);
            List<int> container = new();
            lines.ForEach(line => container.Add(int.Parse(line)));
            return container;
        }

        public int SolveTask1() => _validCombinations.Count;

        private static List<List<int>> GetValidCombinations(List<int> container, int total)
        {
            var combinations = Enumerable
             .Range(1, (1 << container.Count) - 1)
             .Select(index => container
                 .Where((item, idx) => ((1 << idx) & index) != 0)
                 .ToList());

            return combinations.Where(comb => comb.Sum() == total).ToList();
        }

        public int SolveTask2()
        {
            int min = _validCombinations.Min(c => c.Count);
            return _validCombinations.Count(c => c.Count == min);
        }
    }
}
