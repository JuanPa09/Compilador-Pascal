using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevoProcedimiento : Instruccion
    {

        private string nombre;
        private Procedimiento procedimiento;
        int linea,columna;


        public NuevoProcedimiento(string nombre, Procedimiento procedimiento,int linea, int columna)
        {
            this.nombre = nombre;
            this.procedimiento = procedimiento;
            this.linea = linea;
            this.columna = columna;
        }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            if (entorno.existeFuncion(nombre) != null || entorno.existeProcedimiento(nombre) != null)
                throw new util.ErrorPascal(0, 0, "Ya hay un(a) procedimiento/funcion con el nombre \"" + nombre + "\" en este ambito", "semántico",reporte);
            entorno.declararProcedimiento(nombre, procedimiento,linea,columna);
            return null;
        }
    }
}
