using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class Literal : Expresion
    {

        private char tipo;
        private object valor;

        public Literal(char tipo,object valor) 
        {
            this.tipo = tipo;
            this.valor = valor;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            switch (tipo)
            {
                case 'N':
                    return new Simbolo(this.valor, new Tipo(Tipos.NUMBER, null), null);
                case 'D':
                    return new Simbolo(this.valor, new Tipo(Tipos.DOUBLE, null), null);
                case 'S':
                    return new Simbolo(this.valor, new Tipo(Tipos.STRING, null), null);
            }
            return null;
        }

    }
}
