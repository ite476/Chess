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
using System.Xml;

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
        public Tile(int x, int y, Size size, Cordinate cord) : base()
        {
            this.Location = new Point(x, y);
            this.Size = size;
            this.cord = new Cordinate(cord);
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }
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
        /// <summary>
        /// Piece 멤버 객체를 pieceType 객체의 color 색깔로 생성합니다.
        /// </summary>
        /// <param name="pieceType"></param>
        /// <param name="color"></param>
        public void SetPiece(string pieceType,string color)
            
        {
            switch (pieceType)
            {
                case "pawn": case "Pawn":
                    this.Piece = Pawn.Create(this.cord, color);
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
        /// <summary>
        /// Piece 멤버 객체가 존재하면 true를 반환합니다.
        /// 만약 null이면 false를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public bool hasPiece()
        {
            if (this.Piece == null) return false;
            else return true;
        }
    }
    ///////////////////////////////////////////////
    public class Cordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        /// <summary>
        /// (x, y) 위치 정보를 가진 좌표 객체를 생성합니다.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Cordinate(int x, int y)
        {
            this.X = x; this.Y = y;
        }
        /// <summary>
        /// cord 좌표와 같은 위치를 가진 좌표 객체를 생성합니다.
        /// </summary>
        /// <param name="cord"></param>
        public Cordinate(Cordinate cord)
        {
            this.X = cord.X; this.Y = cord.Y;
        }
        //////////////////////////////////////////////
        /// <summary>
        /// cord 좌표로 좌표를 이동시킵니다.
        /// </summary>
        /// <param name="cord"></param>
        public void Locate(Cordinate cord)
        {
            this.X = cord.X; this.Y = cord.Y;
        }
        /// <summary>
        /// cordDelta만큼 좌표를 이동시킵니다.
        /// </summary>
        /// <param name="cordDelta"></param>
        private void Relocate(Cordinate cordDelta)
        {
            this.X += cordDelta.X; this.Y += cordDelta.Y;
        }
        /// <summary>
        /// cordDelta만큼 좌표를 이동시킨 후, cordLimit 범위 내에 존재하면 true를 반환합니다.
        /// 범위 밖으로 나간 경우, 좌표 이동없이 false를 반환합니다.
        /// </summary> 
        /// <param name="cordDelta"></param>
        /// <param name="cordLimit"></param>
        /// <returns></returns>
        public bool tryMove(Cordinate cordDelta, Cordinate cordLimit)
        {
            if ( (this + cordDelta).isInsideOfRange(cordLimit) )
            {
                this.Relocate(cordDelta); return true;
            }
            else { return false; }
        }
        //////////////////////////////////////////////
        /// <summary>
        /// 좌표가 Cordinate(0,0)과 cordLimit을 꼭짓점으로 가지는 사각형 내에 있으면 true를 반환합니다.
        /// </summary>
        /// <param name="cordLimit"></param>
        /// <returns></returns>
        public bool isInsideOfRange(Cordinate cordLimit)
        {
            if ((this.X >= 0 && this.X < cordLimit.X)
                && (this.Y >= 0 && this.Y < cordLimit.Y)) return true;
            else return false;
        }
        /// <summary>
        /// 좌표가 Cordinate(0,0)과 Cordinate(x,y)를 꼭짓점으로 가지는 사각형 내에 있으면 true를 반환합니다.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool isInsideOfRange(int x, int y)
        {
            return isInsideOfRange(new Cordinate(x, y));
        }
        /// <summary>
        /// cord 좌표와 같은 좌표를 가리키면 true를 반환합니다.
        /// </summary>
        /// <param name="cord"></param>
        /// <returns></returns>
        public bool isEqualTo(Cordinate cord)
        {
            return (this.X == cord.X && this.Y == cord.Y);
        }
       
        //////////////////////////////////////////////
        public static Cordinate operator +(Cordinate cordA, Cordinate cordB) => new Cordinate(cordA.X + cordB.X, cordA.Y + cordB.Y);
        public static Cordinate operator -(Cordinate cordA, Cordinate cordB) => new Cordinate(cordA.X - cordB.X, cordA.Y - cordB.Y);
        
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
        public Cordinate? Cord { get; set; }        
        protected List<Cordinate> moveSet = new List<Cordinate>();
        public string color = "";
        public bool isNeverMoved = true;
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
        //public static ChessPiece CreateNewPiece(string type, int x, int y, string color){ return;}


        /// <summary>
        /// 기물이 전체 타일 목록내에 있는 특정 좌표로 이동할 수 있다면 true를 반환합니다.
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="cordTarget"></param>
        /// <returns></returns>
        public virtual bool canMoveInto(Tile[,] tiles, Cordinate cordTarget)
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
        /// <summary>
        /// 목표 좌표로 기물이 이동할 수 있으면 true를 반환합니다.
        /// </summary>
        /// <param name="cordTarget"></param>
        /// <returns></returns>
        protected virtual bool canMoveTo(Cordinate cordTarget)
        {
            return false;
        }
        /// <summary>
        /// 기물을 (x, y)만큼 이동시킵니다.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void MoveOrder(int x, int y)
        {
            moveOrder(new Cordinate(x, y));
        }
        /// <summary>
        /// 기물을 cord 만큼 이동시킵니다.
        /// </summary>
        /// <param name="cord"></param>
        protected void MoveOrder(Cordinate cord)
        {
            this.Cord += cord;
            this.isNeverMoved = false;
        }
        /// <summary>
        /// 기물을 cord 좌표로 이동시킵니다.
        /// </summary>
        /// <param name="cord"></param>
        protected void MoveToOrder(Cordinate cord)
        {
            this.Cord = cord;
            this.isNeverMoved = false;
        }
        /// <summary>
        /// 기물이 전체 타일 목록내에서 움직여 도착할 수 있다면 해당 타일을 반환합니다.
        /// </summary>
        /// <param name="tiles"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        protected Tile? GetTargetTile(Tile[,] tiles, Cordinate move)
        {
            Cordinate cordTemp = new Cordinate(this.Cord);
            cordTemp += move;
            return (cordTemp.isInsideOfRange(8, 8)) ? tiles[cordTemp.X, cordTemp.Y] : null;
        }

        /// <summary>
        /// 목표지점으로 이동할 수 있으면 이동하고 true를 반환합니다.
        /// </summary>
        /// <param name="cordTarget"></param>
        /// <returns></returns>
        public bool tryOrder(Cordinate cordTarget)
        {
            if (this.canMoveTo(cordTarget))
            {
                MoveToOrder(cordTarget); return true;
            }
            else return false;
        }
    }
    //////////////////////////////////////////////
    public class Pawn : ChessPiece, IChessPieceFactory
    {
        //////////////////////////////////////////////
        protected Pawn(int x, int y, string color) : base(x, y, color) { ; }
        protected Pawn(Cordinate cord, string color) : base(cord, color) { ; }
        public static Pawn Create(int x, int y, string color)
        {
            return new Pawn(x,y, color);
        }
        public static Pawn Create(Cordinate cord, string color)
        {
            return new Pawn(cord, color);
        }
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
        
        public override bool canMoveInto(Tile[,] tiles, Cordinate cordTarget)
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
                        this.MoveOrder(moveSet[1]);
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
                    this.MoveOrder(moveSet[0]);
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
                    this.MoveOrder(moveSet[2]);
                    return true;
                }
            }

            tileTemp = GetTargetTile(tiles, moveSet[3]);
            if (tileTemp != null)
            {
                if (((tileTemp.hasPiece()) ? tileTemp.Piece.color != this.color : false)
                    && tileTemp.cord.isEqualTo(cordTarget))
                {
                    this.MoveOrder(moveSet[3]);
                    return true;
                }
            }


            return false;
        }
        protected override bool canMoveTo(Cordinate cordTarget)
        {
            Cordinate cordTemp;
            ChessPiece? PieceTemp = PieceManagement.GetPieceAt(cordTarget);

            cordTemp = this.Cord + moveSet[0];
            if (cordTemp == cordTarget && PieceTemp == null)
                return true;

            cordTemp = this.Cord + moveSet[1];
            if (isNeverMoved && cordTemp == cordTarget && PieceTemp == null) 
                return true;            

            cordTemp = this.Cord + moveSet[2];
            if (cordTemp == cordTarget && !PieceManagement.isTurnOf(PieceTemp))            
                return true;            

            cordTemp = this.Cord + moveSet[3];
            if (cordTemp == cordTarget && !PieceManagement.isTurnOf(PieceTemp))
                return true;

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
        public override bool canMoveInto(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveInto(tiles, cordTarget);
        }

        protected override bool canMoveTo(Cordinate cordTarget)
        {
            Cordinate cordTemp;
            ChessPiece? PieceTemp = PieceManagement.GetPieceAt(cordTarget);

            foreach (Cordinate move in moveSet)
            {
                cordTemp = this.Cord + move;
                if (cordTemp == cordTarget && !PieceManagement.isTurnOf(PieceTemp)) 
                    return true;

            }

            return false;
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

            moveSet.Add(new Cordinate(1, 1));
            moveSet.Add(new Cordinate(1, -1));
            moveSet.Add(new Cordinate(-1, 1));
            moveSet.Add(new Cordinate(-1, -1));
            
            for (int i = 2; i < cordLimit.X || i < cordLimit.Y; i++)
            {
                moveSet.Add(new Cordinate(i, i));
                moveSet.Add(new Cordinate(i, -i));
                moveSet.Add(new Cordinate(-i, i));
                moveSet.Add(new Cordinate(-i, -i));
            }
        }


        public override bool canMoveInto(Tile[,] tiles, Cordinate cordTarget)
        {
            Tile tileTemp;
            Cordinate cordTemp;

            if (false){ 
            cordTemp = cordTarget - this.Cord;
            MessageBox.Show($"{cordTemp.X}, {cordTemp.Y}");
            if (Math.Abs(cordTemp.X) != Math.Abs(cordTemp.Y)) { return false; }

            Cordinate mainMove = moveSet[0];
            if (cordTemp.X < 0) mainMove.X *= -1;
            if (cordTemp.Y < 0) mainMove.Y *= -1;

            cordTemp = this.Cord;
            MessageBox.Show($"{mainMove.X}, {mainMove.Y}");
            int count = 0;
            while (cordTemp.tryMove(mainMove, ChessPiece.cordLimit))
            {
                count++;
                Console.WriteLine($"{cordTemp.X}, {cordTemp.Y}");
                tileTemp = tiles[cordTemp.X, cordTemp.Y];
                if (tileTemp.hasPiece())
                {
                    if (tileTemp.Piece.color != this.color
                         && cordTemp == cordTarget) return true;
                    else return false;
                }
                else
                {
                    if (cordTemp == cordTarget) return true;
                }
            }
            return false; }
            
            foreach (Cordinate move in moveSet)
            {
                cordTemp = this.Cord;
                //tileTemp = this.GetTargetTile(tiles, move);

                while (cordTemp.tryMove(move, cordLimit))
                {
                    tileTemp = tiles[cordTemp.X, cordTemp.Y];
                    if (tileTemp.cord == cordTarget)
                    {
                        if (tileTemp.hasPiece())
                        {

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (tileTemp.hasPiece())
                        {
                            
                        }
                    }
                    continue;
                    if (!tileTemp.hasPiece())
                    {
                        if(tileTemp.cord == cordTarget)
                        {
                            this.moveOrder(move);
                            return true;
                        }
                        
                    }
                    else if (tileTemp.Piece.color != this.color
                        &&tileTemp.cord == cordTarget)
                    {
                        this.moveOrder(move);
                        return true;
                    }
                    else return false;
                    
                }

                continue;
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

        protected override bool canMoveTo(Cordinate cordTarget)
        {
            Cordinate cordTemp;
            ChessPiece? PieceTemp = PieceManagement.GetPieceAt(cordTarget);

            foreach (Cordinate move in moveSet)
            {
                cordTemp = this.Cord + move;
                if (cordTemp == cordTarget && !PieceManagement.isTurnOf(PieceTemp))
                    return true;
                if (PieceManagement.hasPieceAt(cordTemp))
                    return false;
            }

            return false;

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
        public override bool canMoveInto(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveInto(tiles, cordTarget);
        }
        protected override bool canMoveTo(Cordinate cordTarget)
        {
            Cordinate cordTemp;
            ChessPiece? PieceTemp = PieceManagement.GetPieceAt(cordTarget);

            foreach (Cordinate move in moveSet)
            {
                cordTemp = this.Cord + move;
                if (cordTemp == cordTarget && !PieceManagement.isTurnOf(PieceTemp))
                    return true;
                if (PieceManagement.hasPieceAt(cordTemp))
                    return false;
            }

            return false;

        }
    }
    //////////////////////////////////////////////
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
        public override bool canMoveInto(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveInto(tiles, cordTarget);
        }
        protected override bool canMoveTo(Cordinate cordTarget)
        {
            Cordinate cordTemp;
            ChessPiece? PieceTemp = PieceManagement.GetPieceAt(cordTarget);

            foreach (Cordinate move in moveSet)
            {
                cordTemp = this.Cord + move;
                if (cordTemp == cordTarget && !PieceManagement.isTurnOf(PieceTemp))
                    return true;
                if (PieceManagement.hasPieceAt(cordTemp))
                    return false;
            }

            return false;

        }
    }
    //////////////////////////////////////////////
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
        public override bool canMoveInto(Tile[,] tiles, Cordinate cordTarget)
        {
            return base.canMoveInto(tiles, cordTarget);
        }

        protected override bool canMoveTo(Cordinate cordTarget)
        {
            Cordinate cordTemp;
            ChessPiece? PieceTemp = PieceManagement.GetPieceAt(cordTarget);

            foreach (Cordinate move in moveSet)
            {
                cordTemp = this.Cord + move;
                if (cordTemp == cordTarget && !PieceManagement.isTurnOf(PieceTemp))
                    return true;
                if (PieceManagement.hasPieceAt(cordTemp))
                    return false;
            }

            return false;
        }
    }
    //////////////////////////////////////////////
    
    public static class ChessPieceFactory
    {
        static List<IChessPieceFactory> factoryLine = new List<IChessPieceFactory>();

        static ChessPieceFactory()
        {
            

        }

    }
    public class PawnFactory : Pawn, IChessPieceFactory
    {
        PawnFactory(int x, int y, string color) : base(x,y,color) { }
        PawnFactory(Cordinate cord, string color) : base(cord, color) { }

        void Create()
        {

        }
        
    }

    interface IChessPieceFactory
    {
        void Create() { }
    }
}
