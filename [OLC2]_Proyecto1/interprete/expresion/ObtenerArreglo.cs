using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class ObtenerArreglo : Expresion
    {
        string nombre;
        LinkedList<Expresion> valores;
        public ObtenerArreglo(string nombre,LinkedList<Expresion>valores)
        {
            this.nombre = nombre;
            this.valores = valores;
        }

        public override Simbolo evaluar(Entorno entorno, Reporte reporte)
        {
            Dictionary<int,object> arreglo = (Dictionary<int,object>)entorno.obtenerVariable(nombre).valor;

            LinkedList<int> indexes = new LinkedList<int>();
            foreach (Expresion expresion in valores)
            {
                Simbolo valor = expresion.evaluar(entorno,reporte);
                if (valor.tipo.tipo != Tipos.NUMBER)
                    throw new util.ErrorPascal(0, 0, "No se puede tomar \"" + valor.valor + "\" como indice para " + nombre, "semantico",reporte);
                indexes.AddLast(int.Parse(valor.valor.ToString()));
            }

            int i = 1;
            foreach(int index in indexes)
            {                
                if (!arreglo.ContainsKey(index))
                    throw new util.ErrorPascal(0, 0, "Acceso denegado a \""+this.nombre+"\" No se puede acceder al indice \"" + index + "\" en la posicion "+(i - 1), "semantico",reporte);
                if (i == valores.Count)
                    if (arreglo[index] is Dictionary<int, object>)
                    {
                        throw new util.ErrorPascal(0,0,"No se devolvio un valor con los indices dados para \""+nombre+"\"","semantico",reporte);
                    }
                    else
                    {
                        return new Simbolo(arreglo[index], new Tipo(entorno.getTipoArray(nombre), null), null);
                    }
                if (!(arreglo[index] is Dictionary<int, object>))
                    throw new util.ErrorPascal(0,0,"Indices incorrectos para el arreglo \""+this.nombre+"\"","semantico",reporte);
                arreglo = (Dictionary<int, object>)arreglo[index];
                i++;
            }


            throw new util.ErrorPascal(0,0,"No se pudo obtener el valor del array \""+nombre+"\"","semantico",reporte); 
        }
    }
}
