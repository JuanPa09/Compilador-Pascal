using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.simbolo
{
    public enum Tipos
    { 
        INT = 0,
        STRING = 1,
        BOOLEAN = 2,
        DOUBLE = 3,
        OBJECT = 4,
        ARRAY = 5

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
