using System.Drawing;

namespace AoC.src.Puzzles._2023.Day11
{
    public class Puzzle
    {
        private readonly int _size;
        private readonly List<Point> _galaxies;

        public Puzzle(string input)
        {
            var lines = Util.readLines(input);
            _size = lines.First().Length;
            _galaxies = ParseSpace(lines);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }
        private List<Point> ParseSpace(List<string> input)
        {
            var galaxies = new List<Point>();
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (input[y][x] == '#') galaxies.Add(new Point(x, y));
                }
            }
            return galaxies;
        }

        private List<Point> ExpandSpace(List<Point> galaxies, int expandBy = 1)
        {
            var occupiedColumns = new HashSet<int>(galaxies.Select(g => g.X));
            var emptyColumns = Enumerable.Range(0, _size).Where(i => !occupiedColumns.Contains(i));
            var occupiedRows = new HashSet<int>(galaxies.Select(g => g.Y));
            var emptyRows = Enumerable.Range(0, _size).Where(i => !occupiedRows.Contains(i));

            var expandedGalaxies = new List<Point>();

            foreach (var galaxy in galaxies)
            {
                var newX = galaxy.X + emptyColumns.Count(c => c < galaxy.X) * expandBy;
                var newY = galaxy.Y + emptyRows.Count(r => r < galaxy.Y) * expandBy;
                expandedGalaxies.Add(new Point(newX, newY));
            }
            return expandedGalaxies;
        }

        public long SolveTask1() => CalcDistances(ExpandSpace(_galaxies.ToList()));

        public long SolveTask2() => CalcDistances(ExpandSpace(_galaxies.ToList(), 1000000-1));

        private static long CalcDistances(List<Point> galaxies)
        {
            long sum = 0;
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    long manhattanDistance = 
                        Math.Abs(galaxies[j].X - galaxies[i].X) + 
                        Math.Abs(galaxies[j].Y - galaxies[i].Y);
                    sum += manhattanDistance;
                }
            }
            return sum;
        }
    }
}