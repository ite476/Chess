namespace Chess_Practice
{
    public partial class ChessGame : Form
    {
        ///////////////////////////////////////////////////////////////////
        Tile[,] tiles = new Tile[8, 8];
        Tile? selectedTile = null;
        int thisTurn; // Even -> Black, Odd -> White;
        bool _PMusing;
        ///////////////////////////////////////////////////////////////////
        /// <summary>
        /// tiles 내 기물정보를 이용한 게임 세팅 메소드입니다.
        /// </summary>
        void InitializeChessGame1v1()
        {
            _PMusing = false;
            Size tileSize = new Size(board.Width / 8, board.Height / 8);
            ChessPiece.GameConfig();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    tiles[x, y] = new Tile((x * tileSize.Width), (y * tileSize.Height), x, y, tileSize);
                    tiles[x, y].Click += Tile_Click;
                    if ((x + y) % 2 == 0) { tiles[x, y].BackColor = Color.White; }
                    else { tiles[x, y].BackColor = Color.Gray; }


                    board.Controls.Add(tiles[x, y]);
                }

            }

            string pieceType; {
                pieceType = "Pawn";
                for (int x = 0; x < 8; x++)
                {
                    tiles[x, 1].SetPiece(pieceType, "white");
                    tiles[x, 6].SetPiece(pieceType, "black");
                }
                pieceType = "Rook";
                {
                    tiles[0, 0].SetPiece(pieceType, "white");
                    tiles[7, 0].SetPiece(pieceType, "white");
                    tiles[0, 7].SetPiece(pieceType, "black");
                    tiles[7, 7].SetPiece(pieceType, "black");
                }
                pieceType = "Knight";
                {
                    tiles[1, 0].SetPiece(pieceType, "white");
                    tiles[6, 0].SetPiece(pieceType, "white");
                    tiles[1, 7].SetPiece(pieceType, "black");
                    tiles[6, 7].SetPiece(pieceType, "black");
                }
                pieceType = "Bishop";
                {
                    tiles[2, 0].SetPiece(pieceType, "white");
                    tiles[5, 0].SetPiece(pieceType, "white");
                    tiles[2, 7].SetPiece(pieceType, "black");
                    tiles[5, 7].SetPiece(pieceType, "black");
                }
                pieceType = "Queen";
                {
                    tiles[3, 0].SetPiece(pieceType, "white");
                    tiles[3, 7].SetPiece(pieceType, "black");
                }
                pieceType = "King";
                {
                    tiles[4, 0].SetPiece(pieceType, "white");
                    tiles[4, 7].SetPiece(pieceType, "black");
                }
            }

            thisTurn = 0;

            // tiles[0,0].Piece = new Rook(0,0,"black");
            // tiles[0,1].Piece = new Knight(0,1,"black");
            // tiles[0,2].Piece = new Bishop(0,2,"black");
        }
        /// <summary>
        /// PieceManagement 정적 클래스를 이용한 게임 세팅 메소드입니다.
        /// </summary>
        void InitializeChessGameRenewal()
        {
            _PMusing = true;
            Size tileSize = new Size(board.Width / 8, board.Height / 8);
            ChessPiece.GameConfig();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    tiles[x, y] = new Tile((x * tileSize.Width), (y * tileSize.Height), x, y, tileSize);
                    tiles[x, y].Click += Tile_Click;
                    if ((x + y) % 2 == 0) { tiles[x, y].BackColor = Color.White; }
                    else { tiles[x, y].BackColor = Color.Gray; }


                    board.Controls.Add(tiles[x, y]);
                }

            }

            string pieceType; {
                pieceType = "Pawn";
                for (int x = 0; x < 8; x++)
                {
                    PieceManagement.tryAddPiece(Pawn.Create(x, 0, "white"));
                    PieceManagement.tryAddPiece(Pawn.Create(x, 7, "black"));
                }
                pieceType = "Rook";
                {
                    PieceManagement.tryAddPiece(new Rook(0, 0, "white"));
                    PieceManagement.tryAddPiece(new Rook(7, 0, "white"));
                    PieceManagement.tryAddPiece(new Rook(0, 7, "black"));
                    PieceManagement.tryAddPiece(new Rook(7, 7, "black"));
                }
                pieceType = "Knight";
                {
                    PieceManagement.tryAddPiece(new Knight(1, 0, "white"));
                    PieceManagement.tryAddPiece(new Knight(6, 0, "white"));
                    PieceManagement.tryAddPiece(new Knight(1, 7, "black"));
                    PieceManagement.tryAddPiece(new Knight(6, 7, "black"));
                }
                pieceType = "Bishop";
                {
                    PieceManagement.tryAddPiece(new Bishop(2, 0, "white"));
                    PieceManagement.tryAddPiece(new Bishop(5, 0, "white"));
                    PieceManagement.tryAddPiece(new Bishop(2, 7, "black"));
                    PieceManagement.tryAddPiece(new Bishop(5, 7, "black"));
                }
                pieceType = "Queen";
                {
                    PieceManagement.tryAddPiece(new Queen(3, 0, "white"));
                    PieceManagement.tryAddPiece(new Queen(3, 7, "black"));
                }
                pieceType = "King";
                {
                    PieceManagement.tryAddPiece(new King(4, 0, "white"));
                    PieceManagement.tryAddPiece(new King(4, 7, "black"));
                }
            }
            thisTurn = 0;
        }
        ///////////////////////////////////////////////////////////////////
        public ChessGame()
        {
            InitializeComponent();

            // InitializeChessGame1v1();
            InitializeChessGameRenewal();
        }
        ///////////////////////////////////////////////////////////////////
        private void Tile_Click(object sender, EventArgs e)
        {
            Tile_Click((Tile)sender, e);
        }
        private void Tile_Click(Tile sender, EventArgs e)
        {
            
            if (selectedTile != null)
            {
                //MessageBox.Show((selectedTile.Piece == null) ? "null" : $"{selectedTile.Piece.GetType()}");
                if (selectedTile == sender) 
                {
                    if ((selectedTile.cord.X + selectedTile.cord.Y) % 2 == 0) selectedTile.BackColor = Color.White;
                    else selectedTile.BackColor = Color.Gray;
                    selectedTile = null;
                }
                else
                {
                    

                    if (sender.hasPiece() ? (sender.Piece.color == ChessPiece.Parlette[thisTurn % 2]) : false)
                    {
                        if ((selectedTile.cord.X + selectedTile.cord.Y) % 2 == 0) selectedTile.BackColor = Color.White;
                        else selectedTile.BackColor = Color.Gray;
                        selectedTile = sender;
                        selectedTile.BackColor = Color.LightGreen;
                    }
                    else if (selectedTile.Piece.canMoveInto(tiles, sender.cord))
                    {
                        
                        sender.Piece = selectedTile.Piece;
                        if ((selectedTile.cord.X + selectedTile.cord.Y) % 2 == 0) selectedTile.BackColor = Color.White;
                        else selectedTile.BackColor = Color.Gray;
                        selectedTile.Piece = null;
                        selectedTile = null;
                        thisTurn++;
                    }

                    
                }
            }
            else
            {
                if (sender.hasPiece()? (sender.Piece.color == ChessPiece.Parlette[thisTurn % 2]) : false)
                {
                    selectedTile = sender;
                    selectedTile.BackColor = Color.LightGreen;
                }
            }


        }
        ///////////////////////////////////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        ///////////////////////////////////////////////////////////////////
        private void timer1_Tick(object sender, EventArgs e)
        {
            info.Text = $"{ChessPiece.Parlette[thisTurn % 2]}'s turn\n";
            info.Text += (selectedTile == null) ? "null" : $"{selectedTile.Piece.color} {selectedTile.Piece.GetType()} : \n{selectedTile.cord.X},{selectedTile.cord.Y}";
        }
    }
}