﻿using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevoArreglo : Instruccion
    {
        private Dictionary<int,object> arreglo = new Dictionary<int, object>();
        LinkedList<Dictionary<string, int>> dimensiones; //max;val min;val -> * Las dimensiones vienen de derecha a izquierda
        object valorDefecto;
        string nombre;
        Tipo tipo;
        int fila, columna,tipoDatoValorDefecto;

        public NuevoArreglo(string nombre,LinkedList<Dictionary<string,int>> dimensiones,Tipo tipo, int fila, int columna)
        {
            this.tipo = tipo;
            this.nombre = nombre;
            this.dimensiones = dimensiones;
            this.fila = fila;
            this.columna = columna;
        }

        public NuevoArreglo(NuevoArreglo arreglo)
        {
            this.arreglo = arreglo.arreglo;
            this.dimensiones = arreglo.dimensiones;
            this.valorDefecto = arreglo.valorDefecto;
            this.tipo = arreglo.tipo;
            this.fila = arreglo.fila;
            this.columna = arreglo.columna;
        }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {

            switch (tipo.tipo)
            {
                case Tipos.DOUBLE:
                    valorDefecto = 0;
                    tipoDatoValorDefecto = 0;
                    break;
                case Tipos.NUMBER:
                    valorDefecto = 0;
                    tipoDatoValorDefecto = 0;
                    break;
                case Tipos.STRING:
                    valorDefecto = "";
                    tipoDatoValorDefecto = 0;
                    break;
                case Tipos.BOOLEAN:
                    valorDefecto = false;
                    tipoDatoValorDefecto = 0;
                    break;
                case Tipos.TYPE:
                    Simbolo Valorestype = entorno.types[tipo.tipoAuxiliar];


                    if (Valorestype.tipo.tipo == Tipos.ARRAY)
                    {
                        //Es un arreglo de arreglos
                        valorDefecto = new Dictionary<int, object>((Dictionary<int, object>)Valorestype.valor);
                        tipoDatoValorDefecto = 1;
                    }
                    else
                    {
                        //Es un arreglo de objetos
                        valorDefecto = new Objeto((Objeto)Valorestype.valor);
                        tipoDatoValorDefecto = 2;
                    }
                    //valorDefecto = entorno.types[tipo.tipoAuxiliar];
                    break;
            }


            Arreglo hijo = null;
            foreach (Dictionary<string, int> dimension in dimensiones)
            {
                int min = dimension["min"];
                int max = dimension["max"];
                hijo = new Arreglo(hijo, min, max, this.valorDefecto, tipoDatoValorDefecto);
                hijo.ejecutar(entorno, reporte);
            }

            entorno.declararVariables(this.nombre, new Simbolo(new Dictionary<int, object>(hijo.diccionario), new Tipo(Tipos.ARRAY, null), nombre), fila, columna);
            entorno.tipoArreglo.Add(nombre, tipo.tipo);

            return null;
        }



    }
}
