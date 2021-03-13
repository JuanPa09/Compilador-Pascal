using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.util
{
    class ErrorPascal : Exception
    {
        private int linea, columna;
        private string mensaje;
        private string tipo;
        Reporte reporte;

        public ErrorPascal(int linea,int columna, string mensaje, string tipo,Reporte reporte) {
            this.linea = linea;
            this.columna = columna;
            this.mensaje = mensaje;
            this.tipo = tipo;
            if (reporte!= null)
                reporte.nuevoError(linea, columna, "Semántico", mensaje);
        }

        public override string ToString()
        {
            return "Se encontro error " + this.tipo + " en la linea " + this.linea + " y columna " + this.columna + "\n Mensaje: " + this.mensaje;
        }

    }
}
