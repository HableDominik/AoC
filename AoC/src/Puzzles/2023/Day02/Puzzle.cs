using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2023.Day02
{
    public class Puzzle
    {
        private readonly IEnumerable<Game> _games;

        public Puzzle(string input)
        {
            _games = ParseGames(input);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1() => _games.Sum(game => game.IsPossible(12, 13, 14) ? game.Id : 0);

        public int SolveTask2() => _games.Sum(game => game.GetFewestCubesPower());

        private List<Game> ParseGames( string input )
        {
            var lines = Util.readLines(input);
            var games = new List<Game>();
            foreach (var line in lines)
            {
                var game = new Game(TryGetIntValue(line, "*:") ?? -1);
                var split = line.Split(':', ';');
                foreach (var c in split.Skip(1))
                {
                    var r = TryGetIntValue(c, "* red") ?? 0;
                    var g = TryGetIntValue(c, "* green") ?? 0;
                    var b = TryGetIntValue(c, "* blue") ?? 0;
                    game.Reveals.Add(new Reveal(r, g, b));
                }
                games.Add(game);
            }
            return games;
        }

        private static int? TryGetIntValue(string input, string pattern)
        {
            pattern = pattern.Replace("*", "(\\d+)");
            Match match = Regex.Match(input, pattern);
            if (match.Success) return int.Parse(match.Groups[1].Value);
            return null;
        }

    }
}