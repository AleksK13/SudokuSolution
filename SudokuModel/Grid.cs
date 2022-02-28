using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuModel
{
    public class Grid
    {
        public int[,] setka = new int[9, 9];//2d array


        public void PrintToConsole()
        {
            for (int i = 0; i < 9; i++)
            {
                string ciferki = string.Empty;
                for (int j = 0; j < 9; j++)
                {
                    ciferki += setka[i, j] + " ";
                }
                Console.WriteLine(ciferki);
            }
        }
        public void FillRandomGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    setka[i, j] = i;
                }
            }
        }
        public int[,] ReadFromFile()
        {
            String input = File.ReadAllText(@"D:\Programavimas\C#\SudokuSolution\SudokuModel\bin\Debug\Pradzia.txt");

            int i = 0, j;
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Split(','))
                {
                    setka[i, j] = int.Parse(col);
                    j++;
                }
                i++;
            }

            return setka;
        }
    }
}
