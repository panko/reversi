using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public interface RuleEngineInterface
    {
        List<int[]> PossibleMoves(Board board, Tile tile);
        bool CanPlaceTile(Board board, Tile tile, int x, int y);
        bool PosOutOfBounds(int x, int y);
        bool HasImmediateOpponentTile(Board board, Tile tile, int x, int y);
    }
}
