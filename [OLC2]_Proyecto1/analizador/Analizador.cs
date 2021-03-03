using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;
using Irony.Ast;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace _OLC2__Proyecto1.analizador
{
    

    class Analizador
    {
        RichTextBox debuggerConsole;
        RichTextBox console;

        public Analizador(RichTextBox debugger, RichTextBox console) {
            this.debuggerConsole = debugger;
            this.console = console;
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
                foreach (var item in arbol.ParserMessages)
                {
                    //Error Lexico
                    if (item.Message.Contains("Invalid character"))
                    {
                        Debug.WriteLine("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message);
                        debuggerConsole.AppendText("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message + "\n");
                    }
                    else
                    {
                        //Error Sintactico
                        Debug.WriteLine("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + " Mensaje: " + item.Message);
                        debuggerConsole.AppendText("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + " Mensaje: " + item.Message + "\n");
                    }
                }
            }

            if (raiz == null)
            {
                Debug.WriteLine(arbol.ParserMessages[0].Message);
                debuggerConsole.AppendText(arbol.ParserMessages[0].Message + "\n");
                return;
            }
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
                foreach (var item in arbol.ParserMessages)
                {
                    //Error Lexico
                    if (item.Message.Contains("Invalid character"))
                    {
                        Debug.WriteLine("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message);
                        debuggerConsole.AppendText("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + "Mensaje: " + item.Message+"\n");
                    }
                    else {
                        //Error Sintactico
                        Debug.WriteLine("Error Sintáctico: Linea "+item.Location.Line+" Columna: "+item.Location.Column+" Mensaje: "+item.Message);
                        debuggerConsole.AppendText("Error Sintáctico: Linea " + item.Location.Line + " Columna: " + item.Location.Column + " Mensaje: " + item.Message+"\n");
                    }
                }
            }

            if (raiz == null) {
                Debug.WriteLine(arbol.ParserMessages[0].Message);
                debuggerConsole.AppendText(arbol.ParserMessages[0].Message+"\n");
                return;
            }


            Ejecucion ejecucion = new Ejecucion(raiz, console);
            ejecucion.iniciar();
            generarGrafo(raiz);

        }

        
        

        public void generarGrafo(ParseTreeNode raiz) 
        {

            string grafoDot = Graficador.getDot(raiz);
            string path = "ast.txt";
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
    }
}
