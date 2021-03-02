using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;


namespace _OLC2__Proyecto1.analizador
{
    class Graficador
    {
        private static int contador;
        private static string grafo;

        public static string getDot(ParseTreeNode raiz)
        {
            grafo = "digraph G{\nnode[shape=box]\n";
            grafo += "nodo0[label=\"" + escapar(raiz.Term.ToString()) + "\"];\n";
            contador = 1;
            recorrerAst("nodo0", raiz);
            grafo += "}";
            return grafo;
        }

        private static void recorrerAst(string padre, ParseTreeNode raiz)
        {
            foreach (ParseTreeNode hijo in raiz.ChildNodes)
            {
                string nameHijo = "nodo" + contador.ToString();
                grafo += nameHijo + "[label=\"" + escapar(hijo.ToString()) + "\"];\n";
                grafo += padre + "->" + nameHijo + ";\n";
                contador++;
                recorrerAst(nameHijo, hijo);
            }
        }

        private static string escapar(string cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            return cadena;
        }
    }
}
