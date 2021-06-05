using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleShip {
    class Barco {
        private int tamaño;
        private String[] coordenadas;
        private String name;
        private String img;
        private int vidas;
        private bool vertical;

        //Constructores
        public Barco (int size, String name,String imagen)
        {
            Tamaño = size;
            Name = name;
            Img = imagen;
            vidas = size;
        }

        // Métodos

        private Boolean isHit(String coordenadas)
        {
            Boolean hit = false;

            if (this.Coordenadas.Contains(coordenadas))
            {
                hit = true;
            }
            return hit;
        }

        // Setters & Getters

        public int Tamaño { get => tamaño; set => tamaño = value; }
        public string[] Coordenadas { get => coordenadas; set => coordenadas = value; }
        public string Name { get => name; set => name = value; }
        public string Img { get => img; set => img = value; }
        public int Vidas { get => vidas; set => vidas = value; }
        public bool Vertical { get => vertical; set => vertical = value; }
    }
}
