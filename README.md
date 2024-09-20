# ChessApp

Classes: MainWindow,Board, Piece

=================================================================================================================

MainWindow:

Attributes:

BoardUnpaused					bool		- Prevents any changes to the board if true					
currentX					int		- Updated whenever a button is clicked
currentY					int		- Updated whenever a button is clicked



Methods:

MethodName(Parameters)				returns

Create"PieceName"				Image	- Creates an image object for each piece type/colour
UpdateBoard					void	- Clears the WPF grid then re-ads stack panels images and then buttons
ClearBoard					void	- Clears predefined grid which is defined in xaml file
AddStackPanels					void	- Adds 64 stack panels one to each grid square coloured accordingly. 
AddImages					void	- Adds correct image on top of stack panel
AddButtons					void	- Adds transparent control buttons on top of each stackpanel and image

promote_pawn(int,int,bool)			void	- Creates an user input for promotion choice. Pauses board until selection made
promote_"PieceName"				void	- Changes piece type of promoted pawn based on user input.

flip_board					void	- Rotates the board 180 degrees. Connected to a predefined button in xaml file
reset_board					void	- Resets the board. Connected to a predefined button in xaml file

zero_zero private				void	- One method for each square, when square clicked passes the coordinates of the 
							  square to ifSquareClicked
zero-one  
...
zero_seven
one_zero
one_one
...
...
...
seven_seven

ifSquareClicke(int,int)				void	- If PieceToMove for the current board is null ([8,8]) updates PieceToMove to the
							  last square clicked. If PieceToMove has a value call MovePiece on the current board
							  with parameters PieceToMove and the square last clicked.
							  Also avoids moving blank squares and pieces taking pieces of the same colour.
							  This is also where PromotePawn is called if a piece moves onto the last row

=================================================================================================================

Board:

Attributes:

MoveCounter				int			- Increments everytime a move is made
isFlipped				bool			- Is false if white is at the bottom and true if white is at the top
MovesDone				List<int[][]>		- Everytime a move is made the two pairs of coordinates are added to this list
								  along with the value of isFlipped at the time of the move, and the move counter
								  at the time
PieceToMove				int[]			- Variable that stores the first button clicked, ie the selected piece
WhiteKingPosition			int[]			- Tracks the white king
BlackKingPosition			int[]			- Tracks the black king
Grid					Piece[,]		- The 8x8 grid filled with piece objects on which most methods act
BlankPiece				Piece			- Useful to have a blank piece to add where needed
PiecesTaken				List<.Piece>		- When a Piece is taken, it gets added to this list. Includes blank pieces in order
								  to remain in sync with MovesDone



Methods:

Get and set methods for certain attributes

MethodName(Parameters)			returns

IsSquareBlank(int,int)			bool		- Returns true if at the given coordinates in the grid, the piecetype is blank
SetBoard				void		- Populates the grid with pieces in the correct starting position
TestGrid				void		- Outputs the grid to console, for debugging only
OutputLists				void		- Outputs the contents of MovesDone and PiecesTaken to console, for debugging only
MovePiece(int[],int[])			void		- Calls the movePieceObject method after checking if the move is allowed	
movePieceObject(int[],int[])		void		- Moves the Piece in the grid from the first pair of coordinates to the second pair
							  of coordinates
updateIsChecking			void		- Scans the whole grid array and if a non-blank piece is attacking the king of
							  opposite colour, if so the isChecking attribute of the piece is set to true
undoLastMove				void		- Undoes the last move using the MovesDone and PiecesTaken lists
WhiteinCheck				bool		- Returns true if any black non-blank piece's isChecking attribute is true
BlackinCheck				bool		- Returns true if any white non-blank piece's isChecking attribute is true
inStalemate				bool		- Checks if their is a possible move, returns true if not and nobody is in check
WhiteinCheckmate			bool		- Returns true if unable to find a move for white and WhiteinCheck returns true
BlackinCheckmate			bool		- Returns true if unable to find a move for black and BlackinCheck returns true
CheckBetween(int[],int[])   		bool		- Returns false if there is a piece between the two pairs of coordinates, vertically
							  horizontally and diagonally
FlipBoard				void		- Swaps every piece in the grid with its diagonal opposite
SwapPieces(int[],int[])			void		- Swaps two pieces in the grid
findWhiteKing				int[]		- Scans the board to find a piece that is both white and has PieceType 'K'
findBlackKing				int[]		- Scans the board to find a piece that is both black and has PieceType 'K'

=================================================================================================================

Piece: 

Attributes:

IsWhite						bool		- Colour of the piece
IsChecking					bool		- If the piece is attacking the king of the opposite colour
PieceType					char		- 'X' = Blank, 'R' = Rook, 'N'= Knight, 'B' = Bishop, 'Q' = Queen, 'K' = King
MovesDone					int		- Tracks how many moves each piece has done. Needed for castling rules and pawn moves						  

Methods:

Get and Set for all attributes

=================================================================================================================