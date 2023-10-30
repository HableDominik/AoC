using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.src.Puzzles._2015.Day15
{
    internal class Ingredient
    {
        internal int Capacity { get; set; }
        internal int Durability { get; set; }
        internal int Flavor { get; set; }
        internal int Texture { get; set; }
        internal int Calories { get; set; }

        public Ingredient(int cap, int dur, int flav, int text, int cal)
        {
            Capacity = cap;
            Durability = dur;
            Flavor = flav;
            Texture = text;
            Calories = cal;
        }

        public int GetScore() => GetScore(1);

        public int GetScore(int amount)
            => amount * (Capacity + Durability + Flavor + Texture); 
    }
}
