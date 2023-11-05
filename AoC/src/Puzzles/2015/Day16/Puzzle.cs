namespace AoC.src.Puzzles._2015.Day16
{
    public class Puzzle
    {
        private readonly string _input;

        public Puzzle(string input)
        {
            _input = input;
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
        {
            List<Compound> sues = new();
            Util.readLines(_input)
                .ForEach( (line) => sues.Add(new Compound(line)));
            sues.ForEach(sue => Console.WriteLine(sue));
            var aunt = new Compound("children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1");

            return sues.FindIndex(sue => sue.IsAunt(aunt)) + 1;
        }

        public string SolveTask2()
        {
            return string.Empty;
        }
    }
}
