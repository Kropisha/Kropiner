using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Kropiner
{
    class BL
    {
        public enum GameStatus { BEGIN, ON, VICTORY, FAIL }
        public enum ComplLevel
        {
            Normal,
            Medium,
            Hard
        }

        public static ComplLevel _complLevel; 
        public struct userIntConst
        {
            // инициализация генератора случайных чисел
            public static Random rnd = new Random();
            public static double _minesCoeff = 0.1;
            public static int Row = 10;
            public static int Column = 10;
            public static int Mine = 10;
            public static int width = 40;
            public static int height = 40;
        }


        // игровое (минное) поле
        private  static Cell[,] GameField = new Cell[ userIntConst.Row + 2 , userIntConst.Column + 2 ]; 

        public static void haveGameField()
        {
            GameField = new Cell[userIntConst.Row + 2, userIntConst.Column + 2];
        }

        // статус игры
        public static GameStatus status;


        // индексы клетки
        public struct cellField
        {
            public int row;
            public int col;
        }


        public static void SetComplLevel()
        {
            switch (_complLevel)
            {
                case ComplLevel.Normal:
                    {
                        userIntConst.Row = 10;
                        userIntConst.Column = 10;
                        break;
                    }
                case ComplLevel.Medium:
                    {
                        userIntConst.Row = 15;
                        userIntConst.Column = 15;
                        break;
                    }
                case ComplLevel.Hard:
                    {
                        userIntConst.Row = 20;
                        userIntConst.Column = 20;
                        break;
                    }
            }
            userIntConst.Mine = Convert.ToInt32(userIntConst.Row * userIntConst.Column * userIntConst._minesCoeff);
        }
        private static void InitNewGameClearField(int _sellsize)
        {
            cellField cF;
            for (cF.row = 0; cF.row <= userIntConst.Row; cF.row++)
                for (cF.col = 0; cF.col <= userIntConst.Column; cF.col++)
                {
                    GameField[cF.row, cF.col] = new Cell(_sellsize,
                    ((cF.row == 0) || (cF.row == userIntConst.Row - 1) || (cF.col == 0) || (cF.col == userIntConst.Column - 1)) ? true : false);
                }
        }

        public static void newGame()
        {
            cellField cF;
            int qualMin = 0;       // количество поставленных мин
            int k;           // кол-во мин в соседних клетках
            status = GameStatus.BEGIN;
            // очистить поле
            InitNewGameClearField(userIntConst.height);
            
            // расставим мины
            do
            {
                cF.row = userIntConst.rnd.Next(userIntConst.Row) ;//+1
                cF.col = userIntConst.rnd.Next(userIntConst.Column) ;

                if (!GameField[cF.row, cF.col].HasMine)
                {
                    GameField[cF.row, cF.col].HasMine = true;

                    if (cF.row != 0 && cF.col != 0)
                    {
                        GameField[cF.row - 1, cF.col - 1].NeibourMinesQty++;
                    }
                    if (cF.row != 0)
                    {
                        GameField[cF.row - 1, cF.col].NeibourMinesQty++;
                    }
                    if (cF.row != 0 && cF.col != userIntConst.Column - 1)
                    {
                        GameField[cF.row - 1, cF.col + 1].NeibourMinesQty++;
                    }
                    if (cF.col != 0)
                    {
                        GameField[cF.row, cF.col - 1].NeibourMinesQty++;
                    }
                    if (cF.col != userIntConst.Column - 1)
                    {
                        GameField[cF.row, cF.col + 1].NeibourMinesQty++;
                    }
                    if (cF.row != userIntConst.Row - 1 && cF.col != 0)
                    {
                        GameField[cF.row + 1, cF.col - 1].NeibourMinesQty++;
                    }
                    if (cF.row != userIntConst.Row - 1)
                    {
                        GameField[cF.row + 1, cF.col].NeibourMinesQty++;
                    }
                    if (cF.row != userIntConst.Row - 1 && cF.col != userIntConst.Column - 1)
                    {
                        GameField[cF.row + 1, cF.col + 1].NeibourMinesQty++;
                    }

                    qualMin++;
                }
               

            }
            while (qualMin != userIntConst.Mine);
            
            //for (cF.row = 1; cF.row <= userIntConst.Row; cF.row++)
            //{
            //    for (cF.col = 1; cF.col <= userIntConst.Column; cF.col++)
            //    {

            //  GameField[cF.row, cF.col].NeibourMinesQty = qualMin;
            //    }
            //}

        }
    
        public static void open(int row, int col)
        {
            // координаты области вывода
            int x = (col - 1) * userIntConst.width +1,//+1
                y = (row - 1) * userIntConst.height +1;
            if (!GameField[row, col].IsOpen && GameField[row, col].HasMine)
            {
                status = GameStatus.FAIL;
                GameField[row, col].SellBackColor = System.Drawing.Brushes.Red;
            }
            else
            {
                if (!GameField[row, col].IsOpen && GameField[row, col].NeibourMinesQty == 0)
                {

                    GameField[row, col].OpenSell();

                    // открыть примыкающие клетки слева, справа, сверху, снизу
                    if (col != 0) open(row, col - 1);
                    if (row != 0) open(row - 1, col);
                    if (col != userIntConst.Column - 1) open(row, col + 1);
                    if (row != userIntConst.Row - 1) open(row + 1, col);

                    //примыкающие диагонально
                    if (row != 0 && col != 0) open(row - 1, col - 1);
                    if (row != 0 && col != userIntConst.Column - 1) open(row - 1, col + 1);
                    if (row != userIntConst.Row - 1 && col != 0) open(row + 1, col - 1);
                    if (row != userIntConst.Row - 1 && col != userIntConst.Column - 1) open(row + 1, col + 1);
                }
                else
                  if (!GameField[row, col].IsOpen && GameField[row, col].NeibourMinesQty != 0)
                {
                    GameField[row, col].OpenSell();
                }
            }
            GameField[row, col].DrawSell(Drawer.myHelpDrawer.g, row, col, userIntConst.height, status);
        }

        public static void ShowField(System.Drawing.Graphics g , GameStatus status)
        {

            for (int row = 0; row < userIntConst.Row; row++)
                for (int col = 0; col < userIntConst.Column; col++)
                    GameField[row, col].DrawSell(Drawer.myHelpDrawer.g, row, col, userIntConst.height, status);
        }

        internal static void PressTheButton()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = @"D:\Sounds\Sound_8669.mp3";
            player.controls.play();
        }
        public static bool CheckIfVictory()
        {
            int nMinWithFlag = 0;  // кол-во правильно найденных мин

            for (int row = 0; row < userIntConst.Row; row++)
            {
                for (int col = 0; col < userIntConst.Column; col++)
                {
                    if (GameField[row, col].HasMine && GameField[row, col].HasFlag)
                    {
                        nMinWithFlag++;                    
                    }
                    if (GameField[row, col].HasFlag && !GameField[row, col].HasMine)
                    {
                        return false;
                    }
                }
            }
           
            return (nMinWithFlag == userIntConst.Mine ? true : false);
        
        }
        public static void AddFlag(int row, int col)
        {
            if (!GameField[row, col].HasFlag && !GameField[row, col].IsOpen)
            {
                GameField[row, col].HasFlag = true;
            }
            else
                        if (GameField[row, col].HasFlag && !GameField[row, col].IsOpen)
            {
                GameField[row, col].HasFlag = false;
                GameField[row, col].DrawSell(Drawer.myHelpDrawer.g, row, col, userIntConst.height, status);
            }

            GameField[row, col].DrawSell(Drawer.myHelpDrawer.g, row, col, userIntConst.height, status);
        }

        public Form3 Form3
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
        }
    


