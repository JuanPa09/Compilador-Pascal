using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.reportes;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class Sim : Expresion
    {
        Simbolo sim;
        public Sim(Simbolo sim)
        {
            this.sim = sim;
        }

        public override Simbolo evaluar(Entorno entorno, Reporte reporte)
        {
            return sim;
        }
    }
}
