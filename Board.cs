using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Board
    {
        //Attributes
        private int MoveCounter = 0;
        private int[] WhiteKingPosition = [7, 4];
        private int[] BlackKingPosition = [0, 4];
        public Piece[,] Grid = new Piece [8, 8];

        //Get Methods
        public int GetMoveCounter()
        {
            return MoveCounter;
        }

        public int[] GetWhiteKingPosition()
        {
            return WhiteKingPosition;
        }

        public int[] GetBlackKingPosition()
        {
            return BlackKingPosition;
        }

        //Set Positions
        public void SetMoveCounter(int Moves)
        {
            MoveCounter = Moves;
        }

        public void SetWhiteKingPosition(int[] WhiteKing)
        {
            WhiteKingPosition = WhiteKing;
        }

        public void SetBlackPosition(int[] BlackKing)
        {
            BlackKingPosition = BlackKing;
        }

        //Methods
        public void SetBoard()
        {
            //Set all pieces to empty black pieces
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Grid[i, j] = new Piece(false, 'X');
                }
            }

            //Pawns
            for (int i = 0;i < Grid.GetLength(0); i++)
            {
                //Black Pawns
                Grid[1, i].SetPieceType('P');

                //White Pawns
                Grid[6, i].SetPieceType('P');
                Grid[6, i].SetIsWhite(true);
            }

            //Black Rooks
            Grid[0, 0].SetPieceType('R');
            Grid[0, 7].SetPieceType('R');

            //White Rooks
            Grid[7, 0].SetPieceType('R');
            Grid[7, 7].SetPieceType('R');
            Grid[7, 0].SetIsWhite(true);
            Grid[7, 7].SetIsWhite(true);

            //Black Knights
            Grid[0, 1].SetPieceType('N');
            Grid[0, 6].SetPieceType('N');

            //White Knights
            Grid[7, 1].SetPieceType('N');
            Grid[7, 6].SetPieceType('N');
            Grid[7, 1].SetIsWhite(true);
            Grid[7, 6].SetIsWhite(true);

            //Black Bishops
            Grid[0, 2].SetPieceType('B');
            Grid[0, 5].SetPieceType('B');

            //White Bishops
            Grid[7, 2].SetPieceType('B');
            Grid[7, 5].SetPieceType('B');
            Grid[7, 2].SetIsWhite(true);
            Grid[7, 5].SetIsWhite(true);

            //Black Queen
            Grid[0, 3].SetPieceType('Q');

            //White Queen
            Grid[7, 3].SetPieceType('Q');
            Grid[7, 3].SetIsWhite(true);

            //Black King
            Grid[0, 4].SetPieceType('K');

            //White King
            Grid[7, 4].SetPieceType('K');
            Grid[7,4].SetIsWhite(true);
        }
        public void TestGrid()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                //Console.Write("Row " + i + ": ");

                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Console.Write(Grid[i, j].GetPieceType() + " ");
                    Console.Write(Grid[i, j].GetIsWhite() + ", ");

                }
                Console.WriteLine();
            }
        }


    }
}
