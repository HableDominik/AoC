using System.Drawing;

namespace AoC.src.Puzzles._2023.Day16
{
    public class Puzzle
    {
        private readonly int _size;
        private readonly Dictionary<Point, char> _mirrors;
        private List<Laser> _lasers;

        public class Laser
        {
            public Point Pos { get; set; }
            public Point Dir { get; set; }

            public Laser(Point pos, Point dir)
            {
                Pos = pos;
                Dir = dir;
            }

            public void Move()
            {
                Pos = new Point(Pos.X + Dir.X, Pos.Y + Dir.Y);
            }

            public void ChangeDir(Point dir)
            {
                Dir = dir;
            }
        }

        public Puzzle(string input)
        {
            var lines = Util.readLines(input);
            _size = lines.Count();
            _mirrors = ParseMirrors(lines);
            _lasers = new List<Laser>();
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

        public string SolveTask1()
        {
            _lasers.Add(new Laser(new(0,0), new(1,0)));
            for(int i = 0; i < 20; i++)
            {
                foreach (var laser in _lasers)
                {
                    var mirror = _mirrors.GetValueOrDefault(laser.Pos);
                    var dir = laser.Dir;
                    if(mirror != (char)default)
                        laser.ChangeDir(GetDir(mirror, laser.Pos, dir));
                    laser.Move();
                    var pos = laser.Pos;
                    //if(pos.X < 0 || pos.Y < 0 || pos.X >= _size || pos.Y >= _size)
                        
                }
                Print();
            }

            return string.Empty;
        }

        private Point GetDir(char mirror, Point pos, Point dir)
        {
            switch (mirror)
            {
                case '/': return new Point(2* dir.X + dir.Y, 2* dir.Y + dir.X);
                case '\\': return new Point(dir.Y, dir.X);
                case '|':
                    {
                        if (dir.X == 0) return dir;
                        _lasers.Add(new Laser(pos, new Point(0, -1)));
                        return new Point(0, 1);
                    }
                case '-':
                    {
                        if (dir.Y == 0) return dir;
                        _lasers.Add(new Laser(pos, new Point(-1, 0)));
                        return new Point(1, 0);
                    }
                default: return dir;
            }
        }

        private void Print()
        {
            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    var current = new Point(col, row);
                    var symbol = _mirrors.GetValueOrDefault(current);
                    if (symbol == (char)default)
                        symbol = _lasers.Any(l => l.Pos == current) ? '#' : (char)default;
                    Console.Write(symbol == (char)default ? "." : symbol);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public string SolveTask2()
        {
            return string.Empty;
        }
    }
}
