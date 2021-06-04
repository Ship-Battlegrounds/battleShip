using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace battleShip
{
    public partial class Form5 : Form
    {

        WindowsMediaPlayer mainMusic = new WindowsMediaPlayer();
        
        public Form5(Jugador j1)
        {
            InitializeComponent();
            mainMusic.URL = "Sound\\battlefield-3-victory-ost.mp3";
            mainMusic.settings.volume = 20;
            mainMusic.settings.setMode("loop", true);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("./../../img/win.gif");
        }
    }
}
