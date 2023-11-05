using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2015.Day16
{
    internal class Compound
    {
        internal int? Children { get; set; }
        internal int? Cats { get; set; }
        internal int? Samoyeds { get; set; }
        internal int? Pomeranians { get; set; }
        internal int? Akitas { get; set; }
        internal int? Vizslas { get; set; }
        internal int? Goldfish { get; set; }
        internal int? Trees { get; set; }
        internal int? Cars { get; set; }
        internal int? Perfumes { get; set; }

        public Compound(string input)
        {
            Children = GetValue(input, "children");
            Cats = GetValue(input, "cats");
            Samoyeds = GetValue(input, "samoyeds");
            Pomeranians = GetValue(input, "pomeranians");
            Akitas = GetValue(input, "akitas");
            Vizslas = GetValue(input, "vizslas");
            Goldfish = GetValue(input, "goldfish");
            Trees = GetValue(input, "trees");
            Cars = GetValue(input, "cars");
            Perfumes = GetValue(input, "perfumes");
        }

        private static int? GetValue(string input, string sub)
        {
            var pattern = $"{Regex.Escape(sub)}: (\\d{{1,2}})";
            var match = Regex.Match(input, pattern);
            if (!match.Success) return null;
            return int.Parse(match.Groups[1].Value);
        }

        public bool IsAunt1(Compound aunt)
            => (Children is null || Children == aunt.Children)
                && (Cats is null || Cats == aunt.Cats)
                && (Samoyeds is null || Samoyeds == aunt.Samoyeds)
                && (Pomeranians is null || Pomeranians == aunt.Pomeranians)
                && (Akitas is null || Akitas == aunt.Akitas)
                && (Vizslas is null || Vizslas == aunt.Vizslas)
                && (Goldfish is null || Goldfish == aunt.Goldfish)
                && (Trees is null || Trees == aunt.Trees)
                && (Cars is null || Cars == aunt.Cars)
                && (Perfumes is null || Perfumes == aunt.Perfumes);

        public bool IsAunt2(Compound aunt)
            => (Children is null || Children == aunt.Children)
                && (Cats is null || Cats > aunt.Cats)
                && (Samoyeds is null || Samoyeds == aunt.Samoyeds)
                && (Pomeranians is null || Pomeranians < aunt.Pomeranians)
                && (Akitas is null || Akitas == aunt.Akitas)
                && (Vizslas is null || Vizslas == aunt.Vizslas)
                && (Goldfish is null || Goldfish < aunt.Goldfish)
                && (Trees is null || Trees > aunt.Trees)
                && (Cars is null || Cars == aunt.Cars)
                && (Perfumes is null || Perfumes == aunt.Perfumes);

        public override string? ToString()
        {
            return
                "{ Children: " + Children + ", " 
                + "Cats: " + Cats + ", "
                + "Samoyeds: " + Samoyeds + ", "
                + "Pomeranians: " + Pomeranians + ", "
                + "Akitas: " + Akitas + ", "
                + "Vizslas: " + Vizslas + ", "
                + "Goldfish: " + Goldfish + ", "
                + "Trees: " + Trees + ", "
                + "Cars: " + Cars + ", "
                + "Perfumes: " + Perfumes + " }";
        }
    }
}
