using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.simbolo;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    abstract class Instruccion
    {
        public abstract object ejecutar(Entorno entorno);
    }
}
