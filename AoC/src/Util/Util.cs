using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.src
{
    public static class Util
    {
        public static List<string> readLines(string input) 
            => input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
    }
}
