# ChessApp

Classes: MainWindow,Board, Piece

=================================================================================================================

MainWindow:

Attributes:

BoardUnpaused				bool	- Prevents any changes to the board if true					
currentX					int		- Updated whenever a button is clicked
currentY					int		- Updated whenever a button is clicked



Methods:

Create"PieceName"			Image	- Creates an image object for each piece type/colour
UpdateBoard					void	- Clears the WPF grid then re-ads stack panels images and then buttons
ClearBoard					void	- Clears predefined grid which is defined in xaml file
AddStackPanels				void	- Adds 64 stack panels one to each grid square coloured accordingly. 
AddImages					void	- Adds correct image on top of stack panel
AddButtons					void	- Adds transparent control buttons on top of each stackpanel and image

promote_pawn(int,int,bool)	void	- Creates an user input for promotion choice. Pauses board until selection made
promote_"PieceName"			void	- Changes piece type of promoted pawn based on user input.

flip_board					void	- Rotates the board 180 degrees. Connected to a predefined button in xaml file
reset_board					void	- Resets the board. Connected to a predefined button in xaml file

zero_zero private			void	- One method for each square, when square clicked passes the coordinates of the square to ifSquareClicked
zero-one  
...
zero_seven
one_zero
one_one
...
...
...
seven_seven

ifSquareClicke(int,int)		void	- If PieceToMove for the current board is null ([8,8]) updates PieceToMove to the
									last square clicked. If PieceToMove has a value call MovePiece on the current board
									with parameters PieceToMove and the square last clicked.
									Also avoids moving blank squares and pieces taking pieces of the same colour.
									This is also where PromotePawn is called if a piece moves onto the last row

=================================================================================================================

Board:

Attributes:

isFlipped bool						-
MoveCounter int						-
WhiteKingPosition [int,int]			-
BlackKingPosition [int,int]			-
PieceToMove [int,int]				-
PreviousMove [[int,int],[int,int]]	-
knightMoves int[][]					-
Grid Piece[8][8]					-
BlankPiece Piece					-



Methods:

Get and set methods for certain attributes

IsSquareBlank(int,int)		bool	-
SetBoard					void	-
TestGrid					void	-
OutputLists					void	-
MovePiece(int[],int[])		void	-
movePieceObject(int[],int[])void	-
updateIsChecking			void	-
undoLastMove				void	-
WhiteinCheck				bool	-
BlackinCheck				bool	-
inStalemate					bool	-
WhiteinCheckmate			bool	-
BlackinCheckmate			bool	-
CheckBetween(int[],intp[])  bool	-
FlipBoard					void	-
SwapPieces(int[],int[])		void	-
findWhiteKing				int[]	-
findBlackKing				int[]	-

=================================================================================================================

Piece: 

Attributes:

IsWhite						bool	-
IsChecking					bool	-
PieceType					char	-
MovesDone					int		-					  

Methods:

Get and Set for all attributes

=================================================================================================================