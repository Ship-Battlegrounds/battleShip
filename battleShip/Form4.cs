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
        public Form4(Jugador j1, String tiempo)
        {
            InitializeComponent();

            //Carga datos de la partida
            this.label8.Text = j1.Nombre.ToString();
            this.label9.Text = j1.Tiros.ToString();
            this.label10.Text = j1.Aciertos.ToString();
            this.label11.Text = j1.Fallos.ToString();
            this.label12.Text = tiempo;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2.ProveedorForm2.Form2.Show();
            this.Close();
        }
    }
}
