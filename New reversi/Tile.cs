using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public enum Tile_type
    {
        White,
        Black,
        Empty
    }

    public class Tile
    {
        public bool isBlack;
        public Tile_type tile;

        public Tile(bool black, Tile_type tile)
        {
            isBlack = black;
            this.tile = tile;
        }
        public Tile()
        {
            isBlack = true;
            this.tile = Tile_type.Empty;
        }

        public void Flip()
        {
            isBlack = !isBlack;
        }
    }
}
