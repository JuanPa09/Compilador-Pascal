using System;
using System.Collections.Generic;
using System.Text;

using Irony.Ast;
using Irony.Parsing;

namespace _OLC2__Proyecto1.analizador
{
    class Gramatica:Grammar
    {

        public Gramatica() : base(caseSensitive: false)
        {
            #region ER
            var Identificador = new RegexBasedTerminal("ID", "([a-zA-Z])[a-zA-Z0-9_]*");
            var Entero = new NumberLiteral("INT");
            var Decimal = new RegexBasedTerminal("DOUBLE","[0-9]+[.][0-9]+");
            var Cadena = new RegexBasedTerminal("CADENA", "\'[^\"]*\'");

            #endregion

            #region Terminales

            var Program = ToTerm("program");

            var Write = ToTerm("write");
            var WriteLn = ToTerm("writeln");
            var Exit = ToTerm("exit");
            var Graficar = ToTerm("graficar_ts");

            var Begin = ToTerm("begin");
            var End = ToTerm("end");

            var Var = ToTerm("var");
            var Const = ToTerm("const");

            var String = ToTerm("string");
            var Integer = ToTerm("integer");
            var Real = ToTerm("real");
            var Boolean = ToTerm("boolean");

            var Par_Izq = ToTerm("(");
            var Par_Der = ToTerm(")");
            var Pt = ToTerm(".");
            var Pt_Coma = ToTerm(";");
            var Ds_Pts = ToTerm(":");
            var Coma = ToTerm(",");

            var Mas = ToTerm("+");
            var Menos = ToTerm("-");
            var Por = ToTerm("*");
            var Div = ToTerm("/");

            var Igual = ToTerm("=");
            var No_Igual = ToTerm("<>");
            var Men_Que = ToTerm("<=");
            var May_Que = ToTerm(">=");
            var Not = ToTerm("not");

            var Epsilon = this.Empty;

            #endregion

            #region No Terminales

            NonTerminal Raiz = new NonTerminal("Raiz");
            NonTerminal Estructura = new NonTerminal("Estuctura");
            NonTerminal Head = new NonTerminal("Head");
            NonTerminal Body = new NonTerminal("Body");
            NonTerminal Variables = new NonTerminal("Variables");
            NonTerminal Lista_Variables = new NonTerminal("Lista_Variables");
            NonTerminal Lista_Variablesp = new NonTerminal("Lista_Variablesp");
            NonTerminal Instrucciones = new NonTerminal("Instrucciones");
            NonTerminal Instruccion = new NonTerminal("Instruccion");
            NonTerminal Instruccion_Nativa = new NonTerminal("Instruccion_Nativa");
            NonTerminal Tipo_Variable = new NonTerminal("Tipo_Variable");
            NonTerminal Expresion_Cadena = new NonTerminal("Expresion_Cadena");
            NonTerminal Expresion_Numerica = new NonTerminal("Expresion_Numerica");
            NonTerminal Expresion_Numericap = new NonTerminal("Expresion_Numericap");
            NonTerminal Digito = new NonTerminal("Digito");



            #endregion

            #region Gramatica

            Raiz.Rule
                        = Estructura
                        ;

            Estructura.Rule 
                        = Program + Identificador + Pt_Coma + Head + Body
                        ;

            Head.Rule 
                        = Instrucciones;

            Body.Rule 
                        = Begin + End + Pt
                        ;

            Instrucciones.Rule
                            = Instruccion + Instrucciones
                            | Instruccion
                            ;

            Instruccion.Rule
                            = Variables
                            | Epsilon
                            ;

            /*Instruccion_Nativa.Rule
                            =   Write+Pt_Coma
                            | WriteLn+Pt_Coma
                            | Epsilon
                            ;*/

            Variables.Rule
                            = Const + Identificador + Ds_Pts + Tipo_Variable + Igual + Expresion_Cadena + Pt_Coma
                            | Var + Lista_Variables + Pt_Coma + Tipo_Variable + Pt_Coma
                            | Var + Identificador + Ds_Pts + Tipo_Variable + Igual + Expresion_Cadena + Pt_Coma
                            ;

            Variables.ErrorRule = SyntaxError + Pt_Coma
                                ;


            Lista_Variables.Rule
                            = Identificador + Lista_Variablesp
                            ;

            Lista_Variablesp.Rule
                            = Coma + Identificador + Lista_Variablesp
                            | Epsilon
                            ;

            Tipo_Variable.Rule
                            = String
                            | Integer
                            | Real
                            | Boolean
                            ;

            Expresion_Cadena.Rule
                                = Cadena
                                |Expresion_Numerica
                                | Mas + Expresion_Cadena
                                | Identificador
                                ;

            Expresion_Numerica.Rule
                                = Digito + Expresion_Numericap
                                ;

            Expresion_Numericap.Rule
                                = Mas + Digito + Expresion_Numericap
                                | Menos + Digito + Expresion_Numericap
                                | Por + Digito + Expresion_Numericap
                                | Div + Digito + Expresion_Numericap
                                | Par_Izq + Expresion_Numericap + Par_Der
                                | Entero
                                | Decimal
                                | Epsilon
                                ;

            Digito.Rule
                        = Entero
                        | Decimal
                        ;





            #endregion

            #region Preferencias
            this.Root = Raiz;
            this.RegisterOperators(1, Associativity.Left, Mas, Menos);
            this.RegisterOperators(2, Associativity.Left, Por, Div);
            #endregion
        }


    }
}
