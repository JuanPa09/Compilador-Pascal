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
            try
            {
                Simbolo valor = this.valor.evaluar(entorno);
                switch (tipo)
                {
                    case 0:
                        try
                        {
                            if (valor.valor == null)
                                throw new util.ErrorPascal(0, 0, "La variable \"" + valor.id + "\" no tiene valor", "semántico");
                            consola.AppendText(valor.valor.ToString());
                        }
                        catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
                        break;
                    case 1:
                        try
                        {
                            if (valor == null)
                                throw new util.ErrorPascal(0, 0, "El simbolo no tiene valor (probablemente es un procedimiento)", "semántico");

                            if (valor.valor == null)
                                throw new util.ErrorPascal(0,0,"La variable \""+valor.id+"\" no tiene valor","semántico");
                            consola.AppendText(valor.valor.ToString() + "\n");
                        }
                        catch (Exception ex) { Debug.WriteLine(ex.ToString()); }
                        break;
                }
            }catch( Exception ex) { Debug.WriteLine(ex.ToString()); }
            
            return null;
        }
    }
}
