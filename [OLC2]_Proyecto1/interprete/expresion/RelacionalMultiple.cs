﻿using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class RelacionalMultiple : Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private string tipoOperacion;

        public RelacionalMultiple(Expresion izquierda, Expresion derecha, string tipoOperacion)
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.tipoOperacion = tipoOperacion;
        }


        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo izquierda = this.izquierda.evaluar(entorno);
            Simbolo derecha = this.derecha.evaluar(entorno);
            Tipo tipo = new Tipo(Tipos.BOOLEAN, null);

            Tipos tipoResultante = util.TablaTipos.getTipo(izquierda.tipo, derecha.tipo);
            if (tipoResultante == Tipos.NULLL || tipoResultante != Tipos.BOOLEAN)
                throw new util.ErrorPascal(0, 0, "Tipos De Dato Incorrectos. No se puede realizar la operacion relacional múltiple", "Semantico");

            switch(tipoOperacion)
            {
                case "and":
                    return new Simbolo(bool.Parse(izquierda.ToString()) && bool.Parse(derecha.ToString()), tipo, null);
                case "or":
                    return new Simbolo(bool.Parse(izquierda.ToString()) || bool.Parse(derecha.ToString()), tipo, null);
            }

            return null;
        }
    }
}