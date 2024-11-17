using Chess;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Policy;

namespace ChessApp
{
    ///<summary>
    /// Window class used to display board objects and the pieces stored in them. Also used as a user-friendly 
    /// input to interact with board objects. 
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Board object that this class updates and displays
        /// </summary>
        private Board NewBoard = new Board();

        /// <summary>
        /// Updated whenever a button is clicked to the value of the X coordinate of the clicked square
        /// </summary>
        private int currentX = 0;

        /// <summary>
        /// Updated whenever a button is clicked to the value of the Y coordinate of the clicked square
        /// </summary>
        private int currentY = 0;

        /// <summary>
        /// Prevents any changes to the board if true
        /// </summary>
        private bool BoardUnpaused = true;

        public MainWindow()
        {
            InitializeComponent();
            NewBoard.SetBoard();
            UpdateBoard();
        }

        /// <summary>
        /// There is a method for each piece of both colours
        /// </summary>
        /// <returns>
        /// Image object with correct height and width attributes and an image source to the corresponding
        /// piece image
        /// </returns>
        private Image CreateBlackPawn()
        {
            Image BlackPawn = new Image();
            BlackPawn.Width = 40;
            BlackPawn.Height = 40;
            ImageSource BlackPawnImage = new BitmapImage(new Uri("/Images/black-pawn.png", UriKind.Relative));
            BlackPawn.Source = BlackPawnImage;
            return BlackPawn;
        }

        private Image CreateBlackRook()
        {
            Image BlackRook = new Image();
            BlackRook.Width = 40;
            BlackRook.Height = 40;
            ImageSource BlackRookImage = new BitmapImage(new Uri("/Images/black-rook.png", UriKind.Relative));
            BlackRook.Source = BlackRookImage;
            return BlackRook;
        }

        private Image CreateBlackKnight()
        {
            Image BlackKnight = new Image();
            BlackKnight.Width = 40;
            BlackKnight.Height = 40;
            ImageSource BlackKnightImage = new BitmapImage(new Uri("/Images/black-knight.png", UriKind.Relative));
            BlackKnight.Source = BlackKnightImage;
            return BlackKnight;
        }

        private Image CreateBlackBishop()
        {
            Image BlackBishop = new Image();
            BlackBishop.Width = 40;
            BlackBishop.Height = 40;
            ImageSource BlackBishopImage = new BitmapImage(new Uri("/Images/black-bishop.png", UriKind.Relative));
            BlackBishop.Source = BlackBishopImage;
            return BlackBishop;
        }

        private Image CreateBlackQueen()
        {
            Image BlackQueen = new Image();
            BlackQueen.Width = 40;
            BlackQueen.Height = 40;
            ImageSource BlackQueenImage = new BitmapImage(new Uri("/Images/black-queen.png", UriKind.Relative));
            BlackQueen.Source = BlackQueenImage;
            return BlackQueen;
        }

        private Image CreateBlackKing()
        {
            Image BlackKing = new Image();
            BlackKing.Width = 40;
            BlackKing.Height = 40;
            ImageSource BlackKingImage = new BitmapImage(new Uri("/Images/black-king.png", UriKind.Relative));
            BlackKing.Source = BlackKingImage;
            return BlackKing;
        }

        private Image CreateWhitePawn()
        {
            Image WhitePawn = new Image();
            WhitePawn.Width = 40;
            WhitePawn.Height = 40;
            ImageSource WhitePawnImage = new BitmapImage(new Uri("/Images/white-pawn.png", UriKind.Relative));
            WhitePawn.Source = WhitePawnImage;
            return WhitePawn;
        }

        private Image CreateWhiteRook()
        {
            Image WhiteRook = new Image();
            WhiteRook.Width = 40;
            WhiteRook.Height = 40;
            ImageSource WhiteRookImage = new BitmapImage(new Uri("/Images/white-rook.png", UriKind.Relative));
            WhiteRook.Source = WhiteRookImage;
            return WhiteRook;
        }

        private Image CreateWhiteKnight()
        {
            Image WhiteKnight = new Image();
            WhiteKnight.Width = 40;
            WhiteKnight.Height = 40;
            ImageSource WhiteKnightImage = new BitmapImage(new Uri("/Images/white-knight.png", UriKind.Relative));
            WhiteKnight.Source = WhiteKnightImage;
            return WhiteKnight;
        }

        private Image CreateWhiteBishop()
        {
            Image WhiteBishop = new Image();
            WhiteBishop.Width = 40;
            WhiteBishop.Height = 40;
            ImageSource WhiteBishopImage = new BitmapImage(new Uri("/Images/white-bishop.png", UriKind.Relative));
            WhiteBishop.Source = WhiteBishopImage;
            return WhiteBishop;
        }

        private Image CreateWhiteQueen()
        {
            Image WhiteQueen = new Image();
            WhiteQueen.Width = 40;
            WhiteQueen.Height = 40;
            ImageSource WhiteQueenImage = new BitmapImage(new Uri("/Images/white-queen.png", UriKind.Relative));
            WhiteQueen.Source = WhiteQueenImage;
            return WhiteQueen;
        }

        private Image CreateWhiteKing()
        {
            Image WhiteKing = new Image();
            WhiteKing.Width = 40;
            WhiteKing.Height = 40;
            ImageSource WhiteKingImage = new BitmapImage(new Uri("/Images/white-king.png", UriKind.Relative));
            WhiteKing.Source = WhiteKingImage;
            return WhiteKing;
        }


        /// <summary>
        /// Converts a hex code for a colour into a SolidColorBrush of the same colour
        /// </summary>
        /// <returns>
        /// SolidColorBrush object
        /// </returns>
        public SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            byte R = Convert.ToByte(hexaColor.Substring(1, 2), 16);
            byte G = Convert.ToByte(hexaColor.Substring(3, 2), 16);
            byte B = Convert.ToByte(hexaColor.Substring(5, 2), 16);
            SolidColorBrush scb = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, R, G, B));
            return scb;
        }





        //Setting up board

        /// <summary>
        /// Clears the predefined WPF grid then re-ads stack panels images and then buttons.
        /// Also displays which colour moves next
        /// </summary>
        public void UpdateBoard()
        {
            ClearBoard();
            AddStackPanels();
            AddImages();
            AddButtons();

            if (NewBoard.GetMoveCounter() % 2 == 0)
            {
                whosMove.Fill = new SolidColorBrush(Colors.White);
            }
            else
            {
                whosMove.Fill = new SolidColorBrush(Colors.Black);
            }
        }

        /// <summary>
        /// Clears predefined grid
        /// </summary>
        public void ClearBoard()
        {
            MyBoard.Children.Clear();
        }

        /// <summary>
        /// Adds 64 stack panels one to each grid square coloured accordingly
        /// </summary>
        private void AddStackPanels()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    StackPanel panel = new StackPanel();
                    panel.SetValue(Grid.RowProperty, i);
                    panel.SetValue(Grid.ColumnProperty, j);

                    if (i % 2 == j % 2)
                    {
                        panel.Background = GetColorFromHexa("#7092bf");
                    }
                    else
                    {
                        panel.Background = GetColorFromHexa("#c7bfe6");
                    }

                    MyBoard.Children.Add(panel);

                    if (NewBoard.GetPieceToMove()[0] == i & NewBoard.GetPieceToMove()[1] == j)
                    {
                        StackPanel highlight = new StackPanel();
                        panel.SetValue(Grid.RowProperty, i);
                        panel.SetValue(Grid.ColumnProperty, j);

                        panel.Background = new SolidColorBrush(Colors.Yellow);
                        panel.Opacity = 0.4;
                        MyBoard.Children.Add(highlight);
                    }
                }
            }
        }

        /// <summary>
        /// Adds correct image to the grid
        /// </summary>
        private void AddImages()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    char CurrentPieceType = NewBoard.GetPieceTypeAt(i, j);
                    bool CurrentIsWhite = NewBoard.GetIsWhiteAt(i, j);

                    if (CurrentPieceType != 'X')
                    {
                        if (CurrentIsWhite)
                        {
                            switch (CurrentPieceType)
                            {
                                case 'P':
                                    AddImage(CreateWhitePawn, i, j);
                                    break;

                                case 'R':
                                    AddImage(CreateWhiteRook, i, j);
                                    break;

                                case 'N':
                                    AddImage(CreateWhiteKnight, i, j);
                                    break;

                                case 'B':
                                    AddImage(CreateWhiteBishop, i, j);
                                    break;

                                case 'Q':
                                    AddImage(CreateWhiteQueen, i, j);
                                    break;

                                case 'K':
                                    AddImage(CreateWhiteKing, i, j);
                                    break;
                            }
                        }
                        else
                        {
                            switch (CurrentPieceType)
                            {
                                case 'P':
                                    AddImage(CreateBlackPawn, i, j);
                                    break;

                                case 'R':
                                    AddImage(CreateBlackRook, i, j);
                                    break;

                                case 'N':
                                    AddImage(CreateBlackKnight, i, j);
                                    break;

                                case 'B':
                                    AddImage(CreateBlackBishop, i, j);
                                    break;

                                case 'Q':
                                    AddImage(CreateBlackQueen, i, j);
                                    break;

                                case 'K':
                                    AddImage(CreateBlackKing, i, j);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Used by AddImages to add the correct image type to the grid using the corresponding function and the
        /// coordinates where the image is to be added
        /// </summary>
        public void AddImage(Func<Image> MethodName, int X, int Y)
        {
            StackPanel Stack = new StackPanel();
            Image newImage = MethodName();

            newImage.Height = 90;
            newImage.Width = 90;

            Stack.Children.Add(newImage);
            Stack.SetValue(Grid.RowProperty, X);
            Stack.SetValue(Grid.ColumnProperty, Y);
            Stack.Height = 90;
            Stack.Width = 90;

            MyBoard.Children.Add(Stack);
        }

        /// <summary>
        /// Adds transparent control buttons to the grid. Attatches the correct click attribute to link
        /// to the correct square function
        /// </summary>
        private void AddButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    StackPanel Stack = new StackPanel();
                    Button button = new Button();

                    switch (i)
                    {
                        case 0:
                            switch (j)
                            {
                                case 0:
                                    button.Click += zero_zero;
                                    break;
                                case 1:
                                    button.Click += zero_one;
                                    break;
                                case 2:
                                    button.Click += zero_two;
                                    break;
                                case 3:
                                    button.Click += zero_three;
                                    break;
                                case 4:
                                    button.Click += zero_four;
                                    break;
                                case 5:
                                    button.Click += zero_five;
                                    break;
                                case 6:
                                    button.Click += zero_six;
                                    break;
                                case 7:
                                    button.Click += zero_seven;
                                    break;
                            }
                            break;
                        case 1:
                            switch (j)
                            {
                                case 0:
                                    button.Click += one_zero;
                                    break;
                                case 1:
                                    button.Click += one_one;
                                    break;
                                case 2:
                                    button.Click += one_two;
                                    break;
                                case 3:
                                    button.Click += one_three;
                                    break;
                                case 4:
                                    button.Click += one_four;
                                    break;
                                case 5:
                                    button.Click += one_five;
                                    break;
                                case 6:
                                    button.Click += one_six;
                                    break;
                                case 7:
                                    button.Click += one_seven;
                                    break;
                            }
                            break;
                        case 2:
                            switch (j)
                            {
                                case 0:
                                    button.Click += two_zero;
                                    break;
                                case 1:
                                    button.Click += two_one;
                                    break;
                                case 2:
                                    button.Click += two_two;
                                    break;
                                case 3:
                                    button.Click += two_three;
                                    break;
                                case 4:
                                    button.Click += two_four;
                                    break;
                                case 5:
                                    button.Click += two_five;
                                    break;
                                case 6:
                                    button.Click += two_six;
                                    break;
                                case 7:
                                    button.Click += two_seven;
                                    break;
                            }
                            break;
                        case 3:
                            switch (j)
                            {
                                case 0:
                                    button.Click += three_zero;
                                    break;
                                case 1:
                                    button.Click += three_one;
                                    break;
                                case 2:
                                    button.Click += three_two;
                                    break;
                                case 3:
                                    button.Click += three_three;
                                    break;
                                case 4:
                                    button.Click += three_four;
                                    break;
                                case 5:
                                    button.Click += three_five;
                                    break;
                                case 6:
                                    button.Click += three_six;
                                    break;
                                case 7:
                                    button.Click += three_seven;
                                    break;
                            }
                            break;
                        case 4:
                            switch (j)
                            {
                                case 0:
                                    button.Click += four_zero;
                                    break;
                                case 1:
                                    button.Click += four_one;
                                    break;
                                case 2:
                                    button.Click += four_two;
                                    break;
                                case 3:
                                    button.Click += four_three;
                                    break;
                                case 4:
                                    button.Click += four_four;
                                    break;
                                case 5:
                                    button.Click += four_five;
                                    break;
                                case 6:
                                    button.Click += four_six;
                                    break;
                                case 7:
                                    button.Click += four_seven;
                                    break;
                            }
                            break;
                        case 5:
                            switch (j)
                            {
                                case 0:
                                    button.Click += five_zero;
                                    break;
                                case 1:
                                    button.Click += five_one;
                                    break;
                                case 2:
                                    button.Click += five_two;
                                    break;
                                case 3:
                                    button.Click += five_three;
                                    break;
                                case 4:
                                    button.Click += five_four;
                                    break;
                                case 5:
                                    button.Click += five_five;
                                    break;
                                case 6:
                                    button.Click += five_six;
                                    break;
                                case 7:
                                    button.Click += five_seven;
                                    break;
                            }
                            break;
                        case 6:
                            switch (j)
                            {
                                case 0:
                                    button.Click += six_zero;
                                    break;
                                case 1:
                                    button.Click += six_one;
                                    break;
                                case 2:
                                    button.Click += six_two;
                                    break;
                                case 3:
                                    button.Click += six_three;
                                    break;
                                case 4:
                                    button.Click += six_four;
                                    break;
                                case 5:
                                    button.Click += six_five;
                                    break;
                                case 6:
                                    button.Click += six_six;
                                    break;
                                case 7:
                                    button.Click += six_seven;
                                    break;
                            }
                            break;
                        case 7:
                            switch (j)
                            {
                                case 0:
                                    button.Click += seven_zero;
                                    break;
                                case 1:
                                    button.Click += seven_one;
                                    break;
                                case 2:
                                    button.Click += seven_two;
                                    break;
                                case 3:
                                    button.Click += seven_three;
                                    break;
                                case 4:
                                    button.Click += seven_four;
                                    break;
                                case 5:
                                    button.Click += seven_five;
                                    break;
                                case 6:
                                    button.Click += seven_six;
                                    break;
                                case 7:
                                    button.Click += seven_seven;
                                    break;
                            }
                            break;
                    }

                    Stack.Children.Add(button);
                    Stack.SetValue(Grid.RowProperty, i);
                    Stack.SetValue(Grid.ColumnProperty, j);
                    Stack.Height = 90;
                    Stack.Width = 90;
                    button.Opacity = 0;
                    button.Height = 90;
                    button.Width = 90;

                    MyBoard.Children.Add(Stack);
                }
            }
        }

        /// <summary>
        /// Diplays to the window if there is a winner or if the game is drawn
        /// </summary>
        private void DisplayOutcome()
        {
            if (NewBoard.WhiteinCheckmate())
            {
                Winner.Text = "Black wins";
            }
            else if (NewBoard.BlackinCheckmate())
            {
                Winner.Text = "White wins";
            }
            else if (NewBoard.inStalemate())
            {
                Winner.Text = "Stalemate";
            }
            else
            {
                Winner.Text = "";
            }
        }



        //Pawn Promotion

        /// <summary>
        /// Locates a pawn at the top or bottom of the board
        /// </summary>
        /// <returns>
        /// The coordinates of the pawn if found, otherwise returns [8,8]
        /// </returns>
        public int[] findPromotedPawn()
        {
            for (int i = 0; i < 8; i++)
            {
                if (NewBoard.GetPieceTypeAt(0, i) == 'P')
                {
                    return [0, i];
                }
                else if (NewBoard.GetPieceTypeAt(7, i) == 'P')
                {
                    return [7, i];
                }
            }
            return [8, 8];
        }

        /// <summary>
        /// Creates and displays a set of buttons in the window for the user to select which piece they
        /// want to promote the pawn to. Attatches the correct click attribute to each button in order
        /// to link it to the correct Promote_to_"Piece" function. Button images are the same colour as
        /// the promoted pawn. Takes parameters for the location and colour of the pawn
        /// </summary>
        private void promotePawn(bool IsWhite)
        {
            Button rook = new Button();
            Button knight = new Button();
            Button bishop = new Button();
            Button queen = new Button();

            Image rookImage = new Image();
            Image knightImage = new Image();
            Image bishopImage = new Image();
            Image queenImage = new Image();

            if (IsWhite)
            {
                rookImage = CreateWhiteRook();
                knightImage = CreateWhiteKnight();
                bishopImage = CreateWhiteBishop();
                queenImage = CreateWhiteQueen();
            }
            else
            {
                rookImage = CreateBlackRook();
                knightImage = CreateBlackKnight();
                bishopImage = CreateBlackBishop();
                queenImage = CreateBlackQueen();
            }

            rookImage.SetValue(Grid.ColumnProperty, 0);
            knightImage.SetValue(Grid.ColumnProperty, 1);
            bishopImage.SetValue(Grid.ColumnProperty, 2);
            queenImage.SetValue(Grid.ColumnProperty, 3);

            Promotion_Grid.Children.Add(rookImage);
            Promotion_Grid.Children.Add(knightImage);
            Promotion_Grid.Children.Add(bishopImage);
            Promotion_Grid.Children.Add(queenImage);

            rook.Click += Promote_to_Rook;
            knight.Click += Promote_to_Knight;
            bishop.Click += Promote_to_Bishop;
            queen.Click += Promote_to_Queen;

            rook.Opacity = 0;
            knight.Opacity = 0;
            bishop.Opacity = 0;
            queen.Opacity = 0;

            rook.SetValue(Grid.ColumnProperty, 0);
            knight.SetValue(Grid.ColumnProperty, 1);
            bishop.SetValue(Grid.ColumnProperty, 2);
            queen.SetValue(Grid.ColumnProperty, 3);

            Promotion_Grid.Children.Add(rook);
            Promotion_Grid.Children.Add(knight);
            Promotion_Grid.Children.Add(bishop);
            Promotion_Grid.Children.Add(queen);
            return;
        }


        /// <summary>
        /// Method that will convert a promoted pawn to a rook of the same colour and clear the selection
        /// buttons when the rook button is clicked
        /// </summary>
        private void Promote_to_Rook(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'R');
            NewBoard.SetPromotedPawnAt(pawn[0], pawn[1], NewBoard.GetMoveCounter());
            UpdateBoard();
            BoardUnpaused = true;
            NewBoard.updateIsChecking();
            DisplayOutcome();
        }

        /// <summary>
        /// Method that will convert a promoted pawn to a knight of the same colour and clear the selection
        /// buttons when the knight button is clicked
        /// </summary>
        private void Promote_to_Knight(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'N');
            NewBoard.SetPromotedPawnAt(pawn[0], pawn[1], NewBoard.GetMoveCounter());
            UpdateBoard();
            BoardUnpaused = true;
            NewBoard.updateIsChecking();
            DisplayOutcome();
        }

        /// <summary>
        /// Method that will convert a promoted pawn to a bishop of the same colour and clear the selection
        /// buttons when the bishop button is clicked
        /// </summary>
        private void Promote_to_Bishop(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'B');
            NewBoard.SetPromotedPawnAt(pawn[0], pawn[1], NewBoard.GetMoveCounter());
            UpdateBoard();
            BoardUnpaused = true;
            NewBoard.updateIsChecking();
            DisplayOutcome();
        }

        /// <summary>
        /// Method that will convert a promoted pawn to a queen of the same colour and clear the selection
        /// buttons when the queen button is clicked
        /// </summary>
        private void Promote_to_Queen(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'Q');
            NewBoard.SetPromotedPawnAt(pawn[0], pawn[1], NewBoard.GetMoveCounter());
            UpdateBoard();
            BoardUnpaused = true;
            NewBoard.updateIsChecking();
            DisplayOutcome();
        }






        //Control Buttons

        /// <summary>
        /// Linked to a predefined button in the window, flips the board
        /// </summary>
        private void flip_board(object sender, RoutedEventArgs e)
        {
            if (BoardUnpaused)
            {
                NewBoard.FlipBoard();
                UpdateBoard();
            }
        }

        /// <summary>
        /// Linked to a predefined button in the window, resets the board
        /// </summary>
        private void reset_board(object sender, RoutedEventArgs e)
        {
            if (BoardUnpaused)
            {
                NewBoard.SetBoard();
                Winner.Text = "";
                UpdateBoard();
            }
        }

        /// <summary>
        /// Linked to a predefined button in the window, undoes the last move, can be pressed until 
        /// the board returns to starting position
        /// </summary>
        private void Undo_Button(object sender, RoutedEventArgs e)
        {
            if (BoardUnpaused)
            {
                if (NewBoard.GetMoveCounter() > 0)
                {
                    NewBoard.undoLastMove();
                    Winner.Text = "";
                    UpdateBoard();
                }
            }
        }





        //Square Button Control

        /// <summary>
        /// Takes as parameters the coordinates of the last square clicked.
        /// If PieceToMove for the current board is null ([8,8]) updates PieceToMove to the
        /// last square clicked.If PieceToMove has a value, calls MovePiece on the current board
        /// on PieceToMove and the square last clicked. Also avoids moving blank squares and pieces
        /// taking pieces of the same colour. This is also where PromotePawn is called if a piece 
        /// moves onto the last row
        /// </summary>
        private void ifSquareClicked(int currentX, int currentY)
        {
            if (BoardUnpaused)
            {
                //If PieceToMove has no value and the selected square isn't blank
                if (NewBoard.GetPieceToMove()[0] == 8 & NewBoard.GetPieceToMove()[1] == 8 & !NewBoard.IsSquareBlank(currentX, currentY))
                {
                    //If a white piece is selected it must be whites move and vice versa
                    if ((NewBoard.GetIsWhiteAt(currentX, currentY) & NewBoard.GetMoveCounter() % 2 == 0) || (!NewBoard.GetIsWhiteAt(currentX, currentY) & NewBoard.GetMoveCounter() % 2 == 1))
                    {
                        NewBoard.SetPieceToMove([currentX, currentY]);
                        UpdateBoard();
                    }
                    else
                    {
                        NewBoard.SetPieceToMove([8, 8]);
                        UpdateBoard();
                        return;
                    }
                }
                //If the same square is selected twice, deselect the square
                else if (NewBoard.GetPieceToMove()[0] == currentX & NewBoard.GetPieceToMove()[1] == currentY)
                {
                    NewBoard.SetPieceToMove([8, 8]);
                    UpdateBoard();
                    return;
                }
                //No restrictions required when selecting square to move to, handled in Board class
                else if ((NewBoard.GetPieceToMove()[0]<8 & NewBoard.GetPieceToMove()[1]<8))
                {
                    NewBoard.MovePiece(NewBoard.GetPieceToMove(), [currentX, currentY]);

                    //If a pawn is now at the top of bottom of the board, it needs to be promoted
                    if (NewBoard.GetPieceTypeAt(currentX, currentY) == 'P' & (currentX == 0 || currentX == 7))
                    {
                        promotePawn(NewBoard.GetIsWhiteAt(currentX, currentY));
                        BoardUnpaused = false;
                    }

                    //Check for a winner
                    DisplayOutcome();

                    UpdateBoard();
                }
            }
        }


        //NEW COLUMN

 
        /// <summary>
        /// Function linked to the button on square [0,0]. Calls ifSquareClicked(0,0). There are 64 of
        /// these functions, one for each square
        /// </summary>
        private void zero_zero(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        private void one_zero(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        private void two_zero(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        private void three_zero(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        private void four_zero(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        private void five_zero(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        private void six_zero(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_zero(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 0;
            ifSquareClicked(currentX, currentY);
        }

        //NEW COLUMN
        private void zero_one(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        private void one_one(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        private void two_one(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        private void three_one(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        private void four_one(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        private void five_one(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        private void six_one(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_one(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 1;
            ifSquareClicked(currentX, currentY);
        }

        //NEW COLUMN

        private void zero_two(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        private void one_two(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        private void two_two(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        private void three_two(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        private void four_two(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        private void five_two(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        private void six_two(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_two(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 2;
            ifSquareClicked(currentX, currentY);
        }

        //NEW COLUMN

        private void zero_three(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        private void one_three(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        private void two_three(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        private void three_three(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        private void four_three(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        private void five_three(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        private void six_three(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_three(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 3;
            ifSquareClicked(currentX, currentY);
        }

        //NEW COLUMN

        private void zero_four(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        private void one_four(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        private void two_four(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        private void three_four(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        private void four_four(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        private void five_four(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        private void six_four(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_four(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 4;
            ifSquareClicked(currentX, currentY);
        }

        //NEW COLUMN

        private void zero_five(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        private void one_five(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        private void two_five(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        private void three_five(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        private void four_five(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        private void five_five(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        private void six_five(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_five(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 5;
            ifSquareClicked(currentX, currentY);
        }

        //NEW COLUMN

        private void zero_six(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        private void one_six(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        private void two_six(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        private void three_six(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        private void four_six(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        private void five_six(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        private void six_six(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_six(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 6;
            ifSquareClicked(currentX, currentY);
        }

        //NEW COLUMN

        private void zero_seven(object sender, RoutedEventArgs e)
        {
            currentX = 0;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }

        private void one_seven(object sender, RoutedEventArgs e)
        {
            currentX = 1;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }

        private void two_seven(object sender, RoutedEventArgs e)
        {
            currentX = 2;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }

        private void three_seven(object sender, RoutedEventArgs e)
        {
            currentX = 3;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }

        private void four_seven(object sender, RoutedEventArgs e)
        {
            currentX = 4;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }

        private void five_seven(object sender, RoutedEventArgs e)
        {
            currentX = 5;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }

        private void six_seven(object sender, RoutedEventArgs e)
        {
            currentX = 6;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }

        private void seven_seven(object sender, RoutedEventArgs e)
        {
            currentX = 7;
            currentY = 7;
            ifSquareClicked(currentX, currentY);
        }
    }
}