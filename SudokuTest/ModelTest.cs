﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            grid.FillRandomGrid();
            grid.PrintToConsole();
            Assert.AreEqual(3, grid.setka[3, 3]);
        }
        [TestMethod]
        public void CheckValidSolution()
        {
            var grid = new Grid();
            grid.ReadFromFile("Pradzia.txt");
            grid.PrintToConsole();
            Assert.AreEqual(0, grid.setka[3, 3]);
        }
    }
}