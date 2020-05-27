using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    public class Grid
    {
        public Matrix[,] Matrices;

        public Grid()
        {
            Matrices = new Matrix[3, 3];
            for (int i=0;i<3;i++)
            {
                for (int j = 0; j < 3; j++)
                    Matrices[i, j] = new Matrix();
            }
            Matrices[1, 1] = new Matrix();
        }


        public void AutoFill()
        {
            int nbChanged = 0;
            foreach (Matrix ma in this.Matrices)
            {
                if (!ma.Filled)
                {
                    ma.AutoFill();
                }
            }
            this.AutoFillByRows();
            this.AutoFillByColumns();
            foreach (Matrix ma in this.Matrices)
            {
                if (!ma.Filled)
                {
                    nbChanged += ma.Fill();
                }
            }
            while (nbChanged > 0) {
                    foreach (Matrix ma in this.Matrices)
                    {
                        if (!ma.Filled)
                        {
                            ma.AutoFill();
                        }
                    }
                    this.AutoFillByRows();
                    this.AutoFillByColumns();
                    nbChanged = 0;
                    foreach (Matrix ma in this.Matrices)
                    {
                        if (!ma.Filled)
                        {
                            nbChanged += ma.Fill();
                        }
                    }
             }
            do
            {
                nbChanged = FillRowsAlmostFull();
                nbChanged += FillColumnsAlmostFull();
                nbChanged = 0;
                foreach (Matrix ma in this.Matrices)
                {
                    if (!ma.Filled)
                    {
                        nbChanged += ma.Fill();
                    }
                }
            }
            while (nbChanged > 0);
        }

        private int FillRowsAlmostFull()
        {
            //if a value can only be in a specific row in a matrix
            //the other matrices side-by-side cannot have that value on that row.
            int nbChanged = 0;
            for(short val=1;val<10;val++)
            {
                for (short n = 0; n < 3; n++)
                {
                    for (short p = 0; p < 3; p++)
                    {
                        Matrix m = Matrices[n, p];
                        //if (val==2&&n==2&&p==0)
                        //{
                        //    int toto = 1;
                        //}
                        short[] s = new short[3];
                        short row = -1;
                        for (short i = 0; i < 3; i++) //the 3 rows in each matrix
                        {                          
                            for (short j = 0; j < 3; j++)
                            {
                                if (m.Cells[i, j].CouldBe[val - 1] == 1)
                                {
                                    s[i] = 1;
                                    row = i;
                                    break;
                                }
                            }
                        }
                        //test if 2 sums are 0
                        if(s[0]+s[1]+s[2]==1 && row>=0)
                        {
                            //the other 2 matrices cannot have the value in the same row.
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    if (l != p)
                                    {
                                        if (!Matrices[n, l].Cells[row, k].Filled)
                                        {
                                            Matrices[n, l].Cells[row, k].CantBe(val);
                                            nbChanged += 1;
                                        }
                                    }
                                }
                            }                          
                        }
                    }
                }
            }
            return nbChanged;
        }
        private int FillColumnsAlmostFull()
        {
            //if a value can only be in a specific column in a matrix
            //the other matrices above/below cannot have that value on that column.
            int nbChanged = 0;
            for (short val = 1; val < 10; val++)
            {
                for (short n = 0; n < 3; n++)
                {
                    for (short p = 0; p < 3; p++)
                    {
                        Matrix m = Matrices[n, p];
                        //if (val==2&&n==2&&p==0)
                        //{
                        //    int toto = 1;
                        //}
                        short[] s = new short[3];
                        short col = -1;
                        for (short j = 0; j < 3; j++) //the 3 cols in each matrix
                        {
                            for (short i = 0; i < 3; i++)
                            {
                                if (m.Cells[i, j].CouldBe[val - 1] == 1)
                                {
                                    s[j] = 1;
                                    col = j;
                                    break;
                                }
                            }
                        }
                        //test if 2 sums are 0
                        if (s[0] + s[1] + s[2] == 1 && col >= 0)
                        {
                            //the other 2 matrices cannot have the value in the same row.
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    if (l != n)
                                    {
                                        if (!Matrices[l, p].Cells[k, col].Filled)
                                        {
                                            Matrices[l, p].Cells[k, col].CantBe(val);
                                            nbChanged += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return nbChanged;
        }
        private void AutoFillByRows()
            {


                for (int n = 0; n < 3; n++)
                {
                    for (int p = 0; p < 3; p++)
                    {
                        Matrix m = Matrices[n, p];
                        for (int i = 0; i < 3; i++) //the 3 rows in each matrix
                        {
                            List<int> values = new List<int>();
                            for (int j = 0; j < 3; j++) //the 3 columns
                            {
                                if (m.Cells[i, j].Filled)
                                {
                                    values.Add(m.Cells[i, j].Content);
                                }
                            }
                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    if (l != p) {
                                        if (!Matrices[n, l].Cells[i, k].Filled)
                                        {
                                            foreach (int val in values)
                                            {
                                                Matrices[n, l].Cells[i, k].CantBe(val);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        private void AutoFillByColumns()
        {
            for (int n = 0; n < 3; n++)
            {
                for (int p = 0; p < 3; p++)
                {
                    Matrix m = Matrices[n, p];
                    //debug:
                    //int h;
                    //if (n == 1 && p == 2)
                    //    h = 4;
                    //end debug
                    for (int j = 0; j < 3; j++) //the 3 columns in each matrix
                    {
                        //collect the filled values in that column
                        List<int> values = new List<int>();
                        for (int i = 0; i < 3; i++) //the 3 rows
                        {
                            if (m.Cells[i, j].Filled)
                            {
                                values.Add(m.Cells[i, j].Content);
                            }
                        }
                        for (int k = 0; k < 3; k++) //the rows of the other 2 matrices in same main column
                        {
                            for (int l = 0; l < 3; l++) //the other matrices
                            {
                                if (l != n) //not the same matrix
                                {
                                    if (!Matrices[l, p].Cells[k, j].Filled)
                                    {
                                        foreach (int val in values)
                                        {
                                            Matrices[l, p].Cells[k, j].CantBe(val);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Initialize()
        {
            foreach(Matrix m in this.Matrices)
            {
                foreach (Cell c in m.Cells)
                    c.Initialized = (c.Content != 0);
            }
        }
        public void SetExample()
        {
            //top row
            //left
            this.Matrices[0, 0].Cells[0, 0].Content = 5;
            this.Matrices[0, 0].Cells[0, 1].Content = 3;
            this.Matrices[0, 0].Cells[1, 0].Content = 6;
            this.Matrices[0, 0].Cells[2, 1].Content = 9;
            this.Matrices[0, 0].Cells[2, 2].Content = 8;
            //mid
            this.Matrices[0, 1].Cells[0, 1].Content = 7;
            this.Matrices[0, 1].Cells[1, 0].Content = 1;
            this.Matrices[0, 1].Cells[1, 1].Content = 9;
            this.Matrices[0, 1].Cells[1, 2].Content = 5;
            //right
            this.Matrices[0, 2].Cells[2, 1].Content = 6;
            //mid row
            //left
            this.Matrices[1, 0].Cells[0, 0].Content = 8;
            this.Matrices[1, 0].Cells[1, 0].Content = 4;
            this.Matrices[1, 0].Cells[2, 0].Content = 7;
            //mid
            this.Matrices[1, 1].Cells[0, 1].Content = 6;
            this.Matrices[1, 1].Cells[1, 0].Content = 8;
            this.Matrices[1, 1].Cells[1, 2].Content = 3;
            this.Matrices[1, 1].Cells[2, 1].Content = 2;
            //right
            this.Matrices[1, 2].Cells[0, 2].Content = 3;
            this.Matrices[1, 2].Cells[1, 2].Content = 1;
            this.Matrices[1, 2].Cells[2, 2].Content = 6;
            //bottom row
            //left
            this.Matrices[2, 0].Cells[0, 1].Content = 6;
            //mid
            this.Matrices[2, 1].Cells[1, 0].Content = 4;
            this.Matrices[2, 1].Cells[1, 1].Content = 1;
            this.Matrices[2, 1].Cells[1, 2].Content = 9;
            this.Matrices[2, 1].Cells[2, 1].Content = 8;
            //right
            this.Matrices[2, 2].Cells[0, 0].Content = 2;
            this.Matrices[2, 2].Cells[0, 1].Content = 8;
            this.Matrices[2, 2].Cells[1, 2].Content = 5;
            this.Matrices[2, 2].Cells[2, 1].Content = 7;
            this.Matrices[2, 2].Cells[2, 2].Content = 9;
        }
        public void SetExample2()
        {
            //top row
            //left
            this.Matrices[0, 0].Cells[0, 0].Content = 1;
            this.Matrices[0, 0].Cells[0, 1].Content = 4;
            this.Matrices[0, 0].Cells[0, 2].Content = 2;
            this.Matrices[0, 0].Cells[1, 0].Content = 7;
            this.Matrices[0, 0].Cells[2, 0].Content = 8;
            this.Matrices[0, 0].Cells[2, 2].Content = 5;
            //mid
            this.Matrices[0, 1].Cells[0, 1].Content = 9;
            this.Matrices[0, 1].Cells[1, 0].Content = 4;
            
            //right
            this.Matrices[0, 2].Cells[0, 2].Content = 5;
            this.Matrices[0, 2].Cells[1, 1].Content = 8;
            this.Matrices[0, 2].Cells[1, 2].Content = 9;
            this.Matrices[0, 2].Cells[2, 1].Content = 2;
            this.Matrices[0, 2].Cells[2, 2].Content = 4;
            //mid row
            //left
            this.Matrices[1, 0].Cells[0, 0].Content = 2;
            this.Matrices[1, 0].Cells[1, 1].Content = 3;
            this.Matrices[1, 0].Cells[2, 1].Content = 8;
            //mid
            this.Matrices[1, 1].Cells[0, 2].Content = 4;
            this.Matrices[1, 1].Cells[1, 2].Content = 1;
            this.Matrices[1, 1].Cells[2, 1].Content = 7;
            this.Matrices[1, 1].Cells[2, 2].Content = 2;
            //right
            this.Matrices[1, 2].Cells[0, 0].Content = 8;
            this.Matrices[1, 2].Cells[1, 0].Content = 2;
            this.Matrices[1, 2].Cells[1, 1].Content = 6;
            this.Matrices[1, 2].Cells[2, 0].Content = 9;
            this.Matrices[1, 2].Cells[2, 1].Content = 4;
            this.Matrices[1, 2].Cells[2, 2].Content = 1;
            //bottom row
            //left
            this.Matrices[2, 0].Cells[0, 1].Content = 5;
            this.Matrices[2, 0].Cells[1, 1].Content = 2;
            this.Matrices[2, 0].Cells[1, 2].Content = 8;
            this.Matrices[2, 0].Cells[2, 1].Content = 7;
            this.Matrices[2, 0].Cells[2, 2].Content = 9;
            //mid
            this.Matrices[2, 1].Cells[0, 0].Content = 2;
            this.Matrices[2, 1].Cells[0, 2].Content = 6;
            this.Matrices[2, 1].Cells[1, 2].Content = 9;
            this.Matrices[2, 1].Cells[2, 0].Content = 1;
            this.Matrices[2, 1].Cells[2, 2].Content = 8;
            //right

            this.Matrices[2, 2].Cells[1, 0].Content = 4;
            this.Matrices[2, 2].Cells[1, 1].Content = 1;
            this.Matrices[2, 2].Cells[2, 0].Content = 5;
            this.Matrices[2, 2].Cells[2, 1].Content = 3;
        }

        public void SetExampleEasy()
        {
            //top row
            //left
            this.Matrices[0, 0].Cells[1, 0].Content = 6;
            this.Matrices[0, 0].Cells[1, 1].Content = 8;
            this.Matrices[0, 0].Cells[2, 0].Content = 1;
            this.Matrices[0, 0].Cells[2, 1].Content = 9;
            //mid
            this.Matrices[0, 1].Cells[0, 0].Content = 2;
            this.Matrices[0, 1].Cells[0, 1].Content = 6;
            this.Matrices[0, 1].Cells[1, 1].Content = 7;
            this.Matrices[0, 1].Cells[2, 2].Content = 4;
            //right
            this.Matrices[0, 2].Cells[0, 0].Content = 7;
            this.Matrices[0, 2].Cells[0, 2].Content = 1;
            this.Matrices[0, 2].Cells[1, 1].Content = 9;
            this.Matrices[0, 2].Cells[2, 0].Content = 5;
           
            //mid row
            //left
            this.Matrices[1, 0].Cells[0, 0].Content = 8;
            this.Matrices[1, 0].Cells[0, 1].Content = 2;
            this.Matrices[1, 0].Cells[1, 2].Content = 4;
            this.Matrices[1, 0].Cells[2, 1].Content = 5;
            //mid
            this.Matrices[1, 1].Cells[0, 0].Content = 1;
            this.Matrices[1, 1].Cells[1, 0].Content = 6;
            this.Matrices[1, 1].Cells[1, 2].Content = 2;
            this.Matrices[1, 1].Cells[2, 2].Content = 3;
            //right
            this.Matrices[1, 2].Cells[0, 1].Content = 4;
            this.Matrices[1, 2].Cells[1, 0].Content = 9;
            this.Matrices[1, 2].Cells[2, 1].Content = 2;
            this.Matrices[1, 2].Cells[2, 2].Content = 8;
         
            //bottom row
            //left
            this.Matrices[2, 0].Cells[0, 2].Content = 9;
            this.Matrices[2, 0].Cells[1, 1].Content = 4;
            this.Matrices[2, 0].Cells[2, 0].Content = 7;
            this.Matrices[2, 0].Cells[2, 2].Content = 3;
            //mid
            this.Matrices[2, 1].Cells[0, 0].Content = 3;
            
            this.Matrices[2, 1].Cells[1, 1].Content = 5;
            this.Matrices[2, 1].Cells[2, 1].Content = 1;
            this.Matrices[2, 1].Cells[2, 2].Content = 8;
            //right

            this.Matrices[2, 2].Cells[0, 1].Content = 7;
            this.Matrices[2, 2].Cells[0, 2].Content = 4;
            this.Matrices[2, 2].Cells[1, 1].Content = 3;
            this.Matrices[2, 2].Cells[1, 2].Content = 6;
        }
        public void SetExampleMedium()
        {
            //top row
            //left
      
            this.Matrices[0, 0].Cells[1, 1].Content = 2;
            this.Matrices[0, 0].Cells[1, 2].Content = 1;
            this.Matrices[0, 0].Cells[2, 0].Content = 3;
            //mid
            this.Matrices[0, 1].Cells[0, 2].Content = 6;
            this.Matrices[0, 1].Cells[1, 1].Content = 7;
            this.Matrices[0, 1].Cells[2, 1].Content = 8;
            
            //right
            this.Matrices[0, 2].Cells[0, 0].Content = 5;

            //mid row
            //left
            this.Matrices[1, 0].Cells[0, 0].Content = 4;
            this.Matrices[1, 0].Cells[1, 1].Content = 5;
            this.Matrices[1, 0].Cells[1, 2].Content = 2;
            this.Matrices[1, 0].Cells[2, 2].Content = 3;
            //mid
            this.Matrices[1, 1].Cells[0, 2].Content = 9;
            this.Matrices[1, 1].Cells[2, 0].Content = 1;
         
            //right
            this.Matrices[1, 2].Cells[0, 0].Content = 6;
            this.Matrices[1, 2].Cells[1, 0].Content = 7;
            this.Matrices[1, 2].Cells[1, 1].Content = 9;
            this.Matrices[1, 2].Cells[2, 2].Content = 8;

            //bottom row
            //left
            this.Matrices[2, 0].Cells[2, 2].Content = 4;
            //mid
            this.Matrices[2, 1].Cells[0, 1].Content = 4;
            this.Matrices[2, 1].Cells[1, 1].Content = 5;
            this.Matrices[2, 1].Cells[2, 0].Content = 2;
            //right

            this.Matrices[2, 2].Cells[0, 2].Content = 5;
            this.Matrices[2, 2].Cells[1, 0].Content = 8;
            this.Matrices[2, 2].Cells[1, 1].Content = 7;
 
        }
        public void SetExampleHard()
        {
            //top row
            //left
            
            this.Matrices[0, 0].Cells[0, 1].Content = 2;
            this.Matrices[0, 0].Cells[2, 1].Content = 7;
            this.Matrices[0, 0].Cells[2, 2].Content = 4;
            //mid
            
            this.Matrices[0, 1].Cells[1, 0].Content = 6;
            this.Matrices[0, 1].Cells[2, 1].Content = 8;
            //right
            this.Matrices[0, 2].Cells[1, 2].Content = 3;
            //mid row
            //left
           
            this.Matrices[1, 0].Cells[1, 1].Content = 8;
            this.Matrices[1, 0].Cells[2, 0].Content = 6;
            //mid
            this.Matrices[1, 1].Cells[0, 2].Content = 3;
            this.Matrices[1, 1].Cells[1, 1].Content = 4;
            this.Matrices[1, 1].Cells[2, 0].Content = 5;
            
            //right
            
            this.Matrices[1, 2].Cells[0, 2].Content = 2;
            this.Matrices[1, 2].Cells[1, 1].Content = 1;
            
            //bottom row
            //left
            this.Matrices[2, 0].Cells[1, 0].Content = 5;

            //mid
            this.Matrices[2, 1].Cells[0, 1].Content = 1;
            
            this.Matrices[2, 1].Cells[1, 2].Content = 9;
        
            //right

            this.Matrices[2, 2].Cells[0, 0].Content = 7;
            this.Matrices[2, 2].Cells[0, 1].Content = 8;
            
            this.Matrices[2, 2].Cells[2, 1].Content = 4;
        }
    }
}
