using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class DeclararObjeto : Instruccion
    {

        LinkedList<Instruccion> declaracionesObjetos;
        string nombre;
        int linea;
        int columna;

        public DeclararObjeto(string nombre, LinkedList<Instruccion> declaracionesObjetos,int linea,int columna)
        {
            this.nombre = nombre;
            this.declaracionesObjetos = declaracionesObjetos;
            this.linea = linea;
            this.columna = columna;
        }

        

        public override object ejecutar(Entorno entorno, Reporte reporte)
        {
            Objeto objeto = new Objeto(nombre,new Dictionary<string, Simbolo>(),new Dictionary<string, Tipo>(),reporte);
            foreach(Instruccion declaracion in declaracionesObjetos)
            {
                if (declaracion == null)
                    continue;
                NuevaDeclaracion nuevaDeclaracion = (NuevaDeclaracion)declaracion;


                Simbolo literalEvaluado;
                Simbolo variable;



                if (nuevaDeclaracion.literal != null)
                {
                    literalEvaluado = nuevaDeclaracion.literal.evaluar(entorno,reporte);

                    if (literalEvaluado.tipo.tipo != nuevaDeclaracion.tipo.tipo)
                        throw new util.ErrorPascal(0, 0, "No se puede declarar la variable/constante \"" + nuevaDeclaracion.id + "\". Tipos de dato incorrecto", "semántico",reporte);

                    variable = new Simbolo(literalEvaluado.valor, new Tipo(nuevaDeclaracion.tipo.tipo, null), nuevaDeclaracion.id);
                }
                else
                {
                    variable = new Simbolo(null, new Tipo(nuevaDeclaracion.tipo.tipo, null), nuevaDeclaracion.id);
                }

                if (entorno.existeVariable(nuevaDeclaracion.id) || entorno.existeConstante(nuevaDeclaracion.id))
                    throw new util.ErrorPascal(0, 0, "Este id: \"" + nuevaDeclaracion.id + "\" ya existe en este ambito", "semántico",reporte);

                if (nuevaDeclaracion.isVariable)
                {
                    objeto.agregarPropiedad(nuevaDeclaracion.id,nuevaDeclaracion.tipo);
                    //variable.tipo.tipoAuxiliar = nombre;
                    objeto.setValor(nuevaDeclaracion.id, variable);

                }
                else
                {
                   // entorno.declararConstante(nuevaDeclaracion.id, variable);
                }
            }

            entorno.declararType(nombre,new Simbolo(objeto,new Tipo(Tipos.OBJECT,nombre),nombre),linea,columna);

            return null;
        }
    }
}
