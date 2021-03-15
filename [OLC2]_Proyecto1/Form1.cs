using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using _OLC2__Proyecto1.analizador;
using _OLC2__Proyecto1.reportes;
using System.Diagnostics;

namespace _OLC2__Proyecto1
{
    public partial class Form1 : Form
    {

        Reporte reporte;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LinearNumberCompiPascal.Font = CompiPascal.Font;
            LinearNumberPascal.Font = Pascal.Font;
            CompiPascal.Select();
            AddLineNumbers(CompiPascal, LinearNumberCompiPascal);
            AddLineNumbers(Pascal, LinearNumberCompiPascal);
            CompiPascal.Text = "program compiladores2;\nbegin\nend.";
            Pascal.Text = "program compiladores2;\nbegin\nend.";

            reporte = new Reporte(debuggerConsole);

        }

        


        private void CompiPascal_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = CompiPascal.GetPositionFromCharIndex(CompiPascal.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers(CompiPascal, LinearNumberCompiPascal);
            }
        }

        private void CompiPascal_VScroll(object sender, EventArgs e)
        {
            LinearNumberCompiPascal.Text = "";
            AddLineNumbers(CompiPascal, LinearNumberCompiPascal);
            LinearNumberCompiPascal.Invalidate();
        }

        private void CompiPascal_TextChanged(object sender, EventArgs e)
        {
            if (CompiPascal.Text == "")
            {
                AddLineNumbers(CompiPascal,LinearNumberCompiPascal);
            }
        }

        private void LinearNumberCompiPascal_MouseDown(object sender, MouseEventArgs e)
        {
            CompiPascal.Select();
            LinearNumberCompiPascal.DeselectAll();
        }


        private void AddLineNumbers(RichTextBox editor,RichTextBox numbers)
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = editor.GetCharIndexFromPosition(pt);
            int First_Line = editor.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = editor.GetCharIndexFromPosition(pt);
            int Last_Line = editor.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            numbers.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            numbers.Text = "";
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                numbers.Text += i + 1 + "\n";
            }
        }

        private void Pascal_TextChanged(object sender, EventArgs e)
        {
            if (Pascal.Text == "")
            {
                AddLineNumbers(Pascal, LinearNumberPascal);
            }
        }

        private void Pascal_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = Pascal.GetPositionFromCharIndex(Pascal.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers(Pascal, LinearNumberPascal);
            }
        }

        private void Pascal_MouseDown(object sender, MouseEventArgs e)
        {
            Pascal.Select();
            LinearNumberPascal.DeselectAll();
        }

        private void Pascal_VScroll(object sender, EventArgs e)
        {
            LinearNumberPascal.Text = "";
            AddLineNumbers(Pascal, LinearNumberPascal);
            LinearNumberPascal.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            debuggerConsole.Text = "";
            Debug.WriteLine("Iniciando Traduccion!");
            debuggerConsole.AppendText("Iniciando Traduccion!\n");
            reporte.limpiarLista();
            Analizador analizador = new Analizador(this.debuggerConsole,Pascal,reporte);
            analizador.traducir(CompiPascal.Text);
            debuggerConsole.AppendText("Finalizando Traduccion!\n");
            Debug.WriteLine("Finalizando Traduccion!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            debuggerConsole.Text = "";
            debuggerConsole.AppendText("Iniciando Ejecucion!\n");
            reporte.limpiarLista();
            Analizador analizador = new Analizador(this.debuggerConsole,Consola,reporte);
            analizador.analizar(Pascal.Text);
            debuggerConsole.AppendText("Finalizando Ejecucion");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reporte.generarReporte();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reporte.reporteSimbolos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Analizador analizador = new Analizador(this.debuggerConsole, Consola, reporte);
            analizador.reporteAst(Pascal.Text);
        }
    }
}
