using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.src.Puzzles._2023.Day03
{

    internal class Gear
    {
        public int Number { get; init; }
        public List<Point> Coordinates { get; init; }

        public Symbol? Symbol { get; set; }

        public Gear(int number, Point start)
        {
            Number = number;
            Coordinates = new List<Point>();
            for(int i = 0; i < number.ToString().Length; i++)
            {
                Coordinates.Add(new Point(start.X + i, start.Y));
            }
        }
    }

    internal class Symbol
    {
        public char Character { get; set; }
        public Point Coordinate { get; set; }

        public Symbol(char c, Point p)
        {
            Character = c;
            Coordinate = p;
        }
    }
}
