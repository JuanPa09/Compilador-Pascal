using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class Aritmetica:Expresion
    {
        private Expresion izquierda;
        private Expresion derecha;
        private char tipo;


        public Aritmetica(Expresion izquierda, Expresion derecha, char tipo) 
        {
            this.izquierda = izquierda;
            this.derecha = derecha;
            this.tipo = tipo;
        }

        public override Simbolo evaluar(Entorno entorno, Reporte reporte)
        {
            Simbolo izquierda = this.izquierda.evaluar(entorno,reporte);
            Simbolo derecha = this.derecha.evaluar(entorno,reporte);
            Simbolo resultado;
            Tipos tipoResultante = util.TablaTipos.getTipo(izquierda.tipo,derecha.tipo);

            if (tipoResultante != Tipos.NUMBER && tipo != '+')
                throw new util.ErrorPascal(0,0,"Operacion invalida","semantico",reporte); // Cambiar Exception por errorPascal

            switch (tipo)
            {
                case '+':
                    if (izquierda.tipo.tipo == Tipos.STRING || derecha.tipo.tipo == Tipos.STRING) {
                        //Concatenar
                        resultado = new Simbolo(izquierda.ToString() + derecha.ToString(), izquierda.tipo, null);
                    }
                    else
                    {
                        //Sumar
                        resultado = new Simbolo(double.Parse(izquierda.ToString()) + double.Parse(derecha.ToString()), izquierda.tipo, null);
                    }
                    return resultado;
                case '-':
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) - double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                case '*':
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) * double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                case '/':
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) / double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
                default:
                    resultado = new Simbolo(double.Parse(izquierda.ToString()) % double.Parse(derecha.ToString()), izquierda.tipo, null);
                    return resultado;
            }

        }
    }
}
