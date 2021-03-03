using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class If : Instruccion
    {

        private Expresion valor;
        private LinkedList<Instruccion> instrucciones;
        private LinkedList<Instruccion> _else; //Cambiar por LinkedList;

        public If(Expresion valor, LinkedList<Instruccion> instrucciones, LinkedList<Instruccion> _else) 
        {
            this.valor = valor;
            this.instrucciones = instrucciones;
            this._else = _else;

        }

        public override object ejecutar(Entorno entorno)
        {
            Simbolo valor = this.valor.evaluar(entorno);

            //TODO verificar errores
            if (valor.tipo.tipo != Tipos.BOOLEAN)
                throw new util.ErrorPascal(0, 0, "No es una expresion logica", "semantico");
            
            if (bool.Parse(valor.valor.ToString()))
            {
                try
                {
                    foreach (var instruccion in instrucciones)
                    {
                        if (instruccion!=null)
                            instruccion.ejecutar(entorno);
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }else
            {
                if (_else != null)
                    foreach (var instruccion in _else)
                    {
                        if(instruccion!=null)
                            instruccion.ejecutar(entorno);
                    }

            }
            return null;

        }
    }
}
