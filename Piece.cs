using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Piece(bool White, char Piece)
    {
        //Attributes
        private bool IsWhite = White;
        private bool IsChecking = false;
        private char PieceType = Piece;
        private int MovesDone = 0;

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
    }
}
