﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kropiner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BusinessLogic.PressTheButton();
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            // this.Close();
            BusinessLogic.PressTheButton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Close();
            Storycs f = new Storycs();
            f.Show();
            BusinessLogic.PressTheButton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ratecs f = new Ratecs();
            f.Show();
            BusinessLogic.PressTheButton();
        }
    }
}
