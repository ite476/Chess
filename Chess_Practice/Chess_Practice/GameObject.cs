using Chess_Practice;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Practice
{
    public class Tile : Panel
    {
        //////////////////////////////////////////////
        private ChessPiece? piece = null;
        public ChessPiece? Piece { 
            get { return piece; }
            set { this.piece = value;
                if (value == null) this.BackgroundImage = null;
                else this.BackgroundImage = value.Image;
            }
            }
        public Cordinate cord;
        //////////////////////////////////////////////
        public Tile(int x, int y, int cordX, int cordY) : base()
        {
            this.Location = new Point(x, y);
            this.cord = new Cordinate(cordX, cordY);
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }
        public Tile(int x, int y, Cordinate cord) : this(x,y,cord.X,cord.Y) { }       
        public Tile(int x, int y, Cordinate cord, Size size) : this(x,y,cord)
        {
            this.Size = size;
        }
        public Tile(int x, int y, int cordX, int cordY, Size size) : this(x, y, cordX, cordY)
        {
            this.Size = size;
        }
        //////////////////////////////////////////////
        public void SetPiece(string pieceType,string color)
            
        {
            switch (pieceType)
            {
                case "pawn": case "Pawn":
                    this.Piece = new Pawn(this.cord, color);
                    break;
                case "bishop": case "Bishop":
                    this.Piece = new Bishop(this.cord, color);
                    break;
                case "rook": case "Rook":
                    this.Piece = new Rook(this.cord, color);
                    break;
                case "knight": case "Knight":
                    this.Piece = new Knight(this.cord, color);
                    break;
                case "queen": case "Queen":
                    this.Piece = new Queen(this.cord, color);
                    break;
                case "king": case "King":
                    this.Piece = new King(this.cord, color);
                    break;
                default:
                    this.Piece = null;
                    break;
            }
            
        }

        
        public bool hasPiece()
        {
            if (this.Piece == null) return false;
            else return true;
        }

        
        //////////////////////////////////////////////
    }
    ///////////////////////////////////////////////
    public class Cordinate
    {
        //////////////////////////////////////////////
        public int X { get; set; }
        public int Y { get; set; }
        //////////////////////////////////////////////
        public Cordinate(int x, int y)
        {
            this.X = x; this.Y = y;
        }
        public Cordinate(Cordinate cord)
        {
            this.X = cord.X; this.Y = cord.Y;
        }
        //////////////////////////////////////////////
        public void Locate(Cordinate cord)
        {
            this.X = cord.X; this.Y = cord.Y;
        }
        private void Relocate(Cordinate cordDelta)
        {
            this.X += cordDelta.X; this.Y += cordDelta.Y;
        }
        public bool tryMove(Cordinate cordDelta, Cordinate cordLimit)
        {
            if ( (this + cordDelta).isInsideOfRange(cordLimit) )
            {
                this.Relocate(cordDelta); return true;
            }
            else { return false; }
        }
        //////////////////////////////////////////////
        public bool isInsideOfRange(Cordinate cordLimit)
        {
            if ((this.X >= 0 && this.X < cordLimit.X)
                && (this.Y >= 0 && this.Y < cordLimit.Y)) return true;
            else return false;
        }
        public bool isInsideOfRange(int x, int y)
        {
            return isInsideOfRange(new Cordinate(x, y));
        }
        public bool isEqualTo(Cordinate cord)
        {
            return (this.X == cord.X && this.Y == cord.Y);
        }
       
        //////////////////////////////////////////////
        public static Cordinate operator +(Cordinate cordA, Cordinate cordB) => new Cordinate(cordA.X + cordB.X, cordA.Y + cordB.Y);
        public static Cordinate operator -(Cordinate cordA, Cordinate cordB) => new Cordinate(cordA.X - cordB.X, cordA.Y - cordB.Y);
        //public static bool operator ==(Cordinate cordA, Cordinate cordB) => (cordA.X == cordB.X && cordA.Y == cordB.Y);
        //public static bool operator !=(Cordinate cordA, Cordinate cordB) => (cordA.X != cordB.X || cordA.Y != cordB.Y);
    }
    ///////////////////////////////////////////////
    
    public class NewChessPiece<T>
        where T : ChessPiece, new()
    {
        public T GetNewChessPiece()
        {
            return new T();
        }
    }
    public abstract class ChessPiece : PictureBox
    {
        public static Cordinate cordLimit;
        public static string[] Parlette;
        public static void GameConfig()
        {
            ChessPiece.cordLimit = new Cordinate(8, 8);
            ChessPiece.Parlette = new string[] { "black", "white" };
        }
        //////////////////////////////////////////////
        public Cordinate Cord { get; set; }        
        protected List<Cordinate> moveSet = new List<Cordinate>();
        public string color = "";
        public bool isNeverMoved = true;
        //////////////////////////////////////////////
        protected virtual void ChessPieceSettings() {
            if (this.color == ChessPiece.Parlette[0])
            {
                this.Image = null; // black
            }
            else
            {
                this.Image = null; //white
            }
            
            moveSet.Add(new Cordinate(0,0));
        }
        public ChessPiece(int x, int y, string color)
        {
            this.Cord = new Cordinate(x, y);
            this.color = color;
            ChessPieceSettings();
        }
        public ChessPiece(Cordinate cord, string color)
        {
            this.Cord = cord; this.color = color;
            ChessPieceSettings();
        }

        //////////////////////////////////////////////
        public virtual bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            /*Cordinate tempCord;
            foreach (Cordinate pieceMove in moveSet){
                tempCord = this.Cord;
                
                while (tempCord.isInsideOfRange(new Cordinate(8, 8)))
                {
                    if (tempCord.isEqualTo(cord)) return true;
                    tempCord += pieceMove;
                }
            }
            return false;*/

            Tile tileTemp;

            foreach (Cordinate move in moveSet)
            {
                tileTemp = GetTargetTile(tiles, move);

                if (tileTemp != null
                    && tileTemp.cord.isEqualTo(cordTarget))
                {
                    if (!tileTemp.hasPiece())
                    {
                        this.moveOrder(move);
                        return true;
                    }
                    else if (tileTemp.Piece.color != this.color)
                    {
                        this.moveOrder(move);
                        return true;
                    }
                }
            }
            return false;
        }
    
        protected void moveOrder(int x, int y)
        {
            moveOrder(new Cordinate(x, y));
        }
        protected void moveOrder(Cordinate cord)
        {
            this.Cord += cord;
            this.isNeverMoved = false;
        }
        protected Tile? GetTargetTile(Tile[,] tiles, Cordinate move)
        {
            Cordinate cordTemp = new Cordinate(this.Cord);
            cordTemp += move;
            return (cordTemp.isInsideOfRange(8, 8)) ? tiles[cordTemp.X, cordTemp.Y] : null;
        }


        //////////////////////////////////////////////
    }
    public class Pawn : ChessPiece
    {
        //////////////////////////////////////////////
        public Pawn(int x, int y, string color) : base(x, y, color) { ; }
        public Pawn(Cordinate cord, string color) : base(cord, color) { ; }
        //////////////////////////////////////////////
        protected override void ChessPieceSettings()
        {
            

            if (this.color == ChessPiece.Parlette[0])
            {
                this.Image = Resources.Resources.BlackPawn;
                moveSet.Add(new Cordinate(0, -1));
                moveSet.Add(new Cordinate(0, -2));
                moveSet.Add(new Cordinate(-1, -1));
                moveSet.Add(new Cordinate(1, -1));
            }
            else
            {
                this.Image = Resources.Resources.WhitePawn;
                moveSet.Add(new Cordinate(0, 1));
                moveSet.Add(new Cordinate(0, 2));
                moveSet.Add(new Cordinate(-1, 1));
                moveSet.Add(new Cordinate(1, 1));                
            }
        }
        public override bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            Tile tileTemp;

            if (isNeverMoved)
            {

                tileTemp = GetTargetTile(tiles, moveSet[1]);
                if (tileTemp != null)
                {
                    if (!tileTemp.hasPiece()
                    && tileTemp.cord.isEqualTo(cordTarget))
                    {
                        this.moveOrder(moveSet[1]);
                        return true;
                    }
                }
            }

            tileTemp = GetTargetTile(tiles, moveSet[0]);
            if (tileTemp != null)
            {
                if (!tileTemp.hasPiece()
                && tileTemp.cord.isEqualTo(cordTarget))
                {
                    this.moveOrder(moveSet[0]);
                    return true;
                }
            }

            tileTemp = GetTargetTile(tiles, moveSet[2]);
            if (tileTemp != null)
            {
                if (
                    ((tileTemp.hasPiece()) ? tileTemp.Piece.color != this.color : false)
                    && tileTemp.cord.isEqualTo(cordTarget)
                )
                {
                    this.moveOrder(moveSet[2]);
                    return true;
                }
            }

            tileTemp = GetTargetTile(tiles, moveSet[3]);
            if (tileTemp != null)
            {
                if (((tileTemp.hasPiece()) ? tileTemp.Piece.color != this.color : false)
                    && tileTemp.cord.isEqualTo(cordTarget))
                {
                    this.moveOrder(moveSet[3]);
                    return true;
                }
            }


            return false;
        }
        //////////////////////////////////////////////
    }
    //////////////////////////////////////////////
    public class Knight : ChessPiece {
        
        public Knight(int x, int y, string color) : base (x, y, color) { }
        public Knight(Cordinate cord, string color) : base(cord, color) { }
        //////////////////////////////////////////////
        protected override void ChessPieceSettings()
        {
            if (this.color == ChessPiece.Parlette[0])
            {
                this.Image = Resources.Resources.BlackKnight;
            }
            else
            {
                this.Image = Resources.Resources.WhiteKnight;
            }

            moveSet.Add(new Cordinate(1, 2));
            moveSet.Add(new Cordinate(1, -2));
            moveSet.Add(new Cordinate(-1, 2));
            moveSet.Add(new Cordinate(-1, -2));
            moveSet.Add(new Cordinate(2, 1));
            moveSet.Add(new Cordinate(2, -1));
            moveSet.Add(new Cordinate(-2, 1));
            moveSet.Add(new Cordinate(-2, -1));
        }
        public override bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveTo(tiles, cordTarget);
        }
        
    }
    //////////////////////////////////////////////
    public class Bishop : ChessPiece {
        public Bishop(int x, int y, string color) : base(x, y, color) { }
        public Bishop(Cordinate cord, string color) : base(cord, color) { }
        //////////////////////////////////////////////
        protected override void ChessPieceSettings()
        {
            if (this.color == ChessPiece.Parlette[0])
            {
                this.Image = Resources.Resources.BlackBishop;
            }
            else
            {
                this.Image = Resources.Resources.WhiteBishop;
            }

            for (int i = 1; i < cordLimit.X || i < cordLimit.Y; i++)
            {
                moveSet.Add(new Cordinate(i, i));
                moveSet.Add(new Cordinate(i, -i));
                moveSet.Add(new Cordinate(-i, i));
                moveSet.Add(new Cordinate(-i, -i));
            }
        }
        public override bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveTo(tiles, cordTarget);
        }
    }
    //////////////////////////////////////////////
    public class Rook : ChessPiece
    {
        public Rook(int x, int y, string color) : base(x, y, color) { }
        public Rook(Cordinate cord, string color) : base(cord, color) { }
        //////////////////////////////////////////////
        protected override void ChessPieceSettings()
        {
            if (this.color == ChessPiece.Parlette[0])
            {
                this.Image = Resources.Resources.BlackBishop;
            }
            else
            {
                this.Image = Resources.Resources.WhiteBishop;
            }

            for (int i = 1; i < cordLimit.X || i < cordLimit.Y; i++)
            {
                moveSet.Add(new Cordinate(i, 0));
                moveSet.Add(new Cordinate(-i, 0));
                moveSet.Add(new Cordinate(0, i));
                moveSet.Add(new Cordinate(0, -i));
            }
        }
        public override bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveTo(tiles, cordTarget);
        }
    }
    public class Queen : ChessPiece {
        public Queen(int x, int y, string color) : base(x, y, color) { }
        public Queen(Cordinate cord, string color) : base(cord, color) { }
        //////////////////////////////////////////////
        protected override void ChessPieceSettings()
        {
            if (this.color == ChessPiece.Parlette[0])
            {
                this.Image = Resources.Resources.BlackQueen;
            }
            else
            {
                this.Image = Resources.Resources.WhiteQueen;
            }

            for (int i = 1; i < cordLimit.X || i < cordLimit.Y; i++)
            {
                moveSet.Add(new Cordinate(i, 0));
                moveSet.Add(new Cordinate(-i, 0));
                moveSet.Add(new Cordinate(0, i));
                moveSet.Add(new Cordinate(0, -i));
                moveSet.Add(new Cordinate(i, i));
                moveSet.Add(new Cordinate(i, -i));
                moveSet.Add(new Cordinate(-i, i));
                moveSet.Add(new Cordinate(-i, -i));
            }
        }
        public override bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveTo(tiles, cordTarget);
        }
    }
    public class King : ChessPiece{
        public King(int x, int y, string color) : base(x, y, color) { }
        public King(Cordinate cord, string color) : base(cord, color) { }
        //////////////////////////////////////////////
        protected override void ChessPieceSettings()
        {
            if (this.color == ChessPiece.Parlette[0])
            {
                this.Image = Resources.Resources.BlackKing;
            }
            else
            {
                this.Image = Resources.Resources.WhiteKing;
            }


            moveSet.Add(new Cordinate(1, 0));
            moveSet.Add(new Cordinate(-1, 0));
            moveSet.Add(new Cordinate(0, 1));
            moveSet.Add(new Cordinate(0, -1));
            moveSet.Add(new Cordinate(1, 1));
            moveSet.Add(new Cordinate(1, -1));
            moveSet.Add(new Cordinate(-1, 1));
            moveSet.Add(new Cordinate(-1, -1));

        }
        public override bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveTo(tiles, cordTarget);
        }
    }


}
