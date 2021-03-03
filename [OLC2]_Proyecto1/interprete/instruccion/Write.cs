using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.interprete.simbolo;
using System.Diagnostics;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Write : Instruccion
    {
        int tipo; /* 0 -> write ; 1 -> writeln  */
        private Expresion valor;
        RichTextBox consola;

        public Write(RichTextBox consola,Expresion valor,int tipo) 
        {
            this.valor = valor;
            this.tipo = tipo;
            this.consola = consola;
        }

        public override object ejecutar(Entorno entorno)
        {
            Debug.WriteLine("Ejecutando Write");

            Simbolo valor = this.valor.evaluar(entorno);
            switch (tipo) 
            {
                case 0:
                    consola.AppendText(valor.valor.ToString());
                    break ;
                case 1:
                    consola.AppendText(valor.valor.ToString()+"\n");
                    break;
            }
            return null;
        }
    }
}
