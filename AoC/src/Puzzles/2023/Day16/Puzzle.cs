using System.Drawing;

namespace AoC.src.Puzzles._2023.Day16
{
    public class Puzzle
    {
        private readonly int _size;
        private readonly Dictionary<Point, char> _mirrors;

        public class Laser
        {
            public Point Pos { get; set; }
            public Point Dir { get; set; }

            public Laser(Point pos, Point dir) { Pos = pos; Dir = dir; }

            public void Move() { Pos = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y); }

            public void ChangeDir(Point dir) { Dir = dir; }
        }

        public Puzzle(string input)
        {
            var lines = Util.readLines(input);
            _size = lines.Count;
            _mirrors = ParseMirrors(lines);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private Dictionary<Point, char> ParseMirrors(List<string> lines)
        {
            var mirrors = new Dictionary<Point, char>();
            for(int row = 0; row < _size; row++)
            {
                for(int col = 0; col < _size; col++)
                {
                    var symbol = lines[row][col];
                    if(symbol != '.')
                    {
                        mirrors.Add(new Point(col, row), symbol);
                    }
                }
            }
            return mirrors;
        }

        public int SolveTask1() => Energize(new Laser(new(0, 0), new(1, 0)));

        public int SolveTask2()
        {
            var max = 0;
            for (int i = 0; i < _size; i++)
            {
                max = Math.Max(max, Energize(new Laser(new(i, 0), new(0, 1))));
                max = Math.Max(max, Energize(new Laser(new(0, i), new(1, 0))));
                max = Math.Max(max, Energize(new Laser(new(i, _size - 1), new(0, -1))));
                max = Math.Max(max, Energize(new Laser(new(_size - 1, i), new(-1, 0))));
                Console.WriteLine(i + " " + max);
            }
            return max;
        }

        private int Energize(Laser start)
        {
            var lasers = new List<Laser>() { start };
            var visited = new HashSet<Point>();
            var oldVisited = -1;
            var spazi = 0;
            while (visited.Count != oldVisited || spazi < 10)
            {
                if (spazi > 0 && visited.Count > oldVisited) spazi = 0;
                if (visited.Count == oldVisited) spazi++;
                oldVisited = visited.Count;

                var lasersToAdd = new List<Laser>();

                foreach (var laser in lasers)
                {
                    var pos = laser.Pos;
                    if (pos.X < 0 || pos.Y < 0 || pos.X >= _size || pos.Y >= _size)
                    {
                        continue;
                    }
                    visited.Add(pos);

                    var mirror = _mirrors.GetValueOrDefault(laser.Pos);
                    laser.ChangeDir(GetDir(mirror, laser.Pos, laser.Dir, lasersToAdd));
                    laser.Move();
                }
                lasers.AddRange(lasersToAdd);
            }
            return oldVisited;
        }

        private static Point GetDir(char mirror, Point pos, Point dir, List<Laser> newLasers)
        {
            switch (mirror)
            {
                case '/': return new Point(dir.Y * -1, dir.X * -1);
                case '\\': return new Point(dir.Y, dir.X);
                case '|':
                    {
                        if (dir.X == 0) return dir;
                        newLasers.Add(new Laser(pos, new Point(0, -1)));
                        return new Point(0, 1);
                    }
                case '-':
                    {
                        if (dir.Y == 0) return dir;
                        newLasers.Add(new Laser(pos, new Point(-1, 0)));
                        return new Point(1, 0);
                    }
                default: return dir;
            }
        }
    }
}
