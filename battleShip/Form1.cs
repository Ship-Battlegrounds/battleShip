﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleShip {
    public partial class Form1 : Form {

        Jugador j1 = new Jugador("Ricardo");

        // Constructor
        public Form1() {
            InitializeComponent();
            crearTablero();
        }

        private void Form1_Load(object sender, EventArgs e) {
            lbl_NombreJug.Text = j1.Nombre;
            lbl_TotalTiros.Text = j1.Tiros.ToString();
            lbl_TotalAciertos.Text = j1.Aciertos.ToString();
            lbl_TotalFallos.Text = j1.Fallos.ToString();

        }

        private void celda_Click(object sender, EventArgs e) {
            
            PictureBox pictures = sender as PictureBox;
            if (pictures != null) {
                pictures.Tag = "B";
                pictures.Image = Properties.Resources.barco;
                    
                MessageBox.Show(pictures.Tag.ToString());       
            }   
        }

        public void crearTablero () {
            foreach (Control control in tableLayoutPanel1.Controls) {
                PictureBox pictures = control as PictureBox;
                if (pictures != null) {
                    pictures.Tag = "A";
                    pictures.Image = Properties.Resources.mar;
                }
            }
        }
    }
}
