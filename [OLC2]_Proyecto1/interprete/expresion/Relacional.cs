using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class Relacional : Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private string tipoOperacion;


        public Relacional(Expresion izquierda, Expresion derecha, string tipoOperacion)
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.tipoOperacion = tipoOperacion;
        }


        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo izquierda = this.izquierda.evaluar(entorno);
            Simbolo derecha = this.derecha.evaluar(entorno);
            Simbolo resultado;
            Tipo tipo = new Tipo(Tipos.BOOLEAN, null);

            Tipos tipoResultante = util.TablaTipos.getTipo(izquierda.tipo,derecha.tipo);
            if (tipoResultante == Tipos.NULLL)
                throw new util.ErrorPascal(0,0,"Tipos De Dato Incorrectos","Semantico");

            switch (tipoOperacion)
            {
                case "=":
                    return new Simbolo(double.Parse(izquierda.ToString()) == double.Parse(derecha.ToString()), tipo, null);
                case "<>":
                    return new Simbolo(double.Parse(izquierda.ToString()) != double.Parse(derecha.ToString()), tipo, null);
                case ">=":
                    return new Simbolo(double.Parse(izquierda.ToString()) >= double.Parse(derecha.ToString()), tipo, null);
                case ">":
                    return new Simbolo(double.Parse(izquierda.ToString()) > double.Parse(derecha.ToString()), tipo, null);
                case "<=":
                    return new Simbolo(double.Parse(izquierda.ToString()) <= double.Parse(derecha.ToString()), tipo, null);
                case "<":
                    return new Simbolo(double.Parse(izquierda.ToString()) < double.Parse(derecha.ToString()), tipo, null);
                default:
                    return null;
            }

        }
    }
}
