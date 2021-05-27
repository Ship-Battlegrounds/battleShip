using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace battleShip
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            label2.Text = "El juego consiste en hundir la flota del contrincante. Para ello, el primer jugador debe colocar su propia flota de forma estratégica y el segundo jugador debe de encontrar y hundir la flota contraria. ";
            label3.Text = "El primer jugador deberá colocar en los cuadros los siguientes barcos en posición horizontal o vertical:  1 barco que ocupa 4 cuadrados.  2 barcos de tres cuadros  3 barcos de 2 cuadros  4 barcos de un solo cuadro";
            label4.Text = "Los barcos pueden colocarse junto a los bordes de la cuadrícula o junto a otros barcos, pero sin llegar a tocarse.";
            label5.Text = "Finalmente, gana el jugador que destruya todos los barcos antes de usar los 25 tiros.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
