using System;
using System.Text.RegularExpressions;

namespace AoC.src.Puzzles._2015.Day07
{
    public class Puzzle
    {
        private readonly string _input;
        private List<Wire> _wires = new List<Wire>();

        delegate ushort Operator(ushort a, ushort b);

        private static ushort And(ushort a, ushort b) => (ushort)(a & b);
        private static ushort Or(ushort a, ushort b) => (ushort)(a | b);
        private static ushort LShift(ushort a, ushort b) => (ushort)(a << b);
        private static ushort RShift(ushort a, ushort b) => (ushort)(a >> b);
        private static ushort Not(ushort a, ushort b) => (ushort)~a;
        private static ushort Is(ushort a, ushort b) => a;

        private record Wire (string Name, string? LHS, string? RHS, Operator? Op, ushort? Value) { }

        public Puzzle(string input)
        {
            _input = input;
            GetWires();
            var result1 = SolveTask1();
            Console.WriteLine($"Result 1: {result1}");
            Console.WriteLine($"Result 2: {SolveTask2(result1)}");
        }

        public ushort SolveTask1()
        {
            List<Wire> wires = new(_wires);
            while (wires.Any(wire => wire.Value is null))
            {
                ResolveWires(ref wires);
            }
            return GetWireValue("a", wires) ?? throw new Exception("Result is null!");
        }

        private static ushort? GetWireValue(string wireName, List<Wire> wires)
        {
            if (wireName is "") return 0;
            return wires.FirstOrDefault(w => w.Name == wireName)?.Value ?? null;
        }

        private void ResolveWires(ref List<Wire> wires)
        {
            for (int i = 0; i < wires.Count; i++)
            {
                var wire = wires[i];
                if (wire.Value is not null || wire.LHS is null || wire.RHS is null) continue;
                var LHS = GetWireValue(wire.LHS, wires);
                if (LHS is null) continue;
                var RHS = GetWireValue(wire.RHS, wires);
                if (RHS is null) continue;
                wires[i] = wire with { Value = wire.Op((ushort)LHS, (ushort)RHS) };
            }

        }

        private void GetWires()
        {
            foreach(string w in Util.readLines( _input ))
            {
                var subs = w.Split(' ');
                if (subs[1] == "->")
                {
                    if (IsNumeric(subs[0]))
                        _wires.Add(new Wire(subs[2], null, null, null, ushort.Parse(subs[0])));
                    else
                        _wires.Add(new Wire(subs[2], subs[0], string.Empty, Is, null));

                }
                else if (subs[0] == "NOT")
                {
                    _wires.Add(new Wire(subs[3], subs[1], string.Empty, Not, null));
                }
                else
                {
                    _wires.Add(new Wire(subs[4], subs[0], subs[2], GetOp(subs[1]), null));
                    if (IsNumeric(subs[0]) && !_wires.Any(w => w.Name == subs[0]))
                    {
                        _wires.Add(new Wire(subs[0], null, null, null, ushort.Parse(subs[0])));
                    }
                    else if (IsNumeric(subs[2]) && !_wires.Any(w => w.Name == subs[2]))
                    {
                        _wires.Add(new Wire(subs[2], null, null, null, ushort.Parse(subs[2])));
                    }
                }
            }
        }

        private static bool IsNumeric(string x) => Regex.IsMatch(x, @"^\d+$");

        private Operator? GetOp(string op) => op switch
        {
            "AND" => And,
            "OR" => Or,
            "LSHIFT" => LShift,
            "RSHIFT" => RShift,
            _ => throw new InvalidDataException(op)
        };

        public int SolveTask2(ushort result1)
        {
            List<Wire> wires = new(_wires);
            ChangeWireValue(ref wires, "b", result1);          
            while (wires.Any(wire => wire.Value is null))
            {
                ResolveWires(ref wires);
            }
            return GetWireValue("a", wires) ?? throw new Exception("Result is null!");
        }

        private static void ChangeWireValue(ref List<Wire> wires, string wire, ushort value)
        {
            int index = wires.FindIndex(w => w.Name == "b");
            if (index is -1) throw new EntryPointNotFoundException(wire);
            wires[index] = wires[index] with { Value = value };
        }
    }
}
