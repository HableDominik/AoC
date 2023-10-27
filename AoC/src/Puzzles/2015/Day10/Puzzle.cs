namespace AoC.src.Puzzles._2015.Day10
{
    public class Puzzle
    {
        private readonly string _input;

        public Puzzle(string input)
        {
            _input = input;
            //Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1() => Generate(40, _input).Length;

        private string Generate(int iterations, string input)
        {
            Console.WriteLine(iterations);
            string result = string.Empty;
            int j = 0;
            char current = input[0];
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i].Equals(current)) j++;
                else
                {
                    result += $"{j}{current}";
                    j = 1;
                    current = input[i];
                }
            }
            result += $"{j}{current}";
            if (iterations > 1) return Generate(--iterations, result);
            return result;
        }

        public int SolveTask2() => Generate(50, _input).Length;
    }
}
