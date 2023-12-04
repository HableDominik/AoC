using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2023.Day04
{
    public class Puzzle
    {
        private readonly Dictionary<int, int> _wins;

        public Puzzle(string input)
        {
            var cards = Util.readLines(input);
            _wins = CheckWins(cards);
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }
        public int SolveTask1()
            => (int)_wins.Values.Sum(win => win > 0 ? Math.Pow(2, win-1) : 0);
        
        public int SolveTask2()
        {
            var cards = new Dictionary<int, int>();
            for (int cardIndex = 0; cardIndex < _wins.Count; cardIndex++)
            {
                UpdateCardCount(cards, cardIndex, 1);
                for (int wonCardIndex = cardIndex + 1; 
                    wonCardIndex <= cardIndex + _wins.ElementAt(cardIndex).Value; 
                    wonCardIndex++)
                {
                    UpdateCardCount(cards, wonCardIndex, cards.ElementAt(cardIndex).Value);
                }
            }
            return cards.Sum(card => card.Value);
        }

        private void UpdateCardCount(Dictionary<int, int> dict, int index, int amount)
        {
            if (index >= _wins.Count) return;
            if (dict.ContainsKey(index))
            {
                dict[index] += amount;
                return;
            }
            dict[index] = amount;
        }

        private static Dictionary<int, int> CheckWins(List<string> cards)
        {
            var wins = new Dictionary<int, int>();
            for (int i = 0; i < cards.Count; i++)
            {
                var split = cards[i].Split(':', '|');
                var winningNumbers = GetNumberStrings(split[1]);
                var myNumbers = GetNumberStrings(split[2]);
                var matchingCount = winningNumbers.Intersect(myNumbers).Count();
                wins[i] = matchingCount;
            }
            return wins;
        }

        private static List<string> GetNumberStrings(string input)
            => Regex.Matches(input, @"(\d+)").Cast<Match>().Select(m => m.Value).ToList();
    }
}
