using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleShip {
    class Jugador {
        private String nombre;
        private int tiros = 25;
        private int aciertos = 0;
        private int fallos = 0;

        // Constructores

        public Jugador(String nombre) {
            this.Nombre = nombre;
        }

        // Métodos

        public bool comprobarDerrota ()
        {
            if (tiros == 0) return true;
            return false;
        }



        // Setters & Getters

        public String Nombre { get => nombre; set => nombre = value; }
        public int Tiros { get => tiros; set => tiros = value; }
        public int Aciertos { get => aciertos; set => aciertos = value; }
        public int Fallos { get => fallos; set => fallos = value; }
    }
}