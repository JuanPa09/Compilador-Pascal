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

        public Arreglo(Arreglo hijo, int min, int max, object valorDefecto)
        {
            this.hijo = hijo;
            this.min = min;
            this.max = max;
            this.valorDefecto = valorDefecto;
            diccionario = new Dictionary<int, object>();
    }

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            
            if (hijo == null)
            {
                for(int i = min ; i<=max ; i++)
                {
                    diccionario.Add(i, valorDefecto);
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
