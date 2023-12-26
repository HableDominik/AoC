using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;

namespace AoC.src.Puzzles._2023.Day18
{
    public class Puzzle
    {
        private record Plan(char Dir, int Dist);
        private readonly List<string> _lines;

        public Puzzle(string input)
        {
            _lines = Util.readLines(input);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }
        public long SolveTask1() => CalculateVolume(GetBorder(ParsePlan1(_lines)));

        public long SolveTask2() => CalculateVolume(GetBorder(ParsePlan2(_lines)));

        private static List<Plan> ParsePlan1(List<string> lines)
        {
            var plan = new List<Plan>();
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var dist = int.Parse(parts[1]);
                plan.Add(new Plan(parts[0][0], dist));
            }
            return plan;
        }

        private static List<Plan> ParsePlan2(List<string> lines)
        {
            var plan = new List<Plan>();
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var dist = Convert.ToInt32(parts[2][2..^2], 16);
                plan.Add(new Plan(parts[2][^2], dist));
            }
            return plan;
        }

        private static List<Point> GetBorder(List<Plan> plan)
        {
            var trench = new List<Point>();
            var current = new Point(0, 0);
            trench.Add(current);

            foreach (var instruction in plan)
            {
                Point dir = GetDir(instruction.Dir);
                for (int i = 0; i < instruction.Dist; i++)
                {
                    current.Offset(dir);
                    trench.Add(current);
                }
            }
            return trench;
        }

        private static long CalculateVolume(List<Point> trench)
        {
            long area = Math.Abs(trench.Take(trench.Count - 1)
                .Select((p, i) => (long)p.X * trench[i + 1].Y - (long)trench[i + 1].X * p.Y)
                .Sum() / 2L);

            long border = trench.Count / 2 + 1;

            return area + border;
        }

        private static Point GetDir(char dir)
            => dir switch
            {
                'R' => new Point(1, 0),
                '0' => new Point(1, 0),
                'D' => new Point(0, 1),
                '1' => new Point(0, 1),
                'L' => new Point(-1, 0),
                '2' => new Point(-1, 0),
                'U' => new Point(0, -1),
                '3' => new Point(0, -1),
                _ => new Point(0, 0)
            };
    }
}
