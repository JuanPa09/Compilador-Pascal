using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class ObtenerVariable : Expresion
    {
        private string id;

        public ObtenerVariable(string id)
        {
            this.id = id;
        }

        public string getId()
        {
            return this.id;
        }

        public override Simbolo evaluar(Entorno entorno)
        {
            Simbolo variable = entorno.existeLaVariable(id);
            if (variable == null)
                return entorno.obtenerConstane(id);
            return variable;

            /*if (entorno.existeVariable(id))
                return entorno.obtenerVariable(id);
            if (entorno.existeConstante(id))
                return entorno.obtenerConstane(id);*/
            throw new util.ErrorPascal(0,0,"No se puede obtener el valor de \""+id+"\" porque no esta declarado","semantico");
        }
    }
}
