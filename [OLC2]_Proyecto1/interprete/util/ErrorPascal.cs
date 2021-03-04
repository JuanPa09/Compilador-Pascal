using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.util
{
    class ErrorPascal : Exception
    {
        private int linea, columna;
        private string mensaje;
        private string tipo;


        public ErrorPascal(int linea,int columna, string mensaje, string tipo) {
            this.linea = linea;
            this.columna = columna;
            this.mensaje = mensaje;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return "Se encontro error " + this.tipo + " en la linea " + this.linea + " y columna " + this.columna + "\n Mensaje: " + this.mensaje;
        }

    }
}
