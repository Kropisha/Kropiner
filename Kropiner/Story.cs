using System;
using System.Windows.Forms;
using WMPLib;

namespace Kropiner
{
    public partial class Story : Form
    {
        public Story()
        {
            InitializeComponent();
            
        }

        public Form2 Form2
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private void Story_Load(object sender, EventArgs e)
        {
            NewMethod();
        }

        private static void NewMethod()
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = @"D:\Sounds\Space_Station_Experience.mp3";
            player.controls.play();
        }

        private void Story_Shown(object sender, EventArgs e)
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.URL = @"D:\Sounds\Space_Station_Experience.mp3";
            player.controls.play();
        }
    }
}
