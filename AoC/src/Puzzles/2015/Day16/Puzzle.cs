namespace AoC.src.Puzzles._2015.Day16
{
    public class Puzzle
    {
        private readonly List<Compound> sues;
        private readonly Compound aunt = new Compound("children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");

        public Puzzle(string input)
        {
            sues = new();
            Util.readLines(input)
                .ForEach((line) => sues.Add(new Compound(line)));
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
            => sues.FindIndex(sue => sue.IsAunt1(aunt)) + 1;

        public int SolveTask2()
            => sues.FindIndex(sue => sue.IsAunt2(aunt)) + 1;
    }
}
