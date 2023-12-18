using System.Drawing;
using System.Reflection;
using System.Reflection.Metadata;

namespace AoC.src.Puzzles._2023.Day18
{
    public class Puzzle
    {
        private record Plan(char Dir, int Dist, string RGB);
        private readonly List<Plan> _plan;

        public Puzzle(string input)
        {
            _plan = ParsePlan(Util.readLines(input));
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private List<Plan> ParsePlan(List<string> lines)
        {
            var plan = new List<Plan>();
            foreach(var line in lines)
            {
                var parts = line.Split(' ');
                var dist = int.Parse(parts[1]);
                var RGB = parts[2].Substring(2, parts[2].Length - 3);
                plan.Add(new Plan(parts[0][0], dist, RGB));
            }
            return plan;
        }

        public int SolveTask1()
        {
            var trench = new List<Point>();
            var current = new Point(0,0);
            trench.Add(current);

            foreach(var plan in _plan)
            {
                Point dir = GetDir(plan.Dir);
                for(int i = 0; i < plan.Dist; i++)
                {
                    current.Offset(dir);
                    trench.Add(current);
                }
            }

            var area = Math.Abs(trench.Take(trench.Count - 1)
                .Select((p, i) => p.X * trench[i + 1].Y - trench[i + 1].X * p.Y)
            .Sum() / 2);

            var border = trench.Count / 2 + 1;

            return area + border;
        }

        private void Print(List<Point> points)
        {
            var minX = points.Min(p => p.X);
            var maxX = points.Max(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxY = points.Max(p => p.Y);

            for(int y = minY; y <= maxY; y++)
            {
                for(int x = minX; x <= maxX; x++)
                {
                    var current = new Point(x, y);
                    Console.Write(points.Contains(current) ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        private static Point GetDir(char dir)
            => dir switch
            {
                'R' => new Point(1,0),
                'D' => new Point(0,1),
                'L' => new Point(-1,0),
                'U' => new Point(0,-1),
                _ => new Point(0,0)
            };

        public string SolveTask2() => string.Empty;
    }
}
