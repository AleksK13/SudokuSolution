using SudokuModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolution
{
    public class Solution
    {
        public static void SolveGrid(Grid GridObj)
        {
            uint[] row = new uint[9];
            uint[] col = new uint[9];
            uint[] sqr = new uint[9];


            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    int v = GridObj.setka[x, y];
                    if (v == 0)
                    {
                        continue;
                    }
                    uint mask = (uint)1 << v - 1;

                    bool allOk = ((row[x] | col[y] | sqr[GetSqrIndex(x, y)]) & mask) == 0;
                    if (!allOk)
                    {
                        throw new Exception("Invalid input!!!");
                    }
                    row[x] |= mask;
                    col[y] |= mask;
                    sqr[GetSqrIndex(x, y)] |= mask;
                }
            }
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        int v = GridObj.setka[x, y];
                        if (v == 0)
                        {
                            flag = true;
                            uint z = (~row[x] & ~col[y] & ~sqr[GetSqrIndex(x, y)]) & 0b111111111;
                            double rez = Math.Log(z, 2);
                            if (rez % 1 == 0)
                            {
                                v = (int)rez + 1;
                            }

                            if (v != 0)
                            {
                                uint mask = (uint)1 << v - 1;

                                bool allOk = ((row[x] | col[y] | sqr[GetSqrIndex(x, y)]) & mask) == 0;
                                if (!allOk)
                                {
                                    Console.WriteLine($"mask:  {Convert.ToString(mask, toBase: 2).PadLeft(9, '0')}");
                                    Console.WriteLine($"row :  {Convert.ToString(row[x], toBase: 2).PadLeft(9, '0')}");
                                    Console.WriteLine($"col :  {Convert.ToString(col[y], toBase: 2).PadLeft(9, '0')}");
                                    Console.WriteLine($"sqr :  {Convert.ToString(sqr[GetSqrIndex(x, y)], toBase: 2).PadLeft(9, '0')}");
                                    throw new Exception("Invalid input!!!");
                                }
                                row[x] |= mask;
                                col[y] |= mask;
                                sqr[GetSqrIndex(x, y)] |= mask;
                                GridObj.setka[x, y] = v;
                            }
                        }


                    }
                }
               
            }
            GridObj.PrintToConsole();
        }
        private static int GetSqrIndex(int x, int y)
        {
            int a = y / 3 + ((x / 3) << 2) - x / 3;
            return a;
        }
    }
}
