﻿using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.expresion;
using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.interprete.instruccion;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;
using System.Diagnostics;

namespace _OLC2__Proyecto1.analizador
{
    class Ejecucion
    {
        ParseTreeNode nodoRaiz;
        RichTextBox consola;

        public Ejecucion(ParseTreeNode nodoRaiz, RichTextBox consola)
        {
            this.nodoRaiz = nodoRaiz;
            this.consola = consola;
        }

        public void iniciar() 
        {
            LinkedList<Instruccion> listaInstrucciones = instrucciones(nodoRaiz);
            ejectutar(listaInstrucciones);
        }

        public void ejectutar(LinkedList<Instruccion> instrucciones) 
        {
            Entorno global = new Entorno(null);
            foreach (var instruccion in instrucciones)
            {
                if (instruccion!=null)
                    instruccion.ejecutar(global);
            }
        }


        public LinkedList<Instruccion> instrucciones(ParseTreeNode actual)
        {
            LinkedList<Instruccion> listaInstrucciones = new LinkedList<Instruccion>();
            foreach(ParseTreeNode nodo in actual.ChildNodes)
            {
                Debug.WriteLine("Nodo -> "+nodo.Term.ToString());

                if (nodo.ChildNodes.Count == 2)
                {
                    instruccionesMultiples(ref listaInstrucciones, nodo);
                }else
                {
                    listaInstrucciones.AddLast(instruccion(nodo));
                }
            }
            return listaInstrucciones;
        }

        public void instruccionesMultiples(ref LinkedList<Instruccion> listaInstrucciones, ParseTreeNode actual)
        {
            //LLEGA A INSTRUCCIONES QUE PUEDEN TENER MAS INSTRUCCIONES
            foreach(ParseTreeNode nodo in actual.ChildNodes)
            {
                //Aca se van a hacer los 2 ciclos
                Debug.WriteLine("Nodo -> " + nodo.Term.ToString());
                if (nodo.ChildNodes.Count == 2)
                {
                    instruccionesMultiples(ref listaInstrucciones, nodo);
                    continue;
                }

                if (nodo.ChildNodes.Count != 0)
                {
                    Instruccion instr = instruccion(nodo);
                    if (instr!=null) 
                    {
                        listaInstrucciones.AddLast(instr);
                    }else
                    {
                        instruccionesMultiples(ref listaInstrucciones,nodo);
                    }
                }
            }

        }



        public Instruccion instruccion(ParseTreeNode actual)
        {
            Debug.WriteLine("Evaluando: "+actual.ChildNodes[0].Term.ToString());
            string nombreNodo = actual.ChildNodes[0].Term.ToString();
            if (actual.ChildNodes.Count == 0) { nombreNodo = actual.ChildNodes[0].Token.Text; }

            switch (nombreNodo)
            {
                case "program":
                    //Obtener Instrucciones Head e Instrucciones Body
                    return new Estructura(instrucciones(actual.ChildNodes[3]),instrucciones(actual.ChildNodes[5]));
                case "Writes":
                    if (actual.ChildNodes[0].ChildNodes[0].Token.Text == "writeln")
                        return new Write(consola,evaluarExpresionCadena(actual.ChildNodes[0].ChildNodes[2],actual.ChildNodes[0].ChildNodes[3]),1);
                    return new Write(consola, evaluarExpresionCadena(actual.ChildNodes[0].ChildNodes[2], actual.ChildNodes[0].ChildNodes[3]), 0);
                case "If_Statement":
                    return evaluarIf(actual.ChildNodes[0]);


            }

            return null; 
        }

        public Instruccion evaluarIf(ParseTreeNode actual)
        {
            LinkedList<Instruccion> instruccionSimple = new LinkedList<Instruccion>();
            int cantidad = actual.ChildNodes.Count;
            switch (cantidad) 
            {
                case 10:
                    return new If(evaluarExpresionLogica(actual.ChildNodes[2]), instrucciones(actual.ChildNodes[6]), evaluarElse(actual.ChildNodes[8]));
                case 8:
                    return new If(evaluarExpresionLogica(actual.ChildNodes[1]), instrucciones(actual.ChildNodes[4]), evaluarElse(actual.ChildNodes[6]));
                case 7:
                    instruccionSimple.AddLast(instruccion(actual.ChildNodes[5]));
                    return new If(evaluarExpresionLogica(actual.ChildNodes[2]), instruccionSimple, evaluarElse(actual.ChildNodes[6]));
                default:
                    instruccionSimple.AddLast(instruccion(actual.ChildNodes[3]));
                    return new If(evaluarExpresionLogica(actual.ChildNodes[1]), instruccionSimple, evaluarElse(actual.ChildNodes[4]));
            }
        }

        public LinkedList<Instruccion> evaluarElse(ParseTreeNode actual)
        {
            Debug.WriteLine("No Terminal Else "+actual.Term.ToString());
            if (actual.ChildNodes.Count == 0)
                return null;
            int cantidad = actual.ChildNodes.Count;
            switch (cantidad)
            {
                case 4:
                    return instrucciones(actual.ChildNodes[2]);
                default:
                    // 1 Instruccion
                    LinkedList<Instruccion> instruccion = new LinkedList<Instruccion>();
                    instruccion.AddLast(this.instruccion(actual.ChildNodes[1]));
                    return instruccion;
            }
        }



        public Expresion evaluarExpresionCadena(ParseTreeNode expresionCadena, ParseTreeNode masTexto)
        {
            Expresion ExpresionCadena = null;
            ExpresionCadena = this.expresionCadena(expresionCadena);
            Expresion MasTexto = null;
            MasTexto = this.masTexto(masTexto);

            if(MasTexto!=null)
            {
                return new Aritmetica(ExpresionCadena, MasTexto, '+');
            }else
            {
                return ExpresionCadena;
            }


        }


        public Expresion expresionCadena(ParseTreeNode expresionCadena)
        {
            if (expresionCadena.ChildNodes.Count == 1)
            {
                if (expresionCadena.ChildNodes[0].Term.ToString() != "Expresion_Numerica")
                {
                    //Sintetizar
                    return new Literal(Literales(expresionCadena), expresionCadena.ChildNodes[0].Token.Text);
                }
                else
                {
                    //Es una expresion Numerica
                    return evaluarExpresionNumerica(expresionCadena.ChildNodes[0]);
                }
            }else
            {
                //Tiene 3 nodos (Expresion_Cadena Simbolo(+,-...) Expresion_Cadena
                Expresion ExpresionCadena1 = null;
                Expresion ExpresionCadena2 = null;

                ExpresionCadena1 = this.expresionCadena(expresionCadena.ChildNodes[0]);
                ExpresionCadena2 = this.expresionCadena(expresionCadena.ChildNodes[2]);

                if(ExpresionCadena1 != null && ExpresionCadena2 != null)
                {
                    return new Aritmetica(ExpresionCadena1,ExpresionCadena2,getSignoAritmetica(expresionCadena.ChildNodes[1]));
                }else
                {
                    return ExpresionCadena1;
                }
            }
        }

        public Expresion masTexto(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count==0)
            {
                return null;
            }else
            {
                //Tiene 3 hijos (, Expresion_Cadena Mas_Texto)
                Expresion expresionCadena =  this.expresionCadena(actual.ChildNodes[1]);
                Expresion masTexto = this.masTexto(actual.ChildNodes[2]);

                if (expresionCadena!=null && masTexto !=null)
                {
                    return new Aritmetica(expresionCadena, masTexto, '+');
                }else
                {
                    return expresionCadena;
                }

            }
        }

        public char getSignoAritmetica(ParseTreeNode actual)
        {
            switch(actual.Token.Text)
            {
                case "+":
                    return '+';
                case "-":
                    return '-';
                case "*":
                    return '*';
                case "/":
                    return '/';
                default:
                    return '%';
            }
        }
      

        public char Literales(ParseTreeNode actual)
        {
            switch(actual.ChildNodes[0].Term.ToString())
            {
                

                case "INT":
                    return 'N';

                case "CADENA":
                    return 'S';
            }
            return 'E';
        }

        public Expresion evaluarExpresionLogica(ParseTreeNode actual)
        {
            int cantidad = actual.ChildNodes.Count;
            switch (cantidad)
            {
                case 3:
                    // Llevan Or o And
                    return null;
                case 2:
                    // Tiene operador Not
                    return null;
                case 1:
                    // Tiene Expresion Relacional
                    return evaluarExpresionRelacional(actual.ChildNodes[0]);
            }
            return null;
        }

        public Expresion evaluarExpresionRelacional(ParseTreeNode actual)
        {
            if(actual.ChildNodes.Count == 3)
            {
                //string operador = actual.ChildNodes[1].Token.Text;
                return new Relacional(evaluarExpresionNumerica(actual.ChildNodes[0]), evaluarExpresionNumerica(actual.ChildNodes[2]), actual.ChildNodes[1].Token.Text);
            }else
            {
                //Buscar Identificador (Pendiente)
                return null;
            }
        }

        public Expresion evaluarExpresionNumerica(ParseTreeNode actual) {
            
            if(actual.ChildNodes.Count == 3)
            {
                string operador = actual.ChildNodes[1].Token.Text;
                switch (operador)
                {
                    case "+":
                        return new Aritmetica(evaluarExpresionNumerica(actual.ChildNodes[0]), evaluarExpresionNumerica(actual.ChildNodes[2]), '+');
                    case "-":
                        return new Aritmetica(evaluarExpresionNumerica(actual.ChildNodes[0]), evaluarExpresionNumerica(actual.ChildNodes[2]), '-');
                    case "*":
                        return new Aritmetica(evaluarExpresionNumerica(actual.ChildNodes[0]), evaluarExpresionNumerica(actual.ChildNodes[2]), '*');
                    case "/":
                        return new Aritmetica(evaluarExpresionNumerica(actual.ChildNodes[0]), evaluarExpresionNumerica(actual.ChildNodes[2]), '/');
                    default:
                        return new Aritmetica(evaluarExpresionNumerica(actual.ChildNodes[0]), evaluarExpresionNumerica(actual.ChildNodes[2]), '%');
                }
            }else
            {
                //Verificar el tipo y no solo poner la n porque pueden venir strings o bools
                return new Literal('N',actual.ChildNodes[0].Token.Text);
            }
        
        }


        


       




       



    }
}