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
using System.Diagnostics;

namespace _OLC2__Proyecto1
{
    public partial class Form1 : Form
    {
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

        private void button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Iniciando analizador!");
            Analizador analizador = new Analizador();
            analizador.analizar(CompiPascal.Text);
            Debug.WriteLine("Finalizando analizador!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Prueba");
        }
    }
}
