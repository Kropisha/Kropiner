using System;
using WMPLib;

namespace Kropiner
{
    internal class BusinessLogic
    {
        public static ComplLevel ComplexityLevel;

        public struct UserIntConst
        {
            public static Random Rnd = new Random();
            public static double MinesCoeff = 0.1;
            public static int Row = 10;
            public static int Column = 10;
            public static int Mine = 10;
            public static int Width = 40;
            public static int Height = 40;
        }

        // playing field (with mines)
        private static Cell[,] _gameField = new Cell[UserIntConst.Row + 2, UserIntConst.Column + 2];

        public static void HaveGameField()
        {
            _gameField = new Cell[UserIntConst.Row + 2, UserIntConst.Column + 2];
        }

        // status of the game
        public static GameStatus Status;


        // cell indexes
        public struct CellField
        {
            public int Row;
            public int Col;
        }


        public static void SetComplLevel()
        {
            switch (ComplexityLevel)
            {
                case ComplLevel.Normal:
                {
                    UserIntConst.Row = 10;
                    UserIntConst.Column = 10;
                    break;
                }
                case ComplLevel.Medium:
                {
                    UserIntConst.Row = 15;
                    UserIntConst.Column = 15;
                    break;
                }
                case ComplLevel.Hard:
                {
                    UserIntConst.Row = 20;
                    UserIntConst.Column = 20;
                    break;
                }
            }

            UserIntConst.Mine = Convert.ToInt32(UserIntConst.Row * UserIntConst.Column * UserIntConst.MinesCoeff);
        }

        private static void InitNewGameClearField(int sellSize)
        {
            CellField cF;
            for (cF.Row = 0; cF.Row <= UserIntConst.Row; cF.Row++)
            for (cF.Col = 0; cF.Col <= UserIntConst.Column; cF.Col++)
            {
                _gameField[cF.Row, cF.Col] = new Cell(sellSize,
                    ((cF.Row == 0) || (cF.Row == UserIntConst.Row - 1) || (cF.Col == 0) ||
                     (cF.Col == UserIntConst.Column - 1)));
            }
        }

        public static void NewGame()
        {
            int qualMin = 0; // quality of mines
           // int k; // quality of mines in neighbors cells
            Status = GameStatus.BEGIN;
            // clear the field
            InitNewGameClearField(UserIntConst.Height);

            // set mines
            do
            {
                CellField cellField;
                cellField.Row = UserIntConst.Rnd.Next(UserIntConst.Row); //+1
                cellField.Col = UserIntConst.Rnd.Next(UserIntConst.Column);

                if (!_gameField[cellField.Row, cellField.Col].HasMine)
                {
                    _gameField[cellField.Row, cellField.Col].HasMine = true;

                    if (cellField.Row != 0 && cellField.Col != 0)
                    {
                        _gameField[cellField.Row - 1, cellField.Col - 1].NeighborMinesQty++;
                    }

                    if (cellField.Row != 0)
                    {
                        _gameField[cellField.Row - 1, cellField.Col].NeighborMinesQty++;
                    }

                    if (cellField.Row != 0 && cellField.Col != UserIntConst.Column - 1)
                    {
                        _gameField[cellField.Row - 1, cellField.Col + 1].NeighborMinesQty++;
                    }

                    if (cellField.Col != 0)
                    {
                        _gameField[cellField.Row, cellField.Col - 1].NeighborMinesQty++;
                    }

                    if (cellField.Col != UserIntConst.Column - 1)
                    {
                        _gameField[cellField.Row, cellField.Col + 1].NeighborMinesQty++;
                    }

                    if (cellField.Row != UserIntConst.Row - 1 && cellField.Col != 0)
                    {
                        _gameField[cellField.Row + 1, cellField.Col - 1].NeighborMinesQty++;
                    }

                    if (cellField.Row != UserIntConst.Row - 1)
                    {
                        _gameField[cellField.Row + 1, cellField.Col].NeighborMinesQty++;
                    }

                    if (cellField.Row != UserIntConst.Row - 1 && cellField.Col != UserIntConst.Column - 1)
                    {
                        _gameField[cellField.Row + 1, cellField.Col + 1].NeighborMinesQty++;
                    }

                    qualMin++;
                }


            } while (qualMin != UserIntConst.Mine);
        }

        public static void Open(int row, int col)
        {
            // output areas coordinates
         //   int x = (col - 1) * UserIntConst.Width + 1, //+1
          //      y = (row - 1) * UserIntConst.Height + 1;
            if (!_gameField[row, col].IsOpen && _gameField[row, col].HasMine)
            {
                Status = GameStatus.FAIL;
                _gameField[row, col].SellBackColor = System.Drawing.Brushes.Red;
            }
            else
            {
                if (!_gameField[row, col].IsOpen && _gameField[row, col].NeighborMinesQty == 0)
                {

                    _gameField[row, col].OpenSell();

                    // open the left, right, top and bottom adjacent cells 
                    if (col != 0) Open(row, col - 1);
                    if (row != 0) Open(row - 1, col);
                    if (col != UserIntConst.Column - 1) Open(row, col + 1);
                    if (row != UserIntConst.Row - 1) Open(row + 1, col);

                    // adjacent diagonally
                    if (row != 0 && col != 0) Open(row - 1, col - 1);
                    if (row != 0 && col != UserIntConst.Column - 1) Open(row - 1, col + 1);
                    if (row != UserIntConst.Row - 1 && col != 0) Open(row + 1, col - 1);
                    if (row != UserIntConst.Row - 1 && col != UserIntConst.Column - 1) Open(row + 1, col + 1);
                }
                else if (!_gameField[row, col].IsOpen && _gameField[row, col].NeighborMinesQty != 0)
                {
                    _gameField[row, col].OpenSell();
                }
            }

            _gameField[row, col].DrawSell(Drawer.MyHelpDrawer.g, row, col, UserIntConst.Height, Status);
        }

        public static void ShowField(System.Drawing.Graphics g, GameStatus status)
        {

            for (int row = 0; row < UserIntConst.Row; row++)
            for (int col = 0; col < UserIntConst.Column; col++)
                _gameField[row, col].DrawSell(Drawer.MyHelpDrawer.g, row, col, UserIntConst.Height, status);
        }

        internal static void PressTheButton()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer {URL = @"D:\Sounds\Sound_8669.mp3"};
            player.controls.play();
        }

        public static bool CheckIfVictory()
        {
            int nMinWithFlag = 0; // number of mines found

            for (int row = 0; row < UserIntConst.Row; row++)
            {
                for (int col = 0; col < UserIntConst.Column; col++)
                {
                    if (_gameField[row, col].HasMine && _gameField[row, col].HasFlag)
                    {
                        nMinWithFlag++;
                    }

                    if (_gameField[row, col].HasFlag && !_gameField[row, col].HasMine)
                    {
                        return false;
                    }
                }
            }

            return (nMinWithFlag == UserIntConst.Mine);

        }

        public static void AddFlag(int row, int col)
        {
            if (!_gameField[row, col].HasFlag && !_gameField[row, col].IsOpen)
            {
                _gameField[row, col].HasFlag = true;
            }
            else if (_gameField[row, col].HasFlag && !_gameField[row, col].IsOpen)
            {
                _gameField[row, col].HasFlag = false;
                _gameField[row, col].DrawSell(Drawer.MyHelpDrawer.g, row, col, UserIntConst.Height, Status);
            }

            _gameField[row, col].DrawSell(Drawer.MyHelpDrawer.g, row, col, UserIntConst.Height, Status);
        }

        public Form3 Form3
        {
            get { throw new NotImplementedException(); }
        }
    }
}
    


