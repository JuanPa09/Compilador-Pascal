using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.expresion
{
    class ObtenerVariable : Expresion
    {
        private string id;

        public ObtenerVariable(string id)
        {
            this.id = id;
        }
        public override Simbolo evaluar(Entorno entorno)
        {
            return entorno.obtenerVariable(id);
        }
    }
}
