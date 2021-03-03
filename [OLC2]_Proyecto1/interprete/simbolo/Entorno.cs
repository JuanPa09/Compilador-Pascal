using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.simbolo
{
    class Entorno
    {
        Dictionary<string, Simbolo> variables;
        Dictionary<string, object> funciones;
        Dictionary<string, object> structs;
        Entorno padre;

        public Entorno(Entorno padre)
        {
            this.padre = padre;
            this.variables = new Dictionary<string, Simbolo>();
        }

        public void declararVariables(string id, Simbolo variable)
        {
            if (this.variables[id] != null)
            {
                this.variables.Add(id, variable);
            }
            else
            {
                throw new Exception("La variable " + id + "ya existe en este ambito");
            }
        }

        public Simbolo obtenerVariable(string id)
        {
            Entorno actual = this;
            while(actual!=null)
            {
                if(actual.variables[id]!=null)
                    return actual.variables[id];
                actual = actual.padre;              //Busca de padre en padre
            }
            return null;
        }

        public bool existeVariable(string id)
        {
            return false;
        }


    }
}
