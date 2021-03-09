using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Exit : Instruccion
    {

        private Expresion expresion;

        public Exit(Expresion expresion)
        {
            this.expresion = expresion;
        }


        public override object ejecutar(Entorno entorno)
        {
            if (expresion == null)
                return "$$";
            return this.expresion.evaluar(entorno);
        }
    }
}
