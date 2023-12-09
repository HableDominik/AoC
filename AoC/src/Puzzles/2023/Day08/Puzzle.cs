using System.Threading.Channels;

namespace AoC.src.Puzzles._2023.Day08
{
    public class Puzzle
    {
        private readonly string _navigation;
        private readonly Dictionary<string, Tuple<string, string>> _map;

        public Puzzle(string input)
        {
            var lines = Util.readLines(input);
            _navigation = lines[0];
            _map = ParseMap(lines);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private static Dictionary<string, Tuple<string, string>> ParseMap(List<string> lines)
        {
            var map = new Dictionary<string, Tuple<string, string>>();
            foreach( var line in lines.Skip(2) )
            {
                var split = line.Split(' ');
                var node = split[0];
                var left = split[2].Trim('(', ',');
                var right = split[3].Trim(')');
                map.Add(node, new Tuple<string, string>(left, right));
            }
            return map;
        }

        public int SolveTask1() => GetSteps("AAA", "ZZZ");
        

        private int GetSteps(string start, string end)
        {
            var current = start;
            var steps = 0;
            var navCount = _navigation.Length;
            do
            {
                var map = _map[current];
                if (_navigation[steps % navCount] == 'L')
                    current = map.Item1;
                else
                    current = map.Item2;
                steps++;

            } while (!current.EndsWith(end));

            return steps;
        }

        public long SolveTask2()
        {
            var starts = new HashSet<string>(_map.Where(x => x.Key[2] == 'A').Select(x => x.Key));
            var steps = starts.Select(start => GetSteps(start, "Z")).ToList();

            long lcm = steps[0];
            for (int i = 1; i < steps.Count; i++)
            {
                lcm = FindLCM(lcm, steps[i]);
            }
            return lcm;
        }

        private static long FindGCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long FindLCM(long a, long b) => (a / FindGCD(a, b)) * b;
    }
}
