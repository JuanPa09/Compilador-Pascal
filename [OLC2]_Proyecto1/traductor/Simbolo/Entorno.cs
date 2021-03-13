using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.traductor.Simbolo
{
    class Entorno
    {
        Entorno padre;
        string nombre;
        Dictionary<string, string> variables; 
        Dictionary<string, string> constantes;


        public Entorno(Entorno padre,string nombre)
        {
            this.padre = padre;
            variables = new Dictionary<string, string>();
            constantes = new Dictionary<string, string>();
        }



        public string buscarVariable(string id)
        {
            Entorno actual = this.padre; //Empezar a buscar desde el padre
            string nuevoNombre = "";
            while(actual!=null)
            {
                if (variables.ContainsKey(id))
                {
                    return nuevoNombre;
                }
                else
                {
                    if (nuevoNombre == "")
                    {
                        nuevoNombre = actual.nombre;
                    }
                    else
                    {
                        nuevoNombre += "_"+actual.nombre;
                    }
                    actual = actual.padre;
                }
            }
            return null;
        }

        public string buscarConstante(string id)
        {
            Entorno actual = this.padre; //Empezar a buscar desde el padre
            string nuevoNombre = "";
            while (actual != null)
            {
                if (constantes.ContainsKey(id))
                {
                    return nuevoNombre;
                }
                else
                {
                    if (nuevoNombre == "")
                    {
                        nuevoNombre = actual.nombre;
                    }
                    else
                    {
                        nuevoNombre += "_" + actual.nombre;
                    }
                    actual = actual.padre;
                }
            }
            return null;
        }





    }
}
