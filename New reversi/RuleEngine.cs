using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    class RuleEngine : RuleEngineInterface
    {
        public int[][] RELATIVEPOSITION = new int[][]
        {
            new int[] { -1, -1 },
            new int[] { 0, -1 },
            new int[] { 1, -1 },
            new int[] { 1,0 },
            new int[] { 1, 1 },
            new int[] { 0, 1 },
            new int[] { -1, 1 },
            new int[] { -1, 0 }
        };

        public List<int[]> PossibleMoves(Board board, Tile tile)
        {
            List<int[]> possibleMoves = new List<int[]>();

            for (int y = 0; y < 8; ++y)
            {
                for (int x = 0; x < 8; ++x)
                {
                    if (HasImmediateOpponentTile(board, tile, x, y) && board.matrix[x, y] == null)
                    {
                        possibleMoves.Add(new int[2] { x, y });
                    }
                }
            }

            return possibleMoves;
        }

        public bool CanPlaceTile(Board board, Tile tile, int x, int y)
        {
            return (board.matrix[x, y] == null && HasImmediateOpponentTile(board, tile, x, y) && !PosOutOfBounds(x, y));
        }

        public bool PosOutOfBounds(int x, int y)
        {
            return (x < 0 || x > 7 || y < 0 || y > 7);
        }

        public bool HasImmediateOpponentTile(Board board, Tile tile, int x, int y)
        {
            foreach (int[] pos in RELATIVEPOSITION)
            {
                int immediateX = x + pos[0];
                int immediateY = y + pos[1];

                if (!PosOutOfBounds(immediateX, immediateY))
                {
                    if (board.matrix[immediateX, immediateY] is Tile && board.matrix[immediateX, immediateY].isBlack != tile.isBlack)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
