namespace AoC.src.Puzzles._2015.Day15
{
    public class Puzzle
    {
        private readonly Dictionary<string, Ingredient> _ingredients;
        
        record CombinationInfo (int Frosting, int Candy, int Butterscotch, int Sugar);

        public Puzzle(string input)
        {
            _ingredients = ParseIngredients(Util.readLines(input));
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private static Dictionary<string, Ingredient> ParseIngredients(List<string> input)
        {
            Dictionary<string, Ingredient> ing = new();
            foreach(string line in input)
            {
                var s = line.Split(' ', ',');
                ing[s[0].Trim(':')] = new Ingredient(s[2], s[5], s[8], s[11], s[14]);
            }
            return ing;
        }

        public int SolveTask1()
            => GenerateCombinations().Max(comb => CalcScore(comb));
        

        private int CalcScore(CombinationInfo comb)
        {
            var cap = comb.Frosting * _ingredients["Frosting"].Capacity
                + comb.Candy * _ingredients["Candy"].Capacity
                + comb.Butterscotch * _ingredients["Butterscotch"].Capacity
                + comb.Sugar * _ingredients["Sugar"].Capacity;
            if (cap <= 0) return 0;
            var dur = comb.Frosting * _ingredients["Frosting"].Durability
                + comb.Candy * _ingredients["Candy"].Durability
                + comb.Butterscotch * _ingredients["Butterscotch"].Durability
                + comb.Sugar * _ingredients["Sugar"].Durability;
            if (dur <= 0) return 0;
            var flav = comb.Frosting * _ingredients["Frosting"].Flavor
                + comb.Candy * _ingredients["Candy"].Flavor
                + comb.Butterscotch * _ingredients["Butterscotch"].Flavor
                + comb.Sugar * _ingredients["Sugar"].Flavor;
            if (flav <= 0) return 0;
            var text = comb.Frosting * _ingredients["Frosting"].Texture
                + comb.Candy * _ingredients["Candy"].Texture
                + comb.Butterscotch * _ingredients["Butterscotch"].Texture
                + comb.Sugar * _ingredients["Sugar"].Texture;
            if (text <= 0) return 0;

            return cap * dur * flav * text;
        }

        private static List<CombinationInfo> GenerateCombinations()
            => (from a in Enumerable.Range(0, 101)
                from b in Enumerable.Range(0, 101)
                from c in Enumerable.Range(0, 101)
                let d = 100 - a - b - c
                where d >= 0
                select new CombinationInfo(a, b, c, d))
                .ToList();


        public int SolveTask2()
             => GenerateCombinations()
                .Where(comb => CheckCalories(500, comb))
                .Max(comb => CalcScore(comb));

        private bool CheckCalories(int amount, CombinationInfo comb)
            => (  comb.Frosting * _ingredients["Frosting"].Calories
                + comb.Candy * _ingredients["Candy"].Calories
                + comb.Butterscotch * _ingredients["Butterscotch"].Calories
                + comb.Sugar * _ingredients["Sugar"].Calories) == amount;
    }
}
