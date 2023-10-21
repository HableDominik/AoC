using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AoC.src.Puzzles._2015.Day04
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
            for (int i = 0; i <= int.MaxValue; i++)
            {
                var hash = ComputeMD5Hash(_input + i);
                if (startsWith(hash, "00000")) return i;
            }
            throw new Exception("Solution not found.");
        }

        private bool startsWith(string hash, string start) => hash.StartsWith(start);

        private static string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public int SolveTask2()
        {
            for (int i = 0; i <= int.MaxValue; i++)
            {
                var hash = ComputeMD5Hash(_input + i);
                if (startsWith(hash, "000000")) return i;
            }
            throw new Exception("Solution not found.");
        }
    }
}
