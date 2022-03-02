using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuModel;
using System;

namespace SudokuTest
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void CreateGridTest()
        {
            var grid = new Grid();
            grid.FillGrid();
            grid.PrintToConsole();
        }
        [TestMethod]
        public void CheckValidSolution()
        {
            var grid = new Grid();
            grid.ReadFromFile("Pradzia.txt");
            grid.PrintToConsole();
        }
        [TestMethod]
        public void TestBitov()
        {
            int x, y;
            x = 1; //row
            y = 1; //colums
            int c = 7;

            uint[] row = new uint[9];
            uint[] col = new uint[9];
            uint[] sqr = new uint[9];

            uint mask = (uint)1 << c - 1;
            Console.WriteLine($"mask :  {Convert.ToString(mask, toBase: 2)}");
            row[x - 1] |= mask;
            col[y - 1] |= mask;
            row[x - 1] |= mask >> 1;
            sqr[GetSqrIndex(x, y)] |= mask;

            y = 2;
            bool rowok = (row[x - 1] & mask) == 0;//checks if "c" was used in row "x"
            Console.WriteLine($"row :  {Convert.ToString(row[x - 1], toBase: 2).PadLeft(9, '0')}");
            Console.WriteLine($"mask:  {Convert.ToString(mask, toBase: 2).PadLeft(9, '0')}");
            Console.WriteLine($"{rowok}");

            bool colok = (col[y - 1] & mask) == 0;////checks if "c" was used in column "y"
            Console.WriteLine($"col :  {Convert.ToString(col[y - 1], toBase: 2).PadLeft(9, '0')}");
            Console.WriteLine($"mask:  {Convert.ToString(mask, toBase: 2).PadLeft(9, '0')}");
            Console.WriteLine($"{colok}");

            bool sqrok = (sqr[GetSqrIndex(x, y)] & mask) == 0;////checks if "c" was used in square 
            Console.WriteLine($"sqr :  {Convert.ToString(sqr[GetSqrIndex(x, y)], toBase: 2).PadLeft(9, '0')}");
            Console.WriteLine($"mask:  {Convert.ToString(mask, toBase: 2).PadLeft(9, '0')}");
            Console.WriteLine($"{sqrok}");

            bool allOk = ((row[x - 1] | col[y - 1] | sqr[GetSqrIndex(x, y)]) & mask) == 0;

            Console.WriteLine(allOk);

            Console.WriteLine($"{GetSqrIndex(2, 2)},{GetSqrIndex(2, 5)},{GetSqrIndex(2, 8)}");
            Console.WriteLine($"{GetSqrIndex(5, 2)},{GetSqrIndex(5, 5)},{GetSqrIndex(5, 8)}");
            Console.WriteLine($"{GetSqrIndex(7, 2)},{GetSqrIndex(7, 5)},{GetSqrIndex(9, 9)}");


        }
        private int GetSqrIndex(int x, int y)
        {
            x--; y--;
            int a = y / 3 + ((x / 3) *3);
            return a;
        }
        [TestMethod]
        public void TestSolution()
        {
            Grid G = new Grid();
            G.ReadFromFile("Pradzia.txt");
            SudokuSolution.Solution.SolveGrid(G);
            G.PrintToConsole();
        }
        [TestMethod]
        public void TestBoxIndex()
        {
            Console.WriteLine($"{GetSqrIndex(2, 2)},{GetSqrIndex(2, 5)},{GetSqrIndex(2, 8)}");
            Console.WriteLine($"{GetSqrIndex(5, 2)},{GetSqrIndex(5, 5)},{GetSqrIndex(5, 8)}");
            Console.WriteLine($"{GetSqrIndex(7, 2)},{GetSqrIndex(7, 5)},{GetSqrIndex(9, 9)}");
        }
    }
}
