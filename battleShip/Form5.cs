﻿using System;
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
        
        public Form5(Jugador j1, String tiempo)
        {
            InitializeComponent();
            mainMusic.URL = "Sound\\battlefield-3-victory-ost.mp3";
            mainMusic.settings.volume = 20;
            mainMusic.settings.setMode("loop", true);

            //Carga datos de la partida
            this.label8.Text = j1.Nombre.ToString();
            this.label9.Text = j1.Tiros.ToString();
            this.label10.Text = j1.Aciertos.ToString();
            this.label11.Text = j1.Fallos.ToString();
            this.label12.Text = tiempo;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Color c = Color.FromArgb(1, 250, 200, 0);
            btn.ForeColor = c;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
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
