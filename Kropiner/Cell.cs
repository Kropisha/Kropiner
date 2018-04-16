using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kropiner
{
    class Cell
    {
        private int sellSize; //размер клетки

        private bool hasMine; //есть ли в клетке мина
        private bool hasFlag; //стоит ли в клетке флажок
        private bool isOpen;  //открыта ли клетка

        private int neibourMinesQty; //сколько в сумме мин в соседних клетках 

        private Brush sellBackColor; //цвет фона клетки

        public Cell(int _sellSize, bool _isOnBorder)
        {
            sellSize = _sellSize;

            hasMine = false;
            neibourMinesQty = 0;
            sellBackColor = Brushes.GreenYellow;
            hasFlag = false;
            isOpen = false;
        }
        public void DrawSell(Graphics g, int row, int col, int CellSize, BL.GameStatus status)
        {

            // координаты левого верхнего угла клетки
            int x = (col) * CellSize + 1;
            int y = (row) * CellSize + 1;

            if (status == BL.GameStatus.FAIL && this.hasMine)
            {
                this.sellBackColor = Brushes.Red;
            }

            g.FillRectangle(sellBackColor, x - 1, y - 1, CellSize, CellSize);// рисуем фон клетки
            g.DrawRectangle(Pens.Black, x - 1, y - 1, CellSize, CellSize);  // рисуем границу клетки

            if (this.isOpen)
            {
                g.DrawString((this.neibourMinesQty == 0 ? "" : this.neibourMinesQty.ToString()),
                    new Font("Tahoma", 16, System.Drawing.FontStyle.Regular),
                      Brushes.Indigo, x + 10, y + 7);
            }

            // в клетке поставлен флаг
            if (this.hasFlag)
                Drawer.flag(g, x, y);

            //if (this.hasMine)
            //{
            //  this.DrawMine(g, x, y);
            //}

            // если игра завершена поражением, показываем мины
            if (((status == BL.GameStatus.FAIL) || (status == BL.GameStatus.VICTORY)) && (this.hasMine))
            {
                Drawer.mina(g, x, y);
            }
        }

        public void OpenSell()
        {
            this.IsOpen = true;
            this.SellBackColor = Brushes.Khaki;
        }

        public Brush SellBackColor
        {
            //get { return (sellBackColor); }
            set { sellBackColor = value; }
        }
        public int NeibourMinesQty
        {
            get { return (neibourMinesQty); }
            set { neibourMinesQty = value; }
        }
        public bool HasMine
        {
            get { return (hasMine); }
            set { hasMine = value; }
        }

        public bool HasFlag
        {
            get { return (hasFlag); }
            set { hasFlag = value; }
        }
        public bool IsOpen
        {
            get { return (isOpen); }
            set { isOpen = value; }
        }
        public int SellSize
        {
            get { return (sellSize); }
            set { sellSize = value; }
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
