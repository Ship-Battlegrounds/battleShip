using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleShip
{
    public partial class Form3 : Form
    {
        SoundPlayer sfx = new SoundPlayer();

        public Form3()
        {
            InitializeComponent();
            this.panel1.BackColor = Color.FromArgb(80,54,54,54);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            sfx.SoundLocation = "Sound\\Effects\\CURSOL_SELECT.wav";
            sfx.Play();
            this.Cursor = Cursors.Hand;
            Button btn = sender as Button;
            Color c = Color.FromArgb(1, 250, 200, 0);
            btn.ForeColor = c;
        }


        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.ForeColor = Color.Silver;
            this.Cursor = default;
        }
    }
}
