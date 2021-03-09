﻿using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class Not : Expresion
    {

        private Expresion expresion;

        public Not(Expresion expresion)
        {
            this.expresion = expresion;
        }


        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo contenido = expresion.evaluar(entorno);

            if (contenido.tipo.tipo != Tipos.BOOLEAN)
                throw new util.ErrorPascal(0,0,"No se puede operar un Not en una expresion no booleana","semantico");

            if ((bool)(contenido.valor) == true)
                return new Simbolo(false,contenido.tipo,contenido.id);
            return new Simbolo(true, contenido.tipo, contenido.id);
        }
    }
}
