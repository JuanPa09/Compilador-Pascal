using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class For : Instruccion
    {
        private Expresion valInicio;
        private Expresion valFinal;
        private string id;
        private LinkedList<Instruccion> instrucciones;
        int linea, columna;
        string tipo;
        public For(Expresion valInicio, Expresion valFinal, string id, LinkedList<Instruccion> instrucciones,int linea, int columna,string tipo)
        {
            this.valInicio = valInicio;
            this.valFinal = valFinal;
            this.id = id;
            this.instrucciones = instrucciones;
            this.linea = linea;
            this.columna = columna;
            this.tipo = tipo;
        }

        public override object ejecutar(Entorno entorno, Reporte reporte)
        {
            try
            {
                //Declarar nuevo ambito y nueva variable
                Entorno entornoFor = new Entorno(".for",entorno,reporte);
                Simbolo valorInicial = valInicio.evaluar(entorno,reporte); // Evaluar el literal para que me devuelva un simbolo
                Simbolo valorFinal = valFinal.evaluar(entorno,reporte);
                if (valorInicial.tipo.tipo != Tipos.NUMBER || valorFinal.tipo.tipo != Tipos.NUMBER)
                    throw new util.ErrorPascal(0,0,"No se puede evaluar la sentencia for porque \""+valorInicial.valor+"\" y/o \""+valorFinal.valor+"\" no coinciden con tipo numero","semántico",reporte);
                entornoFor.declararVariables(id, valorInicial,linea,columna);

                int inicio = int.Parse(valorInicial.valor.ToString());
                int final = int.Parse(valorFinal.valor.ToString());

                if (tipo.ToLower() == "to")
                {
                    while (inicio <= final)
                    {

                        foreach (Instruccion instruccion in instrucciones)
                        {
                            if (instruccion != null)
                                try
                                {
                                    object retorno = instruccion.ejecutar(entornoFor, reporte);
                                    if (retorno != null)
                                        if (retorno.ToString() == "break")
                                        {
                                            goto Fin;
                                        }
                                        else if (retorno.ToString() == "continue")
                                        {
                                            goto Continuar;
                                        }
                                        else
                                        {
                                            return retorno;
                                        }
                                }
                                catch (Exception ex) { ex.ToString(); }
                        }

                    Continuar:;
                        inicio++;
                        entornoFor.modificarVariable(id, inicio, Tipos.NUMBER, null);


                    }
                }
                else
                {
                    while (inicio >= final)
                    {

                        foreach (Instruccion instruccion in instrucciones)
                        {
                            if (instruccion != null)
                                try
                                {
                                    object retorno = instruccion.ejecutar(entornoFor, reporte);
                                    if (retorno != null)
                                        if (retorno.ToString() == "break")
                                        {
                                            goto Fin;
                                        }
                                        else if (retorno.ToString() == "continue")
                                        {
                                            goto Continuar;
                                        }
                                        else
                                        {
                                            return retorno;
                                        }
                                }
                                catch (Exception ex) { ex.ToString(); }
                        }

                    Continuar:;
                        inicio--;
                        entornoFor.modificarVariable(id, inicio, Tipos.NUMBER, null);


                    }
                }


            }catch (Exception ex ) { ex.ToString(); }
            Fin:
            return null;
        }
    }
}
