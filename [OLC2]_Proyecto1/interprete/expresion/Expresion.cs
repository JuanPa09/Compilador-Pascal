using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.expresion
{
    abstract class Expresion
    {
        public abstract Simbolo evaluar(Entorno entorno, Reporte reporte);
    }
}
