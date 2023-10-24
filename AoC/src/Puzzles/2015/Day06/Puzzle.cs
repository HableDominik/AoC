using System.ComponentModel;
using System.Drawing;

namespace AoC.src.Puzzles._2015.Day06
{
    public class Puzzle
    {
        private readonly string _input;
        private readonly List<string> _ops;
        private bool[,] lights;
        private int[,] dimmingLights;
        private const int size = 1000;

        enum Operation { On, Off, Toggle }

        private record OperationInfo(Point P1, Point P2, Operation Op);

        public Puzzle(string input)
        {
            _input = input;
            _ops = Util.readLines(_input);
            lights = new bool[size, size];
            dimmingLights = new int[size, size];
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        public int SolveTask1()
        {
            _ops.ForEach(op =>  ModifyLights(GetOperationInfo(op)));
            return CountLitLights();
        }

        private int CountLitLights() => lights.Cast<bool>().Count(b => b);

        private OperationInfo GetOperationInfo(string op)
        {
            var strings = op.Split(' ', ',');
            if(strings.Length == 6 ) 
            {
                return new OperationInfo(
                    new Point(Int32.Parse(strings[1]), Int32.Parse(strings[2])),
                    new Point(Int32.Parse(strings[4]), Int32.Parse(strings[5])),
                    Operation.Toggle);
            }
            if (strings[1] == "on")
            {
                return new OperationInfo(
                    new Point(Int32.Parse(strings[2]), Int32.Parse(strings[3])),
                    new Point(Int32.Parse(strings[5]), Int32.Parse(strings[6])),
                    Operation.On);
            }
            return new OperationInfo(
                    new Point(Int32.Parse(strings[2]), Int32.Parse(strings[3])),
                    new Point(Int32.Parse(strings[5]), Int32.Parse(strings[6])),
                    Operation.Off);
        }

        private void ModifyLights(OperationInfo info)
        {
            for (int y = info.P1.Y; y <= info.P2.Y; y++)
            {
                for (int x = info.P1.X; x <= info.P2.X; x++)
                {
                    lights[x, y] = info.Op switch
                    {
                        Operation.On => true,
                        Operation.Off => false,
                        Operation.Toggle => !lights[x, y],
                        _ => throw new InvalidEnumArgumentException(nameof(info.Op), (int)info.Op, typeof(Operation))
                    };

                }
            }
        }

        public int SolveTask2()
        {
            _ops.ForEach(op => ModifyLightBrightness(GetOperationInfo(op)));
            return GetTotalBrightness();          
        }

        private int GetTotalBrightness() => dimmingLights.Cast<int>().Sum(b => b);

        private void ModifyLightBrightness(OperationInfo info)
        {
            for (int y = info.P1.Y; y <= info.P2.Y; y++)
            {
                for (int x = info.P1.X; x <= info.P2.X; x++)
                {
                    dimmingLights[x, y] += info.Op switch
                    {
                        Operation.On => 1,
                        Operation.Off => dimmingLights[x, y] > 0 ? -1 : 0,
                        Operation.Toggle => 2,
                        _ => throw new InvalidEnumArgumentException(nameof(info.Op), (int)info.Op, typeof(Operation))
                    };

                }
            }
        }
    }
}
