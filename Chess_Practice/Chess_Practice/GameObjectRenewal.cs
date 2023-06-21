using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Practice
{
    class newTile
    {
        Cordinate Cordinate { get; set; }

        /// <summary>
        /// 기물관리 시스템에 해당 좌표에 
        /// </summary>
        void hasPiece()
        {

        }
    }
    public static class GameConfigManagement
    {

    }
    
    public static class TileManagement
    {
        private static List<Tile> Tiles = new List<Tile>();
        private static Tile? selectedTile = null;
        public static Cordinate Limitation = new Cordinate(0, 0);

        /// <summary>
        /// 타일 관리 시스템을 비웁니다.
        /// </summary>
        public static void ClearTile()
        {
            Tiles.Clear();
        }
        /// <summary>
        /// 타일 관리 시스템에 타일을 추가합니다.
        /// 좌표에 이미 타일이 존재하면 추가하지 않고 false를 반환합니다.
        /// </summary>
        private static bool tryAddTile(int x, int y, Size size, Cordinate cord)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.cord == cord) { return false; }
            }

            Tile newTile = new(x, y, size, cord);
            newTile.Click += Click;
            Tiles.Add(newTile);

            return true;
        }
        /// <summary>
        /// cordLimit 크기만큼 기본 타일을 생성하고, 각 타일에 Click 델리게이트를 연결합니다
        /// </summary>
        /// <param name="cordLimit">
        /// X, Y는 모두 양수여야 합니다.
        /// </param>
        /// <param name="screenSize"></param>
        public static void Initialize(Cordinate cordLimit, Size screenSize)
        {
            ClearTile();
            int sizeX = screenSize.Width / cordLimit.X;
            int sizeY = screenSize.Height / cordLimit.Y;
            Size size = new Size(screenSize.Width / cordLimit.X, screenSize.Height / cordLimit.Y);
            Cordinate cordTile;
            if (cordLimit.X > 0 && cordLimit.Y > 0) 
            { 
                for (int cordX = 0;  cordX < cordLimit.X; cordX++)
                {
                    for (int cordY = 0; cordY < cordLimit.Y; cordY++)
                    {
                        cordTile = new Cordinate(cordX, cordY);
                        tryAddTile(sizeX * cordX, sizeY * cordY, size, cordTile);
                    }
                }
                return;
            }

            else return;
        }        
        /// <summary>
        /// 타일 클릭 시 발생 이벤트 함수입니다.
        /// 현재 선택 타일을 변경합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Click(Tile sender, EventArgs e)
        {
            if (selectedTile == null) // 선택 기물이 없을 경우
            {
                if (PieceManagement.hasPieceAt(sender.cord))
                {
                    if (PieceManagement.isTurnOf(sender.cord))
                    selectedTile = sender;
                    return;
                }
            }
            else
            {
                if(PieceManagement.tryMove(selectedTile.cord, sender.cord))
                {

                }
            }
        }
        // Work In Progress Now //
        /// <summary>
        /// 차례를 1 턴 진행하며 타일들의 표시를 업데이트합니다.
        /// </summary>
        private static void ProceedTurn()
        {
            PieceManagement.turn++;
            foreach(Tile tile in Tiles)
            {
                ChessPiece chessPiece = PieceManagement.GetPieceAt(tile.cord);
                if (chessPiece != null)
                {
                    tile.BackgroundImage = chessPiece.Image;
                }
                else
                {
                    tile. BackgroundImage = null;
                }
                
            }
        }
        
    }
    public static class PieceManagement
    {
        /// <summary>
        /// 기물 관리 시스템의 기물 목록입니다.
        /// </summary>
        private static List<ChessPiece> Pieces { get; set; } = new List<ChessPiece>();
        public static string[] Parlette = { "black", "white" };
        public static string turnColor;
        public static int turn;
        public static List<object> GameRecord = new();

        /// <summary>
        /// 기물 관리 시스템을 초기화합니다.
        /// </summary>
        public static void ClearPiece()
        {
            PieceManagement.Pieces.Clear();
        }
        /// <summary>
        /// 기물 리스트에 piece 추가를 시도합니다. 성공 시 true를 반환합니다.
        /// 좌표가 겹치는 기물이 존재하면 추가하지 않고 false를 반환합니다.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static bool tryAddPiece(ChessPiece piece)
        {
            foreach (ChessPiece chessPiece in Pieces)
            {
                if (chessPiece.Cord == piece.Cord)
                {
                    return false;
                }
            }

            Pieces.Add(piece);
            return true;
        }
        /// <summary>
        /// 지정 좌표에 기물이 있을 시 true를 반환합니다.
        /// </summary>
        /// <param name="cord"></param>
        /// <returns></returns>
        public static bool hasPieceAt(Cordinate cord)
        {
            foreach (ChessPiece piece in Pieces)
            {
                if (piece.Cord == cord)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 지정 좌표의 기물을 반환합니다. 
        /// </summary>
        /// <param name="cord"></param>
        /// <returns></returns>
        public static ChessPiece? GetPieceAt(Cordinate cord)
        {
            ChessPiece piece;
            foreach (ChessPiece item in Pieces)
            {
                if (item.Cord == cord)
                {
                    piece = item;
                    return piece;
                }
            }

            return null;
        }
        /// <summary>
        /// 현재 기물의 차례가 맞으면 true를 반환합니다.
        /// null이거나 상대 기물이면 false를 반환합니다.
        /// </summary>
        /// <param name="cord"></param>
        /// <returns></returns>
        public static bool isTurnOf(Cordinate cord)
        {
            if (!hasPieceAt(cord)) return false;
            if (turnColor == GetPieceAt(cord).color) return true;
            else return false;
        }
        /// <summary>
        /// 현재 기물의 차례가 맞으면 true를 반환합니다.
        /// null이거나 상대 기물이면 false를 반환합니다.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static bool isTurnOf(ChessPiece? piece)
        {
            if (piece == null) return false;
            if (turnColor == piece.color) return true;
            else return false;
        }
        /// <summary>
        /// 지정 좌표에 있는 기물의 인덱스를 가져옵니다. 존재하지 않을 시 -1을 반환합니다.
        /// </summary>
        /// <param name="cord"></param>
        /// <returns></returns>
        private static int GetPieceIndexAt(Cordinate cord)
        {
            int index = 0;
            for (index = 0; index < Pieces.Count; index++)
            {
                if (Pieces[index].Cord == cord) { return index; }
            }

            return -1;
        }
        /// <summary>
        /// from 좌표에 기물이 있다면 to 좌표로 이동명령을 시도합니다.
        /// 시도가 성공하면 true를 반환합니다.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>        
        public static bool tryMove(Cordinate from, Cordinate to)
        {
            ChessPiece piece = GetPieceAt(from);
            if (piece == null) return false;
            return piece.tryOrder(to);
        }
    }
}

