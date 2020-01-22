using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
     public interface GameEngineInterface
        {
            Board getBoard();
            bool PlaceTile(int x, int y);
        }
    
}
