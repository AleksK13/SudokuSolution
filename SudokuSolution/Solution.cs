﻿using SudokuModel;
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
                            int z = -1;
                            for (int i = 0; i < 9; i++)
                            {
                                uint mask = (uint)1 << i;
                                bool allOk = ((row[x] | col[y] | sqr[GetSqrIndex(x, y)]) & mask) == 0;
                                if (allOk && z < 0)
                                {
                                    z = i;
                                }
                                else if (allOk)
                                {
                                    goto next;//tut ostanovilisj
                                }
                            }
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
            }


        }
        private static int GetSqrIndex(int x, int y)
        {
            int a = y / 3 + ((x / 3) << 2) - x / 3;
            return a;
        }
    }
}
