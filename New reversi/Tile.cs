using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    
    public class Tile
    {
        /// <summary>
        /// isBlack = true => Black player is playing
        /// !isBlack => white player is playing
        /// </summary>
        public bool isBlack;

        /// <summary>
        /// Konstruktor for the class.
        /// </summary>
        /// <param name="black"></param>
        public Tile(bool black)
        {
            isBlack = black;
        }
        
        /// <summary>
        /// For flipping the tiles between two enemy tiles.
        /// </summary>
        public void Flip()
        {
            isBlack = !isBlack;
        }
    }
}
