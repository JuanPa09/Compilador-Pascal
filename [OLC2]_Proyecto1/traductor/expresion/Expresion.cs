using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.traductor.Simbolo;

namespace _OLC2__Proyecto1.traductor.expresion
{
    abstract class Expresion
    {
        public abstract void ejecutar(Entorno entorno);
    }
}
