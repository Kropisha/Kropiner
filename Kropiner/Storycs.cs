using System;
using System.Windows.Forms;
using WMPLib;

namespace Kropiner
{
    public partial class Storycs : Form
    {
        public Storycs()
        {
            InitializeComponent();
            
        }

        public Form2 Form2
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        private void Storycs_Load(object sender, EventArgs e)
        {
            NewMethod();
        }

        private static void NewMethod()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = @"D:\Sounds\Space_Station_Experience.mp3";
            player.controls.play();
        }

        private void Storycs_Shown(object sender, EventArgs e)
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = @"D:\Sounds\Space_Station_Experience.mp3";
            player.controls.play();
        }
    }
}
