using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using System.Diagnostics;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Funcion : Instruccion
    {
        public string nombre;
        public Tipo tipo;
        public Dictionary<string, Instruccion> variables_Valor; //Instruccion -> NuevaDeclaracion
        public Dictionary<string, Instruccion> variables_Referencia; // Instruccion -> NuevaDeclaracion
        public LinkedList<Instruccion> instrucciones;
        public LinkedList<Tipo> varTipos;
        public Dictionary<int, string> ordenVariables;
        public LinkedList<Expresion> valoresParametros = new LinkedList<Expresion>();
        public Funcion(string nombre, Tipo tipo, Dictionary<string, Instruccion> variables_Valor, Dictionary<string, Instruccion> variables_Referencia, LinkedList<Instruccion> instrucciones, LinkedList<Tipo>varTipos,Dictionary<int, string> ordenVariables)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.variables_Valor = variables_Valor;
            this.variables_Referencia = variables_Referencia;
            this.instrucciones = instrucciones;
            this.varTipos = varTipos;
            this.ordenVariables = ordenVariables;
        }

        


        public override object ejecutar(Entorno entorno)
        {
            Entorno entornoFuncion = new Entorno(nombre,entorno);

            entornoFuncion.declararVariables(this.nombre, new Simbolo(null,new Tipo(tipo.tipo,null),null));

            //Variables Por Valor
            foreach (KeyValuePair<string,Instruccion> varValor in variables_Valor)
            {
                varValor.Value.ejecutar(entornoFuncion);
            }
            
            //Variables Por Referencia
            foreach(KeyValuePair<string,Instruccion> varReferencia in variables_Referencia)
            {
                varReferencia.Value.ejecutar(entornoFuncion);
            }

            int index = 1;
            foreach(Expresion valor in valoresParametros)
            {
                Simbolo valorParametro = valor.evaluar(entorno);
                string nombreVariable = ordenVariables[index];
                entornoFuncion.modificarVariable(nombreVariable,valorParametro.valor,valorParametro.tipo.tipo,valorParametro.id);
                index++;
            }



            foreach(Instruccion instruccion in instrucciones)
            {
                //Puede ser que venga un retorno tipo -> funcion := expresion
                object retorno = null;

                try { retorno = instruccion.ejecutar(entornoFuncion); } catch (Exception ex) { Debug.WriteLine(ex.ToString());};

                if (retorno != null)
                {
                    if (retorno == "$$")
                        throw new util.ErrorPascal(0,0,"La funcion \""+nombre+"\" salio sin retornar nada","semantico"); ;
                    //Hay un retorno de funcion -> Retorna un simbolo

                    //Pasar Las Variables Por Referencia
                    agregarValoresReferenciados();

                    //Retornar el valor dado.
                    return retorno;
                }
            }



            int buscarPosicion(string id)
            {
                foreach(KeyValuePair<int,string> orden in ordenVariables)
                {
                    if (orden.Value == id)
                        return orden.Key;
                }
                return -1;
            }

            string buscarNombreVariable(int index)
            {
                int i = 1;
                foreach(Expresion expresion in valoresParametros)
                {
                    if (index == i)
                        return expresion.evaluar(entorno).id;
                    i++;
                }
                return null;
            }

            void agregarValoresReferenciados()
            {
                foreach (KeyValuePair<string, Instruccion> varRef in variables_Referencia)
                {

                    Simbolo variable = entornoFuncion.obtenerVariable(varRef.Key);
                    /*if (variable.tipo.tipo != this.tipo)
                        throw new util.ErrorPascal(0, 0, "Tipo de dato incorrecto para el retorno de la funcion", "semantico");*/

                    int pos = buscarPosicion(varRef.Key);
                    if (pos == -1)
                        throw new util.ErrorPascal(0, 0, "No existe variable de referencia", "semantico");
                    string name = buscarNombreVariable(pos);
                    if (name == null)
                        throw new util.ErrorPascal(0, 0, "No existe variable de referencia", "semantico");
                    entorno.modificarVariable(name, variable.valor, variable.tipo.tipo,variable.id);

                }
            }


            agregarValoresReferenciados();
            return null;
        }
    }
}
