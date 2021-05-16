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
    public partial class Form1 : Form {

        WindowsMediaPlayer mainMusic = new WindowsMediaPlayer();
        WindowsMediaPlayer disparar = new WindowsMediaPlayer();
        WindowsMediaPlayer acertar = new WindowsMediaPlayer();
        WindowsMediaPlayer fallar = new WindowsMediaPlayer();

        Jugador j1 = new Jugador("Ricardo");
        bool atacar;
        List<Barco> barcos = new List<Barco> { };

        // Constructor
        public Form1() {
            InitializeComponent();
            crearTablero();

            //Inicializar música
            mainMusic.URL = "mainWellerman.mp3";
            mainMusic.settings.volume = 10;
            mainMusic.settings.setMode("loop", true);




            //Creación de los barcos
            //Porta aviones
            Barco portaAviones = new Barco(4,"P");

            barcos.Add(portaAviones);

            //Submarinos
            Barco submarino1 = new Barco(3,"S");
            Barco submarino2 = new Barco(3,"S2");

            barcos.Add(submarino1);
            barcos.Add(submarino2);

            //Destructores
            Barco destructor1 = new Barco(2,"D");
            Barco destructor2 = new Barco(2, "D2");
            Barco destructor3 = new Barco(2, "D3");

            barcos.Add(destructor1);
            barcos.Add(destructor2);
            barcos.Add(destructor3);

            //Fragatas
            Barco fragata1 = new Barco(1, "F");
            Barco fragata2 = new Barco(1, "F2");
            Barco fragata3 = new Barco(1, "F3");
            Barco fragata4 = new Barco(1, "F4");

            barcos.Add(fragata1);
            barcos.Add(fragata2);
            barcos.Add(fragata3);
            barcos.Add(fragata4);


           //barcos.ForEach((a) => MessageBox.Show(a.ToString()));

            tableLayoutPanel1.BackgroundImage = Image.FromFile("./../../img/water.gif");
        }

        private void Form1_Load(object sender, EventArgs e) {
            lbl_NombreJug.Text = j1.Nombre;
            lbl_TotalTiros.Text = j1.Tiros.ToString();
            lbl_TotalAciertos.Text = j1.Aciertos.ToString();
            lbl_TotalFallos.Text = j1.Fallos.ToString();

        }

        private void celda_Click(object sender, EventArgs e) {
            
            PictureBox pictures = sender as PictureBox;
            if (atacar)
            {
                if (pictures != null)
                {


                }
            } else
            {
                if (pictures != null)
                {
                    pictures.Tag = "B";

                }
            }
        }

        public void crearTablero () {
            foreach (Control control in tableLayoutPanel1.Controls) {
                PictureBox pictures = control as PictureBox;
                if (pictures != null) {
                    pictures.BackColor = Color.Transparent;
                    pictures.Tag = "A";
                    //pictures.Image = Properties.Resources.mar;
                }
            }
        }

        private void btn_atacar_Click(object sender, EventArgs e)
        {
            atacar = true;
        }
    }
}
