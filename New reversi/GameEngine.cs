using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    class GameEngine : GameEngineInterface
    {

        RuleEngine ruleBook;
        Board board;

        public bool is_against_AI = false;
        public bool blackTurn = true;
        public int blackScore;
        public int whiteScore;
        //OthelloLinqLayer.OthelloSaver a = new OthelloLinqLayer.OthelloSaver();

        public GameEngine()
        {
            ruleBook = new RuleEngine();
            board = new Board();
           // board = a.getBoard("test.xml");
            //blackTurn = a.getTurn("test.xml");

        }

        public void resetGame()
        {
            board.ResetBoard();
            calcScore();
        }

        public bool hasPossibleMove(bool black)
        {
            return ruleBook.PossibleMoves(board, new Tile(black)).Count > 0;
        }


        public Board getBoard()
        {
            return board;
        }

        public Board PlaceTile(int x, int y)
        {
            Tile tile = new Tile(blackTurn);

            if (ruleBook.CanPlaceTile(board, tile, x, y))
            {
                // Place the new tile when we know it's a valid move.
                board.matrix[x, y] = tile;

                // Flip bool to indicate next player's turn.
                blackTurn = !blackTurn;

                // Flip tiles in all directions.
                foreach (int[] pos in ruleBook.RELATIVEPOSITION)
                {
                    int nextX = x + pos[0];
                    int nextY = y + pos[1];
                    List<Tile> tileList = new List<Tile>();
                    int i = 1;

                    while (!ruleBook.PosOutOfBounds(nextX, nextY) && board.matrix[nextX, nextY] != null && board.matrix[nextX, nextY].isBlack != tile.isBlack)
                    {
                        tileList.Add(board.matrix[nextX, nextY]);
                        ++i;
                        nextX = x + pos[0] * i;
                        nextY = y + pos[1] * i;
                    }

                    if (!ruleBook.PosOutOfBounds(nextX, nextY) && board.matrix[nextX, nextY] != null && board.matrix[nextX, nextY].isBlack == tile.isBlack)
                    {
                        foreach (Tile flip in tileList)
                        {
                            flip.Flip();
                        }
                    }
                }
            }
            calcScore();

           // a.save(board, blackTurn);
            // Return the currect board.
            return board;
        }

        private void calcScore()
        {
            blackScore = 0;
            whiteScore = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board.matrix[i, j] is Tile)
                    {
                        if (board.matrix[i, j].isBlack)
                        {
                            blackScore++;
                        }
                        else
                        {
                            whiteScore++;
                        }
                    }
                }
            }
        }

        private int[] calcScore_AI()
        {
            blackScore = 0;
            whiteScore = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board.matrix[i, j] is Tile)
                    {
                        if (board.matrix[i, j].isBlack)
                        {
                            blackScore++;
                        }
                        else
                        {
                            whiteScore++;
                        }
                    }
                }
            }
            return new int[2] { blackScore, whiteScore };
        }
    }
}
