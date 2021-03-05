using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.simbolo
{
    class Entorno
    {
        Dictionary<string, Simbolo> variables = new Dictionary<string,Simbolo>();
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
            if (variables.Count==0 || !variables.ContainsKey(id))
            {
                this.variables.Add(id, variable);
            }
            else
            {
                throw new util.ErrorPascal(0,0,"La variable \"" + id + "\" ya existe en este ambito","semántico");
            }
        }

        public Simbolo obtenerVariable(string id)
        {
            Entorno actual = this;
            while(actual!=null)
            {
                if(actual.variables.ContainsKey(id))
                    return actual.variables[id];
                actual = actual.padre;              //Busca de padre en padre
            }
            throw new util.ErrorPascal(0,0,"No se puede obtener el valor de la variable \"" + id + "\" porque no esta declarada","Semantico");
        }

        public void modificarVariable(string id,object valor,Tipos tipo)
        {

            Entorno actual = this;
            if (actual.variables.ContainsKey(id))
            {
                Simbolo simbolo = variables[id];
                if (simbolo.tipo.tipo != tipo)
                    throw new util.ErrorPascal(0,0,"No se pudo asignar a la variable \""+id.ToString()+"\" el valor \""+valor.ToString()+"\" porque los datos no coinciden","semántico");
                simbolo.valor = valor;
                variables[id] = simbolo;
                return;
            }
            throw new util.ErrorPascal(0, 0, "La variable \"" + id + "\" no ha sido declarada", "semántico");
        }

        public Entorno buscarEntornoVariable(string id)
        {
            Entorno actual = this;
            while (actual!=null)
            {
                if (actual.variables.ContainsKey(id))
                    return actual;
                actual = actual.padre;
            }
            throw new util.ErrorPascal(0, 0, "La variable \"" + id + "\" no ha sido declarada", "semántico");
        }

        public bool existeVariable(string id)
        {
            return false;
        }

       

        


    }
}
