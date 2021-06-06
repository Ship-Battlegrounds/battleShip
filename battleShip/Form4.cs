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
using WMPLib;

namespace battleShip
{
    public partial class Form4 : Form
    {
        SoundPlayer sfx = new SoundPlayer();
        WindowsMediaPlayer mainMusic = new WindowsMediaPlayer();

        public Form4(Jugador j1, String tiempo)
        {
            InitializeComponent();
            mainMusic.URL = "Sound\\bf3-lose.mp3";
            mainMusic.settings.volume = 8;
            mainMusic.settings.setMode("loop", true);

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
            sfx.SoundLocation = "Sound\\Effects\\CURSOL_SELECT.wav";
            sfx.Play();
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
            Form2.ProveedorForm2.Form2.mainMusic.controls.play();
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainMusic.controls.stop();    
        }
    }
}
