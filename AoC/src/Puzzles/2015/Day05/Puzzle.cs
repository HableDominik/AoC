namespace AoC.src.Puzzles._2015.Day05
{
    public class Puzzle
    {
        private readonly string _input;
        private readonly List<string> strings;

        public Puzzle(string input)
        {
            _input = input;
            strings = Util.readLines(_input);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
            => strings
                .Count(s => 
                    ContainsAtLeastThreeVowels(s)
                    && ContainsDoubleLetter(s)
                    && ContainsNoBadStrings(s));

        private static bool ContainsAtLeastThreeVowels(string input)
            => input.Count(c => "aeiou".Contains(c)) >= 3;

        private static bool ContainsDoubleLetter(string input)
            => Enumerable.Range(0, input.Length - 1)
                .Any(index => input[index] == input[index + 1]);

        private static bool ContainsNoBadStrings(string input)
        {
            string[] badStrings = { "ab", "cd", "pq", "xy" };
            return !badStrings.Any(sub => input.Contains(sub));
        }

        public int SolveTask2()
            => strings
                .Count(s =>
                    ContainsDoublePair(s)
                    && ContainsRepeatingLetterWithOneBetween(s));

        private static bool ContainsDoublePair(string input)
        => Enumerable.Range(0, input.Length - 1)
            .Any(i => input.IndexOf(input.Substring(i, 2), i + 2) != -1);

        private static bool ContainsRepeatingLetterWithOneBetween(string input)
            => Enumerable.Range(0, input.Length - 2)
                .Any(index => input[index] == input[index + 2]);
    }
}
