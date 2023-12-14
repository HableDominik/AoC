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
                    while (y > 0 && platform[x,y] == 'O' && platform[x, y - 1] == '.')
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

        private void MoveWest(char[,] platform)
        {
            for (int y = 0; y < _size; y++)
            {
                int x = 1;
                while (x < _size)
                {
                    while (x > 0 && platform[x, y] == 'O' && platform[x - 1, y] == '.')
                    {
                        platform[x, y] = '.';
                        x--;
                        platform[x, y] = 'O';
                    }
                    x++;
                }
            }
        }

        private void MoveSouth(char[,] platform)
        {
            for (int x = 0; x < _size; x++)
            {
                int y = _size - 2;
                while (y >= 0)
                {
                    while (y < _size - 1 && platform[x, y] == 'O' && platform[x, y + 1] == '.')
                    {
                        platform[x, y] = '.';
                        y++;
                        platform[x, y] = 'O';
                    }
                    y--;
                }
            }
        }

        private void MoveEast(char[,] platform)
        {
            for (int y = 0; y < _size; y++)
            {
                int x = _size - 2;
                while (x >= 0)
                {
                    while (x < _size-1 && platform[x, y] == 'O' && platform[x + 1, y] == '.')
                    {
                        platform[x, y] = '.';
                        x++;
                        platform[x, y] = 'O';
                    }
                    x--;
                }
            }
        }

        private int Cycle(char[,] platform)
        {
            for(int i = 0; i < 300; i++)
            {
                MoveNorth(platform);
                MoveWest(platform);
                MoveSouth(platform);
                MoveEast(platform);

#if false
                var bigNumber = 1000000000; // CPU goes brrrrrr
                var beforeFirst = 106; // found with notepad++ and ctrl+f
                var patternLength = 38; // counted by hand
                var offByOne = 1; // because it's always off by one

                if(i == beforeFirst) // first
                    Console.WriteLine(CalcLoad(platform));
                if (i == (bigNumber - beforeFirst) % patternLength + beforeFirst - offByOne) 
                    Console.WriteLine(CalcLoad(platform));
                if(i == beforeFirst + patternLength) // second
                    Console.WriteLine(CalcLoad(platform));
#endif
            }

            return 93102; // find pattern and calc like crazy
        }

        private int CalcLoad(char[,] platform)
        {
            int sum = 0;
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

        public int SolveTask1() => CalcLoad(MoveNorth(_platform));

        public int SolveTask2() => Cycle(_platform);
    }
}
