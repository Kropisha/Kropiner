using System;
using System.Drawing;
using WMPLib;
using System.Windows.Forms;

using Kropiner.DataSet1TableAdapters;
using Kropiner.Properties;

namespace Kropiner
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            GetField();
          
        }
        private int _seconds;
        private int _minutes;

        private void timer1_Tick(object sender, EventArgs e)
        {       
            if (_seconds == 60)
            {
                _minutes++;
                _seconds = 0;
                if (_minutes >= 60)
                {
                    timer1.Stop();
                    MessageBox.Show(Resources.LoseMassage, Resources.LooseStatus);
                }
            }
                    
            label3.Text = _minutes.ToString();
            label5.Text = _seconds.ToString();
            _seconds++;
        }

        public void GetField()
        {
           
            BusinessLogic.SetComplLevel();
            label6.Text = Form2.name;
            BusinessLogic.HaveGameField();
            ClientSize = new Size(BusinessLogic.UserIntConst.Width * BusinessLogic.UserIntConst.Column + 1,
                BusinessLogic.UserIntConst.Height * BusinessLogic.UserIntConst.Row + 1);
            panel1.ClientSize = new Size(BusinessLogic.UserIntConst.Width * BusinessLogic.UserIntConst.Column + 1, 
                BusinessLogic.UserIntConst.Height * BusinessLogic.UserIntConst.Row +1);

            BusinessLogic.NewGame();

            Drawer.MyHelpDrawer.g = panel1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            _seconds = 0;
            _minutes = 0;

            BusinessLogic.PressTheButton();
            BusinessLogic.NewGame();
            BusinessLogic.ShowField(Drawer.MyHelpDrawer.g, BusinessLogic.Status);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            BusinessLogic.ShowField(Drawer.MyHelpDrawer.g, BusinessLogic.Status);
        }
      
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            Anchor = AnchorStyles.None;
            timer1.Start();
            int time = (_minutes * 60) + _seconds;
            KROPINERPROTableAdapter editKrop = new KROPINERPROTableAdapter();

            // game over
            if (BusinessLogic.Status == GameStatus.VICTORY || BusinessLogic.Status == GameStatus.FAIL)
            {
                return;
            }

            // first click
            if (BusinessLogic.Status == GameStatus.BEGIN)
            {
                BusinessLogic.Status = GameStatus.ON;
            }

            int row, col, x, y;
            MouseToCell(e, out row, out col, out x, out y);

            // left mouse button click
            if (e.Button == MouseButtons.Left)
            {
                BusinessLogic.PressTheButton();
                BusinessLogic.Open(row - 1, col - 1);
            }

            if (BusinessLogic.Status == GameStatus.FAIL)
            {
                BusinessLogic.ShowField(Drawer.MyHelpDrawer.g, BusinessLogic.Status);
                timer1.Stop();
                player.URL = @"D:\Sounds\Sound_6332.mp3";
                player.controls.play();
                MessageBox.Show(Resources.Looser, Resources.Motivate);

                // redraw form
                panel1.Invalidate();
            }

            // right click
            if (e.Button == MouseButtons.Right)
            {
                BusinessLogic.PressTheButton();
                BusinessLogic.AddFlag(row - 1, col - 1);
                if (BusinessLogic.CheckIfVictory())
                {   
                    BusinessLogic.Status = GameStatus.VICTORY;
                    BusinessLogic.ShowField(Drawer.MyHelpDrawer.g, BusinessLogic.Status);
                    timer1.Stop();
                    player.URL = @"D:\Sounds\Sound_8464.mp3";
                    player.controls.play();
                    MessageBox.Show(Resources.Victory, Resources.Praise);

                    editKrop.InsertKroPro(Form2.name, time);              
                }

            }
        }

        private static void MouseToCell(MouseEventArgs e, out int row, out int col, out int x, out int y)
        {
            // convert mouse coordinates to indexes
            // cells of the field which was clicked;
            // (e.X, e.Y) - coordinates of the shape point,
            // in which the mouse button was pressed;            
            row = e.Y / BusinessLogic.UserIntConst.Height + 1;
            col = e.X / BusinessLogic.UserIntConst.Width + 1;

            // output area coordinates
                        x = (col - 1) * BusinessLogic.UserIntConst.Width + 1;
            y = (row - 1) * BusinessLogic.UserIntConst.Height + 1;
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessLogic.ComplexityLevel = ComplLevel.Normal;
            GetField();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessLogic.ComplexityLevel = ComplLevel.Hard;
            GetField();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessLogic.ComplexityLevel = ComplLevel.Medium;
            GetField();
        }
    }
}
