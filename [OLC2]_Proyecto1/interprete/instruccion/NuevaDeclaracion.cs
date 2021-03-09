using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;


namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevaDeclaracion : Instruccion
    {
        private Expresion literal;
        private string id;
        Tipos tipo;
        private bool isVariable;

        public NuevaDeclaracion(Expresion literal, string id,Tipos tipo, bool isVariable) 
        {
            this.literal = literal;
            this.id = id;
            this.tipo = tipo;
            this.isVariable = isVariable;
        }

        public override object ejecutar(Entorno entorno)
        {
            Simbolo literalEvaluado;
            Simbolo variable;

            

            if (literal != null)
            {
                literalEvaluado = literal.evaluar(entorno);

                if (literalEvaluado.tipo.tipo != tipo)
                    throw new util.ErrorPascal(0,0,"No se puede declarar la variable/constante \""+id+"\". Tipos de dato incorrecto","semántico");

                variable = new Simbolo(literalEvaluado.valor, new Tipo(this.tipo,null), this.id);
            }
            else
            {
                variable = new Simbolo(null, new Tipo(this.tipo, null), this.id);
            }

            if (entorno.existeVariable(id) || entorno.existeConstante(id))
                throw new util.ErrorPascal(0, 0, "Este id: \"" + id + "\" ya existe en este ambito", "semántico");

            if (isVariable)
            {
                entorno.declararVariables(id, variable);
            }
            else
            {
                entorno.declararConstante(id, variable);
            }

            return null;
        }
    }
}
