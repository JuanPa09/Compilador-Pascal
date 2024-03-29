﻿using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.interprete.expresion;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class AsignacionObjeto : Instruccion
    {

        string nombre;
        string parametro;
        Expresion expresion;

        public AsignacionObjeto(string nombre, string parametro, Expresion expresion)
        {
            this.nombre = nombre;
            this.parametro = parametro;
            this.expresion = expresion;
        }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            Simbolo type = entorno.obtenerVariable(nombre);
            if (type.tipo.tipo != Tipos.OBJECT)
                throw new util.ErrorPascal(0,0,"La variable "+nombre+" no es un objeto","nombre",reporte);

            Objeto objeto = (Objeto)type.valor;
            Simbolo valor = expresion.evaluar(entorno,reporte);
            //valor.tipo.tipoAuxiliar = nombre;
            objeto.setValor(parametro, valor);

            return null;
        }
    }
}
