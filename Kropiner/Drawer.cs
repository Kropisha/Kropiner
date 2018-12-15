using System.Drawing;

namespace Kropiner
{
    class Drawer
    {
        public Form3 Form3
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public struct MyHelpDrawer
        {
            public static Graphics g;
        }

        public static void Mine(Graphics g, int x, int y)
        {
            // body
            g.FillRectangle(Brushes.Green,
                x + 16, y + 26, 8, 4);
            g.FillRectangle(Brushes.Green,
                x + 8, y + 30, 24, 4);
            g.DrawPie(Pens.Black,
                x + 6, y + 28, 28, 16, 0, -180);
            g.FillPie(Brushes.Green,
                x + 6, y + 28, 28, 16, 0, -180);

            // strip on the body
            g.DrawLine(Pens.Black,
                x + 12, y + 32, x + 28, y + 32);

            // vertical "mustache"
            g.DrawLine(Pens.Black,
                x + 20, y + 22, x + 20, y + 26);

            // side "mustache"
            g.DrawLine(Pens.Black,
                x + 8, y + 30, x + 6, y + 28);
            g.DrawLine(Pens.Black,
                x + 32, y + 30, x + 34, y + 28);
        }

        // draw flag
        public static void Flag(Graphics g, int x, int y)
        {
            Point[] p = new Point[3];
            Point[] m = new Point[5];

            p[0].X = x + 4; p[0].Y = y + 4;
            p[1].X = x + 30; p[1].Y = y + 12;
            p[2].X = x + 4; p[2].Y = y + 20;
            g.FillPolygon(Brushes.Red, p);

            // line
            g.DrawLine(Pens.Black,
                x + 4, y + 4, x + 4, y + 35);

            // letter M on flag
            m[0].X = x + 8; m[0].Y = y + 14;
            m[1].X = x + 8; m[1].Y = y + 8;
            m[2].X = x + 10; m[2].Y = y + 10;
            m[3].X = x + 12; m[3].Y = y + 8;
            m[4].X = x + 12; m[4].Y = y + 14;
            g.DrawLines(Pens.White, m);
        }
    }
}
