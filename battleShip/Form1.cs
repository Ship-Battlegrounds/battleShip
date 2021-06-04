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
using System.Drawing;

namespace battleShip {
    public partial class Form1 : Form
    {

        //Variable para pruebas
        String text = "";

        //Reproductores de sonido
        WindowsMediaPlayer mainMusic = new WindowsMediaPlayer();
        WindowsMediaPlayer disparar = new WindowsMediaPlayer();
        WindowsMediaPlayer acertar = new WindowsMediaPlayer();
        WindowsMediaPlayer fallar = new WindowsMediaPlayer();

        Jugador j1 = new Jugador("Ricardo");
        bool atacar;
        bool isVertical = true;

        List<Barco> barcos = new List<Barco> { };

        //Constructor
        public Form1()
        {
            InitializeComponent();
            crearTablero();
            crearBarcos();
            //Inicializar música
            /*
             mainMusic.URL = "mainWellerman.mp3";
             mainMusic.settings.volume = 10;
             mainMusic.settings.setMode("loop", true);
            */
            
            //Establece el formato de la lista del Form1
            lw_Barcos.View = View.Details;

            //Bucle para poblar la lista de barcos
            foreach (Barco item in barcos)
            {
                ListViewItem LVItem = new ListViewItem(item.Name);
                lw_Barcos.Items.Add(LVItem);
                LVItem.SubItems.Add(item.Tamaño.ToString());
            }

            lw_Barcos.Items[0].Selected = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_NombreJug.Text = j1.Nombre;
            lbl_TotalTiros.Text = j1.Tiros.ToString();
            lbl_TotalAciertos.Text = j1.Aciertos.ToString();
            lbl_TotalFallos.Text = j1.Fallos.ToString();
        }

        private void celda_Click(object sender, EventArgs e)
        {
            PictureBox pictures = sender as PictureBox;

            //Array que contiene datos sobre la celda (coordenadas X/Y, si esta ocupado, etc.)
            String[] tagPicture = pictures.Tag.ToString().Split('#');
            Barco barcoAEliminar = null;
            
            if (pictures == null) return;
            if (atacar)
            {
                //Si ya hemos disparado en la celda
                if (tagPicture[3] == "Dado")
                {
                    MessageBox.Show("Ahí ya has disparado. Casilla no válida.");
                    return;
                }

                //Si hemos disparado al agua
                if (tagPicture[0] == "A")
                {
                    MessageBox.Show("Has fallado");
                    j1.Tiros--;
                    j1.Fallos++;
                    lbl_TotalFallos.Text = j1.Fallos.ToString();
                    lbl_TotalTiros.Text = j1.Tiros.ToString();
                    pictures.Tag = tagPicture[0] + "#" + tagPicture[1] + "#" + tagPicture[2] + "#" + "Dado";
                    comprobarPartida();
                    return;
                }

                //Si hemos llegado aqui, hemos disparado a un barco
                int countTemp = 1;
                barcos.ForEach(a =>
                {
                    if (a.Name == tagPicture[0])
                    {
                        a.Tamaño--;
                        pictures.Tag = tagPicture[0] + "#" + tagPicture[1] + "#" + tagPicture[2] + "#" + "Dado";
                        pictures.Image = Image.FromFile("./../../img/defeat.jpg");
                        j1.Aciertos++;
                        lbl_TotalAciertos.Text = j1.Aciertos.ToString();
                    }

                    if (a.Tamaño == 0)
                    {
                        MessageBox.Show("Se ha destruido un barco");
                        foreach (Control control in tableLayoutPanel1.Controls)
                        {
                            PictureBox picture = control as PictureBox;
                            String[] tagPicture2 = picture.Tag.ToString().Split('#');

                            if (picture == null) return;
                            if (a.Name == tagPicture2[0])
                            {

                                //Este if es innecesario completamente solamente esta hasta que dispongamos 
                                //de todos los sprites.

                                if (a.Name == "Portaaviones XRT")
                                {
                                    picture.Image = Image.FromFile(a.Img + countTemp + ".png");
                                    countTemp++;


                                }
                                else
                                {
                                    picture.Image = Image.FromFile(a.Img);
                                }
                            }
                        }
                        barcoAEliminar = a;
                    }
                });
                barcos.Remove(barcoAEliminar);
                comprobarPartida();
            }
            else
            {
                if (lw_Barcos.SelectedItems.Count == 0) MessageBox.Show("No hay un barco seleccionado.");
                else
                {
                    // Comprueba si la casilla es agua para poder situar un barco
                    if (tagPicture[0] == "A")
                    {
                        int tamaño = Convert.ToInt32(lw_Barcos.SelectedItems[0].SubItems[1].Text); // Selecciona el tamaño del barco de la lista
                        if (!isVertical)
                        {
                            // Comprueba si hay espacio horizontal suficiente, y si lo hay, elimina al barco de la lista (Falta añadir barco al tablero. De momento solo añade la primera parte)
                            asignarBarco(lw_Barcos.SelectedItems[0].Text, tamaño, Convert.ToInt32(tagPicture[1]), Convert.ToInt32(tagPicture[2]));
                        }
                        else if (isVertical)
                        {
                            // Comprueba si hay espacio vertical suficiente, y si lo hay, elimina al barco d ela lista (Falta añadir barco al tablero. De momento solo añade la primera parte)
                            asignarBarco(lw_Barcos.SelectedItems[0].Text, tamaño, Convert.ToInt32(tagPicture[1]), Convert.ToInt32(tagPicture[2]));
                        }
                    }
                    else
                    {
                        MessageBox.Show("El rango de casillas seleccionado ya está ocupado.");
                    }
                }

                // Comprueba si la lista se ha vaciado
                if (lw_Barcos.Items.Count == 0)
                {
                    btn_atacar.Enabled = true;
                    btn_rotar.Enabled = false;
                    lw_Barcos.Enabled = false;
                }
            }
        }

        private void crearTablero()
        {
            //Variables que almacenan las coordenadas X e Y de cada celda del tablero
            int x = 1, y = 1;

            //Recorre las celdas por orden de numero que tiene en el nombre
            foreach (Control control in tableLayoutPanel1.Controls.Cast<Control>()
                                                                .OrderBy(c => Int32.Parse(c.Name.Substring(10))))
            {
                PictureBox pictures = control as PictureBox;
                if (pictures != null)
                {
                    pictures.BackColor = Color.Transparent;

                    //Si X es 11, significa que tenemos que bajar una fila, y reestablecer el valor de X a 1, y además aumentar el valor de Y
                    if (x == 11)
                    {
                        x = 1;
                        y++;
                    }

                    //Asignamos las coordenadas, y otros datos
                    pictures.Tag = "A" + "#" + x + "#" + y + "#" + "Normal";

                    //Pasamos a la siguiente celda del eje X
                    x++;
                }
            }
        }

        private void btn_atacar_Click(object sender, EventArgs e)
        {
            atacar = true;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox picture = control as PictureBox;
                if (picture != null)
                {
                    picture.Image = null;
                }
            }
            btn_atacar.Enabled = false;
        }

        private void btn_rotar_Click(object sender, EventArgs e)
        {
            if (isVertical)
                isVertical = false;
            else
                isVertical = true;

        }

        private void crearBarcos()
        {
            //Creación de los barcos
            //Porta aviones
            Barco portaAviones = new Barco(4, "Portaaviones XRT", "./../../img/spritesBarcos/Portaaviones/portaaviones");

            barcos.Add(portaAviones);

            //Submarinos
            Barco submarino1 = new Barco(3, "Submarino X1", "../../img/barco.jpg");
            Barco submarino2 = new Barco(3, "Submarino X2", "../../img/barco.jpg");

            barcos.Add(submarino1);
            barcos.Add(submarino2);

            //Destructores
            Barco destructor1 = new Barco(2, "Destructor R1", "../../img/barco.jpg");
            Barco destructor2 = new Barco(2, "Destructor R2", "../../img/barco.jpg");
            Barco destructor3 = new Barco(2, "Destructor R3", "../../img/barco.jpg");

            barcos.Add(destructor1);
            barcos.Add(destructor2);
            barcos.Add(destructor3);

            // Fragatas
            Barco fragata1 = new Barco(1, "Fragata T1", "../../img/barco.jpg");
            Barco fragata2 = new Barco(1, "Fragata T2", "../../img/barco.jpg");
            Barco fragata3 = new Barco(1, "Fragata T3", "../../img/barco.jpg");
            Barco fragata4 = new Barco(1, "Fragata T4", "../../img/barco.jpg");

            barcos.Add(fragata1);
            barcos.Add(fragata2);
            barcos.Add(fragata3);
            barcos.Add(fragata4);
        }

        private void asignarBarco(String nombre, int tamaño, int valorX, int valorY)
        {
            //Sacar todas las posisciones de x que necesitamos
            // valoresY.Add(tamaño);
            int counTemp = 1;

            if (isVertical)
            {
                //Lista con las coordenadas Y
                List<int> valoresY = new List<int> { };
                for (int i = 0; i < tamaño; i++) valoresY.Add(valorY + i);
                
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
                                picture.Tag = nombre + "#" + tagPicture[1] + "#" + tagPicture[2] + "#" + "normal";
                                String text = "";
                                switch (tamaño)
                                {
                                    case 1:

                                        text = "./../../img/spritesBarcos/Fragata/fragata.png";
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        picture.Image = Image.FromFile(text);
                                        counTemp++;
                                        break;
                                    case 2:

                                        text = "./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png";
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        picture.Image = Image.FromFile(text);
                                        counTemp++;
                                        break;
                                    case 3:

                                        text = "./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png";
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        picture.Image = Image.FromFile(text);
                                        counTemp++;
                                        break;
                                    case 4:

                                        text = "./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png";
                                        picture.SizeMode = PictureBoxSizeMode.StretchImage;
                                        picture.Image = Image.FromFile(text);
                                        counTemp++;
                                        break;
                                    default:
                                        Image img = Image.FromFile("../../img/barco.jpg");
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        picture.Image = img;
                                        break;
                                }
                            }
                        }
                    }
                }
                lw_Barcos.SelectedItems[0].Remove();
            }
            else
            {
                //Lista con las coordenadas Y
                List<int> valoresX = new List<int> { };
                for (int i = 0; i < tamaño; i++) valoresX.Add(valorX + i);

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
                                picture.Tag = nombre + "#" + tagPicture[1] + "#" + tagPicture[2] + "#" + "normal";
                                switch (tamaño)
                                {
                                    case 1:

                                        Bitmap fragata = new Bitmap("./../../img/spritesBarcos/Fragata/fragata.png");
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        fragata.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        picture.Image = fragata;
                                        counTemp++;
                                        break;

                                    case 2:

                                        Bitmap submarino = new Bitmap("./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png");
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        submarino.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        picture.Image = submarino;
                                        counTemp++;
                                        break;
                                    case 3:

                                        Bitmap destructor = new Bitmap("./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png");
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        destructor.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        picture.Image = destructor;
                                        counTemp++;
                                        break;
                                    case 4:

                                        Bitmap portaaviones = new Bitmap("./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png");
                                        picture.SizeMode = PictureBoxSizeMode.StretchImage;
                                        portaaviones.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        picture.Image = portaaviones;
                                        counTemp++;
                                        break;
                                    default:

                                        Image img = Image.FromFile("../../img/barco.jpg");
                                        picture.SizeMode = PictureBoxSizeMode.Zoom;
                                        picture.Image = img;
                                        break;
                                }
                            }
                        }
                    }
                }
                lw_Barcos.SelectedItems[0].Remove();
            }
        }

        public bool comprobarSiCabeElBarco(List<int> valores, int valorX, int valorY)
        {
            int tamaño = Convert.ToInt32(lw_Barcos.SelectedItems[0].SubItems[1].Text);

            if (isVertical)
            {
                //Comprueba si sale del tablero
                if ((valorY + tamaño) > 11)
                {
                    MessageBox.Show("El barco no cabe. Sale del borde.");
                    return false;
                }

                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (picture != null)
                    {

                        for (int i = 0; i < valores.Count; i++)
                        {
                            //Comprueba si hay barcos en medio
                            if (Convert.ToInt32(tagPicture[2]) == valores[i] && Convert.ToInt32(tagPicture[1]) == valorX && tagPicture[0] != "A")
                            {
                                MessageBox.Show("El barco no cabe. Hay un barco en medio.");
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                //Comprobar si se sale por los lados
                if ((valorX + tamaño) > 11)
                {
                    MessageBox.Show("El barco no cabe. Sale del borde.");
                    return false;
                }

                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (picture != null)
                    {
                        //Comprobar si hay otros barcos
                        for (int i = 0; i < valores.Count; i++)
                        {
                            if (Convert.ToInt32(tagPicture[1]) == valores[i] && Convert.ToInt32(tagPicture[2]) == valorY && tagPicture[0] != "A")
                            {
                                MessageBox.Show("El barco no cabe. Hay un barco en medio");
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e) //Muestra como quedará el barco en la ubicación del cursor
        {
            if (lw_Barcos.SelectedItems.Count == 0) return;

            PictureBox pictures = sender as PictureBox;            
            int counTemp = 1;
            int tamaño = Convert.ToInt32(lw_Barcos.SelectedItems[0].SubItems[1].Text); // Selecciona el tamaño del barco de la lista
            String[] tagSelectedPicture = pictures.Tag.ToString().Split('#');
            int valorY = int.Parse(tagSelectedPicture[2]);
            int valorX = int.Parse(tagSelectedPicture[1]);

            if (isVertical)
            {
                //Comprobar si se sale de la pantalla
                if (valorY + tamaño > 11) return;

                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');

                    //Comprobar si cabe el barco
                    for (int i = 0; i < tamaño; i++) if (Convert.ToInt32(tagPicture[2]) == valorY + i && Convert.ToInt32(tagPicture[1]) == valorX && tagPicture[0] != "A") return;

                    //Bucle que asigna las imágenes
                    for (int i = 0; i < tamaño; i++)
                    {                        
                        if (Convert.ToInt32(tagPicture[2]) == (valorY + i) && Convert.ToInt32(tagPicture[1]) == valorX)
                        {
                            //Distingue las imágenes segun su tamaño
                            switch (tamaño)
                            {
                                case 1:
                                    Bitmap fragata = new Bitmap("./../../img/spritesBarcos/Fragata/fragata.png");
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    picture.Image = fragata;
                                    counTemp++;
                                    break;

                                case 2:
                                    Bitmap submarino = new Bitmap("./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png");
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    picture.Image = submarino;
                                    counTemp++;
                                    break;

                                case 3:
                                    Bitmap destructor = new Bitmap("./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png");
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    picture.Image = destructor;
                                    counTemp++;
                                    break;

                                case 4:
                                    Bitmap portaaviones = new Bitmap("./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png");
                                    picture.SizeMode = PictureBoxSizeMode.StretchImage;
                                    picture.Image = portaaviones;
                                    counTemp++;
                                    break;

                                default:
                                    Image img = Image.FromFile("../../img/barco.jpg");
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    picture.Image = img;                                 
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                //Comprobar si cabe el barco
                if (valorX + tamaño > 11) return;
                
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');

                    //Comprobar si cabe el barco
                    for (int i = 0; i < tamaño; i++) if (Convert.ToInt32(tagPicture[2]) == valorY && Convert.ToInt32(tagPicture[1]) == valorX + i && tagPicture[0] != "A") return;

                    //Bucle que asigna las imágenes
                    for (int i = 0; i < tamaño; i++)
                    {
                        if (Convert.ToInt32(tagPicture[1]) == (valorX + i) && Convert.ToInt32(tagPicture[2]) == valorY)
                        {
                            //Distingue las imágenes segun su tamaño
                            switch (tamaño)
                            {
                                case 1:
                                    Bitmap fragata = new Bitmap("./../../img/spritesBarcos/Fragata/fragata.png");
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    fragata.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    picture.Image = fragata;
                                    counTemp++;
                                    break;

                                case 2:
                                    Bitmap submarino = new Bitmap("./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png");
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    submarino.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    picture.Image = submarino;
                                    counTemp++;
                                    break;

                                case 3:
                                    Bitmap destructor = new Bitmap("./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png");
                                    destructor.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    picture.Image = destructor;
                                    counTemp++;
                                    break;
                                case 4:

                                    Bitmap portaaviones = new Bitmap("./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png");
                                    portaaviones.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    picture.SizeMode = PictureBoxSizeMode.StretchImage;
                                    picture.Image = portaaviones;
                                    counTemp++;
                                    break;

                                default:
                                    Image img = Image.FromFile("../../img/barco.jpg");
                                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                                    picture.Image = img;
                                    break;
                            }
                        }
                    }
                   
                }
            }
        }

        private void pictureBox100_MouseLeave(object sender, EventArgs e)
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox picture = control as PictureBox;
                String[] tagPicture = picture.Tag.ToString().Split('#');
                if (tagPicture[0].Equals("A")) picture.Image = null;
            }
        }

        //Comprueba si el jugador a ganado o perdido
        private void comprobarPartida()
        {
            if (barcos.Count == 0)
            {
                Form5 f5 = new Form5();
                f5.Show();
            }
            if (j1.comprobarDerrota())
            {
                Form4 f4 = new Form4();
                f4.Show();
            }
        }

        //Método que vuelve a mostrar el menu principal (Form2) al cerrar
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2.ProveedorForm2.Form2.Show();
        }
    }
}
