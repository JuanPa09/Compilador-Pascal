using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevaFuncion : Instruccion
    {

        private string nombre;
        private Funcion funcion;
        int linea, columna;

        public NuevaFuncion(string nombre, Funcion funcion,int linea,int columna)
        {
            this.nombre = nombre;
            this.funcion = funcion;
            this.linea = linea;
            this.columna = columna;
        }

        public override object ejecutar(Entorno entorno, Reporte reporte)
        {
            if (entorno.existeFuncion(nombre) != null || entorno.existeProcedimiento(nombre) != null)
                throw new util.ErrorPascal(0, 0, "Ya hay un(a) procedimiento/funcion con el nombre \"" + nombre + "\" en este ambito", "semántico",reporte);
            entorno.declararFuncion(nombre, funcion,linea,columna);
            return null;
        }
    }
}
