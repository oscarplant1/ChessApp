using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

///<summary>
/// Main class containing a grid of piece objects and methods that act on that grid to move pieces and apply
/// the logic and rules of chess.
/// </summary>
///<remarks>
/// </remarks>

namespace Chess
{
    public class Board
    {
        //Attributes

        /// <summary>
        /// Increments everytime a move is made
        /// </summary>
        private int MoveCounter = 0;

        /// <summary>
        /// Is false if white is at the bottom and true if white is at the top
        /// </summary>
        private bool isFlipped = false;

        /// <summary>
        /// Everytime a move is made the two pairs of coordinates are added to this list
        ///	along with the value of isFlipped at the time of the move, and the move counter
        ///	at the time
        /// </summary>
        private List <int[][]> MovesDone = new List<int[][]>();

        /// <summary>
        /// Variable that stores the first button clicked, ie the selected piece
        /// </summary>
        private int[] PieceToMove = [8, 8];

        /// <summary>
        /// Tracks the white king
        /// </summary>
        private int[] WhiteKingPosition = [7, 4];

        /// <summary>
        /// Tracks the black king
        /// </summary>
        private int[] BlackKingPosition = [0, 4];

        /// <summary>
        /// The 8x8 grid filled with piece objects on which most methods act
        /// </summary>
        private Piece[,] Grid = new Piece [8, 8];

        /// <summary>
        /// Useful to have a blank piece to add where needed
        /// </summary>
        private Piece BlankPiece = new Piece(false, 'X');

        /// <summary>
        /// When a Piece is taken, it gets added to this list. Includes blank pieces in order
        ///	to remain in sync with MovesDoneMoveCounter
        /// </summary>
        private List<Piece> PiecesTaken = new List<Piece>();

        //Get Methods
        public int GetMoveCounter()
        {
            return MoveCounter;
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

        public void SetPieceTypeAt(int X, int Y, char PieceType)
        {
            Grid[X,Y].SetPieceType(PieceType);
        }

        public void SetPromotedPawnAt(int X, int Y, int Promoted)
        {
            Grid[X,Y].SetPromotedPawn(Promoted);
        }

        public void SetPieceToMove(int[] Piece)
        {
            PieceToMove = Piece;
        }

        //Methods



        /// <summary>
        /// Returns true if at the given coordinates in the grid, the piecetype is blank
        /// </summary>
        /// <param X coordinate ="X"></param>
        /// <param Y coordinate ="Y"></param>
        /// <returns>
        /// Bool depending on Grid attribute
        /// </returns>
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

        /// <summary>
        /// Populates the grid with pieces in the correct starting position
        /// </summary>
        public void SetBoard()
        {
            PiecesTaken = new List<Piece>();
            MovesDone = new List<int[][]>();

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

            if (isFlipped)
            {
                Grid[0, 4].SetPieceType('Q');
                Grid[0, 4].SetIsWhite(isFlipped);
            }
            else
            { 
                Grid[0, 3].SetPieceType('Q');
                Grid[0, 3].SetIsWhite(isFlipped);
            }
            //White Queen
            if (isFlipped)
            {
                Grid[7, 4].SetPieceType('Q');
                Grid[7, 4].SetIsWhite(!isFlipped);
            }
            else
            {
                Grid[7, 3].SetPieceType('Q');
                Grid[7, 3].SetIsWhite(!isFlipped);
            }
            //Black King
            if (isFlipped)
            {
                Grid[0, 3].SetPieceType('K');
                Grid[0, 3].SetIsWhite(isFlipped);
            }
            else
            {
                Grid[0, 4].SetPieceType('K');
                Grid[0, 4].SetIsWhite(isFlipped);
            }
            //White King
            if (isFlipped)
            {
                Grid[7, 3].SetPieceType('K');
                Grid[7, 3].SetIsWhite(!isFlipped);
            }
            else
            {
                Grid[7, 4].SetPieceType('K');
                Grid[7, 4].SetIsWhite(!isFlipped);
            }
        }

        /// <summary>
        /// Outputs the grid to console, for debugging only
        /// </summary>
        public void TestGrid()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Console.Write(Grid[i, j].GetPieceType() + " ");
                    //Console.Write(Grid[i, j].GetIsWhite() + ", ");
                    Console.Write(Grid[i, j].GetIsChecking()+ ", ");
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------------------");
        }

        /// <summary>
        /// Outputs the contents of MovesDone and PiecesTaken to console, for debugging only
        /// </summary>
        public void OutputLists()
        {
            
            Console.WriteLine("==========================================================");
            Console.WriteLine("==========================================================");
            Console.WriteLine("==========================================================");
            Console.WriteLine();

            for (int i = 0; i < MovesDone.Count(); i++)
            {
                int PieceX = MovesDone[i][0][0];
                int PieceY = MovesDone[i][0][1];

                int DestinationX = MovesDone[i][1][0];
                int DestinationY = MovesDone[i][1][1];

                int CurrentIsFlipped = MovesDone[i][2][0];
                int CurrentMove = MovesDone[i][2][1];

                char CurrentPieceTakenType = PiecesTaken[i].GetPieceType();

                Console.WriteLine(PieceX + "," + PieceY+ "   "+DestinationX+","+DestinationY);
                Console.WriteLine() ;
                Console.WriteLine(CurrentMove);
                Console.WriteLine();
                Console.WriteLine(CurrentIsFlipped);
                Console.WriteLine();
                Console.WriteLine(CurrentPieceTakenType + " " + PiecesTaken[i].GetIsWhite());
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine();
            

            Console.WriteLine();

            }
        }


        /// <summary>
        /// Calls the movePieceObject method after checking if the move is allowed
        /// </summary>
        /// <param Piece to attempt to move="Piece"></param>
        /// <param Square to attempt move to="Destination"></param>
        public void MovePiece(int[] Piece, int[] Destination)
        {
            int multiplier;
            bool isWhitesMove;

            //Define a multiplier that will change the direction for the current move
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
            
            //Only allows a piece to move if it is their turn
            if(MoveCounter % 2 == 0 & !SelectedPiece.GetIsWhite() || MoveCounter % 2 == 1 & SelectedPiece.GetIsWhite())
            {
                PieceToMove = [8, 8];
                return;
            }

            //Can't take a piece of the same colour
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
                                    //Move 1 square forward from start
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
                                        //Move two squares forward
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
                                //Move 1 square forward, not from start
                                if (Destination[0] - Piece[0] == (-1 * multiplier) & TargetSquare.GetPieceType() == 'X')
                                {
                                    MovePieceObject(Piece, Destination);
                                }
                            }
                        }
                        else
                        {
                            //Ordinary pawn taking
                            if (Piece[0] == Destination[0] + multiplier & (Piece[1] == Destination[1] + 1 || Piece[1] == Destination[1] - 1) & TargetSquare.GetPieceType() != 'X')
                            {
                                MovePieceObject(Piece, Destination);
                            }
                            else
                            {
                                //En passant
                                if (Grid[MovesDone.Last()[1][0], MovesDone.Last()[1][1]].GetPieceType() == 'P' & Math.Abs(MovesDone.Last()[0][0] - MovesDone.Last()[1][0]) == 2 & Math.Abs(MovesDone.Last()[1][1] - Piece[1]) == 1 & Destination[1] == MovesDone.Last()[1][1] & Destination[0] - MovesDone.Last()[1][0] == -1 * multiplier)
                                {
                                    MovePieceObject(Piece, [Destination[0]+multiplier, Destination[1]]);
                                    MoveCounter--;
                                    MovePieceObject([Destination[0]+multiplier, Destination[1]], Destination);
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
                    //Knight rules
                    case 'N':
                        int[][] knightMoves = [];
                        //Explicitily define the 8 possible moves a knight can make
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
                    //Bishop rules
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
                    //Queen rules
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
                    //King rules
                    case 'K':
                        //Ordinary king movement
                        if (Math.Abs(Destination[0] - Piece[0])<=1 & Math.Abs(Destination[1] - Piece[1])<=1)
                        {
                            MovePieceObject(Piece, Destination);

                            if (SelectedPiece.GetIsWhite())
                            {
                                WhiteKingPosition = Destination;
                            }
                            else
                            {
                                BlackKingPosition = Destination;
                            }
                        }
                        //Castle
                        else if (Math.Abs(Destination[1] - Piece[1]) == 2 & Destination[0] == Piece[0] & SelectedPiece.GetMovesDone()==0 & !WhiteinCheck() & !BlackinCheck())
                        {
                            bool Allowed;
                            //King Side - unflipped
                            if (Piece[1] - Destination[1] == -2 & Grid[Destination[0], 7].GetMovesDone() == 0 & Grid[Destination[0], 5].GetPieceType() == 'X' & !isFlipped)
                            {
                                MovePieceObject(Piece, [Destination[0], 5]);

                                if (Grid[Destination[0], 5].GetPieceType() == 'X')
                                {
                                    Allowed = false;
                                }
                                else
                                {
                                    Allowed = true;
                                    undoLastMove();
                                }

                                if (Allowed)
                                {
                                    MovePieceObject(Piece, Destination);


                                    if (Grid[Piece[0], Piece[1]].GetPieceType() != 'K')
                                    {
                                        MoveCounter--;
                                        MovePieceObject([Destination[0], 7], [Destination[0], 5]);

                                        if (SelectedPiece.GetIsWhite())
                                        {
                                            WhiteKingPosition = Destination;
                                        }
                                        else
                                        {
                                            BlackKingPosition = Destination;
                                        }
                                    }
                                }
                                else
                                {
                                    PieceToMove = [8, 8];
                                    return;
                                }
                            }
                            //Queen side - unflipped
                            else if (Piece[1] - Destination[1] == 2 & Grid[Destination[0], 0].GetMovesDone() == 0 & Grid[Destination[0], 1].GetPieceType() == 'X' && Grid[Destination[0], 3].GetPieceType() == 'X' & !isFlipped)
                            {
                                MovePieceObject(Piece, [Destination[0], 3]);

                                if (Grid[Destination[0], 3].GetPieceType() == 'X')
                                {
                                    Allowed = false;
                                }
                                else
                                {
                                    Allowed = true;
                                    undoLastMove();
                                }

                                if (Allowed)
                                {
                                    MovePieceObject(Piece, Destination);

                                    if (Grid[Piece[0], Piece[1]].GetPieceType() != 'K')
                                    {
                                        MoveCounter--;
                                        MovePieceObject([Destination[0], 0], [Destination[0], 3]);

                                        if (SelectedPiece.GetIsWhite())
                                        {
                                            WhiteKingPosition = Destination;
                                        }
                                        else
                                        {
                                            BlackKingPosition = Destination;
                                        }
                                    }
                                }
                                else
                                {
                                    PieceToMove = [8, 8];
                                    return;
                                }
                            }
                            //King side - flipped
                            else if (Piece[1] - Destination[1] == 2 & Grid[Destination[0], 0].GetMovesDone() == 0 & Grid[Destination[0], 2].GetPieceType() == 'X' & isFlipped)
                            {
                                MovePieceObject(Piece, [Destination[0], 2]);

                                if (Grid[Destination[0], 2].GetPieceType() == 'X')
                                {
                                    Allowed = false;
                                }
                                else
                                {
                                    Allowed = true;
                                    undoLastMove();
                                }

                                if (Allowed)
                                {
                                    MovePieceObject(Piece, Destination);

                                    if (Grid[Piece[0], Piece[1]].GetPieceType() != 'K')
                                    {
                                        MoveCounter--;
                                        MovePieceObject([Destination[0], 0], [Destination[0], 2]);

                                        if (SelectedPiece.GetIsWhite())
                                        {
                                            WhiteKingPosition = Destination;
                                        }
                                        else
                                        {
                                            BlackKingPosition = Destination;
                                        }
                                    }
                                }
                                else
                                {
                                    PieceToMove = [8, 8];
                                    return;
                                }
                            }
                            //Queen side - flipped
                            else if (Piece[1] - Destination[1] == -2 & Grid[Destination[0], 7].GetMovesDone() == 0 & Grid[Destination[0], 4].GetPieceType() == 'X' & Grid[Destination[0], 6].GetPieceType() == 'X' & isFlipped)
                            {
                                MovePieceObject(Piece, [Destination[0], 4]);

                                if (Grid[Destination[0], 4].GetPieceType() == 'X')
                                {
                                    Allowed = false;
                                }
                                else
                                {
                                    Allowed = true;
                                    undoLastMove();
                                }


                                if (Allowed)
                                {
                                    MovePieceObject(Piece, Destination);

                                    if (Grid[Piece[0], Piece[1]].GetPieceType() != 'K')
                                    {
                                        MoveCounter--;
                                        MovePieceObject([Destination[0], 7], [Destination[0], 4]);

                                        if (SelectedPiece.GetIsWhite())
                                        {
                                            WhiteKingPosition = Destination;
                                        }
                                        else
                                        {
                                            BlackKingPosition = Destination;
                                        }
                                    }
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

        /// <summary>
        /// Moves the Piece in the grid from the first pair of coordinates to the second pair of coordinates
        /// </summary>
        /// <param Square of piece to move="Piece"></param>
        /// <param Square to move piece to="Destination"></param>
        public void MovePieceObject(int[] Piece, int[] Destination)
        {
            Piece SelectedPiece = Grid[Piece[0], Piece[1]];
            Piece TargetSquare = Grid[Destination[0], Destination[1]];

            //Increment move counter for both piece and board
            SelectedPiece.SetMovesDone(SelectedPiece.GetMovesDone() + 1);
            MoveCounter++;

            TargetSquare.SetIsWhite(!SelectedPiece.GetIsWhite());
            PiecesTaken.Add(TargetSquare);

            //Moving piece object in grid
            Grid[Piece[0], Piece[1]] = BlankPiece;
            Grid[Destination[0], Destination[1]] = SelectedPiece;

            //Storing the MoveCounter and the if the board is flipped along with the coordinates of the move
            if (isFlipped)
            {
                MovesDone.Add([Piece, Destination, [1,MoveCounter]]);
            }
            else
            {
                MovesDone.Add([Piece, Destination, [-1,MoveCounter]]);
            }
            PieceToMove = [8, 8];

            //Undo the move if in check
            updateIsChecking();

            if (MoveCounter % 2 == 1 & WhiteinCheck())
            {
                undoLastMove();
            }
            if (MoveCounter % 2 == 0 & BlackinCheck())
            {
                undoLastMove();
            }
        }





        //Check and Checkmate logic

        /// <summary>
        /// Scans the whole grid array and if a non-blank piece is attacking the king of
        /// opposite colour, if so the isChecking attribute of the piece is set to true
        /// </summary>
        public void updateIsChecking()
        {
            Piece PieceToUpdate;

            int multiplier;

            if (isFlipped)
            {
                multiplier = -1;
            }
            else
            {
                multiplier = 1;
            }

            //Get the positions of the kings
            WhiteKingPosition = findWhiteKing();
            BlackKingPosition = findBlackKing();

            for (int X = 0; X < 8; X++)
            {
                for (int Y = 0; Y < 8; Y++)
                {
                    PieceToUpdate = Grid[X, Y];

                    //The current piece is white
                    if (PieceToUpdate.GetIsWhite()&PieceToUpdate.GetPieceType()!='X')
                    {
                        //Checking if the current piece is attacking the black king is white depending on piece type
                        switch (PieceToUpdate.GetPieceType())
                        {
                            case 'P':
                                if (X == BlackKingPosition[0] + multiplier & (Y == BlackKingPosition[1] + 1 || Y == BlackKingPosition[1] - 1))
                                {
                                    Grid[X, Y].SetIsChecking(true);
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'R':
                                if (X == BlackKingPosition[0] || Y == BlackKingPosition[1])
                                {
                                    if (CheckBetween([X,Y],BlackKingPosition))
                                    {
                                        Grid[X, Y].SetIsChecking(true);
                                    }
                                    else
                                    {
                                        Grid[X, Y].SetIsChecking(false);
                                    }
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'N':
                                int Xdifference = Math.Abs(X-BlackKingPosition[0]);
                                int Ydifference = Math.Abs(Y-BlackKingPosition[1]);

                                if((Xdifference == 1 & Ydifference == 2)|| (Xdifference == 2 & Ydifference == 1))
                                {
                                    Grid[X, Y].SetIsChecking(true);
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'B':
                                if (X - BlackKingPosition[0] == Y - BlackKingPosition[1]|| X - BlackKingPosition[0] == BlackKingPosition[1] - Y)
                                {
                                    if (CheckBetween([X, Y], BlackKingPosition))
                                    {
                                        Grid[X, Y].SetIsChecking(true);
                                    }
                                    else
                                    {
                                        Grid[X, Y].SetIsChecking(false);
                                    }
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'Q':
                                if (X == BlackKingPosition[0] || Y == BlackKingPosition[1]|| X - BlackKingPosition[0] == Y - BlackKingPosition[1] || X - BlackKingPosition[0] == BlackKingPosition[1] - Y)
                                {
                                    if (CheckBetween([X, Y], BlackKingPosition))
                                    {
                                        Grid[X, Y].SetIsChecking(true);
                                    }
                                    else
                                    {
                                        Grid[X, Y].SetIsChecking(false);
                                    }
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'K':
                                if (Math.Abs(X - BlackKingPosition[0]) <= 1 & Math.Abs(Y - BlackKingPosition[1]) <= 1)
                                {
                                    Grid[X, Y].SetIsChecking(true);
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                        }
                    }
                    //The current piece is black
                    else if (PieceToUpdate.GetPieceType()!='X')
                    {
                        //Checking if the current piece is attacking the white king is white depending on piece type
                        switch (PieceToUpdate.GetPieceType())
                        {
                            case 'P':
                                if (X == WhiteKingPosition[0] - multiplier & (Y == WhiteKingPosition[1] + 1 || Y == WhiteKingPosition[1] - 1))
                                {
                                    Grid[X, Y].SetIsChecking(true);
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'R':
                                if (X == WhiteKingPosition[0] || Y == WhiteKingPosition[1])
                                {
                                    if (CheckBetween([X, Y], WhiteKingPosition))
                                    {
                                        Grid[X, Y].SetIsChecking(true);
                                    }
                                    else
                                    {
                                        Grid[X, Y].SetIsChecking(false);
                                    }
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'N':
                                int Xdifference = Math.Abs(X - WhiteKingPosition[0]);
                                int Ydifference = Math.Abs(Y - WhiteKingPosition[1]);

                                if ((Xdifference == 1 & Ydifference == 2) || (Xdifference == 2 & Ydifference == 1))
                                {
                                    Grid[X, Y].SetIsChecking(true);
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'B':
                                if (X - WhiteKingPosition[0] == Y - WhiteKingPosition[1] || X - WhiteKingPosition[0] == WhiteKingPosition[1] - Y)
                                {
                                    if (CheckBetween([X, Y], WhiteKingPosition))
                                    {
                                        Grid[X, Y].SetIsChecking(true);
                                    }
                                    else
                                    {
                                        Grid[X, Y].SetIsChecking(false);
                                    }
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'Q':
                                if (X == WhiteKingPosition[0] || Y == WhiteKingPosition[1] || X - WhiteKingPosition[0] == Y - WhiteKingPosition[1] || X - WhiteKingPosition[0] == WhiteKingPosition[1] - Y)
                                {
                                    if (CheckBetween([X, Y], WhiteKingPosition))
                                    {
                                        Grid[X, Y].SetIsChecking(true);
                                    }
                                    else
                                    {
                                        Grid[X, Y].SetIsChecking(false);
                                    }
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                            case 'K':
                                if (Math.Abs(X - WhiteKingPosition[0]) <= 1 & Math.Abs(Y - WhiteKingPosition[1])<=1)
                                {
                                    Grid[X, Y].SetIsChecking(true);
                                }
                                else
                                {
                                    Grid[X, Y].SetIsChecking(false);
                                }
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Undoes the last move using the MovesDone and PiecesTaken lists
        /// </summary>
        public void undoLastMove()
        {
            updateIsChecking();

            //Dont do anything if its the first move
            if (PiecesTaken.Count() > 0)
            {

                //Retrieve all information from the end of the MovesDone list and the PiecesTaken list
                int CurrentMoveCount = MovesDone.Last()[2][1];

                bool isFlippedNow = isFlipped;
                bool isFlippedWhenMoved;

                int[] Piece = MovesDone.Last()[1];
                int[] Destination = MovesDone.Last()[0];

                if (MovesDone.Last()[2][0] == 1)
                {
                    isFlippedWhenMoved = true;
                }
                else
                {
                    isFlippedWhenMoved = false;
                }

                //If the board is flipped compared to when the move was made, need to adjust the piece positions
                if (isFlippedNow != isFlippedWhenMoved)
                {
                    Piece[0] = 7 - Piece[0];
                    Piece[1] = 7 - Piece[1];

                    Destination[0] = 7 - Destination[0];
                    Destination[1] = 7 - Destination[1];
                }

                //Get the piece from pieces taken to add back to the board
                Piece PieceToReplace = PiecesTaken.Last();

                //Decrement piece move counter
                Grid[Piece[0], Piece[1]].SetMovesDone(Grid[Piece[0], Piece[1]].GetMovesDone() - 1);

                Piece temp = Grid[Piece[0], Piece[1]];
                //Move the pieces back
                Grid[Piece[0], Piece[1]] = PieceToReplace;
                Grid[Destination[0], Destination[1]] = temp;

                //Undo pawn promotion
                if (Grid[Destination[0], Destination[1]].GetPromotedPawn() == MoveCounter)
                {
                    Grid[Destination[0], Destination[1]].SetPieceType('P');
                    Grid[Destination[0], Destination[1]].SetPromotedPawn(-1);
                }

                //Decrement the move counter
                MoveCounter--;

                //Remove the undone move and piece taken from the end of the list
                PiecesTaken.RemoveAt(PiecesTaken.Count() - 1);
                MovesDone.RemoveAt(MovesDone.Count() - 1);

                //Some moves are made up of two moves (castling and En passent) meaning sometimes two moves need to be undone.
                //If the movecounter in the MovesDone list is the same for two moves - repeat the above
                if (PiecesTaken.Count() > 0)
                {
                    if (CurrentMoveCount == MovesDone.Last()[2][1])
                    {

                        Piece = MovesDone.Last()[1];
                        Destination = MovesDone.Last()[0];

                        if (isFlippedNow != isFlippedWhenMoved)
                        { 
                            Piece[0] = 7 - Piece[0];
                            Piece[1] = 7 - Piece[1];

                            if (Grid[Piece[0],Piece[1]].GetPieceType() != 'P')
                            {
                                Piece[0] = 7 - Piece[0];
                            }

                            Destination[0] = 7 - Destination[0];
                            Destination[1] = 7 - Destination[1];
                        }

                        PieceToReplace = PiecesTaken.Last();

                        Grid[Piece[0], Piece[1]].SetMovesDone(Grid[Piece[0], Piece[1]].GetMovesDone() - 1);

                        temp = Grid[Piece[0], Piece[1]];

                        Grid[Piece[0], Piece[1]] = PieceToReplace;
                        Grid[Destination[0], Destination[1]] = temp;

                        PiecesTaken.RemoveAt(PiecesTaken.Count() - 1);
                        MovesDone.RemoveAt(MovesDone.Count() - 1);
                    }
                }

                updateIsChecking();
            }
        }

        /// <summary>
        /// Returns true if any black non-blank piece's isChecking attribute is true
        /// </summary>
        /// <returns>
        /// Bool, true if white is in check, false if not
        /// </returns>
        public bool WhiteinCheck()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece CurrentPiece = Grid[i, j];

                    if (CurrentPiece.GetIsChecking() & !CurrentPiece.GetIsWhite())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if any white non-blank piece's isChecking attribute is true
        /// </summary>
        /// <returns>
        /// Bool, true if black is in check, false if not
        /// </returns>
        public bool BlackinCheck()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece CurrentPiece = Grid[i, j];

                    if (CurrentPiece.GetIsChecking() & CurrentPiece.GetIsWhite())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if their is a possible move, returns true if not and nobody is in check
        /// </summary>
        /// <returns>
        /// Bool, true if the game has been drawn, false otherwise
        /// </returns>
        public bool inStalemate()
        {
            //Nobody is in check
            if(!BlackinCheck() & !WhiteinCheck())
            {
                //If its whites move, try to find a possible move
                if(MoveCounter % 2 == 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Piece CurrentPiece = Grid[i, j];

                            if (CurrentPiece.GetIsWhite() & CurrentPiece.GetPieceType() != 'X')
                            {
                                for (int k = 0; k < 8; k++)
                                {
                                    for (int l = 0; l < 8; l++)
                                    {
                                        //Attempt move, if unsuccessful, no move will be done
                                        MovePiece([i, j], [k, l]);
                                        
                                        //If successful undo the move and return false
                                        if (Grid[i, j].GetPieceType() == 'X')
                                        {
                                            undoLastMove();
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return true;
                }
                //If its blacks move, try to find a possible move
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Piece CurrentPiece = Grid[i, j];

                            if (!CurrentPiece.GetIsWhite() & CurrentPiece.GetPieceType() != 'X')
                            {
                                for (int k = 0; k < 8; k++)
                                {
                                    for (int l = 0; l < 8; l++)
                                    {
                                        //Attempt move, if unsuccessful, no move will be done
                                        MovePiece([i, j], [k, l]);

                                        //If successful undo the move and return false
                                        if (Grid[i, j].GetPieceType() == 'X')
                                        {
                                            undoLastMove();
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if unable to find a move for white and WhiteinCheck returns true
        /// </summary>
        /// <returns>
        /// Bool, true if black has won, false otherwise
        /// </returns>
        public bool WhiteinCheckmate()
        {
            if (WhiteinCheck())
            {
                for(int i = 0;i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Piece CurrentPiece = Grid[i, j];

                        if (CurrentPiece.GetIsWhite() & CurrentPiece.GetPieceType() != 'X')
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    //Attempt move, if unsuccessful, no move will be done
                                    MovePiece([i, j], [k, l]);

                                    //If successful undo the move and return false
                                    if (Grid[i, j].GetPieceType() == 'X')
                                    {
                                        undoLastMove();
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if unable to find a move for black and BlackinCheck returns true
        /// </summary>
        /// <returns>
        /// Bool, true if white has won, false otherwise
        /// </returns>
        public bool BlackinCheckmate()
        {
            if (BlackinCheck())
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Piece CurrentPiece = Grid[i, j];

                        if (!CurrentPiece.GetIsWhite() & CurrentPiece.GetPieceType() != 'X')
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    //Attempt move, if unsuccessful, no move will be done
                                    MovePiece([i, j], [k, l]);

                                    //If successful undo the move and return false
                                    if (Grid[i, j].GetPieceType() == 'X')
                                    {
                                        undoLastMove();
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }





        /// <summary>
        /// Returns false if there is a piece between the two pairs of coordinates, vertically
        /// horizontally and diagonally
        /// </summary>
        /// <param 1st pair of coordinates ="Piece"></param>
        /// <param 2nd pair of coordinates ="Destination"></param>
        /// <returns>
        /// False if there is a piece between, true if not
        /// </returns>
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

        /// <summary>
        /// Swaps every piece in the grid with its diagonal opposite
        /// </summary>
        public void FlipBoard()
        {
            //Only need to loop over the top half of the board
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0;j < 8; j++)
                {
                    SwapPieces([i, j], [(7-i),(7-j)]);
                }
            }

            //Update king positions
            int[] WhiteKingPosition = findWhiteKing();
            int[] BlackKingPosition = findBlackKing();

            isFlipped = !isFlipped;
        }

        /// <summary>
        /// Swaps two pieces in the grid
        /// </summary>
        /// <param 1st Piece="Piece1"></param>
        /// <param 2nd Piece="Piece2"></param>
        public void SwapPieces(int[] Piece1, int[] Piece2)
        {
            Piece temp = Grid[Piece1[0],Piece1[1]];
            Grid[Piece1[0],Piece1[1]] = Grid[Piece2[0],Piece2[1]];
            Grid[Piece2[0], Piece2[1]] = temp;
        }

        /// <summary>
        /// Scans the board to find a piece that is both white and has PieceType 'K'
        /// </summary>
        /// <returns>
        /// Coordinates of the white king
        /// </returns>
        public int[] findWhiteKing()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Grid[i, j].GetPieceType() == 'K')
                    {
                        if (Grid[i, j].GetIsWhite())
                        {
                            return [i,j];
                        }
                    }
                }
            }

            return [8,8];
        }

        /// <summary>
        /// Scans the board to find a piece that is both black and has PieceType 'K'
        /// </summary>
        /// <returns>
        /// Coordinates of the black king
        /// </returns>
        public int[] findBlackKing()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Grid[i, j].GetPieceType() == 'K')
                    {
                        if (!Grid[i, j].GetIsWhite())
                        {
                            return [i, j];
                        }
                    }
                }
            }

            return [8, 8];
        }

    }
}
