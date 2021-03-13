using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.interprete.simbolo;
using System.Diagnostics;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Repeat : Instruccion
    {
        LinkedList<Instruccion> instrucciones;
        Expresion expresionLogica;

        public Repeat(Expresion expresionLogica, LinkedList<Instruccion> instrucciones)
        {
            this.expresionLogica = expresionLogica;
            this.instrucciones = instrucciones;
        }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            Entorno entornoRepeat = new Entorno(".repeat",entorno,reporte);
            try
            {
                do
                {
                    Simbolo expresionLogica = this.expresionLogica.evaluar(entorno,reporte);
                    if (expresionLogica.tipo.tipo != Tipos.BOOLEAN)
                        throw new util.ErrorPascal(0, 0, "No es una expresion logica", "semantico",reporte);

                    if (!bool.Parse(expresionLogica.valor.ToString()))
                    {
                        foreach (Instruccion instruccion in instrucciones)
                        {
                            if (instruccion != null)
                                try
                                {
                                    object retorno = instruccion.ejecutar(entornoRepeat,reporte);
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
                    }
                    else
                    {
                        break;
                    }
                    Continuar:;
                } while (true);
            }catch(Exception ex) { Debug.WriteLine(ex.ToString()); }
            Fin:;
            return null;

        }
    }
}
