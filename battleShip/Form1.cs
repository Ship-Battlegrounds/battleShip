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
            Barco portaAviones = new Barco(4,"Portaaviones XRT");

            barcos.Add(portaAviones);

            //Submarinos
            Barco submarino1 = new Barco(3,"Submarino X1");
            Barco submarino2 = new Barco(3,"Submarino X2");

            barcos.Add(submarino1);
            barcos.Add(submarino2);

            //Destructores
            Barco destructor1 = new Barco(2,"Destructor R1");
            Barco destructor2 = new Barco(2, "Destructor R2");
            Barco destructor3 = new Barco(2, "Destructor R3");

            barcos.Add(destructor1);
            barcos.Add(destructor2);
            barcos.Add(destructor3);

            // Fragatas
            Barco fragata1 = new Barco(1, "Fragata T1");
            Barco fragata2 = new Barco(1, "Fragata T2");
            Barco fragata3 = new Barco(1, "Fragata T3");
            Barco fragata4 = new Barco(1, "Fragata T4");

            barcos.Add(fragata1);
            barcos.Add(fragata2);
            barcos.Add(fragata3);
            barcos.Add(fragata4);

            //barcos.ForEach((a) => MessageBox.Show(a.Name.ToString()));

            // Bucle para poblar la lista de barcos


            lw_Barcos.View = View.Details;

            foreach (Barco item in barcos) {
                ListViewItem LVItem = new ListViewItem(item.Name);
                lw_Barcos.Items.Add(LVItem);
                LVItem.SubItems.Add(item.Tamaño.ToString());
            }

            lw_Barcos.Items[0].Selected = true;
        }

        private void Form1_Load(object sender, EventArgs e) {
            lbl_NombreJug.Text = j1.Nombre;
            lbl_TotalTiros.Text = j1.Tiros.ToString();
            lbl_TotalAciertos.Text = j1.Aciertos.ToString();
            lbl_TotalFallos.Text = j1.Fallos.ToString();
        }

        //  Método al clickar la celda

        private void celda_Click(object sender, EventArgs e) {
            
            PictureBox pictures = sender as PictureBox;
            if (atacar) {
                if (pictures != null) {

                    // Codigo si la celda no ha sido comprobada

                }
                else {
                    // Código si la celda ha sido comprobada ya de antes
                }
            }
            else {
                if (lw_Barcos.SelectedItems.Count == 1) {
                    // Elimina de la lista el barco una vez colocado (falta añadir comprobaciones de celda)

                    if (pictures.Tag.ToString() != "A" || pictures.Tag.ToString() != "B") {

                        MessageBox.Show(pictures.Tag as String);
                        //pictures.Tag = lw_Barcos.SelectedItems[0];
                        //lw_Barcos.SelectedItems[0].Remove();
                    
                    } else {
                        MessageBox.Show("El rango de casillas seleccionado ya está ocupado o no es suficientemente grande");
                    }     

                    // Comprueba si la lista se ha vaciado

                    if(lw_Barcos.Items.Count == 0) {
                        btn_atacar.Enabled = true;
                        btn_rotar.Enabled = false;
                        lw_Barcos.Enabled = false;
                    }
                }               
            }
        }

        public void crearTablero () {

            int x = 1;
            int y = 1;
            foreach (Control control in tableLayoutPanel1.Controls.Cast<Control>()
                                                                .OrderBy(c => Int32.Parse(c.Name.Substring(10)))) {
                PictureBox pictures = control as PictureBox;
                if (pictures != null) {
                    pictures.BackColor = Color.Transparent;

                    if (x == 11) {
                        x = 1;
                        y++;
                    }        
                    pictures.Tag = "A" + "#" + x + "#" + y;
                    x++;
                }
            }
        }

        private void btn_atacar_Click(object sender, EventArgs e)
        {
            atacar = true;
        }
    }
}
