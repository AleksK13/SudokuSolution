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
        public void ReadFromFile(string file)
        {
            FileStream fs = File.Open(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                int[,] setka = new int[9, 9];
                for (int i = 0; i < 9; i++)
                {
                    string ciferki = string.Empty;
                    for (int j = 0; j < 9; j++)
                    {
                        ciferki += setka[i, j] + " ";
                    }
                }

            }
            fs.Close();
        }
    }

}
