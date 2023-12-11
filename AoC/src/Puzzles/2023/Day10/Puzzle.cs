using System.Drawing;

namespace AoC.src.Puzzles._2023.Day10
{
    public class Puzzle
    {
        private readonly int size = 140;
        private readonly string _input;
        private List<Point> points = new();
        private char[,] _expandedMap;

        public Puzzle(string input)
        {
            _input = input.Replace("\r\n", "");
            _expandedMap = new char[size, size];
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
        {
            var current = IndexToPoint(_input.IndexOf('S'));
            var direction = new Point(0, 1); //kinda cheaty but idc
            int steps = 0;
            char pipe;
            do
            {
                current.Offset(direction);
                points.Add(current);
                pipe = GetPipeAt(current);
                direction = GetDirection(pipe, direction);
                steps++;
            } while (!pipe.Equals('S'));
            return steps / 2;
        }

        private Point IndexToPoint(int i) => new (i % size, i / size);

        private static Point GetDirection(char pipe, Point direction)
            => pipe switch
            {
                '|' => direction,
                '-' => direction,
                'L' => direction.X == 0 ? new Point(1, 0) : new Point(0, -1),
                'J' => direction.X == 0 ? new Point(-1, 0) : new Point(0, -1),
                '7' => direction.X == 0 ? new Point(-1, 0) : new Point(0, 1),
                'F' => direction.X == 0 ? new Point(1, 0) : new Point(0, 1),
                _ => new Point(0, 0)
            };

        private char GetPipeAt(Point point) => _input[point.X + point.Y * size];

        public int SolveTask2()
        {
            _expandedMap = ExpandMap();
            FillOutside();
            return CountInside();
        }

        private void FillOutside()
        {
            var points = new Queue<Point>();
            points.Enqueue(new Point(0, 0));
            while (points.Count > 0)
            {
                var p = points.Dequeue();
                var x = p.X;
                var y = p.Y;
                if (x < 0 || y < 0 || x >= size * 2 || y >= size * 2) continue;
                if (_expandedMap[x, y] != ' ') continue;
                _expandedMap[x, y] = '#';
                points.Enqueue(new Point(x + 1, y));
                points.Enqueue(new Point(x - 1, y));
                points.Enqueue(new Point(x, y + 1));
                points.Enqueue(new Point(x, y - 1));
            }
        }

        private char[,] ExpandMap()
        {
            var map = new char[size * 2, size * 2];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    var p = new Point(x, y);
                    var c = points.Contains(p) ? GetPipeAt(p) : ' ';
                    map[x * 2, y * 2] = c;
                    map[x * 2 + 1, y * 2] = GetRight(c);
                    map[x * 2, y * 2 + 1] = GetBottom(c);
                    map[x * 2 + 1, y * 2 + 1] = ' ';
                }
            }
            return map;
        }

        private int CountInside()
        {
            var sum = 0;
            for (int y = 0; y < size * 2; y += 2)
            {
                for (int x = 0; x < size * 2; x += 2)
                {
                    if (_expandedMap[x, y] == ' ') sum++;
                }
            }
            return sum;
        }

        private static char GetRight(char c)
            => c switch
            {
                '|' => ' ',
                '-' => '-',
                'L' => '-',
                'J' => ' ',
                '7' => ' ',
                'F' => '-',
                'S' => ' ',
                _ => ' '
            };

        private static char GetBottom(char c)
           => c switch
           {
               '|' => '|',
               '-' => ' ',
               'L' => ' ',
               'J' => ' ',
               '7' => '|',
               'F' => '|',
               'S' => '|',
               _ => ' '
           };
    }
}
