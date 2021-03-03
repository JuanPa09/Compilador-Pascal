using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.simbolo
{
    public enum Tipos
    { 
        NUMBER = 0,
        STRING = 1,
        BOOLEAN = 2,
        NULLL = 3,
        ARRAY = 4,
        OBJECT = 5,
        DOUBLE = 6

    }
    class Tipo
    {
        public Tipos tipo;
        public string tipoAuxiliar;

        public Tipo(Tipos tipo, string tipoAuxiliar)
        {
            this.tipo = tipo;
            this.tipoAuxiliar = tipoAuxiliar;
        }

    }
}
