namespace AoC.src.Puzzles._2015.Day07
{
    public class Puzzle
    {
        private readonly string _input;

        delegate ushort Operator(ushort a, ushort b);

        public static ushort And(ushort a, ushort b) => (ushort)(a & b);
        public static ushort Or(ushort a, ushort b) => (ushort)(a | b);
        public static ushort LShift(ushort a, ushort b) => (ushort)(a << b);
        public static ushort RShift(ushort a, ushort b) => (ushort)(a >> b);
        public static ushort Not(ushort a, ushort b) => (ushort)~a;


        public Puzzle(string input)
        {
            _input = input;
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
        {
            testOperations();
            return 0;
        }

        private void testOperations()
        {
            ushort x = 123;
            ushort y = 456;
            Operator and = And;
            ushort d = and(x, y);
            Operator or = Or;
            ushort e = or(x, y);
            Operator lshift = LShift;
            ushort f = lshift(x, 2);
            Operator rshift = RShift;
            ushort g = rshift(y, 2);
            Operator not = Not;
            ushort h = not(x, 0);
            ushort i = not(y, 0);
            Console.WriteLine(d);
            Console.WriteLine(e);
            Console.WriteLine(f);
            Console.WriteLine(g);
            Console.WriteLine(h);
            Console.WriteLine(i);
            Console.WriteLine(x);
            Console.WriteLine(y);
        }

        public int SolveTask2()
        {
            return 0;
        }
    }
}
