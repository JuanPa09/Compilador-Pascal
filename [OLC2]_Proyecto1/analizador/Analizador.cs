using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Irony.Ast;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.analizador
{
    

    class Analizador
    {
        RichTextBox debuggerConsole;
        RichTextBox console;
        Reporte reporte;

        public Analizador(RichTextBox debugger, RichTextBox console, Reporte reporte) {
            this.debuggerConsole = debugger;
            this.console = console;
            this.reporte = reporte;
        }


        public void traducir(string cadena) 
        {
            Gramatica_Ascendente gramatica = new Gramatica_Ascendente();
            LanguageData lenguaje = new LanguageData(gramatica);
            foreach (var item in lenguaje.Errors)
            {
                Debug.WriteLine(item);
            }

            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (arbol.ParserMessages.Count > 0)
            {
                int i = 1;
                foreach (var item in arbol.ParserMessages)
                {
                    //Error Lexico
                    if (item.Message.Contains("Invalid character"))
                    {
                        Debug.WriteLine(i+") Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message);
                        debuggerConsole.AppendText("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message + "\n\n");
                    }
                    else
                    {
                        //Error Sintactico
                        Debug.WriteLine("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + " Mensaje: " + item.Message);
                        debuggerConsole.AppendText(i+") Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + " Mensaje: " + item.Message + "\n\n");
                    }
                    i++;
                }
            }

            if (raiz == null)
            {
                Debug.WriteLine(arbol.ParserMessages[0].Message);
                debuggerConsole.AppendText(arbol.ParserMessages[0].Message + "\n");
                return;
            }

            Traduccion traduccion = new Traduccion(raiz, console);
            console.Text = "";
            debuggerConsole.Text = "";

            traduccion.iniciar();

            generarGrafo(raiz);
        }

        public void analizar(string cadena) 
        {
            Gramatica_Ascendente gramatica = new Gramatica_Ascendente();
            LanguageData lenguaje = new LanguageData(gramatica);
            foreach (var item in lenguaje.Errors) {
                Debug.WriteLine(item);
            }

            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if (arbol.ParserMessages.Count > 0) 
            {
                int i = 1;
                foreach (var item in arbol.ParserMessages)
                {
                    //Error Lexico
                    if (item.Message.Contains("Invalid character"))
                    {
                        Debug.WriteLine("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message);
                        debuggerConsole.AppendText(i + ") Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message+"\n\n");
                        reporte.nuevoError(item.Location.Line, item.Location.Column, "Léxico", item.Message);

                    }
                    else {
                        //Error Sintactico
                        Debug.WriteLine("Error Sintáctico: Linea "+item.Location.Line+" Columna: "+item.Location.Column+" Mensaje: "+item.Message);
                        debuggerConsole.AppendText(i+") Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + " Mensaje: " + item.Message+"\n\n");
                        reporte.nuevoError(item.Location.Line, item.Location.Column, "Sintáctico", item.Message);
                    }
                    i++;
                }
            }

            if (raiz == null) {
                Debug.WriteLine(arbol.ParserMessages[0].Message);
                debuggerConsole.AppendText(arbol.ParserMessages[0].Message+"\n");
                return;
            }


            Ejecucion ejecucion = new Ejecucion(raiz, console,reporte);
            console.Text = "";
            debuggerConsole.Text = "";
            ejecucion.iniciar();
            generarGrafo(raiz);

        }

        public void generarGrafo(ParseTreeNode raiz) 
        {

            string grafoDot = Graficador.getDot(raiz);
            string path = "C:\\compiladores2\\ast.txt";
            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(grafoDot);
                    fs.Write(info, 0, info.Length);
                }
                debuggerConsole.AppendText("Arbol generado!\n");
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.ToString());
            }
        }


        public void reporteAst(string cadena)
        {
            try
            {
                Gramatica_Ascendente gramatica = new Gramatica_Ascendente();
                LanguageData lenguaje = new LanguageData(gramatica);
                foreach (var item in lenguaje.Errors)
                {
                    Debug.WriteLine(item);
                }

                Parser parser = new Parser(lenguaje);
                ParseTree arbol = parser.Parse(cadena);
                ParseTreeNode raiz = arbol.Root;

                generarGrafo(raiz);


                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.WriteLine("cd C:\\compiladores2");
                cmd.StandardInput.WriteLine("dot -Tsvg ast.txt -o ast.svg");
                cmd.Close();


            }
            catch(Exception ex)
            {
                this.debuggerConsole.Text = ex.Message;
            }


        }


    }
}
