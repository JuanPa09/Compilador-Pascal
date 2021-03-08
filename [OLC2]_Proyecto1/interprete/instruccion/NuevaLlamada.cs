using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using System.Diagnostics;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevaLlamada : Instruccion
    {

        private string nombre;
        private LinkedList<Expresion> valores = new LinkedList<Expresion>();

        public NuevaLlamada(string nombre, LinkedList<Expresion> valores)
        {
            this.nombre = nombre;
            this.valores = valores;
        }

        public override object ejecutar(Entorno entorno)
        {

            

            Funcion funcion = entorno.existeFuncion(nombre);

            if (funcion == null)
                goto esProcedimiento;

            //Comprobar variables
            if (funcion.varTipos.Count != valores.Count)
                throw new util.ErrorPascal(0,0,"Numero de entradas incorrectas para la funcion \""+nombre+"\"", "semantico");

            //Asignar los valores
            funcion.valoresParametros = valores;

            Simbolo retorno = (Simbolo)funcion.ejecutar(entorno); //Aca

            if (retorno == null)
                throw new util.ErrorPascal(0,0,"La funcion \""+this.nombre+"\" no devolvio ningun valor","semantico");
            return retorno;

        esProcedimiento:

            Procedimiento procedimiento = entorno.existeProcedimiento(nombre);
            if(procedimiento == null)
                throw new util.ErrorPascal(0, 0, "La funcion/procedimiento \"" + nombre + "\" no existe", "semantico");

            //Comprobar variables
            if (procedimiento.varTipos.Count != valores.Count)
                throw new util.ErrorPascal(0, 0, "Numero de entradas incorrectas para la funcion \"" + nombre + "\"", "semantico");

            //Asignar los valores
            procedimiento.valoresParametros = valores;

            procedimiento.ejecutar(entorno); //Aca

            return null;


        }
    }
}
