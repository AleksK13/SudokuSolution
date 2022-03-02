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
        //3 list of 9 int lists
        private static List<int>[] rowList = new List<int>[9];
        private static List<int>[] colList = new List<int>[9];
        private static List<int>[] sqrList = new List<int>[9];
        //static constructor to initialise all lists
        static Solution()
        {
            for (int i = 0; i < 9; i++)
            {
                rowList[i] = new List<int>();
                colList[i] = new List<int>();
                sqrList[i] = new List<int>();
            }
        }
        //writing value to cell by "x" or "y" in 
        private static bool SetCell(int x, int y, int value)
        {
            //get list of numbers already used in row "x"
            List<int> row = rowList[x];

            //get list of numbers already used in column "y"
            List<int> col = colList[y];

            //get list of numbers already used in the same box as the cell in "x,y" coords
            List<int> sqr = sqrList[GetSqrIndex(x, y)];

            //cheking if row column and box contains our value
            bool rowOk = !row.Contains(value);
            bool colOk = !col.Contains(value);
            bool sqrOk = !sqr.Contains(value);
            //check if the value not presents in the lists
            bool allOk = (rowOk && colOk && sqrOk);

            if (allOk)
            {
                // adds value to lists to mark as used in this row, column, and box
                row.Add(value);
                col.Add(value);
                sqr.Add(value);
            }
            return allOk;
        }
        public static void SolveGrid(Grid GridObj)
        {
            //marks all used values from input Grid
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    int v = GridObj.setka[x, y];
                    if (v == 0)
                    {
                        continue;
                    }

                    bool allOk = SetCell(x, y, v);
                    if (!allOk)
                    {
                        throw new Exception("Invalid input!!!");
                    }
                }
            }
            ////
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
                            int[] res = GetValidGueses(x, y);
                            if (res.Length == 1)
                            {
                                v = res[0];
                            }
                            else
                            {
                                //calculating box first cell coords
                                var rowStart = (x / 3) * 3;
                                var colStart = (y / 3) * 3;
                                foreach (var item in res)
                                {
                                    bool notRepeats = true;
                                    //Check all empty cells in current box if they can contain item value
                                    for (int i = rowStart; i < rowStart + 3; i++)
                                    {
                                        for (int j = colStart; j < colStart + 3; j++)
                                        {
                                            //skip current cell
                                            if (i == x && j == y)
                                            {
                                                continue;
                                            }
                                            //skip all cells with values
                                            if (GridObj.setka[i, j] != 0)
                                            {
                                                continue;
                                            }
                                            notRepeats = notRepeats && (colList[j].Contains(item) || rowList[i].Contains(item));
                                        }
                                    }
                                    //if not repeats then item is answer
                                    if (notRepeats)
                                    {
                                        v = item;
                                        break;
                                    }
                                    notRepeats = true;
                                    //cheks if item can be placed in any of empty cells in current row
                                    for (int i = 0; i < 9; i++)
                                    {
                                        if (GridObj.setka[x, i] == 0)
                                        {
                                            if (i != y && GetSqrIndex(x, y) != GetSqrIndex(x, i))
                                            {
                                                notRepeats = notRepeats && (colList[i].Contains(item) || sqrList[GetSqrIndex(x, i)].Contains(item));
                                            }
                                            else if (i != y)
                                            {
                                                notRepeats = notRepeats && colList[i].Contains(item);
                                            }
                                        }
                                    }
                                    if (notRepeats)
                                    {
                                        v = item;
                                        break;
                                    }
                                    notRepeats = true;
                                    //cheks if item can be placed in any of empty cells in current column
                                    for (int i = 0; i < 9; i++)
                                    {
                                        if (GridObj.setka[i, y] == 0)
                                        {
                                            if (i != x && GetSqrIndex(x, y) != GetSqrIndex(i, y))
                                            {
                                                notRepeats = notRepeats && (rowList[i].Contains(item) || sqrList[GetSqrIndex(i, y)].Contains(item));
                                            }
                                            else if (i != x)
                                            {
                                                notRepeats = notRepeats && rowList[i].Contains(item);
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
                            //if we made a guess
                            if (v != 0)
                            {
                                //updates our list with our guess
                                bool allOk = SetCell(x, y, v);
                                if (!allOk)
                                {
                                    throw new Exception("Invalid input!!!");
                                }
                                //write it to GridObj
                                GridObj.setka[x, y] = v;
                                flag = true;
                            }
                        }
                    }
                }
            }
        }
        //find all valid answers to "x,y" Cell
        private static int[] GetValidGueses(int x, int y)
        {
            List<int> row = rowList[x];
            List<int> col = colList[y];
            List<int> sqr = sqrList[GetSqrIndex(x, y)];
            List<int> result = new List<int>();
            //check all values from 1 to 9 for validity
            for (int i = 1; i <= 9; i++)
            {
                bool rowOk = !row.Contains(i);
                bool colOk = !col.Contains(i);
                bool sqrOk = !sqr.Contains(i);
                bool allOk = (rowOk && colOk && sqrOk);
                if (allOk)
                {
                    //add to list of valid answers
                    result.Add(i);
                }
            }
            return result.ToArray();
        }
        //calculates box index where cell"x,y" is located
        private static int GetSqrIndex(int x, int y)
        {
            int a = y / 3 + ((x / 3) * 3);
            return a;
        }
    }
}
