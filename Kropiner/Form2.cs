using System;
using System.Collections.Generic;
using WMPLib;
using System.Windows.Forms;

namespace Kropiner
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        

        public static string name;
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                WindowsMediaPlayer player = new WindowsMediaPlayer();
                player.URL = @"D:\Sounds\Sound_19638.mp3";
                player.controls.play();
                MessageBox.Show("I insist on acquaintance!");
            }
            else
            {
                name = textBox1.Text.ToString();
                Form3 f = new Form3();
                f.Show();
                BL.PressTheButton();
                this.Close();          
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
