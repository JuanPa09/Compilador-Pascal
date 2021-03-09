using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class AsignacionArreglo : Instruccion
    {

        string nombre;
        LinkedList<int> indices;
        Expresion expresion;

        public AsignacionArreglo(string nombre, LinkedList<int> indices, Expresion expresion)
        {
            this.indices = indices;
            this.expresion = expresion;
            this.nombre = nombre;
        }


        public override object ejecutar(Entorno entorno)
        {

            Simbolo variable = entorno.obtenerVariable(nombre);

            Dictionary<int, object> diccionario = (Dictionary<int,object>)variable.valor;

            int i = 1;
            foreach(int indice in indices)
            {
                if (!diccionario.ContainsKey(indice))
                    throw new util.ErrorPascal(0, 0, "Acceso denegado a \"" + this.nombre + "\" No se puede acceder al indice \"" + indice + "\" en la posicion " + (i - 1), "semantico");
                if (i == indices.Count)
                {
                    if (diccionario[indice] is Dictionary<int,object>)
                    {
                        throw new util.ErrorPascal(0, 0, "No se puede asignar un valor con los indices dados para \"" + nombre + "\"", "semantico");
                    }
                    else
                    {
                        Simbolo valExpresion = expresion.evaluar(entorno);
                        if (valExpresion.tipo.tipo != entorno.getTipoArray(nombre))
                            throw new util.ErrorPascal(0,0,"No se puede asignar tipos de datos diferentes en el arreglo \""+nombre+"\"","semantico"); ;
                        diccionario[indice] = expresion.evaluar(entorno);
                    }
                }
                else
                {
                    if (!(diccionario[indice] is Dictionary<int, object>))
                        throw new util.ErrorPascal(0, 0, "Indices incorrectos para el arreglo \"" + this.nombre + "\"", "semantico");
                    diccionario = (Dictionary<int,object>)diccionario[indice];
                }
                i++;
            }


            return null;
        }
    }
}
