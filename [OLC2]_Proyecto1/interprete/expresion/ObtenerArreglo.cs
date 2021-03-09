using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class ObtenerArreglo : Expresion
    {
        string nombre;
        LinkedList<int> valores;
        public ObtenerArreglo(string nombre,LinkedList<int>valores)
        {
            this.nombre = nombre;
            this.valores = valores;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            Dictionary<int,object> arreglo = (Dictionary<int,object>)entorno.obtenerVariable(nombre).valor;

            int i = 1;
            foreach(int index in valores)
            {                
                if (!arreglo.ContainsKey(index))
                    throw new util.ErrorPascal(0, 0, "Acceso denegado a \""+this.nombre+"\" No se puede acceder al indice \"" + index + "\" en la posicion "+(i - 1), "semantico");
                if (i == valores.Count)
                    if (arreglo[index] is Dictionary<int, object>)
                    {
                        throw new util.ErrorPascal(0,0,"No se devolvio un valor con los indices dados para \""+nombre+"\"","semantico");
                    }
                    else
                    {
                        return new Simbolo(arreglo[index], new Tipo(entorno.getTipoArray(nombre), null), null);
                    }
                if (!(arreglo[index] is Dictionary<int, object>))
                    throw new util.ErrorPascal(0,0,"Indices incorrectos para el arreglo \""+this.nombre+"\"","semantico");
                arreglo = (Dictionary<int, object>)arreglo[index];
                i++;
            }


            throw new util.ErrorPascal(0,0,"No se pudo obtener el valor del array \""+nombre+"\"","semantico"); 
        }
    }
}
