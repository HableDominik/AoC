using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2015.Day08
{
    public class Puzzle
    {
        private readonly List<string> _strings;

        public Puzzle(string input)
        {
            _strings = Util.readLines(input);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1() => _strings.Sum(s => s.Length - DecodeString(s).Length);

        private string DecodeString(string str)
            => Regex.Replace(str, "\\\\x[a-f0-9]{2}", ".")
                    .Replace("\\\\", ".")
                    .Replace("\\\"", ".")
                    .Trim('"');


        public int SolveTask2() => _strings.Sum(s => EncodeString(s).Length - s.Length);

        private string EncodeString(string str)
            => "\"" +
                str.Replace("\"", "..")
                    .Replace("\\", "..")
                + "\"";
    }
}
