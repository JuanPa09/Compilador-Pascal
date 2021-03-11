using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using System.Linq;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class AsignacionArreglo : Instruccion
    {

        string nombre;
        LinkedList<Expresion> indices;
        Expresion expresion;

        public AsignacionArreglo(string nombre, LinkedList<Expresion> indices, Expresion expresion)
        {
            this.indices = indices;
            this.expresion = expresion;
            this.nombre = nombre;
        }


        public override object ejecutar(Entorno entorno)
        {

            Simbolo variable = entorno.obtenerVariable(nombre);

            
            Dictionary<int, object> diccionario = (Dictionary<int, object>)variable.valor;



            LinkedList<int> indexes = new LinkedList<int>();
             foreach(Expresion expresion in indices)
             {
                 Simbolo valor = expresion.evaluar(entorno);
                 if (valor.tipo.tipo != Tipos.NUMBER)
                     throw new util.ErrorPascal(0,0,"No se puede tomar \""+valor.valor+"\" como indice para "+nombre,"semantico");
                 indexes.AddLast(int.Parse(valor.valor.ToString()));
             }

             int i = 1;
             foreach(int indice in indexes)
             {
                 if (diccionario == null)
                     throw new util.ErrorPascal(0, 0, "No se puede acceder al indice \""+indice+"\" porque no se ha inicializado el arreglo \""+nombre+"\"", "semantico");
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
                             throw new util.ErrorPascal(0,0,"No se puede asignar tipos de datos diferentes en el arreglo \""+nombre+"\"","semantico"); 
                         diccionario[indice] = valExpresion.valor;

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
