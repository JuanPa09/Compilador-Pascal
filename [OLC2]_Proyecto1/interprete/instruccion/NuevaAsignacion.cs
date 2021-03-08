using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using System.Diagnostics;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class NuevaAsignacion : Instruccion
    {
        private string id;
        private Expresion valor;

        public NuevaAsignacion(string id, Expresion valor)
        {
            this.id = id;
            this.valor = valor;
        }

        public override object ejecutar(Entorno entorno)
        {
            try
            {
                Simbolo valor = this.valor.evaluar(entorno);
                Entorno entornoVariable = entorno.buscarEntornoVariable(this.id);

                if (valor.valor == null)
                    throw new util.ErrorPascal(0,0,"La variable \""+valor.id+"\" no tiene valor asignado","semántico");
                return entornoVariable.modificarVariable(id, valor.valor, valor.tipo.tipo);
            }
            catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
            return null;
        }
    }
}
