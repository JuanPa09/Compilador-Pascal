using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using System.Diagnostics;
using _OLC2__Proyecto1.reportes;

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

        public override object ejecutar(Entorno entorno,Reporte reporte)
        {
            Simbolo valor = this.valor.evaluar(entorno,reporte);

            //TODO verificar errores
            if (valor.tipo.tipo != Tipos.BOOLEAN)
                throw new util.ErrorPascal(0, 0, "No es una expresion logica", "semantico",reporte);

            Entorno entornoIf = new Entorno(".if",entorno,reporte);
            if (bool.Parse(valor.valor.ToString()))
            {
                
                foreach (Instruccion instruccion in instrucciones)
                {
                    //Entorno entornoIf = new Entorno(entorno);
                    if (instruccion!=null)
                        try
                        {
                            return instruccion.ejecutar(entornoIf,reporte);
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }    
                }
            }else
            {
                if (_else != null)
                    foreach (Instruccion instruccion in _else)
                    {
                        if(instruccion!=null)
                            try
                            {
                                return instruccion.ejecutar(entorno,reporte);
                            }
                            catch(Exception ex) { Debug.WriteLine(ex.ToString()); }
                    }

            }
            return null;

        }

        
    }
}
