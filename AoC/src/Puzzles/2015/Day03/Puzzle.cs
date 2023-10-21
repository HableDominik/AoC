using System.ComponentModel;
using System.Drawing;

namespace AoC.src.Puzzles._2015.Day03
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
            var visited = new HashSet<Point>();
            var santa = new Point( 0, 0 );

            visited.Add(santa);

            foreach (var dir in _input)
            {
                MoveAndRecord(dir, ref santa, visited);
            }

            return visited.Count;
        }

        private static void Move(char dir, ref Point current)
        {
            _ = dir switch
            {
                '>' => current.X++,
                '<' => current.X--,
                '^' => current.Y++,
                'v' => current.Y--,
                _ => throw new InvalidDataException($"{dir} is not a valid direction")
            };
        }

        private void MoveAndRecord(char dir, ref Point mover, HashSet<Point> visited)
        {
            Move(dir, ref mover);
            visited.Add(mover);
        }

        public int SolveTask2()
        {
            var visited = new HashSet<Point>();
            var santa = new Point(0, 0);
            var robo = new Point(0, 0);

            visited.Add(santa);

            for(int i = 0; i < _input.Length; i++)
            {
                if (i % 2 == 0) MoveAndRecord(_input[i], ref santa, visited);
                else MoveAndRecord(_input[i], ref robo, visited);
            }

            return visited.Count;
        }
    }
}
