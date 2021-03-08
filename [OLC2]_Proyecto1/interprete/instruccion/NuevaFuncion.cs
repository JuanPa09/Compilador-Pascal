using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevaFuncion : Instruccion
    {

        private string nombre;
        private Funcion funcion;

        public NuevaFuncion(string nombre, Funcion funcion)
        {
            this.nombre = nombre;
            this.funcion = funcion;
        }

        public override object ejecutar(Entorno entorno)
        {
            entorno.declararFuncion(nombre, funcion);
            return null;
        }
    }
}
