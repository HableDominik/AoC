using System;
using System.Collections.Generic;
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
    }
}
