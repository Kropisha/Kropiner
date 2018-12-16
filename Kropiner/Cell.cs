using System.Drawing;

namespace Kropiner
{
    internal class Cell
    {
        private int _sellSize; // the size of the cell

        private bool _hasMine; // if the cell has mine
        private bool _hasFlag; // if the cell has flag
        private bool _isOpen;  // if the cell is open

        private int _neighborMinesQty; //how much mines in the neighbors cells 

        private Brush _sellBackColor; //the background color of cell

        public Cell(int sellSize, bool _isOnBorder)
        {
            _sellSize = sellSize;

            _hasMine = false;
            _neighborMinesQty = 0;
            _sellBackColor = Brushes.GreenYellow;
            _hasFlag = false;
            _isOpen = false;
        }
        public void DrawSell(Graphics g, int row, int col, int cellSize, GameStatus status)
        {

            // coordinates of the upper left corner of the cell
            int x = (col) * cellSize + 1;
            int y = (row) * cellSize + 1;

            if (status == GameStatus.FAIL && _hasMine)
            {
                _sellBackColor = Brushes.Red;
            }

            g.FillRectangle(_sellBackColor, x - 1, y - 1, cellSize, cellSize);// fill the cell with color
            g.DrawRectangle(Pens.Black, x - 1, y - 1, cellSize, cellSize);  // draw the border of the cell

            if (_isOpen)
            {
                g.DrawString((_neighborMinesQty == 0 ? "" : _neighborMinesQty.ToString()),
                    new Font("Tahoma", 16, FontStyle.Regular),
                      Brushes.Indigo, x + 10, y + 7);
            }

            // set flag in the cell
            if (_hasFlag)
                Drawer.Flag(g, x, y);

            // if the game is over show mines
            if (((status == GameStatus.FAIL) || (status == GameStatus.VICTORY)) && (_hasMine))
            {
                Drawer.Mine(g, x, y);
            }
        }

        public void OpenSell()
        {
            IsOpen = true;
            SellBackColor = Brushes.Khaki;
        }

        public Brush SellBackColor
        {
            //get { return (sellBackColor); }
            set { _sellBackColor = value; }
        }
        public int NeighborMinesQty
        {
            get { return (_neighborMinesQty); }
            set { _neighborMinesQty = value; }
        }
        public bool HasMine
        {
            get { return (_hasMine); }
            set { _hasMine = value; }
        }

        public bool HasFlag
        {
            get { return (_hasFlag); }
            set { _hasFlag = value; }
        }
        public bool IsOpen
        {
            get { return (_isOpen); }
            set { _isOpen = value; }
        }
        public int SellSize
        {
            get { return (_sellSize); }
            set { _sellSize = value; }
        }

        public Form3 Form3
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
