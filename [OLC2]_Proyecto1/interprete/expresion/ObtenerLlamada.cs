using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.instruccion;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class ObtenerLlamada : Expresion
    {

        Instruccion llamada;

        public ObtenerLlamada(Instruccion llamada)
        {
            this.llamada = llamada;
        }

        public override Simbolo evaluar(Entorno entorno,Reporte reporte)
        {
            return (Simbolo)llamada.ejecutar(entorno,reporte);
        }
    }
}
