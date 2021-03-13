using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.reportes;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class GraficarSimbolos : Instruccion
    {
        public override object ejecutar(Entorno entorno, Reporte report)
        {
            try
            {
                entorno.graficarSimbolos();
            }catch(Exception ex)
            {

            }
            return null;
        }
    }
}
