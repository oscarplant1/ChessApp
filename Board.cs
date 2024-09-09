using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Board
    {
        //Attributes
        private int MoveCounter = 0;
        private bool isFlipped = false;
        private int[][] PreviousMove = [[8,8], [8,8]];
        private int[] PieceToMove = [8, 8]; //Use [8,8] as a null variable as grid doesn't go past [7,7]
        private int[] WhiteKingPosition = [7, 4];
        private int[] BlackKingPosition = [0, 4];
        private int[][] knightMoves = [];
        public Piece[,] Grid = new Piece [8, 8];
        private Piece BlankPiece = new Piece(false, 'X');

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

        public int[] GetPieceToMove()
        {
            return PieceToMove;
        }

        //Get Methods for Piece objects at chosen square
        public char GetPieceTypeAt(int X, int Y)
        {
            return Grid[X, Y].GetPieceType();
        }

        public bool GetIsWhiteAt(int X, int Y)
        {
            return Grid[X, Y].GetIsWhite();
        }

        //Set Methods
        public void SetMoveCounter(int Moves)
        {
            MoveCounter = Moves;
        }

        public void SetPieceTypeAt(int X, int Y, char PieceType)
        {
            Grid[X,Y].SetPieceType(PieceType);
        }

        public void SetWhiteKingPosition(int[] WhiteKing)
        {
            WhiteKingPosition = WhiteKing;
        }

        public void SetBlackKingPosition(int[] BlackKing)
        {
            BlackKingPosition = BlackKing;
        }

        public void SetPieceToMove(int[] Piece)
        {
            PieceToMove = Piece;
        }

        //Methods

        public bool IsSquareBlank(int X, int Y)
        {
            if (Grid[X, Y].GetPieceType() == 'X')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Sets board with Pieces in starting positions
        public void SetBoard()
        {
            MoveCounter = 0;
            PieceToMove = [8, 8];

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
                Grid[1, i].SetIsWhite(isFlipped);

                //White Pawns
                Grid[6, i].SetPieceType('P');
                Grid[6, i].SetIsWhite(!isFlipped);
            }

            //Black Rooks
            Grid[0, 0].SetPieceType('R');
            Grid[0, 7].SetPieceType('R');
            Grid[0, 0].SetIsWhite(isFlipped);
            Grid[0, 7].SetIsWhite(isFlipped);

            //White Rooks
            Grid[7, 0].SetPieceType('R');
            Grid[7, 7].SetPieceType('R');
            Grid[7, 0].SetIsWhite(!isFlipped);
            Grid[7, 7].SetIsWhite(!isFlipped);

            //Black Knights
            Grid[0, 1].SetPieceType('N');
            Grid[0, 6].SetPieceType('N');
            Grid[0, 1].SetIsWhite(isFlipped);
            Grid[0, 6].SetIsWhite(isFlipped);

            //White Knights
            Grid[7, 1].SetPieceType('N');
            Grid[7, 6].SetPieceType('N');
            Grid[7, 1].SetIsWhite(!isFlipped);
            Grid[7, 6].SetIsWhite(!isFlipped);

            //Black Bishops
            Grid[0, 2].SetPieceType('B');
            Grid[0, 5].SetPieceType('B');
            Grid[0, 2].SetIsWhite(isFlipped);
            Grid[0, 5].SetIsWhite(isFlipped);

            //White Bishops
            Grid[7, 2].SetPieceType('B');
            Grid[7, 5].SetPieceType('B');
            Grid[7, 2].SetIsWhite(!isFlipped);
            Grid[7, 5].SetIsWhite(!isFlipped);

            //Black Queen
            Grid[0, 3].SetPieceType('Q');
            Grid[0, 3].SetIsWhite(isFlipped);

            //White Queen
            Grid[7, 3].SetPieceType('Q');
            Grid[7, 3].SetIsWhite(!isFlipped);

            //Black King
            Grid[0, 4].SetPieceType('K');
            Grid[0, 4].SetIsWhite(isFlipped);

            //White King
            Grid[7, 4].SetPieceType('K');
            Grid[7,4].SetIsWhite(!isFlipped);
        }

        //Outputs current board state to console - Debugging only
        public void TestGrid()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Console.Write(Grid[i, j].GetPieceType() + " ");
                    Console.Write(Grid[i, j].GetIsWhite() + ", ");
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------------------");
        }

        public void MovePiece(int[] Piece, int[] Destination)
        {
            int multiplier;
            bool isWhitesMove;

            if (MoveCounter % 2 == 0)
            {
                isWhitesMove = true;
            }
            else
            {
                isWhitesMove = false;
            }

            if (isFlipped != isWhitesMove) 
            {
                multiplier = 1;
            }
            else 
            {
                multiplier = -1;
            }

            Piece SelectedPiece = Grid[Piece[0], Piece[1]];
            Piece TargetSquare = Grid[Destination[0], Destination[1]];
            
            char CurrentPieceType = SelectedPiece.GetPieceType();
            
            if(MoveCounter % 2 == 0 & !SelectedPiece.GetIsWhite() || MoveCounter % 2 == 1 & SelectedPiece.GetIsWhite())
            {
                PieceToMove = [8, 8];
                return;
            }

            if (SelectedPiece.GetIsWhite() != TargetSquare.GetIsWhite() || TargetSquare.GetPieceType() == 'X')
            {
                switch (CurrentPieceType)
                {
                    //Pawn rules
                    case 'P':
                        if (Piece[1] == Destination[1])
                        {
                            if (SelectedPiece.GetMovesDone() == 0)
                            {
                                if (Destination[0] - Piece[0] == (-1 * multiplier) || Destination[0] - Piece[0] == (-2 * multiplier))
                                {
                                    if (Destination[0] - Piece[0] == (-1 * multiplier))
                                    {
                                        if (TargetSquare.GetPieceType() == 'X')
                                        {
                                            MovePieceObject(Piece, Destination);
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (CheckBetween(Piece, Destination) & TargetSquare.GetPieceType() == 'X')
                                        {
                                            MovePieceObject(Piece, Destination);
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Destination[0] - Piece[0] == (-1 * multiplier) & TargetSquare.GetPieceType() == 'X')
                                {
                                    MovePieceObject(Piece, Destination);
                                }
                            }
                        }
                        else
                        {
                            if (Piece[0] == Destination[0] + (multiplier * 1) & (Piece[1] == Destination[1] + 1 || Piece[1] == Destination[1] - 1) & TargetSquare.GetPieceType() != 'X')
                            {
                                MovePieceObject(Piece, Destination);
                            }
                            else
                            {
                                if (Grid[PreviousMove[1][0], PreviousMove[1][1]].GetPieceType() == 'P' & Math.Abs(PreviousMove[0][0] - PreviousMove[1][0]) == 2 & Math.Abs(PreviousMove[1][1] - Piece[1]) == 1 & Destination[1] == PreviousMove[1][1] & Destination[0] - PreviousMove[1][0] == -1 * multiplier)
                                {
                                    Grid[PreviousMove[1][0], PreviousMove[1][1]] = BlankPiece;
                                    MovePieceObject(Piece, Destination);
                                }
                                else
                                {
                                    PieceToMove = [8, 8];
                                    return;
                                }
                            }  
                        }
                        break;
                    //Rook rules
                    case 'R':
                        if (Piece[0] == Destination[0] || Piece[1] == Destination[1])
                        {
                            if (CheckBetween(Piece, Destination))
                            { 
                                MovePieceObject(Piece, Destination);
                            }
                            else
                            {
                                PieceToMove = [8, 8];
                                return;
                            }
                        }
                        else
                        {
                            PieceToMove = [8, 8];
                            return;
                        }
                        break;
                    case 'N':
                        knightMoves = [[Piece[0]-2,Piece[1]-1], [Piece[0]-2, Piece[1]+1], [Piece[0]-1, Piece[1]+2], [Piece[0]+1, Piece[1]+2], [Piece[0]+2, Piece[1]+1], [Piece[0]+2, Piece[1]-1], [Piece[0]+1, Piece[1]-2], [Piece[0]-1, Piece[1]-2]];
                        bool found = false;
                        for (int i = 0; i < knightMoves.Length; i++)
                        {
                            if (knightMoves[i][0] == Destination[0] & knightMoves[i][1] == Destination[1])
                            {
                                MovePieceObject(Piece, Destination);
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            PieceToMove = [8, 8];
                            return;
                        }
                        break;
                    case 'B':
                        if (Math.Abs(Piece[0] - Destination[0]) == Math.Abs(Piece[1] - Destination[1]))
                        {
                            if (CheckBetween(Piece, Destination))
                            {
                                MovePieceObject(Piece, Destination);
                            }
                            else
                            {
                                PieceToMove = [8, 8];
                                return;
                            }
                        }
                        else
                        {
                            PieceToMove = [8, 8];
                            return;
                        }
                        break;
                    case 'Q':
                        if (Math.Abs(Piece[0] - Destination[0]) == Math.Abs(Piece[1] - Destination[1])|| Piece[0] == Destination[0] || Piece[1] == Destination[1])
                        {
                            if (CheckBetween(Piece, Destination))
                            {
                                MovePieceObject(Piece, Destination);
                            }
                            else
                            {
                                PieceToMove = [8, 8];
                                return;
                            }
                        }
                        else
                        {
                            PieceToMove = [8, 8];
                            return;
                        }
                        break;
                    case 'K':
                        if (Math.Abs(Destination[0] - Piece[0])<=1 & Math.Abs(Destination[1] - Piece[1])<=1)
                        {
                            MovePieceObject(Piece, Destination);
                        }
                        else
                        {
                            PieceToMove = [8, 8];
                            return;
                        }
                        break;
                }
            }
        }

        public void MovePieceObject(int[] Piece, int[] Destination)
        {
            Piece SelectedPiece = Grid[Piece[0], Piece[1]];
            Piece TargetSquare = Grid[Destination[0], Destination[1]];

            SelectedPiece.SetMovesDone(SelectedPiece.GetMovesDone() + 1);
            MoveCounter++;

            Grid[Piece[0], Piece[1]] = BlankPiece;
            Grid[Destination[0], Destination[1]] = SelectedPiece;

            PreviousMove = [Piece, Destination];
            PieceToMove = [8, 8];
        }

        //Returns false if there is a piece between two given points
        public bool CheckBetween(int[] Piece, int[] Destination)
        {
            int PieceX = Piece[0];
            int PieceY = Piece[1];

            int DestinationX = Destination[0];
            int DestinationY = Destination[1];

            //Horizontal move
            if(DestinationX == PieceX)
            {
                int start_point = Math.Min(PieceY, DestinationY);

                for(int i= 1; i< Math.Abs(DestinationY - PieceY); i++)
                {
                    if (Grid[PieceX,start_point + i].GetPieceType()!= 'X')
                    {
                        return false;
                    }
                }
                return true;
            }
            //Vertical move
            else if(DestinationY == PieceY)
            {
                int start_point = Math.Min(PieceX, DestinationX);

                for (int i = 1; i < Math.Abs(DestinationX - PieceX); i++)
                {
                    if (Grid[start_point + i, PieceY].GetPieceType() != 'X')
                    {
                        return false;
                    }
                }
                return true;
            }
            //Diagonal move
            else if (Math.Abs(DestinationX - PieceX) == Math.Abs(DestinationY - PieceY)) 
            {
                int start_pointY = Math.Min(PieceY, DestinationY);
                int end_pointY = Math.Max(PieceY, DestinationY);

                int start_pointX;
                int end_pointX;

                if(start_pointY == PieceY)
                {
                    start_pointX = PieceX;
                    end_pointX = DestinationX;    
                }
                else
                {
                    start_pointX = DestinationX;
                    end_pointX = PieceX;
                }

                bool startAbove;

                if(start_pointX < end_pointX)
                {
                    startAbove = true;
                }
                else
                {
                    startAbove = false;
                }

                for (int i = 1; i < Math.Abs(DestinationX - PieceX); i++)
                {
                    if (startAbove)
                    {
                        if (Grid[start_pointX + i, start_pointY + i].GetPieceType() != 'X')
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (Grid[start_pointX - i, start_pointY + i].GetPieceType() != 'X')
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            //Not in line
            else
            {
                return false;
            }
        }

        public void FlipBoard()
        {
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0;j < 8; j++)
                {
                    SwapPieces([i, j], [(7-i),(7-j)]);
                }
            }
            isFlipped = !isFlipped;
        }
        public void SwapPieces(int[] Piece1, int[] Piece2)
        {
            Piece temp = Grid[Piece1[0],Piece1[1]];
            Grid[Piece1[0],Piece1[1]] = Grid[Piece2[0],Piece2[1]];
            Grid[Piece2[0], Piece2[1]] = temp;
        }

    }
}
