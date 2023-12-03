using static AoC.src.Puzzles._2023.Day02.Puzzle;

namespace AoC.src.Puzzles._2023.Day02
{
    internal record Reveal(int R, int G, int B);

    internal class Game
    {
        internal int Id { get; init; }
        internal List<Reveal> Reveals { get; set; }
        internal bool Possible { get; set; }

        internal Game(int id)
        {
            Id = id;
            Reveals = new List<Reveal>();
            Possible = true;
        }

        internal bool IsPossible(int r, int g, int b)
        {
            foreach (var reveal in Reveals)
            {
                if (reveal.R > r) return false;
                if (reveal.G > g) return false;
                if (reveal.B > b) return false;
            }
            return true;
        }

        internal int GetFewestCubesPower()
        {
            var r = Reveals.Max(reveal => reveal.R);
            var g = Reveals.Max(reveal => reveal.G);
            var b = Reveals.Max(reveal => reveal.B);
            return r * g * b;
        }
    }
}
