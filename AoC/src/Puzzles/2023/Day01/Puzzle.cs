using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2023.Day01
{
    public class Puzzle
    {
        private readonly List<string> _inputs;

        public Puzzle(string input)
        {
            _inputs = Util.readLines(input);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
            => _inputs.Sum(input => GetFirstAndLastDigitAsNumber(input));
        

        private static int GetFirstAndLastDigitAsNumber(string s)
            => int.Parse(Regex.Match(s, @"\d").Value + Regex.Match(s, @"\d(?=\D*$)").Value);

        public int SolveTask2()
            => _inputs.Sum(input => GetFirstAndLastDigitAsNumber(ReplaceNumbers(input)));

        private static string ReplaceNumbers(string s)
            =>  s.Replace("one", "o1e")
                .Replace("two", "t2o")
                .Replace("three", "t3e")
                .Replace("four", "f4r")
                .Replace("five", "f5e")
                .Replace("six", "s6x")
                .Replace("seven", "s7n")
                .Replace("eight", "e8t")
                .Replace("nine", "n9e");
    }
}
