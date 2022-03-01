using Newtonsoft.Json;
using SudokuModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolution
{
    public class Solution
    {
        private static List<int>[] rowList = new List<int>[9];
        private static List<int>[] colList = new List<int>[9];
        private static List<int>[] sqrList = new List<int>[9];

        private static bool SetCell(int x, int y, int value)
        {
            List<int> row = rowList[x];
            if (row == null)
            {
                row = new List<int>();
                rowList[x] = row;
            }

            List<int> col = colList[y];
            if (col == null)
            {
                col = new List<int>();
                colList[y] = col;
            }

            List<int> sqr = sqrList[GetSqrIndex(x, y)];
            if (sqr == null)
            {
                sqr = new List<int>();
                sqrList[GetSqrIndex(x, y)] = sqr;
            }

            bool rowOk = !row.Contains(value);
            bool colOk = !col.Contains(value);
            bool sqrOk = !sqr.Contains(value);
            bool allOk = (rowOk && colOk && sqrOk);

            if (allOk)
            {
                row.Add(value);
                col.Add(value);
                sqr.Add(value);
            }
            return allOk;
        }
        public static void SolveGrid(Grid GridObj)
        {
            //uint[] row = new uint[9];
            //uint[] col = new uint[9];
            //uint[] sqr = new uint[9];

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    int v = GridObj.setka[x, y];
                    if (v == 0)
                    {
                        continue;
                    }
                    //uint mask = (uint)1 << v - 1;

                    //bool allOk = ((row[x] | col[y] | sqr[GetSqrIndex(x, y)]) & mask) == 0;
                    bool allOk = SetCell(x, y, v);
                    if (!allOk)
                    {
                        throw new Exception("Invalid input!!!");
                    }
                    //row[x] |= mask;
                    //col[y] |= mask;
                    //sqr[GetSqrIndex(x, y)] |= mask;
                }
            }
            bool flag = true;
            while (flag)
            {
                flag = false;
                bool flag1 = false;
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        int v = GridObj.setka[x, y];
                        if (v == 0)
                        {
                            flag = true;
                            //uint z = (~row[x] & ~col[y] & ~sqr[GetSqrIndex(x, y)]) & 0b111111111;
                            //double rez = Math.Log(z, 2);
                            //if (rez % 1 == 0)
                            //{
                            //    v = (int)rez + 1;
                            //}
                            //////////////////
                            int[] res = GetValidGueses(x, y);

                            if (res.Length == 1)
                            {
                                v = res[0];
                            }
                            else
                            {
                                var rowStart = (x / 3) * 3;
                                var colStart = (y / 3) * 3;
                                foreach (var item in res)
                                {
                                    bool notRepeats = true;
                                    for (int i = rowStart; i < rowStart + 3; i++)
                                    {

                                        for (int j = colStart; j < colStart + 3; j++)
                                        {
                                            if (i == x && j == y)
                                            {
                                                continue;
                                            }
                                            if (GridObj.setka[i, j] != 0)
                                            {
                                                continue;
                                            }
                                            notRepeats = notRepeats && (colList[j].Contains(item) || rowList[i].Contains(item));
                                        }

                                    }
                                    if (notRepeats)
                                    {
                                        v = item;
                                        break;
                                    }
                                    notRepeats = true;
                                    for (int i = 0; i < 9; i++)
                                    {
                                        if (GridObj.setka[x, i] == 0)
                                        {
                                            if (i != y)
                                            {
                                                notRepeats = notRepeats && (colList[i].Contains(item) || sqrList[GetSqrIndex(x, i)].Contains(item));
                                            }
                                        }
                                    }
                                    if (notRepeats)
                                    {
                                        v = item;
                                        break;
                                    }
                                    notRepeats = true;
                                    for (int i = 0; i < 9; i++)
                                    {
                                        if (GridObj.setka[i, y] == 0)
                                        {
                                            if (i != x)
                                            {
                                                notRepeats = notRepeats && (rowList[i].Contains(item) || sqrList[GetSqrIndex(i, y)].Contains(item));
                                            }
                                        }
                                    }
                                    if (notRepeats)
                                    {
                                        v = item;
                                        break;
                                    }
                                }

                                if (v == 0)
                                {
                                    Debug.WriteLine($"{x} ------ {y} --------{JsonConvert.SerializeObject(res)}");
                                    if (res.Length == 0)
                                    {
                                        GridObj.PrintToDebug();
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                            //////////////////////
                            if (v != 0)
                            {
                                //uint mask = (uint)1 << v - 1;

                                //bool allOk = ((row[x] | col[y] | sqr[GetSqrIndex(x, y)]) & mask) == 0;
                                bool allOk = SetCell(x, y, v);
                                if (!allOk)
                                {
                                    //Console.WriteLine($"mask:  {Convert.ToString(mask, toBase: 2).PadLeft(9, '0')}");
                                    //Console.WriteLine($"row :  {Convert.ToString(row[x], toBase: 2).PadLeft(9, '0')}");
                                    //Console.WriteLine($"col :  {Convert.ToString(col[y], toBase: 2).PadLeft(9, '0')}");
                                    //Console.WriteLine($"sqr :  {Convert.ToString(sqr[GetSqrIndex(x, y)], toBase: 2).PadLeft(9, '0')}");
                                    throw new Exception("Invalid input!!!");
                                }
                                //row[x] |= mask;
                                //col[y] |= mask;
                                //sqr[GetSqrIndex(x, y)] |= mask;
                                GridObj.setka[x, y] = v;
                                flag1 = true;
                            }
                        }
                    }
                }
                if (!flag1)
                {
                    break;
                }
            }
            GridObj.PrintToConsole();
        }

        private static int[] GetValidGueses(int x, int y)
        {
            List<int> row = rowList[x];
            if (row == null)
            {
                row = new List<int>();
                rowList[x] = row;

            }
            List<int> col = colList[y];
            if (col == null)
            {
                col = new List<int>();
                colList[y] = col;
            }
            List<int> sqr = sqrList[GetSqrIndex(x, y)];
            if (sqr == null)
            {
                sqr = new List<int>();
                sqrList[GetSqrIndex(x, y)] = sqr;
            }
            List<int> result = new List<int>();

            for (int i = 1; i <= 9; i++)
            {
                bool rowOk = !row.Contains(i);
                bool colOk = !col.Contains(i);
                bool sqrOk = !sqr.Contains(i);
                bool allOk = (rowOk && colOk && sqrOk);
                if (allOk)
                {
                    result.Add(i);
                }
            }
            return result.ToArray();
        }

        private static int GetSqrIndex(int x, int y)
        {
            int a = y / 3 + ((x / 3) * 4) - x / 3;
            return a;
        }
    }
}
