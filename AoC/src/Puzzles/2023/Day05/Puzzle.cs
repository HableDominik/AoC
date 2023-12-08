namespace AoC.src.Puzzles._2023.Day05
{
    public class Puzzle
    {
        record Map(long Dest, long Src, long Len);
        private readonly List<long> _seeds;
        private readonly List<List<Map>> _maps;

        public Puzzle(string input)
        {
            var lines = Util.readLines(input);
            _seeds = ParseSeeds(lines[0]);
            _maps = ParseMaps(lines.Skip(2));
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private static List<List<Map>> ParseMaps(IEnumerable<string> lines)
        {
            var mapLists = new List<List<Map>>();
            var currentList = new List<Map>();

            foreach (var line in lines)
            {
                if (line.EndsWith("map:"))
                {
                    mapLists.Add(currentList);
                    currentList = new List<Map>();
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    var parts = line.Split(' ').Select(long.Parse).ToArray();
                    currentList.Add(new Map(parts[0], parts[1], parts[2]));
                }
            }
            mapLists.Add(currentList);
            return mapLists;
        }

        private static List<long> ParseSeeds(string input)
            => input.Split(' ').Skip(1).Select(i => long.Parse(i)).ToList();

        public long SolveTask1() => _seeds.Min(seed => MapSeed(seed));

        public long SolveTask2()
        {
            long min = long.MaxValue;
            for (int i = 0; i < _seeds.Count; i += 2)
            {
                long start = _seeds[i];
                for (long j = 0; j < _seeds[i + 1]; j++)
                {
                    min = Math.Min(min, MapSeed(start + j));
                }
            }
            return min;
        }

        private long MapSeed(long seed)
        {
            foreach (var map in _maps)
            {
                foreach (var m in map)
                {
                    if (m.Src <= seed && seed < (m.Src + m.Len))
                    {
                        seed += m.Dest - m.Src;
                        break;
                    }
                }
            }
            return seed;
        }
    }
}