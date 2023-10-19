namespace AoC.src.Puzzles._2015.Day02
{
    public class Puzzle
    {
        private readonly string _input;
        private IEnumerable<string> presents;

        public Puzzle(string input)
        {
            _input = input;
            presents = new List<string>();
            Console.WriteLine("Result 1: " + SolveTask1());
            Console.WriteLine("Result 2: " + SolveTask2());
        }

        public string SolveTask1()
        {
            presents = readLines(_input);

            var result = presents.Sum(present => calculateSurface(present));

            return result.ToString();
        }

        private int calculateSurface(string present)
        {            
            var (l, w, h) = getLWH(present);

            var smallestSide = Math.Min(Math.Min(l * w, w * h), h * l);

            return 2 * l * w + 2 * w * h + 2 * h * l + smallestSide;
        }

        private (int l, int w, int h) getLWH(string present)
        {
            var lwh = present.Split('x');
            var l = Int32.Parse(lwh[0]);
            var w = Int32.Parse(lwh[1]);
            var h = Int32.Parse(lwh[2]);

            return (l, w, h);
        }

        public string SolveTask2()
        {
            var result = presents.Sum(present => calcualteRibbon(present));

            return result.ToString();
        }

        private decimal calcualteRibbon(string present)
        {
            var (l, w, h) = getLWH(present);

            int[] mask = { 1, 1, 1 };
            if (l >= w && l >= h) mask[0] = 0;
            else if (w >= l && w >= h) mask[1] = 0;
            else if (h >= l && h >= w) mask[2] = 0;
            else throw new InvalidProgramException();

            var wrap = 2 * l * mask[0] + 2 * w * mask[1] + 2 * h * mask[2];

            return wrap + l * w* h;
        }

        private IEnumerable<string> readLines(string input) // TODO: utils
        {
            return input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

    }
}
