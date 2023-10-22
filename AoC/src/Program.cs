using AoC.src.Repository;
using System.Reflection;

namespace AoC
{
    public class Programm
    {
        public static void Main()
        {
            var year = "2015";
            var day = "5";

            var repository = new Repository();

            var input = repository.getInput(year, day);

            var taskType = repository.getTaskType(year, day);

            repository.Solve(taskType, input);
        }
    }
}