using _OLC2__Proyecto1.interprete.simbolo;
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
        int fila, columna;

        public NuevoArreglo(string nombre,LinkedList<Dictionary<string,int>> dimensiones,Tipo tipo, int fila, int columna)
        {
            this.tipo = tipo;
            this.nombre = nombre;
            this.dimensiones = dimensiones;
            this.fila = fila;
            this.columna = columna;
            switch (tipo.tipo)
            {
                case Tipos.DOUBLE: valorDefecto = 0;
                    break;
                case Tipos.NUMBER: valorDefecto = 0;
                    break;
                case Tipos.STRING: valorDefecto = "";
                    break;
                case Tipos.BOOLEAN: valorDefecto = false;
                    break;
            }

        }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            
            Arreglo hijo = null;
            foreach(Dictionary<string,int> dimension in dimensiones)
            {
                int min = dimension["min"];
                int max = dimension["max"];
                hijo = new Arreglo(hijo, min, max, this.valorDefecto);
                hijo.ejecutar(entorno,reporte);
            }

            entorno.declararVariables(this.nombre,new Simbolo(new Dictionary<int,object>(hijo.diccionario),new Tipo(Tipos.ARRAY,null),nombre),fila,columna);
            entorno.tipoArreglo.Add(nombre,tipo.tipo);

            return null;
        }



    }
}
