namespace AoC.src.Puzzles._2023.Day13
{
    public class Puzzle
    {
        private readonly List<List<string>> _lava;

        public Puzzle(string input)
        {
            _lava = ParseLava(Util.readLines(input));
            Console.WriteLine($"Result 1: {SolveTask1()}");
            Console.WriteLine($"Result 2: {SolveTask2()}");
        }

        private List<List<string>> ParseLava(List<string> lines)
        {
            var lava = new List<List<string>> { new() };
            foreach ( var line in lines )
            {
                if (line.Length == 0) lava.Add(new List<string>());
                else lava[^1].Add(line);
            }
            return lava;
        }

        public int SolveTask1() =>_lava.Sum(lava => FindMirror(lava));       
        public int SolveTask2() =>_lava.Sum(lava => FindMirror2(lava));       

        private int FindMirror(List<string> lava)
        {
            var val = FindRow(lava) * 100;
            if (val == 0) val = FindCol(lava);
            return val;
        }
        private int FindMirror2(List<string> lava)
        {
            var val = FindRow2(lava) * 100;
            if (val == 0) val = FindCol2(lava);
            return val;
        }

        private int FindCol(List<string> lava)
        {
            for (int col = 0; col < lava[0].Length - 1; col++)
            {
                if (StringAtIndex(lava, col) == StringAtIndex(lava, col + 1))
                {
                    if (CheckColSymetry(lava, col) == 1) return col + 1;
                }
            }
            return 0;
        }

        private int FindRow(List<string> lava)
        {
            for (int row = 0; row < lava.Count - 1; row++)
            {
                if (lava[row] == lava[row + 1])
                {
                    if (CheckRowSymetry(lava, row) == 1) return row + 1;
                }
            }
            return 0;
        }

        private int FindCol2(List<string> lava)
        {
            for (int col = 0; col < lava[0].Length - 1; col++)
            {
                var dif = DifferingChars(StringAtIndex(lava, col), StringAtIndex(lava, col + 1));
                if (dif <= 1)
                {
                    if (CheckColSymetry(lava, col) == 0) return col + 1;
                }
            }
            return 0;
        }

        private int FindRow2(List<string> lava)
        {
            for (int row = 0; row < lava.Count - 1; row++)
            {
                var dif = DifferingChars(lava[row], lava[row + 1]);
                if (dif <= 1)
                {
                    if (CheckRowSymetry(lava, row) == 0) return row + 1;
                }
            }
            return 0;
        }


        private int CheckColSymetry(List<string> lava, int col)
        {
            var res = 1;
            int i = 0;
            while (col - i >= 0 && col + 1 + i < lava[0].Count())
            {
                if (StringAtIndex(lava, col - i) != StringAtIndex(lava, col + 1 + i)) res--;
                i++;
            }
            return res;
        }

        private int CheckRowSymetry(List<string> lava, int row)
        {
            int res = 1;
            int i = 0;
            while(row-i >= 0 && row+1+i < lava.Count)
            {
                if (lava[row - i] != lava[row + 1 + i]) res--;
                i++;
            }
            return res;
        }

        private int DifferingChars(string str1, string str2)
            => str1.Zip(str2, (c1, c2) => new { c1, c2 })
                .Where(pair => pair.c1 != pair.c2)
                .Count();

        private string StringAtIndex(List<string> list, int i)
            => new String(list.Select(s => s[i]).ToArray());
        
    }
    // too high 30922,30767
    // not right 27350
}
