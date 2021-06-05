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
using System.Media;

namespace battleShip {
    public partial class Form1 : Form
    {

        //Variables
        String text = "";
        List<PictureBox> picturesHover = new List<PictureBox> { };
        PictureBox pictureChange = new PictureBox();

        //Reproductores de sonido
        WindowsMediaPlayer mainMusic = new WindowsMediaPlayer();
        SoundPlayer disparar = new SoundPlayer();
        SoundPlayer acertar = new SoundPlayer();
        SoundPlayer fallar = new SoundPlayer();

        List<String> nombres = new List<string>() {"Fideito18", "Trevor Belmont", "Coronel Sanders", "The_Legend_27", "Ranbaudi", "Danzi", "Aleen", "Jeff", 
                                                    "Samu", "Ricardo", "Kirito", "Reiner", "Boruto", "Umaru-chan", "Useless Goddess", "Ezio Auditore", "orson",
                                                    "Jon Snow", "Colmillosauro", "Arthas", "Tyrion Lannister", "Mr. Robot", "Meliodas"};

        Jugador j1;
        bool atacar;
        bool cerradoVerificado;
        bool isVertical = true;
        float tiempo = 0.0f;

        List<Barco> barcos = new List<Barco> { };

        //Constructor
        public Form1()
        {
            InitializeComponent();
            crearTablero();
            crearBarcos();

            // Crear jugador

            Random rand = new Random();
            int numero = rand.Next(nombres.Count);
            j1 = new Jugador(nombres[numero]);

            //Inicializar música

             mainMusic.URL = "Sound\\battlefield-1942-ost.mp3";
             mainMusic.settings.volume = 10;
             mainMusic.settings.setMode("loop", true);
             cerradoVerificado = false;


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
            label4.Text = j1.Nombre;
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
                //Si le das demasiado rápido
                if (timerDisparo.Enabled) return;

                //Si ya hemos disparado en la celda
                if (tagPicture[3] == "Dado")  return;

                this.Cursor = new Cursor("../../icons/hitmarker.ico");
                timerCur.Start();

                //Si hemos disparado al agua
                if (tagPicture[0] == "A")
                {
                    timerDisparo.Interval = 1050;
                    timerDisparo.Start();
                    j1.Tiros--;
                    j1.Fallos++;
                    lbl_TotalFallos.Text = j1.Fallos.ToString();
                    lbl_TotalTiros.Text = j1.Tiros.ToString();
                    pictures.Image = Image.FromFile("./../../img/miss.gif");
                    pictureChange = pictures;
                    timerExplosionAgua.Start();
                    pictures.Tag = tagPicture[0] + "#" + tagPicture[1] + "#" + tagPicture[2] + "#" + "Dado";
                    comprobarPartida();
                    return;
                }

                timerDisparo.Interval = 1400;
                timerDisparo.Start();
                //Si hemos llegado aqui, hemos disparado a un barco
                int counTemp;
                barcos.ForEach(a =>
                {
                    if (a.Name == tagPicture[0])
                    {
                        a.Vidas--;
                        pictures.Tag = tagPicture[0] + "#" + tagPicture[1] + "#" + tagPicture[2] + "#" + "Dado";
                        pictures.Image = Image.FromFile("./../../img/explosion.gif");


                        pictureChange = pictures;
                        if (a.Vidas != 0 ) timerExplosion.Start();


                        j1.Aciertos++;
                        lbl_TotalAciertos.Text = j1.Aciertos.ToString();
                    }

                    if (a.Vidas == 0)
                    {


                        counTemp = a.Tamaño;
                        foreach (Control control in tableLayoutPanel1.Controls.Cast<Control>()
                                                                        .OrderBy(c => Int32.Parse(c.Name.Substring(10))))

                        {
                            PictureBox picture = control as PictureBox;
                            String[] tagPicture2 = picture.Tag.ToString().Split('#');
                            
                            if (picture == null) return;
                            if (a.Name == tagPicture2[0])
                            {
                               if (a.Vertical)
                                {
                                    switch (a.Tamaño)
                                    {
                                        case 1:

                                            text = "./../../img/spritesBarcos/Fragata/fragataD.png";
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            picture.Image = Image.FromFile(text);
                                            counTemp--;
                                            break;
                                        case 2:

                                            text = "./../../img/spritesBarcos/Destructor/destructorD" + counTemp + ".png";
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            picture.Image = Image.FromFile(text);
                                            counTemp--;
                                            break;
                                        case 3:

                                            text = "./../../img/spritesBarcos/Submarino/submarinoD" + counTemp + ".png";
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            picture.Image = Image.FromFile(text);
                                            counTemp--;
                                            break;
                                        case 4:

                                            text = "./../../img/spritesBarcos/Portaaviones/portaavionesD" + counTemp + ".png";
                                            picture.SizeMode = PictureBoxSizeMode.StretchImage;
                                            picture.Image = Image.FromFile(text);
                                            counTemp--;
                                            break;
                                        default:
                                            Image img = Image.FromFile("../../img/barco.jpg");
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            picture.Image = img;
                                            break;
                                    }
                                    barcoAEliminar = a;
                                } 
                                else
                                {
                                    switch (a.Tamaño)
                                    {
                                        case 1:

                                            Bitmap fragata = new Bitmap("./../../img/spritesBarcos/Fragata/fragataD.png");
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            fragata.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                            picture.Image = fragata;
                                            counTemp--;
                                            break;

                                        case 2:

                                            Bitmap submarino = new Bitmap("./../../img/spritesBarcos/Destructor/destructorD" + counTemp + ".png");
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            submarino.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                            picture.Image = submarino;
                                            counTemp--;
                                            break;
                                        case 3:

                                            Bitmap destructor = new Bitmap("./../../img/spritesBarcos/Submarino/submarinoD" + counTemp + ".png");
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            destructor.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                            picture.Image = destructor;
                                            counTemp--;
                                            break;
                                        case 4:

                                            Bitmap portaaviones = new Bitmap("./../../img/spritesBarcos/Portaaviones/portaavionesD" + counTemp + ".png");
                                            picture.SizeMode = PictureBoxSizeMode.StretchImage;
                                            portaaviones.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                            picture.Image = portaaviones;
                                            counTemp--;
                                            break;
                                        default:

                                            Image img = Image.FromFile("../../img/barco.jpg");
                                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                                            picture.Image = img;
                                            break;
                                    }
                                    barcoAEliminar = a;
                                }
                            }
                        }
                    }
                });
                //this.Cursor = new Cursor("../../icons/mira.ico");
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
                            asignarBarco(lw_Barcos.SelectedItems[0].Text, tamaño, Convert.ToInt32(tagPicture[1]), Convert.ToInt32(tagPicture[2]), int.Parse(pictures.Name.Substring(10)));
                        }
                        else if (isVertical)
                        {
                            // Comprueba si hay espacio vertical suficiente, y si lo hay, elimina al barco d ela lista (Falta añadir barco al tablero. De momento solo añade la primera parte)
                            asignarBarco(lw_Barcos.SelectedItems[0].Text, tamaño, Convert.ToInt32(tagPicture[1]), Convert.ToInt32(tagPicture[2]), int.Parse(pictures.Name.Substring(10)));
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
                pictures.Image = null;
            }
        }

        private void btn_atacar_Click(object sender, EventArgs e)
        {
            timer1.Start();
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

        private void asignarBarco(String nombre, int tamaño, int valorX, int valorY, int valuePicture)
        {
                       
            int counTemp = tamaño;

            if (isVertical)
            {
                //Lista con las coordenadas Y
                List<int> valoresY = new List<int> { };
                for (int i = 0; i < tamaño; i++) valoresY.Add(valorY + i);

                //Comprobar si cabe el barco
                if (!comprobarSiCabeElBarco(valoresY, valorX, valorY)) return;

                foreach (Control control in tableLayoutPanel1.Controls.Cast<Control>()
                                                                .OrderBy(c => Int32.Parse(c.Name.Substring(10))))
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (int.Parse(picture.Name.Substring(10)) == valuePicture)
                    {
                        if (picturesHover.Count < tamaño)
                        {
                            picturesHover.Add(picture);
                            valuePicture += 10;
                        }
                    }
                }

                bool cabe = true;
                picturesHover.ForEach(a =>
                {
                    String[] tagA = a.Tag.ToString().Split('#');
                    if (tagA[0] != "A") cabe = false;
                });
                if (!cabe) return;

                picturesHover.ForEach(a =>
                {
                    String[] tagA = a.Tag.ToString().Split('#');
                    a.Tag = nombre + "#" + tagA[1] + "#" + tagA[2] + "#" + "normal";
                    String text = "";
                    switch (tamaño)
                    {
                        case 1:

                            text = "./../../img/spritesBarcos/Fragata/fragata.png";
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = Image.FromFile(text);
                            counTemp--;
                            break;
                        case 2:

                            text = "./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png";
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = Image.FromFile(text);
                            counTemp--;
                            break;
                        case 3:

                            text = "./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png";
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = Image.FromFile(text);
                            counTemp--;
                            break;
                        case 4:

                            text = "./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png";
                            a.SizeMode = PictureBoxSizeMode.StretchImage;
                            a.Image = Image.FromFile(text);
                            counTemp--;
                            break;
                        default:
                            Image img = Image.FromFile("../../img/barco.jpg");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = img;
                            break;
                    }
                });
                barcos.ForEach(a =>
                {
                    if (a.Name.Equals(nombre))
                    {
                        if (isVertical) a.Vertical = true;
                        else a.Vertical = false;
                    }
                });

                lw_Barcos.SelectedItems[0].Remove();
                if (lw_Barcos.Items.Count > 0) lw_Barcos.Items[0].Selected = true;
                
                
            }
            else
            {
                //Lista con las coordenadas Y
                List<int> valoresX = new List<int> { };
                for (int i = 0; i < tamaño; i++) valoresX.Add(valorX + i);

                //Comprobar si cabe el barco
                if (!comprobarSiCabeElBarco(valoresX, valorX, valorY)) return;

                foreach (Control control in tableLayoutPanel1.Controls.Cast<Control>()
                                                                .OrderBy(c => Int32.Parse(c.Name.Substring(10))))
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (int.Parse(picture.Name.Substring(10)) == valuePicture)
                    {
                        if (picturesHover.Count < tamaño)
                        {
                            picturesHover.Add(picture);
                            valuePicture += 1;
                        }
                    }
                }

                bool cabe = true;
                picturesHover.ForEach(a =>
                {
                    String[] tagA = a.Tag.ToString().Split('#');
                    if (tagA[0] != "A") cabe = false;
                });
                if (!cabe) return;

                picturesHover.ForEach(a =>
                {
                    String[] tagA = a.Tag.ToString().Split('#');
                    a.Tag = nombre + "#" + tagA[1] + "#" + tagA[2] + "#" + "normal";
                    switch (tamaño)
                    {
                        case 1:

                            Bitmap fragata = new Bitmap("./../../img/spritesBarcos/Fragata/fragata.png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            fragata.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.Image = fragata;
                            counTemp--;
                            break;

                        case 2:

                            Bitmap submarino = new Bitmap("./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            submarino.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.Image = submarino;
                            counTemp--;
                            break;
                        case 3:

                            Bitmap destructor = new Bitmap("./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            destructor.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.Image = destructor;
                            counTemp--;
                            break;
                        case 4:

                            Bitmap portaaviones = new Bitmap("./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png");
                            a.SizeMode = PictureBoxSizeMode.StretchImage;
                            portaaviones.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.Image = portaaviones;
                            counTemp--;
                            break;
                        default:

                            Image img = Image.FromFile("../../img/barco.jpg");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = img;
                            break;
                    }
                });
                lw_Barcos.SelectedItems[0].Remove();
                if (lw_Barcos.Items.Count > 0) lw_Barcos.Items[0].Selected = true;
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
            if (atacar) this.Cursor = new Cursor("../../icons/mira.ico");

            if (lw_Barcos.SelectedItems.Count == 0) return;

            PictureBox pictures = sender as PictureBox;            
            
            int tamaño = Convert.ToInt32(lw_Barcos.SelectedItems[0].SubItems[1].Text); // Selecciona el tamaño del barco de la lista
            int counTemp = tamaño;
            int valuePicture = int.Parse(pictures.Name.Substring(10));

            String[] tagSelectedPicture = pictures.Tag.ToString().Split('#');
            int valorY = int.Parse(tagSelectedPicture[2]);
            int valorX = int.Parse(tagSelectedPicture[1]);

            if (isVertical)
            {
                //Comprobar si se sale de la pantalla
                if (valorY + tamaño > 11) return;
               
                foreach (Control control in tableLayoutPanel1.Controls.Cast<Control>()
                                                                .OrderBy(c => Int32.Parse(c.Name.Substring(10))))
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (int.Parse(picture.Name.Substring(10)) == valuePicture)
                    {
                        if (picturesHover.Count < tamaño)
                        {
                            picturesHover.Add(picture);
                            valuePicture += 10;
                        }
                    }
                }

                bool cabe = true;
                picturesHover.ForEach(a =>
                {
                    String[] tagA = a.Tag.ToString().Split('#');
                    if (tagA[0] != "A") cabe = false;
                });
                if (!cabe) return;

                picturesHover.ForEach(a =>
                {
                    switch (tamaño)
                    {
                        case 1:
                            Bitmap fragata = new Bitmap("./../../img/spritesBarcos/Fragata/fragata.png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = fragata;
                            counTemp--;
                            break;

                        case 2:
                            Bitmap submarino = new Bitmap("./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = submarino;
                            counTemp--;
                            break;

                        case 3:
                            Bitmap destructor = new Bitmap("./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = destructor;
                            counTemp--;
                            break;

                        case 4:
                            Bitmap portaaviones = new Bitmap("./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png");
                            a.SizeMode = PictureBoxSizeMode.StretchImage;
                            a.Image = portaaviones;
                            counTemp--;
                            break;

                        default:
                            Image img = Image.FromFile("../../img/barco.jpg");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = img;
                            break;
                    }
                });
            }
            else
            {

                //Comprobar si se sale de la pantalla
                if (valorX + tamaño > 11) return;

                foreach (Control control in tableLayoutPanel1.Controls.Cast<Control>()
                                                                .OrderBy(c => Int32.Parse(c.Name.Substring(10))))
                {
                    PictureBox picture = control as PictureBox;
                    String[] tagPicture = picture.Tag.ToString().Split('#');
                    if (int.Parse(picture.Name.Substring(10)) == valuePicture)
                    {
                        if (picturesHover.Count < tamaño)
                        {
                            picturesHover.Add(picture);
                            valuePicture += 1;
                        }
                    }
                }

                bool cabe = true;
                picturesHover.ForEach(a =>
                {
                    String[] tagA = a.Tag.ToString().Split('#');
                    if (tagA[0] != "A") cabe = false;
                });
                if (!cabe) return;

                picturesHover.ForEach(a =>
                {
                    
                       switch (tamaño)
                    {
                        case 1:
                            Bitmap fragata = new Bitmap("./../../img/spritesBarcos/Fragata/fragata.png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            fragata.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.Image = fragata;
                            counTemp--;
                            break;

                        case 2:
                            Bitmap submarino = new Bitmap("./../../img/spritesBarcos/Destructor/destructor" + counTemp + ".png");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            submarino.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.Image = submarino;
                            counTemp--;
                            break;

                        case 3:
                            Bitmap destructor = new Bitmap("./../../img/spritesBarcos/Submarino/submarino" + counTemp + ".png");
                            destructor.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = destructor;
                            counTemp--;
                            break;
                        case 4:

                            Bitmap portaaviones = new Bitmap("./../../img/spritesBarcos/Portaaviones/portaaviones" + counTemp + ".png");
                            portaaviones.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            a.SizeMode = PictureBoxSizeMode.StretchImage;
                            a.Image = portaaviones;
                            counTemp--;
                            break;

                        default:
                            Image img = Image.FromFile("../../img/barco.jpg");
                            a.SizeMode = PictureBoxSizeMode.Zoom;
                            a.Image = img;
                            break;
                    }
                });
            }
        }

        private void pictureBox100_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = default;
            bool modi = true;
            picturesHover.ForEach(a => {
                String[] tagA = a.Tag.ToString().Split('#');
                if (tagA[0] != "A") modi = false;   
            });
            if (modi) picturesHover.ForEach(a => a.Image = null);
            picturesHover.Clear();
        }

        //Comprueba si el jugador ha ganado o perdido
        private void comprobarPartida()
        {
            if (barcos.Count == 0)
            {
                mainMusic.controls.stop();
                timer1.Stop();
                cerradoVerificado = true;
                Form5 f5 = new Form5(j1, labelTiempo.Text);
                f5.Show();
                this.Close();
            }
            if (j1.comprobarDerrota())
            {
                mainMusic.controls.stop();
                timer1.Stop();
                cerradoVerificado = true;
                Form4 f4 = new Form4(j1, labelTiempo.Text);
                f4.Show();
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tiempo += 0.1f;
            labelTiempo.Text = tiempo.ToString("N1");
        }

        //Método que vuelve a mostrar el menu principal (Form2) al cerrar
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cerradoVerificado)
            {
                mainMusic.close();
                Form2.ProveedorForm2.Form2.Show();
            }
        }

        private void btnMouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Color c = Color.FromArgb(1, 250, 200, 0);
            btn.ForeColor = c;
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.ForeColor = Color.Silver;
        }

        public void resetear()
        {
            //Poner la partida de zero
            barcos.Clear();
            lw_Barcos.Items.Clear();

            isVertical = true;
            atacar = false;
            btn_rotar.Enabled = true;
            btn_atacar.Enabled = true;

            //Volver a crear los elementos
            crearTablero();
            crearBarcos();
            barcos.ForEach(a =>
            {
                ListViewItem LVItem = new ListViewItem(a.Name);
                lw_Barcos.Items.Add(LVItem);
                LVItem.SubItems.Add(a.Tamaño.ToString());
            });

            lbl_NombreJug.Text = j1.Nombre;
            lbl_TotalTiros.Text = j1.Tiros.ToString();
            lbl_TotalAciertos.Text = j1.Aciertos.ToString();
            lbl_TotalFallos.Text = j1.Fallos.ToString();


            lw_Barcos.Items[0].Selected = true;

            mainMusic.URL = "Sound\\battlefield-1942-ost.mp3";
            mainMusic.settings.volume = 10;
            mainMusic.settings.setMode("loop", true);
        }

        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            if (atacar) this.Cursor = new Cursor("../../icons/mira.ico");
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = default;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timerCur.Stop();
            if (atacar) this.Cursor = new Cursor("../../icons/mira.ico");

        }

        private void timerExplosion_Tick(object sender, EventArgs e)
        {
            timerExplosion.Stop();
            pictureChange.Image = Image.FromFile("./../../img/acertar.png");

        }

        private void timerExplosionAgua_Tick(object sender, EventArgs e)
        {
            timerExplosionAgua.Stop();
            pictureChange.Image = Image.FromFile("./../../img/circle.png");

        }

        private void timerDisparo_Tick(object sender, EventArgs e)
        {
            timerDisparo.Stop();
        }
    }
}
    
