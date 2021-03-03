using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Estructura : Instruccion
    {
        LinkedList<Instruccion> instruccionesHead;
        LinkedList<Instruccion> instruccionesBody;

        public Estructura(LinkedList<Instruccion> instruccionesHead, LinkedList<Instruccion> instruccionesBody)
        {
            this.instruccionesBody = instruccionesBody;
            this.instruccionesHead = instruccionesHead;
        }

        public override object ejecutar(Entorno entorno)
        {
            foreach (var instruccion in instruccionesHead)
            {
                if (instruccion != null)
                    instruccion.ejecutar(entorno);
            }

            foreach (var instruccion in instruccionesBody)
            {
                if (instruccion != null)
                    instruccion.ejecutar(entorno);
            }


            return null;
        }
    }
}
