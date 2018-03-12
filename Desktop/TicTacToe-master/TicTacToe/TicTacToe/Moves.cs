using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public class Move
    {
        private int x_;
        public int x
        {
            get { return x_;}
            set { x_ = value;}

        }
        private int y_;
        public int y
        {
            get { return y_; }
            set { y_ = value;}
        }
        public Move(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
