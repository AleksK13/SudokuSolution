using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SudokuModel;
using SudokuSolution;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Welcome");

            int choise = 1;
            var tasks = Directory.GetFiles("Tasks", "*.txt");

            foreach (var file in tasks)
            {
                Console.WriteLine($"{choise++} {file}");
            }

            int kakojnibudj = -1;
            while (true)
            {
                Console.WriteLine("Please select one of the tasks");
                var c = Console.ReadLine();
                if (!int.TryParse(c, out kakojnibudj)) continue;
                if (kakojnibudj < 0 || kakojnibudj >= choise) continue;

                break;

            }
            Grid G = new Grid();
            G.ReadFromFile(tasks[kakojnibudj - 1]);
            G.PrintToConsole();
            Solution.SolveGrid(G);
            Console.WriteLine();
            if (G.IsSolved() == true)
            {
                Console.WriteLine("Sudoku solved!!");
            }
            else { Console.WriteLine("Sorry not enough brains!!"); }
            G.PrintToConsole();

            Console.ReadLine();
        }
    }
}
