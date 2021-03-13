using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.util;
using _OLC2__Proyecto1.interprete.instruccion;
using System.Diagnostics;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class ObtenerObjeto : Expresion
    {
        Expresion nombre;
        Expresion parametro;
        
        public ObtenerObjeto(Expresion nombre, Expresion parametro)
        {
            this.nombre = nombre;
            this.parametro = parametro;
        }

        public override Simbolo evaluar(Entorno entorno,Reporte reporte)
        {
            string Nombre = "";
            string Parametro = "";
            object evaluado = getValor(nombre, entorno,reporte);
            if (evaluado is Objeto)
            {
                return ((Objeto)getValor(nombre, entorno,reporte)).getValor(getValor(parametro,entorno,reporte).ToString());
            }

            Nombre = evaluado.ToString();

            Parametro = getValor(parametro, entorno,reporte).ToString();


            Simbolo objectType = entorno.obtenerVariable(Nombre);

            if (objectType.tipo.tipo != Tipos.OBJECT)
                throw new ErrorPascal(0, 0, "La variable \"" + Nombre + "\" no es un objeto", "semantico",reporte);

            Objeto objeto = (Objeto)objectType.valor;

            return (objeto.getValor(Parametro));
        }

        private object getValor(Expresion expresion,Entorno entorno,Reporte reporte)
        {
            if(expresion is ObtenerVariable)
            {
                return ((ObtenerVariable)expresion).id;
            }
            else if (expresion is ObtenerObjeto)
            {
                return nombre.evaluar(entorno,reporte).valor;
            }
            else
            {
                throw new util.ErrorPascal(0, 0, "valor no valido para obtener objeto", "semantico",reporte);
            }
        }
    }
}
