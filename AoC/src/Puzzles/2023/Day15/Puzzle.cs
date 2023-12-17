namespace AoC.src.Puzzles._2023.Day15
{
    public class Puzzle
    {
        private readonly List<string> _inputs;

        public Puzzle(string input)
        {
            _inputs = input.Split(',').ToList();
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private static int Hash( string input )
        {
            var value = 0;
            foreach (var c in input)
            {
                value += (int)c;
                value *= 17;
                value %= 256;
            }
            return value;
        }

        public int SolveTask1() => _inputs.Sum(str => Hash(str));

        public long SolveTask2()
        {
            var boxes = new Dictionary<int, List<Tuple<string, int>>>();

            for(int i = 0; i < 256; i++) boxes[i] = new List<Tuple<string, int>>();
            
            foreach(var input in _inputs)
            {
                if (input.EndsWith('-'))
                {
                    var str = input.Trim('-');
                    var hash = Hash(str);

                    boxes[hash].RemoveAll(tuple => tuple.Item1 == str);
                }
                else
                {
                    var val = input[^1] - '0';
                    var str = input.Substring(0, input.Length-2);
                    var lens = new Tuple<string, int>(str, val);
                    var hash = Hash(str);
                    
                    var index = boxes[hash].FindIndex(lens => lens.Item1 == str);
                    if (index == -1) boxes[hash].Add(lens);
                    else boxes[hash][index] = lens;
                }
            }

            long result = 0;

            for(int i = 0; i < boxes.Count; i++)
            {
                var box = boxes[i];
                if (!box.Any()) continue;

                for(int j = 0; j < box.Count; j++)
                {
                    result += (i + 1) * (j + 1) * box[j].Item2;
                }
            }

            return result;
        }
    }
}
