using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2015.Day11
{
    public class Puzzle
    {
        private readonly string _input;

        public Puzzle(string input)
        {
            _input = input;
            var result1 = SolveTask1();
            Console.WriteLine($"Result 1: {result1}");
            Console.WriteLine($"Result 2: {SolveTask2(result1)}");
        }

        public string SolveTask1()
        {
            var password = _input;
            while (!Check(password))
            {
                password = Increment(password);

            }
            return password;
        }


        public string SolveTask2(string result1)
        {
            var password = Increment(result1);
            while (!Check(password))
            {
                password = Increment(password);

            }
            return password;
        }

        private bool Check(string password)
        {
            if(!ContainsStraightOfThree(password)) return false;
            if(ContainsIOL(password)) return false;
            if(!ContainsTwoDifferentPairs(password)) return false;
            return true;

        }

        private static bool ContainsStraightOfThree(string input)
        {
            for (int i = 0; i <= input.Length - 3; i++)
            {
                if (input.Skip(i).Take(3).Select(c => (int)c).SequenceEqual(Enumerable.Range(input[i], 3)))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsIOL(string input)
            => input.Contains('i') || input.Contains('o') || input.Contains('l');

        private static bool ContainsTwoDifferentPairs(string input)
        {
            var matches = Regex
                .Matches(input, @"(\w)\1")      // match repeated characters
                .Cast<Match>()
                .Select(m => m.Value)           // extract matched strings
                .Distinct()                     // remove duplicates
                .ToList();
            return matches.Count >= 2;
        }

        private string Increment(string str) => Increment(str, 7);

        private string Increment(string str, int digit)
        {
            char[] chars = str.ToCharArray();
            int carry = 1;
            for (int i = digit; i >= 0; i--)
            {
                if (carry == 0) break;
                var val = chars[i] - 'a' + carry;
                carry = val / 26;
                chars[i] = (char)('a' + val % 26);
            }
            return new string(chars);
        }
    }
}
