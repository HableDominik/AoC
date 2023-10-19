using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.src.Puzzles._2015.Day01
{
    public class Puzzle
    {
        private readonly string _input;

        public Puzzle(string input)
        {
            _input = input;
            Console.WriteLine("Result 1: " + SolveTask1());
            Console.WriteLine("Result 2: " + SolveTask2());
        }

        public string SolveTask1()
        {
            var floor = 0;

            foreach(var c in _input)
            {
                floor += c is '(' ? 1 : -1;
            }

            return floor.ToString();
        }

        public string SolveTask2()
        {
            var floor = 0;
            var count = 0;

            while(floor is not -1)
            {
                floor += _input[count] is '(' ? 1 : -1;
                count++;
            }

            return count.ToString();
        }
    }
}
