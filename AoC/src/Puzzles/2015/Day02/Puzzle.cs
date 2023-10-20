namespace AoC.src.Puzzles._2015.Day02
{
    public class Puzzle
    {
        private List<Present> _presents;

        public Puzzle(string input)
        {
            _presents = new List<Present>();
            var dimensions = Util.readLines(input);
            dimensions.ForEach(dimension => _presents.Add(new Present(dimension)));

            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1() => _presents.Sum(present => present.calculateSurface());

        public int SolveTask2() => _presents.Sum(present => present.calculateRibbon());      
    }
}
