using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    abstract class Instruccion
    {
        public abstract object ejecutar(Entorno entorno,Reporte reporte);
    }
}
