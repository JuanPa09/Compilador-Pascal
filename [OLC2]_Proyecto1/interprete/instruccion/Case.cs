using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.interprete.simbolo;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Case : Instruccion
    {
        private Expresion valor;
        private LinkedList<Instrucciones> instrucciones;
        

        public override object ejecutar(Entorno entorno)
        {
            Simbolo valor = this.valor.evaluar(entorno);


            return null;
        }
    }
}
