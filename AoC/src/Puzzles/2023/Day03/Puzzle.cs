using System.Text.RegularExpressions;
using System.Text;
using System.Drawing;

namespace AoC.src.Puzzles._2023.Day03
{
    public class Puzzle
    {
        private const int size = 140;
        private readonly List<Gear> _gears;
        private readonly List<Symbol> _symbols;

        public Puzzle(string input)
        {
            input = input.Replace("\r\n", "");
            _gears = ParseGears(input);
            _symbols = ParseSymbols(input);
            MatchSymbolToGears();
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }


        public int SolveTask1()
            => _gears.Sum(g => g.Symbol is not null ? g.Number : 0);

        public int SolveTask2()
        {
            var gearsGroupedWithStarSymbol = _gears
                .Where(g => g.Symbol?.Character == '*')
                .GroupBy(g => g.Symbol)
                .Where(group => group.Count() > 1);

            return gearsGroupedWithStarSymbol.Sum(group => 
                group.Aggregate(1, (acc, gear) => acc * gear.Number));
        }


        private List<Gear> ParseGears(string input)
        {
            var gears = new List<Gear>();
            var matches = Regex.Matches(input, @"\d+");
            foreach (Match match in matches)
            {
                var number = int.Parse(match.Value);
                var index = match.Index;
                gears.Add(new Gear(number, IndexToPoint(index)));
            }
            return gears;
        }

        private List<Symbol> ParseSymbols(string input)
        {
            var symbols = new List<Symbol>();
            var matches = Regex.Matches(input, "[^.\\d]");
            foreach (Match match in matches)
            {
                var ch = match.Value;
                var index = match.Index;
                symbols.Add(new Symbol(ch[0], IndexToPoint(index)));
            }
            return symbols;
        }

        private void MatchSymbolToGears()
        {
            foreach (var gear in _gears)
            {
                foreach (var coordinate in gear.Coordinates)
                {
                    var neighbor = _symbols.FirstOrDefault(p => IsNeighbor(coordinate, p.Coordinate));
                    if (neighbor != default(Symbol))
                    {
                        gear.Symbol = neighbor;
                    }
                }
            }
        }

        private static bool IsNeighbor(Point p1, Point p2)
            => Math.Abs(p1.X - p2.X) <= 1 && Math.Abs(p1.Y - p2.Y) <= 1; 

        private Point IndexToPoint(int index)
        {
            int x = index % size;
            int y = index / size;
            return new Point(x, y);
        }
    }
}
