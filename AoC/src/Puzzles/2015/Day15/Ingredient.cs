namespace AoC.src.Puzzles._2015.Day15
{
    internal class Ingredient
    {
        internal int Capacity { get; set; }
        internal int Durability { get; set; }
        internal int Flavor { get; set; }
        internal int Texture { get; set; }
        internal int Calories { get; set; }

        public Ingredient(string cap, string dur, string flav, string text, string cal)
        {
            Capacity = int.Parse(cap);
            Durability = int.Parse(dur);
            Flavor = int.Parse(flav);
            Texture = int.Parse(text);
            Calories = int.Parse(cal);
        }

        public int GetScore() => GetScore(1);

        public int GetScore(int amount)
            => amount * (Capacity + Durability + Flavor + Texture); 
    }
}
