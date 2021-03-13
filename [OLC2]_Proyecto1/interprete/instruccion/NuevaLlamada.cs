using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using System.Diagnostics;
using _OLC2__Proyecto1.reportes;

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

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {

            

            Funcion funcion = entorno.existeFuncion(nombre);

            if (funcion == null)
                goto esProcedimiento;

            //Comprobar variables
            if (funcion.varTipos.Count != valores.Count)
                throw new util.ErrorPascal(0,0,"Numero de entradas incorrectas para la funcion \""+nombre+"\"", "semantico",reporte);

            //Asignar los valores
            funcion.valoresParametros = valores;

            Simbolo retorno = (Simbolo)funcion.ejecutar(entorno,reporte); //Aca

            if (retorno == null)
                throw new util.ErrorPascal(0,0,"La funcion \""+this.nombre+"\" no devolvio ningun valor","semantico",reporte);
            return retorno;

        esProcedimiento:

            Procedimiento procedimiento = entorno.existeProcedimiento(nombre);
            if(procedimiento == null)
                throw new util.ErrorPascal(0, 0, "La funcion/procedimiento \"" + nombre + "\" no existe", "semantico",reporte);

            //Comprobar variables
            if (procedimiento.varTipos.Count != valores.Count)
                throw new util.ErrorPascal(0, 0, "Numero de entradas incorrectas para la funcion \"" + nombre + "\"", "semantico",reporte);

            //Asignar los valores
            procedimiento.valoresParametros = valores;

            procedimiento.ejecutar(entorno,reporte); //Aca

            return null;


        }
    }
}
