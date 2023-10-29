using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2015.Day12
{
    public class Puzzle
    {
        private readonly string _input;

        public Puzzle(string input)
        {
            _input = input;
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
        {
            return SumNumbersInString(_input);
        }

        private static int SumNumbersInString(string input)
        {            
            var matches = Regex.Matches(input, @"-?\d+")
                .Cast<Match>()
                .Select(m => int.Parse(m.Value))
                .ToList();

            return matches.Sum();
        }

        public long SolveTask2()
        {
            dynamic o = JsonConvert.DeserializeObject(_input);
            return GetSum(o);
        }

        private int GetSum(JObject o)
        {
            bool avoid = o.Properties()
                .Select(x => x.Value).OfType<JValue>()
                .Select(v => v.Value).Contains("red");

            if (avoid) return 0;

            return o.Properties().Sum((dynamic x) => (int)GetSum(x.Value));
        }

        private long GetSum(JArray arr) => arr.Sum((dynamic x) => (long)GetSum(x));

        private long GetSum(JValue val) => val.Type == JTokenType.Integer ? (long)val.Value : 0;
    }
}