using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Arreglo : Instruccion
    {
        Arreglo hijo;
        int min;
        int max;
        object valorDefecto;
        public Dictionary<int, object> diccionario;
        int tipo; //0,normal;1,array;2,object

        public Arreglo(Arreglo hijo, int min, int max, object valorDefecto, int tipo)
        {
            this.hijo = hijo;
            this.min = min;
            this.max = max;
            this.valorDefecto = valorDefecto;
            diccionario = new Dictionary<int, object>();
            this.tipo = tipo;
    }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            
            if (hijo == null)
            {
                for(int i = min ; i<=max ; i++)
                {
                    switch(tipo)
                    {
                        case 0:
                            diccionario.Add(i, valorDefecto);
                            break;
                        case 1:
                            diccionario.Add(i, new Dictionary<int,object>((Dictionary<int,object>)valorDefecto));
                            break;
                        case 2:
                            diccionario.Add(i, new Objeto((Objeto)valorDefecto)); ;
                            break;
                    }
                }
                    
            }
            else
            {

                for(int i=min;i<=max; i++)
                {
                    
                        this.diccionario.Add(i,new Dictionary<int,object>(hijo.diccionario));
                    
                }
            }
            return null;
        }

        
    }
}
