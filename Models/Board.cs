using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
   public  class Board
    {
        public Tile[,] matrix = new Tile[8, 8];

        public Board()
        {
            ResetBoard();
        }

        public void ResetBoard()
        {
            matrix = new Tile[8, 8];
            matrix[3, 3] = new Tile(true);
            matrix[4, 3] = new Tile(false);
            matrix[3, 4] = new Tile(false);
            matrix[4, 4] = new Tile(true);
        }
    }
}
