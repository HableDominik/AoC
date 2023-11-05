namespace AoC.src.Puzzles._2015.Day18
{
    public class Puzzle
    {
        private readonly int _size;
        private readonly int _steps;

        public Puzzle(string input)
        {
            var lines = Util.readLines(input);
            _size = lines.FirstOrDefault()!.Length;
            _steps = 100;
            Console.WriteLine($"Result 1: {SolveTask1(ParseInput(lines))}");
            Console.WriteLine($"Result 2: {SolveTask2(ParseInput(lines))}");
        }

        private bool[,] ParseInput(List<string> lines)
        {
            var grid = new bool[_size, _size];
            for (int y = 0; y < _size; y++)
                for (int x = 0; x < _size; x++)
                    grid[x, y] = lines[y][x] == '#';
            return grid;
        }

        public int SolveTask1(bool[,] lights)
            => AnimateLights(lights, _steps, false).Cast<bool>().Count(on => on);

        public int SolveTask2(bool[,] lights)
        {
            lights[0, 0] = true;
            lights[0, _size-1] = true;
            lights[_size-1, 0] = true;
            lights[_size-1, _size-1] = true;
            return AnimateLights(lights, _steps, true).Cast<bool>().Count(on => on);
        }


        private bool[,] AnimateLights(bool[,]lights, int steps, bool cornerOn)
        {
            for (int i = 0; i < steps; i++)
            {
                var oldLights = (bool[,])lights.Clone();
                for (int y = 0; y < _size; y++)
                    for (int x = 0; x < _size; x++)
                        lights[x, y] = AnimateOneLight(oldLights, x, y, cornerOn);
            }
            return lights;
        }

        private bool AnimateOneLight(bool[,] oldLights, int lx, int ly, bool cornerOn)
        {
            if (cornerOn)
            {
                if ((lx == 0 && (ly == 0 || ly == (_size - 1))
                    || (lx == _size - 1 && (ly == 0 || ly == _size - 1)))) 
                        return true;
            }

            var dx = new int[] { -1,  0,  1,  1,  1,  0, -1, -1 };
            var dy = new int[] { -1, -1, -1,  0,  1,  1,  1,  0 };
            int OnNeighbors = 0;
            for (int i = 0; i < 8; i++)
            {
                var x = lx + dx[i];
                var y = ly + dy[i];
                if (x < 0 || x >= _size) continue;
                if (y < 0 || y >= _size) continue;
                OnNeighbors += oldLights[x, y] ? 1 : 0;
            }
            if (!oldLights[lx, ly] && OnNeighbors == 3) return true;
            if (oldLights[lx, ly] && (OnNeighbors == 2 || OnNeighbors == 3)) return true;
            return false;
        }
    }
}
