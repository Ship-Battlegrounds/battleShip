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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            crearTablero();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
                PictureBox pictures = sender as PictureBox;
                if (pictures != null)
                {
                    pictures.Image = Image.FromFile("./../../img/barco.jpg");
                    pictures.Tag = "B";
                    MessageBox.Show(pictures.Tag.ToString());
                    
                }
            
        }

        public void crearTablero ()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox pictures = control as PictureBox;
                if (pictures != null)
                {
                    pictures.Tag = 'A';
                    pictures.Image = Image.FromFile("./../../img/mar.jpg");
                }
            }
        }
    }
  
}
