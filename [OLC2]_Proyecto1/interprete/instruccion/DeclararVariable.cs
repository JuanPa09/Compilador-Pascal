using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.reportes;
namespace _OLC2__Proyecto1.interprete.instruccion
{
    class DeclararVariable : Instruccion
    {
        

        private LinkedList<Instruccion> listaDeclaraciones;

        public DeclararVariable (LinkedList<Instruccion> listaDeclaraciones)
        {
            this.listaDeclaraciones = listaDeclaraciones;
        }
        

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {

            foreach(Instruccion declaracion in listaDeclaraciones)
            {
                if (declaracion!=null)
                    declaracion.ejecutar(entorno,reporte);
            }

            return null;

        }
    }
}
