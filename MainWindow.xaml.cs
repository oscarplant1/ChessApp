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

namespace ChessApp
{
    public partial class MainWindow : Window
    {
        // Create the Board object
        Board NewBoard = new Board();

        private int currentX = 0;
        private int currentY = 0;
        private bool BoardUnpaused = true;

        public MainWindow()
        {
            InitializeComponent();
            NewBoard.SetBoard();
            UpdateBoard();
        }

        //Creating image object for each piece, black and white
        private Image CreateBlackPawn()
        {
            Image BlackPawn = new Image();
            BlackPawn.Width = 40;
            BlackPawn.Height = 40;
            ImageSource BlackPawnImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/black-pawn.png"));
            BlackPawn.Source = BlackPawnImage;
            return BlackPawn;
        }

        private Image CreateBlackRook()
        {
            Image BlackRook = new Image();
            BlackRook.Width = 40;
            BlackRook.Height = 40;
            ImageSource BlackRookImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/black-rook.png"));
            BlackRook.Source = BlackRookImage;
            return BlackRook;
        }

        private Image CreateBlackKnight()
        {
            Image BlackKnight = new Image();
            BlackKnight.Width = 40;
            BlackKnight.Height = 40;
            ImageSource BlackKnightImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/black-knight.png"));
            BlackKnight.Source = BlackKnightImage;
            return BlackKnight;
        }

        private Image CreateBlackBishop()
        {
            Image BlackBishop = new Image();
            BlackBishop.Width = 40;
            BlackBishop.Height = 40;
            ImageSource BlackBishopImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/black-bishop.png"));
            BlackBishop.Source = BlackBishopImage;
            return BlackBishop;
        }

        private Image CreateBlackQueen()
        {
            Image BlackQueen = new Image();
            BlackQueen.Width = 40;
            BlackQueen.Height = 40;
            ImageSource BlackQueenImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/black-queen.png"));
            BlackQueen.Source = BlackQueenImage;
            return BlackQueen;
        }

        private Image CreateBlackKing()
        {
            Image BlackKing = new Image();
            BlackKing.Width = 40;
            BlackKing.Height = 40;
            ImageSource BlackKingImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/black-king.png"));
            BlackKing.Source = BlackKingImage;
            return BlackKing;
        }

        private Image CreateWhitePawn()
        {
            Image WhitePawn = new Image();
            WhitePawn.Width = 40;
            WhitePawn.Height = 40;
            ImageSource WhitePawnImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/white-pawn.png"));
            WhitePawn.Source = WhitePawnImage;
            return WhitePawn;
        }

        private Image CreateWhiteRook()
        {
            Image WhiteRook = new Image();
            WhiteRook.Width = 40;
            WhiteRook.Height = 40;
            ImageSource WhiteRookImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/white-rook.png"));
            WhiteRook.Source = WhiteRookImage;
            return WhiteRook;
        }

        private Image CreateWhiteKnight()
        {
            Image WhiteKnight = new Image();
            WhiteKnight.Width = 40;
            WhiteKnight.Height = 40;
            ImageSource WhiteKnightImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/white-knight.png"));
            WhiteKnight.Source = WhiteKnightImage;
            return WhiteKnight;
        }

        private Image CreateWhiteBishop()
        {
            Image WhiteBishop = new Image();
            WhiteBishop.Width = 40;
            WhiteBishop.Height = 40;
            ImageSource WhiteBishopImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/white-bishop.png"));
            WhiteBishop.Source = WhiteBishopImage;
            return WhiteBishop;
        }

        private Image CreateWhiteQueen()
        {
            Image WhiteQueen = new Image();
            WhiteQueen.Width = 40;
            WhiteQueen.Height = 40;
            ImageSource WhiteQueenImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/white-queen.png"));
            WhiteQueen.Source = WhiteQueenImage;
            return WhiteQueen;
        }

        private Image CreateWhiteKing()
        {
            Image WhiteKing = new Image();
            WhiteKing.Width = 40;
            WhiteKing.Height = 40;
            ImageSource WhiteKingImage = new BitmapImage(new Uri("C:/Users/oscar/OneDrive/Desktop/C#/ChessApp/white-king.png"));
            WhiteKing.Source = WhiteKingImage;
            return WhiteKing;
        }

        //Function to convert hex code into brush colour
        public SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            byte R = Convert.ToByte(hexaColor.Substring(1, 2), 16);
            byte G = Convert.ToByte(hexaColor.Substring(3, 2), 16);
            byte B = Convert.ToByte(hexaColor.Substring(5, 2), 16);
            SolidColorBrush scb = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, R, G, B));
            return scb;
        }





        //Setting up board//

        //Method to refresh the current board graphic
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

        //Clears the predefined 8x8 grid
        public void ClearBoard()
        {
            MyBoard.Children.Clear();
        }

        //Add coloured StackPanels to the predefined 8x8 grid
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

        //Adds the predefined piece images to the board graphic
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

        //Method used by AddImages() to add an image object to a chosen grid spot
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

        //Adds transparent buttons to every grid spot that all have a corresponding function below
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





        //Pawn Promotion
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

        private void Promote_to_Rook(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'R');
            UpdateBoard();
            BoardUnpaused = true;
        }

        private void Promote_to_Knight(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'N');
            UpdateBoard();
            BoardUnpaused = true;
        }

        private void Promote_to_Bishop(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'B');
            UpdateBoard();
            BoardUnpaused = true;
        }

        private void Promote_to_Queen(object sender, RoutedEventArgs e)
        {
            Promotion_Grid.Children.Clear();
            int[] pawn = findPromotedPawn();
            NewBoard.SetPieceTypeAt(pawn[0], pawn[1], 'Q');
            UpdateBoard();
            BoardUnpaused = true;
        }
        private void promotePawn(int X, int Y, bool IsWhite)
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





        //Control Buttons
        private void flip_board(object sender, RoutedEventArgs e)
        {
            NewBoard.FlipBoard();
            UpdateBoard();
        }

        private void reset_board(object sender, RoutedEventArgs e)
        {
            NewBoard.SetBoard();
            UpdateBoard();
        }





        //Button Control
        private void ifSquareClicked(int currentX, int currentY)
        {
            if (BoardUnpaused)
            {
                if (NewBoard.GetPieceToMove()[0] == 8 & NewBoard.GetPieceToMove()[1] == 8 & !NewBoard.IsSquareBlank(currentX, currentY))
                {
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
                else if (NewBoard.GetPieceToMove()[0] == currentX & NewBoard.GetPieceToMove()[1] == currentY)
                {
                    NewBoard.SetPieceToMove([8, 8]);
                    UpdateBoard();
                    return;
                }
                else if ((NewBoard.GetPieceToMove()[0]<8 & NewBoard.GetPieceToMove()[1]<8))
                {
                    NewBoard.MovePiece(NewBoard.GetPieceToMove(), [currentX, currentY]);
                    if (NewBoard.GetPieceTypeAt(currentX, currentY) == 'P' & (currentX == 0 || currentX == 7))
                    {
                        promotePawn(currentX, currentY, NewBoard.GetIsWhiteAt(currentX, currentY));
                        BoardUnpaused = false;
                    }
                    UpdateBoard();
                }
            }
        }
    
 

        ///NEW COLUMN

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

        ///NEW COLUMN
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

        ///NEW COLUMN

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

        ///NEW COLUMN

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

        ///NEW COLUMN

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

        ///NEW COLUMN

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

        ///NEW COLUMN

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

        ///NEW COLUMN

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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}