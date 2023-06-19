namespace Chess_Practice
{
    public partial class ChessGame : Form
    {

        Tile[,] tiles = new Tile[8, 8];
        Tile? selectedTile = null;
        void InitializeChessGame1v1()
        {
            Size tileSize = new Size(board.Width / 8, board.Height / 8);

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

            for (int x = 0; x < 8; x++)
            {
                tiles[x, 6].Piece = new Pawn(x, 6, "white");
                tiles[x, 1].Piece = new Pawn(x, 1, "black");
            }

            // tiles[0,0].Piece = new Rook(0,0,"black");
            // tiles[0,1].Piece = new Knight(0,1,"black");
            // tiles[0,2].Piece = new Bishop(0,2,"black");
        }
        public ChessGame()
        {
            InitializeComponent();

            InitializeChessGame1v1();

        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Tile_Click((Tile)sender, e);
        }
        private void Tile_Click(Tile sender, EventArgs e)
        {
            if (selectedTile != null)
            {
                if (selectedTile == sender)
                {
                    if ((selectedTile.cord.X + selectedTile.cord.Y) % 2 == 0) selectedTile.BackColor = Color.White;
                    else selectedTile.BackColor = Color.Gray;
                    selectedTile = null;
                }
                else
                {
                    //
                    if (sender.hasPiece())
                    {
                        if ((selectedTile.cord.X + selectedTile.cord.Y) % 2 == 0) selectedTile.BackColor = Color.White;
                        else selectedTile.BackColor = Color.Gray;
                        selectedTile = sender;
                        selectedTile.BackColor = Color.LightGreen;
                    }
                    else if ((selectedTile.Piece).canMoveTo(tiles, sender.cord))
                    {
                        MessageBox.Show("!!");
                        sender.Piece = selectedTile.Piece;
                        if ((selectedTile.cord.X + selectedTile.cord.Y) % 2 == 0) selectedTile.BackColor = Color.White;
                        else selectedTile.BackColor = Color.Gray;
                        selectedTile = null;
                    }
                }
            }
            else
            {
                if (sender.hasPiece())
                {
                    selectedTile = sender;
                    selectedTile.BackColor = Color.LightGreen;
                }
            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void test(object sender, EventArgs e)
        {

        }
    }
}