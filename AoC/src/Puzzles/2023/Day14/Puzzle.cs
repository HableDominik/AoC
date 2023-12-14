namespace AoC.src.Puzzles._2023.Day14
{
    public class Puzzle
    {
        private readonly int _size;
        private readonly char[,] _platform;

        public Puzzle(string input)
        {
            var lines = Util.readLines(input);
            _size = lines.Count;
            _platform = ParsePlatform(lines);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private char[,] ParsePlatform(List<string> lines)
        {
            var platform = new char[_size, _size];
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    platform[x, y] = lines[y][x];
                }
            }
            return platform;
        }

        private char[,] MoveNorth(char[,] platform)
        {
            for(int x = 0; x < _size; x++)
            {
                int y = 1;
                while(y < _size)
                {
                    while (y > 0 && platform[x,y] == 'O' && platform[x,y-1] == '.')
                    {
                        platform[x, y] = '.';
                        y--;
                        platform[x, y] = 'O';
                    }
                    y++;
                }
            }
            return platform;
        }

        
        private long CalcLoad(char[,] platform)
        {
            long sum = 0;
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (platform[x, y] == 'O')
                    {
                        sum += _size - y;
                    }
                }
            }
            return sum;
        }

        public long SolveTask1() => CalcLoad(MoveNorth(_platform));

        public string SolveTask2() => string.Empty;
    }
}
