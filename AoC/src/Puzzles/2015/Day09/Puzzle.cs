namespace AoC.src.Puzzles._2015.Day09
{
    public class Puzzle
    {
        private readonly string _input;
        private WeightedGraph graph;

        public Puzzle(string input)
        {
            _input = input;
            graph = new WeightedGraph(Util.readLines(_input));
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1() => graph.GetVertices().Min(vertex => GetDistance(vertex, 0, new List<string>(), (a, b) => Math.Min(a, b)));

        public int SolveTask2() => graph.GetVertices().Max(vertex => GetDistance(vertex, 0, new List<string>(), (a, b) => Math.Max(a, b)));

        private int GetDistance(string current, int traveled, List<string> visited, Func<int, int, int> comparator)
        {
            var copyVisited = visited.ToList();
            copyVisited.Add(current);
            var validDestinations = graph.GetValidDestinationsFor(current, visited);
            if (!validDestinations.Any()) return traveled;
            return traveled + validDestinations.Select(dest => GetDistance(dest.To, dest.Weight, copyVisited, comparator)).Aggregate(comparator);
        }
    }
}
