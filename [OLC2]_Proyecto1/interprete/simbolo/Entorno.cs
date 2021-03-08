using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.instruccion;
using System.Diagnostics;

namespace _OLC2__Proyecto1.interprete.simbolo
{
    class Entorno
    {
        string nombre;
        Dictionary<string, Simbolo> variables /*= new Dictionary<string,Simbolo>()*/;
        Dictionary<string, Funcion> funciones;
        Dictionary<string, Procedimiento> procedimiento;
        Dictionary<string, object> structs;
        public Entorno padre;

        public Entorno(string nombreEntorno,Entorno padre)
        {
            this.nombre = nombreEntorno;
            this.padre = padre;
            this.variables = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
            this.procedimiento = new Dictionary<string, Procedimiento>();
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

        public object modificarVariable(string id,object valor,Tipos tipo)
        {

            Entorno actual = this;
            if (actual.variables.ContainsKey(id))
            {
                Simbolo simbolo = variables[id];
                if (simbolo.tipo.tipo != tipo)
                    throw new util.ErrorPascal(0,0,"No se pudo asignar a la variable \""+id.ToString()+"\" el valor \""+valor.ToString()+"\" porque los datos no coinciden","semántico");
                simbolo.valor = valor;
                variables[id] = simbolo;
                if (id == nombre) // Si es un retorno de funcion
                    return new Simbolo(valor, new Tipo(tipo,null), null);
                return null;
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


        public void declararFuncion(string nombre, Funcion funcion)
        {
            Debug.WriteLine("Nombre Entorno -> "+ this.nombre.ToString() + " Funcion -> "+funcion.nombre);
            this.funciones.Add(nombre, funcion);
        }

        public Funcion existeFuncion(string id)
        {
            Entorno actual = this;
            Debug.WriteLine("Nombre Entorno -> "+actual.nombre.ToString());
            while (actual != null)
            {
                if (actual.funciones.ContainsKey(id))
                    return actual.funciones[id];
                actual = actual.padre;
            }
            return null;
        }



        public void declararProcedimiento(string nombre, Procedimiento procedimiento)
        {
            Debug.WriteLine("Nombre Entorno -> " + this.nombre.ToString() + " Funcion -> " + procedimiento.nombre);
            this.procedimiento.Add(nombre, procedimiento);
        }

        public Procedimiento existeProcedimiento(string id)
        {
            Entorno actual = this;
            Debug.WriteLine("Nombre Entorno -> " + actual.nombre.ToString());
            while (actual != null)
            {
                if (actual.procedimiento.ContainsKey(id))
                    return actual.procedimiento[id];
                actual = actual.padre;
            }
            return null;
        }



        public bool existeVariable(string id)
        {
            return false;
        }

       

        


    }
}
