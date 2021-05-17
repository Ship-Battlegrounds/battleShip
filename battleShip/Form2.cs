using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleShip {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, EventArgs e) {

            /*
            Form1 f1 = new Form1();
            f1.Show();
            foreach (Form frm in Application.OpenForms) {
                if (frm.GetType() == typeof(Form1)) {
                    frm.Show();
                    break;
                }
            }
            */
            this.Close();
        }

        private void btnInstrucciones_Click(object sender, EventArgs e) {
            MessageBox.Show("Destruye los barcos wey");
        }

        private void btnSalir_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
