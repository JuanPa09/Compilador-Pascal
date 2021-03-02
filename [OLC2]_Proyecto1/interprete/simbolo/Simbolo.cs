using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.simbolo
{
    class Simbolo
    {
        public object valor;
        public string id;
        public Tipo tipo;


        public Simbolo(object valor, Tipo tipo, string id)
        {
            this.valor = valor;
            this.id = id;
            this.tipo = tipo;
        }

        public override string ToString()
        {
            return this.valor.ToString();
        }

    }
}
