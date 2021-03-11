using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.util;
using _OLC2__Proyecto1.interprete.instruccion;
using System.Diagnostics;

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

        public override Simbolo evaluar(Entorno entorno)
        {
            string Nombre = "";
            string Parametro = "";
            object evaluado = getValor(nombre, entorno);
            if (evaluado is Objeto)
            {
                return ((Objeto)getValor(nombre, entorno)).getValor(getValor(parametro,entorno).ToString());
            }

            Nombre = evaluado.ToString();

            Parametro = getValor(parametro, entorno).ToString();


            Simbolo objectType = entorno.obtenerVariable(Nombre);

            if (objectType.tipo.tipo != Tipos.OBJECT)
                throw new ErrorPascal(0, 0, "La variable \"" + Nombre + "\" no es un objeto", "semantico");

            Objeto objeto = (Objeto)objectType.valor;

            return (objeto.getValor(Parametro));
        }

        private object getValor(Expresion expresion,Entorno entorno)
        {
            if(expresion is ObtenerVariable)
            {
                return ((ObtenerVariable)expresion).id;
            }
            else if (expresion is ObtenerObjeto)
            {
                return nombre.evaluar(entorno).valor;
            }
            else
            {
                throw new util.ErrorPascal(0, 0, "valor no valido para obtener objeto", "semantico");
            }
        }
    }
}
