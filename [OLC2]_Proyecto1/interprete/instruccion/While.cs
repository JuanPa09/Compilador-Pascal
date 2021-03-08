using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
namespace _OLC2__Proyecto1.interprete.instruccion
{

    class While : Instruccion
    {

        LinkedList<Instruccion> instrucciones;
        Expresion expresionLogica;

        public While(LinkedList<Instruccion> instrucciones, Expresion expresionLogica)
        {
            this.instrucciones = instrucciones;
            this.expresionLogica = expresionLogica;
        }

        public override object ejecutar(Entorno entorno)
        {
            Entorno entornoWhile = new Entorno(".while",entorno);
            try
            {
                while (true)
                {
                    Simbolo expresionLogica = this.expresionLogica.evaluar(entorno);
                    if (expresionLogica.tipo.tipo != Tipos.BOOLEAN)
                        throw new util.ErrorPascal(0, 0, "No es una expresion logica", "semantico");

                    if (bool.Parse(expresionLogica.valor.ToString()))
                    {
                        foreach (Instruccion instruccion in instrucciones)
                        {
                            if (instruccion != null)
                                try
                                {
                                    object retorno = instruccion.ejecutar(entornoWhile);
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
                }
            }catch(Exception ex) { ex.ToString(); }
            Fin:;
            return null;
        }
    }
}
