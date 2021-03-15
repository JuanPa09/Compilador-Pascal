using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.reportes
{
    class FormatoSimbolos
    {
        public string nombre,tipo,ambito;
        public int fila, columna;

        public FormatoSimbolos(string nombre, string tipo, string ambito, int fila, int columna)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.ambito = ambito;
            this.fila = fila;
            this.columna = columna;
        }



    }
}
