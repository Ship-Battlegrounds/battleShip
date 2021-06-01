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
      //  public static bool cerrado = false;

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
            mainMusic.controls.stop();
            this.Close();
        }
      
        private void btnInstrucciones_Click(object sender, EventArgs e) {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }
      
        private void btnSalir_Click(object sender, EventArgs e) {
          //  cerrado = true;
            Application.Exit();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //cerrado = true;
        }

    }
}
