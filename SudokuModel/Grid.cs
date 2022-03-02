using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SudokuModel
{
    public class Grid
    {
        public int[,] setka = new int[9, 9];//2d array

        //Printing all Grid
        public void PrintToConsole()
        {
            for (int i = 0; i < 9; i++)
            {
                string numbers = string.Empty;
                for (int j = 0; j < 9; j++)
                {
                    numbers += setka[i, j] + " ";
                }
                Console.WriteLine(numbers);
            }
        }
        // Fills grid of numbers
        public void FillGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    setka[i, j] = i;
                }
            }
        }
        //Reads file and fills grid from it
        public void ReadFromFile(string file)
        {
            String input = File.ReadAllText(file);

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

        }
        public void PrintToDebug()
        {
            for (int i = 0; i < 9; i++)
            {
                string numbers = string.Empty;
                for (int j = 0; j < 9; j++)
                {
                    numbers += setka[i, j] + " ";
                }
                Debug.WriteLine(numbers);
            }
        }
        //Cheking if sudoku solved or not
        public bool IsSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                string ciferki = string.Empty;
                for (int j = 0; j < 9; j++)
                {
                    if (setka[i, j] == 0) return false;                    
                }                
            }
            return true;
        }
    }
}
