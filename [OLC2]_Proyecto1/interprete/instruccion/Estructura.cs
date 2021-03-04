using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

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
                    try
                    {
                        instruccion.ejecutar(entorno);
                    }
                    catch( Exception ex) { Debug.WriteLine(ex.ToString()); }
                    
            }

            foreach (var instruccion in instruccionesBody)
            {
                if (instruccion != null)
                    try
                    {
                        instruccion.ejecutar(entorno);
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
            }


            return null;
        }
    }
}
