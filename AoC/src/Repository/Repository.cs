using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC.src.Repository
{
    public class Repository
    {
        public Type getTaskType(string year, string day)
        {
            validateYearAndDay(year, ref day);

            day = addZeroIfNeeded(day);
            var path = $"AoC.src.Puzzles._{year}.Day{day}.Puzzle";
            Type? taskType = Type.GetType(path);
            if (taskType is null)
            {
                throw new FileNotFoundException();
            }
            return taskType;
        }

        private void validateYearAndDay(string year, ref string day)
        {
            if(!isBetween(Int32.Parse(year), 2015, DateTime.Now.Year))
            {
                throw new ArgumentException($"{year} is not a valid year.");
            }
            
            if (!isBetween(Int32.Parse(day), 1, 25))
            {
                throw new ArgumentException($"{day} is not a valid day.");
            }

            day = addZeroIfNeeded(day);
        }

        public void Solve(Type taskType, string input)
        {
            Activator.CreateInstance(taskType, input);
        }

        private string addZeroIfNeeded(string day) => day.Length is 2 ? day : $"0{day}";

        private bool isBetween(int n, int min, int max) => n >= min && n <= max;

        internal string getInput(string year, string day)
        {
            validateYearAndDay(year, ref day);

            var basePath = GetProjectRootPath();

            var path = Path.Combine(basePath, $"src\\Puzzles\\{year}\\Day{day}\\Input.txt");

            Console.WriteLine(path);

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                throw new FileNotFoundException($"{path} not found.");
            }
        }

        public static string GetProjectRootPath()
        {
            string baseDirectory = AppContext.BaseDirectory;
            int index = baseDirectory.IndexOf("bin");
            return baseDirectory[..index];
        }
    }
}
