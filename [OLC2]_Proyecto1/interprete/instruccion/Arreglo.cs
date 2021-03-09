using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Arreglo : Instruccion
    {
        Arreglo hijo;
        int min;
        int max;
        object valorDefecto;
        public Dictionary<int, object> diccionario = new Dictionary<int, object>();

        public Arreglo(Arreglo hijo, int min, int max, object valorDefecto)
        {
            this.hijo = hijo;
            this.min = min;
            this.max = max;
            this.valorDefecto = valorDefecto;
        }

        public override object ejecutar(Entorno entorno)
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
                    diccionario.Add(i, hijo.diccionario);
                }
            }
            return null;
        }
    }
}
