using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    public class Matrix
    {
        public Cell[,] Cells { get; set; }

        public Matrix()
        {
            Cells = new Cell[3, 3];
            for (int i=0;i<3; i++)
            {
                for (int j = 0; j < 3; j++)
                    Cells[i, j] = new Cell();
            }
            
        }

        public bool Filled
        { get; set; }

        public void AutoFill()
        {
            int a;
            foreach (Cell c in this.Cells)
            {
                if (c.Filled)
                {
                    a = c.Content;
                    foreach (Cell c2 in this.Cells)
                    {
                        if (!c2.Filled)
                            c2.CantBe(a);
                    }
                }
            }
        }

        public int Fill()
        {
            int nbChanged = 0;
            for (short v=1; v<10; v++)
            {
                int s = 0;
                foreach (Cell c in Cells)
                {
                    if (!c.Filled)
                    {
                        s = s + c.CouldBe[v-1];
                    }
                }
                if (s==1) //only 1 cell can have the value
                {
                    foreach (Cell c in Cells)
                    {
                        if (!c.Filled && c.CouldBe[v - 1] == 1)
                        {
                            c.Content = v;
                            nbChanged++;
                            break;
                        }
                    }
                }
            }
            //potentially change status of matrix if all its 9 cells are filled.
            bool filled = true;
            foreach (Cell c in Cells)
            {
                filled = filled && c.Filled;
                if (!filled)
                    break;
            }
            this.Filled = filled;
            return nbChanged;
        }
    }
}
