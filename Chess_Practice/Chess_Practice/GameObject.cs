using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Practice
{
    public class Tile : Panel
    {
        //////////////////////////////////////////////
        private ChessPiece? piece;
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
        public bool hasPiece()
        {
            if (this.Piece != null) return true;
            else return false;
        }
        //////////////////////////////////////////////
    }
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
        //////////////////////////////////////////////
        public bool isInsideOfRange(Cordinate cordLimit)
        {
            if ((this.X >= 0 && this.X < cordLimit.X) 
                && (this.Y>=0 && this.Y < cordLimit.Y)) return true;
            else return false;
        }
        public bool isInsideOfRange(int x, int y) 
        {
            return isInsideOfRange(new Cordinate(x, y));
        }
        //////////////////////////////////////////////
        public static Cordinate operator +(Cordinate cordA, Cordinate cordB) => new Cordinate(cordA.X + cordB.X, cordA.Y + cordB.Y);
        public static Cordinate operator -(Cordinate cordA, Cordinate cordB) => new Cordinate(cordA.X - cordB.X, cordA.Y - cordB.Y);
    }

    public abstract class ChessPiece : PictureBox
    {
      
        //////////////////////////////////////////////
        public Cordinate Cord { get; set; }
        
        protected List<Cordinate> moveSet = new List<Cordinate>();

        public string color = "";

        public bool isNeverMoved = true;
        //////////////////////////////////////////////
        public ChessPiece(int x, int y, string color)
        {
            this.Cord = new Cordinate(x, y);
            this.color = color;
        }
        public ChessPiece(Cordinate cord, string color)
        {
            this.Cord = cord; this.color = color;
        }
        //////////////////////////////////////////////
        public virtual bool canMoveTo(Tile[,] tiles, Cordinate cord)
        {
            Cordinate tempCord;
            foreach (Cordinate pieceMove in moveSet){
                tempCord = this.Cord;
                
                while (tempCord.isInsideOfRange(new Cordinate(8, 8)))
                {
                    if (tempCord == cord) return true;
                    tempCord += pieceMove;
                }
            }
            return false;
            
        }

        public void moveOrder(int x, int y)
        {
            moveOrder(new Cordinate(x, y));
        }
        public void moveOrder(Cordinate cord)
        {
            this.Cord += cord;
            this.isNeverMoved = false;
        }
        //////////////////////////////////////////////
    }

    public class Pawn : ChessPiece
    {
        

        private void PawnSettings()
        {
            this.Image = Resources.Resources.Pawn;            
            moveSet.Add(new Cordinate(0, -1));
            moveSet.Add(new Cordinate(0, -2));
            moveSet.Add(new Cordinate(-1, -1));
            moveSet.Add(new Cordinate(1, -1));
        }

        public Pawn(int x, int y, string color) : base(x, y, color)
        {
            PawnSettings();
        }

        public Pawn(Cordinate cord, string color) : base(cord, color)
        {
            PawnSettings();
        }

        public override bool canMoveTo(Tile[,] tiles, Cordinate cordTarget)
        {
            Cordinate cordTemp = new Cordinate(this.Cord);
            Cordinate cordLimit = new Cordinate(8,8);
            if (isNeverMoved)
            {
                cordTemp.Locate(this.Cord);
                cordTemp += moveSet[1];
                if (cordTemp.isInsideOfRange(8, 8))
                {
                    if (!tiles[cordTemp.X, cordTemp.Y].hasPiece()
                    && cordTemp == cordTarget) { return true; }
                }                
            }

            cordTemp.Locate(this.Cord);
            cordTemp += moveSet[0];
            if (cordTemp.isInsideOfRange(cordLimit))
            {
                if (!tiles[cordTemp.X, cordTemp.Y].hasPiece()
                && cordTemp == cordTarget) { return true; }
            }
            cordTemp.Locate(this.Cord);
            cordTemp += moveSet[2];
            if (cordTemp.isInsideOfRange(cordLimit))
            {
                if (tiles[cordTemp.X, cordTemp.Y].hasPiece()
                && cordTemp == cordTarget) { return true; }
            }
            cordTemp.Locate(this.Cord);
            cordTemp += moveSet[3];
            if (cordTemp.isInsideOfRange(cordLimit))
            {
                if (tiles[cordTemp.X, cordTemp.Y].hasPiece()
                && cordTemp == cordTarget) { return true; }
            }
            

            return false;
        }

       
    }
    public class Knight {

    }
    public class Bishop { }
    public class Rook { }
    public class Queen { }
    public class King { }


}
