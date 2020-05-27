using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    public class Cell
    {
        private Int16 _content;
        public Int16 Content {
            get
            { return _content; }
            set
            { _content = value;
                Filled = true;
                for(int i=1;i<10;i++)
                {
                    if (i != value)
                        this.CantBe(i);
                }
            }
        }
        public bool Filled { get; set; }
        //public bool[,] CanBe { get; set; }
        public short[] CouldBe {get; set;}

        public bool Initialized { get; set; }

        public Cell()
        {
            //this.Content = 0;
            /*
            CanBe = new bool[3, 3]
            {
                {true,true,true },
                {true, true, true },
                {true, true, true }
            };
            */
            //CouldBe = new bool[]{ true, true, true, true, true, true, true, true, true };
            CouldBe = new short[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        public void CantBe(int val)
        {
            CouldBe[val - 1] = 0;
            /*
            switch (val)
            {
                case 1:
                    CanBe[0, 0] = false;
                    break;
                case 2:
                    CanBe[0, 1] = false;
                    break;
                case 3:
                    CanBe[0, 2] = false;
                    break;
                case 4:
                    CanBe[1, 0] = false;
                    break;
                case 5:
                    CanBe[1, 1] = false;
                    break;
                case 6:
                    CanBe[1, 2] = false;
                    break;
                case 7:
                    CanBe[2, 0] = false;
                    break;
                case 8:
                    CanBe[2, 1] = false;
                    break;
                case 9:
                    CanBe[2, 2] = false;
                    break;
            }
            */
        }
    }
}
