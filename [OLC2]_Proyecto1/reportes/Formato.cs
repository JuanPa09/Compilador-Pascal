using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.reportes
{
    class formato
    {
        public int fila;
        public int columna;
        public string tipo;
        public string mensaje;

        public formato(int fila, int columna, string tipo, string mensaje)
        {
            this.fila = fila;
            this.columna = columna;
            this.tipo = tipo;
            this.mensaje = mensaje;
        }

    }
}
