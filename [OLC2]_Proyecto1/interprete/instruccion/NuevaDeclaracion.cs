using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevaDeclaracion : Instruccion
    {
        public Expresion literal;
        public string id;
        public Tipo tipo;
        public bool isVariable;
        int linea,columna;


        public NuevaDeclaracion(Expresion literal, string id,Tipo tipo, bool isVariable,int linea,int columna) 
        {
            this.literal = literal;
            this.id = id;
            this.tipo = tipo;
            this.isVariable = isVariable;
            this.linea = linea;
            this.columna = columna;
        }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            Simbolo literalEvaluado;
            Simbolo variable;

            if(tipo.tipo == Tipos.TYPE)
            {
                Simbolo type = entorno.obtenerType(tipo.tipoAuxiliar);
                if(type.tipo.tipo == Tipos.OBJECT)
                {
                    Objeto nuevoObjeto = new Objeto((Objeto)type.valor);
                    tipo.tipo = Tipos.OBJECT;
                    entorno.declararVariables(id,new Simbolo(nuevoObjeto,tipo,id),linea,columna);
                }
                else
                {
                    //Es array
                    Dictionary<int,object> nuevoArreglo = new Dictionary<int, object>((Dictionary<int, object>)type.valor);

                    //tipo.tipo = Tipos.ARRAY;
                    //entorno.tipoArreglo[id] = entorno.tipoArreglo[tipo.tipoAuxiliar];[
                    tipo.tipo = Tipos.ARRAY;
                    entorno.tipoArreglo.Add(id,entorno.buscarTipoArreglo(type.id));
                    entorno.declararVariables(id, new Simbolo(nuevoArreglo, tipo, id), linea, columna);
                }
                return null;
            }
            

            if (literal != null)
            {
                literalEvaluado = literal.evaluar(entorno,reporte);

                if (literalEvaluado.tipo.tipo != tipo.tipo)
                    throw new util.ErrorPascal(0,0,"No se puede declarar la variable/constante \""+id+"\". Tipos de dato incorrecto","semántico",reporte);

                variable = new Simbolo(literalEvaluado.valor, tipo/*new Tipo(this.tipo.tipo,null)*/, this.id);
            }
            else
            {
                object valorDefecto = null;
                switch(tipo.tipo)
                {
                    case Tipos.NUMBER:
                        valorDefecto = 0;
                        break;
                    case Tipos.STRING:
                        valorDefecto = "";
                        break;
                    case Tipos.DOUBLE:
                        valorDefecto = 0;
                        break;
                    case Tipos.BOOLEAN:
                        valorDefecto = false;
                        break;
                }
                variable = new Simbolo(valorDefecto, /*new Tipo(this.tipo.tipo*/tipo, this.id);
            }

            if (entorno.existeVariable(id) || entorno.existeConstante(id))
                throw new util.ErrorPascal(0, 0, "Este id: \"" + id + "\" ya existe en este ambito", "semántico",reporte);

            if (isVariable)
            {
                entorno.declararVariables(id, variable,linea,columna);
            }
            else
            {
                entorno.declararConstante(id, variable,linea,columna);
            }

            return null;
        }
    }
}
