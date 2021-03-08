using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevoProcedimiento : Instruccion
    {

        private string nombre;
        private Procedimiento procedimiento;

        public NuevoProcedimiento(string nombre, Procedimiento procedimiento)
        {
            this.nombre = nombre;
            this.procedimiento = procedimiento;
        }

        public override object ejecutar(Entorno entorno)
        {
            entorno.declararProcedimiento(nombre,procedimiento);

            return null;
        }
    }
}
