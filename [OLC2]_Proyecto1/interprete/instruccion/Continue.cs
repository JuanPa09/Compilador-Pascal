using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Continue : Instruccion
    {
        public override object ejecutar(Entorno entorno, Reporte reporte)
        {
            return "continue";
        }
    }
}
