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
            if (valor == null)
                throw new util.ErrorPascal(0,0,"La variable \""+id+"\" no tiene valor","semántico");
            return this.valor.ToString();
        }

    }
}
