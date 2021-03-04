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

        public NuevaDeclaracion(Expresion literal, string id,Tipos tipo) 
        {
            this.literal = literal;
            this.id = id;
            this.tipo = tipo;
        }

        public override object ejecutar(Entorno entorno)
        {
            Simbolo literalEvaluado;
            Simbolo variable;

            

            if (literal != null)
            {
                literalEvaluado = literal.evaluar(entorno);

                if (literalEvaluado.tipo.tipo != tipo)
                    throw new util.ErrorPascal(0,0,"No se puede declarar la variable \""+id+"\". Tipos de dato incorrecto","semántico");

                variable = new Simbolo(literalEvaluado.valor, new Tipo(this.tipo,null), this.id);
            }
            else
            {
                variable = new Simbolo(null, new Tipo(this.tipo, null), this.id);
            }

            entorno.declararVariables(id, variable);

            return null;
        }
    }
}
