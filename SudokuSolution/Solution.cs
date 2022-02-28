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
        Grid GridObj = new Grid();

        public void CheckMembers()
        {
            foreach (var item in GridObj.setka)
            {
                if (item == 0)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            
                        }
                    }
                }

            }
        }
