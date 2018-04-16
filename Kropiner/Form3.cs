using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using WMPLib;
using System.Windows.Forms;

using Kropiner.DataSet1TableAdapters;

namespace Kropiner
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            GetField();
          
        }
        private int seconds = 0;
        private int minutes = 0;
        Form2 f = new Form2();
        private void timer1_Tick(object sender, EventArgs e)
        {       
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
                if (minutes >= 60)
                {
                    timer1.Stop();
                    MessageBox.Show("Game over! Too much time!!", "Loser");
                }
            }
                    
            label3.Text = minutes.ToString();
            label5.Text = seconds.ToString();
            seconds++;
        }
        public void GetField()
        {
           
            BL.SetComplLevel();
            label6.Text = Form2.name;
            BL.haveGameField();
            this.ClientSize = new Size(BL.userIntConst.width * BL.userIntConst.Column + 1, BL.userIntConst.height * BL.userIntConst.Row + 1);
            this.panel1.ClientSize = new Size(BL.userIntConst.width * BL.userIntConst.Column + 1, BL.userIntConst.height * BL.userIntConst.Row +1); //+ groupBox1.Height + 1

            BL.newGame();

            Drawer.myHelpDrawer.g = panel1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            seconds = 0;
            minutes = 0;
            Form2 f = new Form2();
            BL.PressTheButton();
            BL.newGame();
            BL.ShowField(Drawer.myHelpDrawer.g, BL.status);
            //timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            BL.ShowField(Drawer.myHelpDrawer.g, BL.status);
        }
      
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            this.Anchor = AnchorStyles.None;
            timer1.Start();
            string need = label3.Text + label5.Text;
            int time = (minutes * 60) + seconds;
            KROPINERPROTableAdapter editKrop = new KROPINERPROTableAdapter();

            // игра завершена
            if (BL.status == BL.GameStatus.VICTORY || BL.status == BL.GameStatus.FAIL)
            {
                return;
            }

            // первый щелчок
            if (BL.status == BL.GameStatus.BEGIN)
            {
                BL.status = BL.GameStatus.ON;
            }

            int row, col, x, y;
            MouseToCell(e, out row, out col, out x, out y);

            // щелчок левой кнопки мыши
            if (e.Button == MouseButtons.Left)
            {
                BL.PressTheButton();
                BL.open(row - 1, col - 1);
            }

            if (BL.status == BL.GameStatus.FAIL)
            {
                BL.ShowField(Drawer.myHelpDrawer.g, BL.status);
                timer1.Stop();
                // timer1 = 0;
                player.URL = @"D:\Sounds\Sound_6332.mp3";
                player.controls.play();
                MessageBox.Show("Looser..", "Never give up");

                // перерисовать форму
                this.panel1.Invalidate();
            }

            // щелчок правой кнопки мыши
            if (e.Button == MouseButtons.Right)
            {
                BL.PressTheButton();
                BL.AddFlag(row - 1, col - 1);
                if (BL.CheckIfVictory())
                {
                    DataSet1TableAdapters.KROPINERPROTableAdapter NN = new DataSet1TableAdapters.KROPINERPROTableAdapter();
                    
                    BL.status = BL.GameStatus.VICTORY;
                    BL.ShowField(Drawer.myHelpDrawer.g, BL.status);
                    timer1.Stop();
                    player.URL = @"D:\Sounds\Sound_8464.mp3";
                    player.controls.play();
                    MessageBox.Show("It's victory!! Get it!", "You are good at this");
                    //if(NN.GetName().ToString() == Form2.name) //!!!!
                    //{
                    //    if(time < )

                    //}
                    editKrop.InsertKroPro(Form2.name, time);
                    
                }

            }
        }

        private static void MouseToCell(MouseEventArgs e, out int row, out int col, out int x, out int y)
        {
            // преобразуем координаты мыши в индексы
            // клетки поля, в которой был сделан щелчок;
            // (e.X, e.Y) - координаты точки формы,
            // в которой была нажата кнопка мыши;            
            row = (int)(e.Y / BL.userIntConst.height) + 1;
            col = (int)(e.X / BL.userIntConst.width) + 1;

            // координаты области вывода
            x = (col - 1) * BL.userIntConst.width + 1;
            y = (row - 1) * BL.userIntConst.height + 1;
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BL._complLevel = BL.ComplLevel.Normal;
            GetField();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BL._complLevel = BL.ComplLevel.Hard;
            GetField();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BL._complLevel = BL.ComplLevel.Medium;
            GetField();
        }
    }
}
