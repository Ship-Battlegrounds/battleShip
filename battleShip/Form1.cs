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

        //Pruebas

        WindowsMediaPlayer mainMusic = new WindowsMediaPlayer();
        WindowsMediaPlayer disparar = new WindowsMediaPlayer();
        WindowsMediaPlayer acertar = new WindowsMediaPlayer();
        WindowsMediaPlayer fallar = new WindowsMediaPlayer();

        Jugador j1 = new Jugador("Ricardo");
        bool atacar;
        bool isVertical = true;
        List<Barco> barcos = new List<Barco> { };
        
        
        // Constructor
        public Form1() {
            InitializeComponent();
            crearTablero();
            //Inicializar música

            /*
             mainMusic.URL = "mainWellerman.mp3";
             mainMusic.settings.volume = 10;
             mainMusic.settings.setMode("loop", true);
            */
            crearBarcos();


           

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
            String[] tagPicture = pictures.Tag.ToString().Split('#');

            // for (int i = 0; i < tagPicture.Length; i++) MessageBox.Show(tagPicture[i]);            
            MessageBox.Show(pictures.Tag.ToString());
           
            if (atacar) {
                if (pictures != null) {

                    // Codigo si la celda no ha sido comprobada
                    return;
                }
                else {
                    // Código si la celda ha sido comprobada ya de antes
                }
            } else {
                if (lw_Barcos.SelectedItems.Count == 1) {

                    // Comprueba si la casilla es agua para poder situar un barco

                    if (tagPicture[0] == "A") {

                        int tamaño = Convert.ToInt32(lw_Barcos.SelectedItems[0].SubItems[1].Text); // Selecciona el tamaño del barco de la lista

                        if (!isVertical) {

                            int espacio = Convert.ToInt32(tagPicture[1]);
                            bool hayEspacio = (espacio + tamaño) < 12; // Devuelve true si hay espacio para los barcos

                            // Comprueba si hay espacio horizontal suficiente, y si lo hay, elimina al barco de la lista (Falta añadir barco al tablero. De momento solo añade la primera parte)

                            if (hayEspacio) {

                                //Asigna el barco a todas las posiciones
                                
                                asignarElBarco(lw_Barcos.SelectedItems[0].Text, tamaño, Convert.ToInt32(tagPicture[1]), Convert.ToInt32(tagPicture[2]));

                                // pictures.Tag = "B#" + tagPicture[1] + tagPicture[2] + lw_Barcos.SelectedItems[0].Text;
                              //  lw_Barcos.SelectedItems[0].Remove();
                              //  MessageBox.Show(pictures.Tag.ToString());
                            } else
                            {
                                //MessageBox.Show("No hay espacio horizontal para este barco en las casillas seleccionadas");

                            }

                        } else if (isVertical){

                            //MessageBox.Show("\nEspacio: " + pictures.Tag.ToString().Split('#')[2] + " \nTamaño: " + lw_Barcos.SelectedItems[0].SubItems[1].Text);
                            int espacio = Convert.ToInt32(tagPicture[2]);
                            bool hayEspacio = (espacio + tamaño < 12); // Devuelve true si hay espacio para los barcos

                            // Comprueba si hay espacio vertical suficiente, y si lo hay, elimina al barco d ela lista (Falta añadir barco al tablero. De momento solo añade la primera parte)

                            if (hayEspacio) {

                                //Asigna el barco a todas las posiciones

                                asignarElBarco(lw_Barcos.SelectedItems[0].Text, tamaño, Convert.ToInt32(tagPicture[1]), espacio);

                                // pictures.Tag = "B#" + tagPicture[1] + tagPicture[2] + lw_Barcos.SelectedItems[0].Text;

                                //lw_Barcos.SelectedItems[0].Remove();
                            
                            } else {
                     //           MessageBox.Show("No hay suficiente espacio vertical para este barco en las casillas seleccionadas");
                            }
                        }
                    }/**/ else
                    {
                         MessageBox.Show("El rango de casillas seleccionado ya está ocupado");

                    }
                } 

                    // Comprueba si la lista se ha vaciado

                if (lw_Barcos.Items.Count == 0) {
                        btn_atacar.Enabled = true;
                        btn_rotar.Enabled = false;
                        lw_Barcos.Enabled = false;
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
                    //String[] arr = { "A", x.ToString(), y.ToString() };
                    pictures.Tag = "A" + "#" + x + "#" + y;
                    x++;
                }
            }
        }

        private void btn_atacar_Click(object sender, EventArgs e) {
            atacar = true;
        }

        private void btn_rotar_Click(object sender, EventArgs e) {
            if (isVertical)
                isVertical = false;
            else
                isVertical = true;

        }

        private void crearBarcos()
        {
            //Creación de los barcos
            //Porta aviones
            Barco portaAviones = new Barco(4, "Portaaviones XRT");

            barcos.Add(portaAviones);

            //Submarinos
            Barco submarino1 = new Barco(3, "Submarino X1");
            Barco submarino2 = new Barco(3, "Submarino X2");

            barcos.Add(submarino1);
            barcos.Add(submarino2);

            //Destructores
            Barco destructor1 = new Barco(2, "Destructor R1");
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
        }

        public void asignarElBarco(String nombre, int tamaño, int valorX, int valorY)
        {
            
            //Sacar todas las posisciones de x que necesitamos
            // valoresY.Add(tamaño);
            if (isVertical)
            {
                List<int> valoresY = new List<int> { };

                for (int i = 0; i < tamaño; i++)
                {
                    valoresY.Add(valorY + i);
                }

                //Comprobar si cabe el barco
                if (!comprobarSiCabeElBarco(valoresY, valorX, valorY)) return;
                

                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (picture != null)
                    {
                        for (int i = 0; i < valoresY.Count; i++)
                        {
                            if (Convert.ToInt32(tagPicture[2]) == valoresY[i] && Convert.ToInt32(tagPicture[1]) == valorX)
                            {
                                picture.Tag = nombre + "#" + tagPicture[1] + "#" + tagPicture[2];
                                Image img = Image.FromFile("../../img/barco.jpg");
                                picture.Image = img;
                            }
                        }
                    }
                }
                lw_Barcos.SelectedItems[0].Remove();
            }
            else
            {
                List<int> valoresX = new List<int> { };

                for (int i = 0; i < tamaño; i++)
                {
                    valoresX.Add(valorX + i);
                }

                //Comprobar si cabe el barco
                if (!comprobarSiCabeElBarco(valoresX, valorX, valorY)) return;

                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (picture != null)
                    {

                        for (int i = 0; i < valoresX.Count; i++)
                        {
                            if (Convert.ToInt32(tagPicture[1]) == valoresX[i] && Convert.ToInt32(tagPicture[2]) == valorY)
                            {
                                picture.Tag = nombre + "#" + tagPicture[1] + "#" + tagPicture[2];
                                Image img = Image.FromFile("../../img/barco.jpg");
                                picture.Image = img;
                            }
                        }
                    }
                }
                lw_Barcos.SelectedItems[0].Remove();
            }
        }

        public bool comprobarSiCabeElBarco(List<int> valores, int valorX, int valorY)
        {
            if (isVertical)
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (picture != null)
                    {
                        
                        for (int i = 0; i < valores.Count; i++)
                        {
                            if (Convert.ToInt32(tagPicture[2]) == valores[i] && Convert.ToInt32(tagPicture[1]) == valorX && tagPicture[0] != "A")
                            {
                                MessageBox.Show("El barco no cabe verticalmente");
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (picture != null)
                    {

                        for (int i = 0; i < valores.Count; i++)
                        {
                            if (Convert.ToInt32(tagPicture[1]) == valores[i] && Convert.ToInt32(tagPicture[2]) == valorY && tagPicture[0] != "A")
                            {
                                MessageBox.Show("El barco no cabe horizontalmente");
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }



       
    }
}
