﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleShip {
    class Barco {
        private int tamaño;
        private String[] coordenadas;

        // Métodos

        private Boolean isHit(String coordenadas) { 
            Boolean hit = false;

                if (this.Coordenadas.Contains(coordenadas)) {
                hit = true;
                }
            return hit;
        }

        // Setters & Getters

        public int Tamaño { get => tamaño; set => tamaño = value; }
        public string[] Coordenadas { get => coordenadas; set => coordenadas = value; }
    }
}