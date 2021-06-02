using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

using System.Windows.Forms;

namespace battleShip {
    public partial class Form2 : Form {
        WindowsMediaPlayer mainMusic = new WindowsMediaPlayer();

        public Form2() {
            InitializeComponent();
              mainMusic.URL = "mainWellerman.mp3";
              mainMusic.settings.volume = 20;
              mainMusic.settings.setMode("loop", true);

            //Aplicar cursor
            //this.Cursor = new Cursor("./../../icons/mira.ico");
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

            Form1 f1 = new Form1();
            f1.Show();
            mainMusic.controls.stop();
            ProveedorForm2.Form2.Hide();
        }
      
        private void btnInstrucciones_Click(object sender, EventArgs e) {
            Form3 f3 = new Form3();
            f3.Show();
        }
      
        private void btnSalir_Click(object sender, EventArgs e) {
            this.Close();
        }

        public class ProveedorForm2
        {
            public static Form2 Form2
            {
                get
                {
                    if (_form2 == null)
                    {
                        _form2 = new Form2();
                    }
                    return _form2;
                }
            }
            private static Form2 _form2;
        }

    }
}