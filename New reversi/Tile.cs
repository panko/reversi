using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    
    public class Tile
    {
        public bool isBlack;
        public Tile(bool black)
        {
            isBlack = black;
        }
        public Tile()
        {
            isBlack = true;
        }

        public void Flip()
        {
            isBlack = !isBlack;
        }
    }
}
