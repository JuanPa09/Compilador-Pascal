﻿using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class For : Instruccion
    {
        private Expresion valInicio;
        private Expresion valFinal;
        private string id;
        private LinkedList<Instruccion> instrucciones;
        public For(Expresion valInicio, Expresion valFinal, string id, LinkedList<Instruccion> instrucciones)
        {
            this.valInicio = valInicio;
            this.valFinal = valFinal;
            this.id = id;
            this.instrucciones = instrucciones;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                //Declarar nuevo ambito y nueva variable
                Entorno entornoFor = new Entorno(entorno);
                Simbolo valorInicial = valInicio.evaluar(entorno); // Evaluar el literal para que me devuelva un simbolo
                Simbolo valorFinal = valFinal.evaluar(entorno);
                if (valorInicial.tipo.tipo != Tipos.NUMBER || valorFinal.tipo.tipo != Tipos.NUMBER)
                    throw new util.ErrorPascal(0,0,"No se puede evaluar la sentencia for porque \""+valorInicial.valor+"\" y/o \""+valorFinal.valor+"\" no coinciden con tipo numero","semántico");
                entornoFor.declararVariables(id, valorInicial);

                int inicio = int.Parse(valorInicial.valor.ToString());
                int final = int.Parse(valorFinal.valor.ToString());

                while (inicio != final)
                {
                    try
                    {
                        foreach (Instruccion instruccion in instrucciones)
                        {
                            instruccion.ejecutar(entornoFor);
                        }
                    }catch( Exception ex) { ex.ToString(); }
                    inicio++;
                    entornoFor.modificarVariable(id, inicio,Tipos.NUMBER);
                    if (inicio > final)
                        throw new Exception("Error en for");
                }


            }catch (Exception ex ) { ex.ToString(); }
            return null;
        }
    }
}
