using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.simbolo;

namespace _OLC2__Proyecto1.interprete.expresion
{
    abstract class Expresion
    {
        public abstract Simbolo evaluar(Entorno entorno);
    }
}
