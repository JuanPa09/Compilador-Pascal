using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.interprete.simbolo;
using System.Diagnostics;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Case : Instruccion
    {

        Dictionary<Expresion, LinkedList<Instruccion>> casos;
        Expresion valor;
        LinkedList<Instruccion> caseDefault;

        public Case(Expresion valor, Dictionary<Expresion, LinkedList<Instruccion>> casos, LinkedList<Instruccion> caseDefault)
        {
            this.valor = valor;
            this.casos = casos;
            this.caseDefault = caseDefault;
        }
        

        public override object ejecutar(Entorno entorno)
        {
            Simbolo valorReal = valor.evaluar(entorno);
            Dictionary<object, LinkedList<Instruccion>>  casosEvaluados = new Dictionary<object, LinkedList<Instruccion>>();
            foreach(KeyValuePair<Expresion,LinkedList<Instruccion>> entry in casos)
            {
                casosEvaluados.Add(entry.Key.evaluar(entorno).valor,entry.Value);
            }

            try
            {
                Entorno entornoCasos = new Entorno(".caso",entorno);
                if (casosEvaluados.Count != 0 && casosEvaluados.ContainsKey(valorReal.valor))
                {
                    foreach (Instruccion instruccion in casosEvaluados[valorReal.valor])
                    {
                        try
                        {
                            return instruccion.ejecutar(entornoCasos);
                        }
                        catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
                    }
                    return null;
                }
                //Evaluar else condition
                foreach (Instruccion instruccion in caseDefault)
                {
                    try
                    {
                        return instruccion.ejecutar(entornoCasos);
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.ToString()); }

            return null;
        }
    }
}
