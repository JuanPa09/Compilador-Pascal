using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class For : Instruccion
    {
        private Expresion valor;
        private LinkedList<Instruccion> instrucciones;
        //PENDIENTE ...................
        public For(Expresion valor, LinkedList<Instruccion> instrucciones)
        {

        }

        public override object ejecutar(Entorno entorno)
        {
            throw new NotImplementedException();
        }
    }
}
