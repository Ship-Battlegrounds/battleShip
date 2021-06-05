using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleShip
{
    public partial class Form4 : Form
    {
        public Form4(Jugador j1)
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("./../../img/defeat.jpg");
        }

        private void btnMouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Color c = Color.FromArgb(1, 250, 200, 0);
            btn.ForeColor = c;
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.ForeColor = Color.Silver;
        }
    }
}
