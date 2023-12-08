namespace AoC.src.Puzzles._2023.Day07
{
    public class Puzzle
    {
        record Hand(string Cards, int Bid, int Val);
        private readonly string _input;

        public Puzzle(string input)
        {
            _input = input
                .Replace('A', 'E')
                .Replace('K', 'D')
                .Replace('Q', 'C')
                .Replace('J', 'B')
                .Replace('T', 'A');           
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
            => ParseHands(_input)
                .OrderBy(hand => hand.Cards)
                .OrderBy(hand => hand.Val)
                .Select((hand, index) => hand.Bid * (index + 1))
                .Sum();

        public int SolveTask2()
            => ParseHands(_input.Replace('B', '1'))
                .OrderBy(hand => hand.Cards)
                .OrderBy(hand => hand.Val)
                .Select((hand, index) => hand.Bid * (index + 1))
                .Sum();

        private static List<Hand> ParseHands(string input)
            => Util.readLines(input)
               .Select(line => line.Split(' '))
               .Select(split => new Hand(
                   split[0],
                   int.Parse(split[1]),
                   CalcValue(split[0])))
               .ToList();

        private static int CalcValue(string cards)
        {
            var cardDict = new Dictionary<char, int>();
            foreach (var card in cards)
            {
                if (cardDict.ContainsKey(card)) cardDict[card]++;
                else cardDict[card] = 1;
            }

            int jCount = 0;
            if (cardDict.ContainsKey('1'))
            {
                jCount = cardDict['1'];
                cardDict.Remove('1');
            }

            var sorted = cardDict.OrderByDescending(c => c.Value).ToList();

            if (sorted.Count == 5) return 0;                        // no pair
            if (sorted.Count == 4) return 1;                        // 1 pair
            if (sorted.Count == 3)
            {
                if (sorted.First().Value + jCount == 3) return 3;   // 3 oak
                else return 2;                                      // 2 pair
            }
            if (sorted.Count == 2)
            {
                if (sorted.First().Value + jCount == 4) return 5;   // 4 oak
                else return 4;                                      // full house
            }
            return 6;                                               // 5 oak/joker
        }   
    }
}
