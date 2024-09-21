using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
/// Simple class containing all the attributes required to make a chess piece
/// </summary>
///<remarks>
/// </remarks>

namespace Chess
{
    public class Piece(bool White, char Piece)
    {
        //Attributes

        /// <summary>
        /// Colour of the piece
        /// </summary>
        private bool IsWhite = White;

        /// <summary>
        /// If the piece is attacking the king of the opposite colour
        /// </summary>
        private bool IsChecking = false;

        /// <summary>
        /// 'X' = Blank, 'R' = Rook, 'N'= Knight, 'B' = Bishop, 'Q' = Queen, 'K' = King
        /// </summary>
        private char PieceType = Piece;

        /// <summary>
        /// Tracks how many moves each piece has done. Needed for castling rules and pawn moves
        /// </summary>
        private int MovesDone = 0;

        /// <summary>
        /// Flag for all pieces, set to 1 if a piece was previously a pawn
        /// </summary>
        private int PromotedPawn = -1;

        //Get Methods
        public bool GetIsWhite()
        {
            return IsWhite;
        }

        public bool GetIsChecking()
        {
            return IsChecking;
        }

        public char GetPieceType()
        {
            return PieceType;
        }

        public int GetMovesDone()
        {
            return MovesDone;
        }

        public int GetPromotedPawn()
        {
            return PromotedPawn;
        }

        //Set Methods
        public void SetIsWhite(bool White)
        {
            IsWhite = White;
        }

        public void SetIsChecking(bool Checking)
        {
            IsChecking = Checking;
        }

        public void SetPieceType(char Piece)
        {
            PieceType = Piece;
        }

        public void SetMovesDone(int Moves)
        {
            MovesDone = Moves;
        }

        public void SetPromotedPawn(int promoted)
        {
            PromotedPawn = promoted;
        }
    }
}
