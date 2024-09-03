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

namespace ChessApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board NewBoard = new Board();
        private int currentX = 0;
        private int currentY = 0;
        public MainWindow()
        {
            //Console.WriteLine(NewBoard.GetBlackKingPosition()[0]);
            //Console.WriteLine(NewBoard.GetBlackKingPosition()[1]);

            NewBoard.SetBoard();
            NewBoard.TestGrid();
            UpdateGrid();
            InitializeComponent();
        }

        private void UpdateGrid()
        {
            for (int rowIndex = 0; rowIndex < 7; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 7; columnIndex++)
                {
                    char CurrentPieceType = NewBoard.GetPieceTypeAt(rowIndex, columnIndex);
                    bool CurrentIsWhite = NewBoard.GetIsWhiteAt(rowIndex, columnIndex);

                    var imageSourceWP = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/white-pawn.jpg"));
                    var whitepawn = new Image { Source = imageSourceWP };

                    var imageSourceWR = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/white-rook.jpg"));
                    var whiterook = new Image { Source = imageSourceWR };

                    var imageSourceWN = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/white-knight.jpg"));
                    var whiteknight = new Image { Source = imageSourceWN };

                    var imageSourceWB = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/white-bishop.jpg"));
                    var whitebishop = new Image { Source = imageSourceWB };

                    var imageSourceWQ = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/white-queen.jpg"));
                    var whitequeen = new Image { Source = imageSourceWQ };

                    var imageSourceWK = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/white-king.jpg"));
                    var whiteking = new Image { Source = imageSourceWK };

                    var imageSourceBP = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/black-pawn.jpg"));
                    var blackpawn = new Image { Source = imageSourceBP };

                    var imageSourceBR = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/black-rook.jpg"));
                    var blackrook = new Image { Source = imageSourceBR };

                    var imageSourceBN = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/black-knight.jpg"));
                    var blackknight = new Image { Source = imageSourceBN };

                    var imageSourceBB = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/black-bishop.jpg"));
                    var blackbishop = new Image { Source = imageSourceBB };

                    var imageSourceBQ = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/black-queen.jpg"));
                    var blackqueen = new Image { Source = imageSourceBQ };

                    var imageSourceBK = new BitmapImage(new Uri("pack://application:,,,/ChessApp;component/Assets/black-king.jpg"));
                    var blackking = new Image { Source = imageSourceBK };

                    if (CurrentIsWhite)
                    {
                        switch (CurrentPieceType)
                        {
                            case 'X':
                                break;

                            case 'P':
                                Grid.SetRow(whitepawn, rowIndex);
                                Grid.SetColumn(whitepawn, columnIndex);
                                Board.Children.Add(whitepawn);
                                break;

                            case 'R':
                                Grid.SetRow(whiterook, rowIndex);
                                Grid.SetColumn(whiterook, columnIndex);
                                Board.Children.Add(whiterook);
                                break;

                            case 'N':
                                Grid.SetRow(whiteknight, rowIndex);
                                Grid.SetColumn(whiteknight, columnIndex);
                                Board.Children.Add(whiteknight);
                                break;

                            case 'B':
                                Grid.SetRow(whitebishop, rowIndex);
                                Grid.SetColumn(whitebishop, columnIndex);
                                Board.Children.Add(whitebishop);
                                break;

                            case 'Q':
                                Grid.SetRow(whitequeen, rowIndex);
                                Grid.SetColumn(whitequeen, columnIndex);
                                Board.Children.Add(whitequeen);
                                break;

                            case 'K':
                                Grid.SetRow(whiteking, rowIndex);
                                Grid.SetColumn(whiteking, columnIndex);
                                Board.Children.Add(whiteking);
                                break;
                        }
                    }
                    else
                    {
                        switch (CurrentPieceType)
                        {

                            case 'X':
                                break;

                            case 'P':
                                Grid.SetRow(blackpawn, rowIndex);
                                Grid.SetColumn(blackpawn, columnIndex);
                                Board.Children.Add(blackpawn);
                                break;

                            case 'R':
                                Grid.SetRow(blackrook, rowIndex);
                                Grid.SetColumn(blackrook, columnIndex);
                                Board.Children.Add(blackrook);
                                break;

                            case 'N':
                                Grid.SetRow(blackknight, rowIndex);
                                Grid.SetColumn(blackknight, columnIndex);
                                Board.Children.Add(blackknight);
                                break;

                            case 'B':
                                Grid.SetRow(blackbishop, rowIndex);
                                Grid.SetColumn(blackbishop, columnIndex);
                                Board.Children.Add(blackbishop);
                                break;

                            case 'Q':
                                Grid.SetRow(blackqueen, rowIndex);
                                Grid.SetColumn(blackqueen, columnIndex);
                                Board.Children.Add(blackqueen);
                                break;

                            case 'K':
                                Grid.SetRow(blackking, rowIndex);
                                Grid.SetColumn(blackking, columnIndex);
                                Board.Children.Add(blackking);
                                break;
                        }
                    }

                    //Grid.SetRow(image, rowIndex);
                    //Grid.SetColumn(image, columnIndex);
                    //Board.Children.Add(image);
                }
            }
        }

        private void ifSquareClicked(int currentX, int currentY)
        {
            if (NewBoard.GetPieceToMove()[0] == 8 & NewBoard.GetPieceToMove()[1] == 8)
            {
                if (NewBoard.IsSquareBlank(currentX, currentY)) 
                {
                    return;
                }
                else
                {
                    NewBoard.SetPieceToMove([currentX, currentY]);
                }
            }
            else
            {
                if (NewBoard.GetPieceToMove()[0] == currentX & NewBoard.GetPieceToMove()[1] == currentY)
                {
                    NewBoard.SetPieceToMove([8, 8]);
                    return;
                }
                else
                {
                    NewBoard.MovePiece(NewBoard.GetPieceToMove(), [currentX, currentY]);
                }
            }
            return;
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
    }
}