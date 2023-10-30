namespace AoC.src.Puzzles._2015.Day13
{
    public class Puzzle
    {
        private Dictionary<string, int> happiness;
        private HashSet<char> people = new();

        public Puzzle(string input)
        {
            happiness = ParseHappiness(Util.readLines(input));
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private Dictionary<string, int> ParseHappiness(List<string> input)
        {
            var dict = new Dictionary<string, int>();
            foreach (string line in input)
            {
                var split = line.Split(' ', '.');
                char person1 = split[0][0];
                people.Add(person1);
                var person2 = split[10][0];
                var score = int.Parse(split[3]) * (split[2].Equals("lose") ? -1 : 1);
                dict[$"{person1}{person2}"] = score; 
            }
            return dict;
        }

        public int SolveTask1()
        {
            var permutations = GetPermutations(people).ToList();
            return CalcMaxHappiness(permutations);
        }

        private int CalcMaxHappiness(List<string> permutations)
        {
            int max = 0;
            foreach (var permutation in permutations)
            {
                int sum = 0;
                for (int i = 0; i < people.Count; i++)
                {
                    string combination = $"{permutation[i]}{permutation[(i + 1) % people.Count]}";
                    sum += happiness.GetValueOrDefault(combination);
                    combination = $"{permutation[(i + 1) % people.Count]}{permutation[i]}";
                    sum += happiness.GetValueOrDefault(combination);
                }
                max = Math.Max(max, sum);
            }
            return max;
        }

        private static IEnumerable<string> GetPermutations(HashSet<char> source)
        {
            if (source.Count == 1) return new[] { source.First().ToString() };

            var permutations = new List<string>();
            foreach (var charAtI in source)
            {
                var remainingChars = new HashSet<char>(source);
                remainingChars.Remove(charAtI);
                foreach (var subPermutation in GetPermutations(remainingChars))
                {
                    permutations.Add(charAtI + subPermutation);
                }
            }
            return permutations;
        }

        public int SolveTask2()
        {
            var me = 'X';
            foreach(var p in people)
            {
                happiness[$"{p}{me}"] = 0;
                happiness[$"{me}{p}"] = 0;
            }
            people.Add(me);
            var permutations = GetPermutations(people).ToList();
            return CalcMaxHappiness(permutations);
        }
    }
}
