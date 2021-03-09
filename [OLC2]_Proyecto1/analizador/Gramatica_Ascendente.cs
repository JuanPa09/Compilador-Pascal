using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

namespace _OLC2__Proyecto1.analizador
{
    class Gramatica_Ascendente : Grammar
    {
        public Gramatica_Ascendente() : base(caseSensitive:false)
        {
            #region ER
            var Identificador = new IdentifierTerminal("ID");
            var Entero = new NumberLiteral("INT");
            var Decimal = new RegexBasedTerminal("DOUBLE", "[0-9]+[.][0-9]+");
            var Cadena = new RegexBasedTerminal("CADENA", "\'[^\']*\'");

            var Comentario_Simple = new CommentTerminal("Comentario_Simple", "//", "\n", "\r\n");
            var Comentario_Multi1 = new CommentTerminal("Comentario_Multilinea1", "(*", "*)");
            var Comentario_Multi2 = new CommentTerminal("Comentario_Multilinea2", "{", "}");

            #endregion

            #region Terminales
            var Program = ToTerm("program");
            var Write = ToTerm("write");
            var WriteLn = ToTerm("writeln");
            var Exit = ToTerm("exit");
            var Graficar = ToTerm("graficar_ts");
            var Function = ToTerm("function");
            var Procedure = ToTerm("Procedure");
            var Begin = ToTerm("begin");
            var End = ToTerm("end");
            var Var = ToTerm("var");
            var Const = ToTerm("const");
            var Void = ToTerm("void");
            var Type = ToTerm("type");
            var String = ToTerm("string");
            var Integer = ToTerm("integer");
            var Real = ToTerm("real");
            var Boolean = ToTerm("boolean");
            var Object = ToTerm("object");
            var Array = ToTerm("array");
            var Cor_Izq = ToTerm("[");
            var Cor_Der = ToTerm("]");
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
            var Mod = ToTerm("%");
            var Igual = ToTerm("=");
            var No_Igual = ToTerm("<>");
            var Men_Que = ToTerm("<");
            var May_Que = ToTerm(">");
            var Men_Ig_Que = ToTerm("<=");
            var May_Ig_Que = ToTerm(">=");
            var And = ToTerm("and");
            var Or = ToTerm("or");
            var Not = ToTerm("not");
            var If = ToTerm("if");
            var Then = ToTerm("then");
            var Else = ToTerm("else");
            var For = ToTerm("for");
            var To = ToTerm("to");
            var Do = ToTerm("do");
            var While = ToTerm("while");
            var Repeat = ToTerm("repeat");
            var Until = ToTerm("until");
            var Case = ToTerm("case");
            var Of = ToTerm("of");
            var Break = ToTerm("break");
            var Continue = ToTerm("continue");
            var True = ToTerm("true");
            var False = ToTerm("false");
            var Epsilon = this.Empty;
            NonGrammarTerminals.Add(Comentario_Simple);
            NonGrammarTerminals.Add(Comentario_Multi1);
            NonGrammarTerminals.Add(Comentario_Multi2);

            #endregion

            #region No Terminales

            NonTerminal Raiz = new NonTerminal("Raiz");
            NonTerminal Estructura = new NonTerminal("Estuctura");
            NonTerminal Head = new NonTerminal("Head");
            NonTerminal Body = new NonTerminal("Body");
            NonTerminal Variables = new NonTerminal("Variables");
            NonTerminal Lista_Variables = new NonTerminal("Lista_Variables");
            NonTerminal Lista_Variablesp = new NonTerminal("Lista_Variablesp");
            NonTerminal Instrucciones_Head = new NonTerminal("Instrucciones_Head");
            NonTerminal Instrucciones_Headp = new NonTerminal("Instrucciones_Headp");
            NonTerminal Instruccion_Head = new NonTerminal("Instruccion_Head");
            NonTerminal Instrucciones_Body = new NonTerminal("Instrucciones_Body");
            NonTerminal Instrucciones_Bodyp = new NonTerminal("Instrucciones_Bodyp");
            NonTerminal Instruccion_Body = new NonTerminal("Instruccion_Body");
            NonTerminal Instruccion_Funcion = new NonTerminal("Instruccion_Funcion");
            NonTerminal Instrucciones_Funcion = new NonTerminal("Instrucciones_Funcion");
            NonTerminal Retorno_Funcion = new NonTerminal("Retorno_Funcion");
            NonTerminal Tipo_Variable = new NonTerminal("Tipo_Variable");
            NonTerminal Expresion_Cadena = new NonTerminal("Expresion_Cadena");
            NonTerminal Expresion_Cadenap = new NonTerminal("Expresion_Cadenap");
            NonTerminal Expresion_Numerica = new NonTerminal("Expresion_Numerica");
            NonTerminal Expresion_Numericap = new NonTerminal("Expresion_Numericap");
            NonTerminal Expresion_Relacional = new NonTerminal("Expresion_Relacional");
            NonTerminal Expresion_Logica = new NonTerminal("Expresion_Logica");
            NonTerminal Valor = new NonTerminal("Valor");
            NonTerminal Writep = new NonTerminal("Mas_Texto");
            NonTerminal Parametros_Asignacion = new NonTerminal("Parametros_Asignacion");
            NonTerminal ParametrosAsignacionp = new NonTerminal("Parametros_Asignacionp");
            NonTerminal Parametros_Entrada = new NonTerminal("Parametros_Entrada");
            NonTerminal Asignacion = new NonTerminal("Asignacion");
            NonTerminal If_Statement = new NonTerminal("If_Statement");
            NonTerminal Else_Statement = new NonTerminal("Else_Statement");
            NonTerminal Else_Instruccion = new NonTerminal("Else_Instruccion");
            NonTerminal For_Statement = new NonTerminal("For_Statement");
            NonTerminal While_Statement = new NonTerminal("While_Statement");
            NonTerminal Case_Statement = new NonTerminal("Case_Statement");
            NonTerminal Case_Else_Statement = new NonTerminal("Case_Else_Statement");
            NonTerminal Cases_Statement = new NonTerminal("Cases_Statement");
            NonTerminal Cases_Statementp = new NonTerminal("Cases_Statementp");
            NonTerminal Repeat_Statement = new NonTerminal("Repeat_Statement");
            NonTerminal Declaraciones_Type = new NonTerminal("Declaraciones_Type");
            NonTerminal Types = new NonTerminal("Types");
            NonTerminal Objeto = new NonTerminal("Objeto");
            NonTerminal Tipo_Array = new NonTerminal("Tipo_Array");
            NonTerminal Sentencia = new NonTerminal("Sentencia");
            NonTerminal Pt_Comas = new NonTerminal("Pt_Comas");
            NonTerminal Asignacion_Variable = new NonTerminal("Asigacion_Variable");
            NonTerminal Nueva_Asignacion_Variable = new NonTerminal("Nueva_Asignacion_Variable");
            NonTerminal Nueva_Asignacion_Variablep = new NonTerminal("Nueva_Asignacion_Variablep");
            NonTerminal Nueva_Asignacion_Constante = new NonTerminal("Nueva_Asignacion_Constante");
            NonTerminal Asignacion_Constante = new NonTerminal("Asignacion_Constante");
            NonTerminal Valor_Arreglo = new NonTerminal("Valor_Arreglo");
            NonTerminal Funcion = new NonTerminal("Funcion");
            NonTerminal Procedimiento = new NonTerminal("Procedimiento");
            NonTerminal Llamada = new NonTerminal("Llamada");
            NonTerminal Writes = new NonTerminal("Writes");
            NonTerminal Instrucciones_Ciclo = new NonTerminal("Instrucciones_Ciclo");
            NonTerminal Instruccion_Ciclo = new NonTerminal("Instruccion_Ciclo");
            NonTerminal Sentencias_Transferencia = new NonTerminal("Sentencias_Transferencia");
            NonTerminal Nueva_Asignacion_Constantep = new NonTerminal("Nueva_Asignacion_Constantep");
            NonTerminal Dimensiones = new NonTerminal("Dimensiones");
            NonTerminal Indices_Array = new NonTerminal("Indices_Array");
            NonTerminal A = new NonTerminal("a");
            NonTerminal B = new NonTerminal("b");
            NonTerminal C = new NonTerminal("c");
            NonTerminal D = new NonTerminal("d");
            NonTerminal E = new NonTerminal("e");



            #endregion

            #region Gramatica

            Raiz.Rule
                                        = Estructura
                                        ;

            Estructura.Rule
                                        = Program + Identificador + Pt_Coma + Head + Begin + Body + End + Pt
                                        ;

            Head.Rule
                                        = Instrucciones_Head
                                        | Epsilon
                                        ;

            /*Head.ErrorRule
                                        = SyntaxError + Begin
                                        ;*/


            Instrucciones_Head.Rule
                                        = Instruccion_Head + Instrucciones_Headp
                                        ;

            Instrucciones_Headp.Rule
                                        = Instrucciones_Head
                                        | Epsilon
                                        ;


            Instruccion_Head.Rule
                                        = Variables
                                        | Funcion
                                        | Procedimiento
                                        | Types
                                        ;

            Instruccion_Head.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        | SyntaxError + Pt_Coma
                                        ;


            Types.Rule
                                        = Objeto
                                        | Tipo_Array
                                        ;

            Objeto.Rule
                                        = Type + Identificador + Igual + Object + Declaraciones_Type + End + Pt_Coma
                                        ;

            /*Objeto.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        ;*/


            Tipo_Array.Rule
                                        = Type + Identificador + Igual + Array + Cor_Izq + Entero + Pt + Pt + Entero + Cor_Der + Of + Tipo_Variable + Pt_Coma
                                        ;

            /*Tipo_Array.ErrorRule
                                        = SyntaxError + Pt_Coma
                                        ;*/


            Declaraciones_Type.Rule
                                        = Variables + Declaraciones_Type
                                        | Epsilon
                                        ;


            Body.Rule
                                        = Instrucciones_Body
                                        | Epsilon
                                        ;

            /*Body.ErrorRule
                                        = SyntaxError + End
                                        ;*/


            Instrucciones_Body.Rule
                                        = Instruccion_Body + Instrucciones_Bodyp
                                        | Instruccion_Head + Instrucciones_Bodyp
                                        ;

            Instrucciones_Bodyp.Rule
                                        = Instrucciones_Body
                                        | Epsilon
                                        ;

            Instruccion_Funcion.Rule
                                        = Instruccion_Body + Instrucciones_Funcion
                                        | Instruccion_Head + Instrucciones_Funcion
                                        | Retorno_Funcion + Instrucciones_Funcion
                                        ;

            /*Instruccion_Funcion.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        | SyntaxError + Pt_Coma
                                        ;*/

            Instrucciones_Funcion.Rule
                                        = Instruccion_Funcion
                                        | Epsilon
                                        ;



            Retorno_Funcion.Rule
                                        = Exit + Par_Izq + Parametros_Entrada + Par_Der + Pt_Comas
                                        ;

            Instrucciones_Ciclo.Rule
                                        = Instruccion_Ciclo
                                        | Epsilon
                                        ;

            Instruccion_Ciclo.Rule
                                        = Instruccion_Body + Instrucciones_Ciclo
                                        | Instruccion_Head + Instrucciones_Ciclo
                                        | Sentencias_Transferencia + Instrucciones_Ciclo
                                        ;

            /*Instruccion_Ciclo.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        | SyntaxError + Pt_Coma
                                        ;*/


            Sentencias_Transferencia.Rule
                                        = Break
                                        | Continue
                                        ;


            Instruccion_Body.Rule
                                        = Writes
                                        | Asignacion
                                        | If_Statement
                                        | Case_Statement
                                        | For_Statement
                                        | While_Statement
                                        | Repeat_Statement
                                        | Llamada + Pt_Comas
                                        | Break + Pt_Comas
                                        | Continue + Pt_Comas
                    /*Ambiguedad en (*/ | Exit + Par_Izq + Expresion_Cadena + Par_Der + Pt_Coma
                                        | Exit + Par_Izq + Par_Der + Pt_Comas
                                        | Exit + Pt_Comas
                                        ;

            /*Instruccion_Body.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        | SyntaxError + Pt_Coma
                                        ;*/


            Writes.Rule
                                        = Write + Par_Izq + Expresion_Cadena + Writep + Par_Der + Pt_Comas
                                        | WriteLn + Par_Izq + Expresion_Cadena + Writep + Par_Der + Pt_Comas
                                        ;

            /*Writes.ErrorRule
                                        = SyntaxError + Pt_Coma
                                        ;*/

            Writep.Rule
                                        = Coma + Expresion_Cadena + Writep
                                        | Epsilon
                                        ;


            Funcion.Rule
                                        = Function + Identificador + Par_Izq + Parametros_Asignacion + Par_Der + Ds_Pts + Tipo_Variable + Pt_Coma + Instrucciones_Headp + Begin + Instrucciones_Funcion + End + Pt_Coma
                                        | Function + Identificador + Ds_Pts + Tipo_Variable + Pt_Coma + Instrucciones_Headp + Begin + Instrucciones_Funcion + End + Pt_Coma
                                        ;

            /*Funcion.ErrorRule           = SyntaxError + End + Pt
                                        ;*/


            Procedimiento.Rule
                                        = Procedure + Identificador + Par_Izq + Parametros_Asignacion + Par_Der + Pt_Coma + Instrucciones_Headp + Begin + Instrucciones_Bodyp + End + Pt_Coma
                                        | Procedure + Identificador + Pt_Coma + Instrucciones_Headp + Begin + Instrucciones_Bodyp + End + Pt_Coma
                                        ;

            /*Procedimiento.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        ;*/


            Sentencia.Rule
                                        = If_Statement
                                        | Case_Statement
                                        | For_Statement
                                        | While_Statement
                                        | Repeat_Statement
                                        ;



            Asignacion.Rule
                                        = Identificador + Ds_Pts + Igual + Expresion_Cadena + Pt_Coma
                                        | Valor_Arreglo + Ds_Pts + Igual + Expresion_Cadena + Pt_Coma
                                        ;

            /*Asignacion.ErrorRule
                                        = SyntaxError + Pt_Coma
                                        ;*/


            If_Statement.Rule
                                        = If + Par_Izq + Expresion_Logica + Par_Der + Then + Begin + Instrucciones_Bodyp + End + Else_Statement + Pt_Coma
                                        | If + Expresion_Logica + Then + Begin + Instrucciones_Bodyp + End + Else_Statement + Pt_Coma
                                        | If + Par_Izq + Expresion_Logica + Par_Der + Then + Instruccion_Body + Else_Instruccion
                                        | If + Expresion_Logica + Then + Instruccion_Body + Else_Instruccion
                                        ;

            /*If_Statement.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        ;*/


            Else_Statement.Rule
                                        = Else + Begin + Instrucciones_Bodyp + End
                                        | Epsilon
                                        ;



            Else_Instruccion.Rule
                                        = Else + Instruccion_Body
                                        | Epsilon
                                        ;

            Case_Statement.Rule
                                        = Case + Par_Izq + Expresion_Cadena + Par_Der + Of + Cases_Statement + End + Pt_Coma
                                        | Case + Expresion_Cadena + Of + Cases_Statement + End + Pt_Coma
                                        ;

            /*Cases_Statement.ErrorRule
                                        = End + Pt_Coma
                                        ;*/


            Cases_Statement.Rule
                                        = Expresion_Cadena + Ds_Pts + Begin + Instrucciones_Bodyp + End + Pt_Coma + Cases_Statementp //Varias Instrucciones
                                        | Expresion_Cadena + Ds_Pts + Instruccion_Body + Cases_Statementp                   //Una Instruccion
                                        ;

            /*Cases_Statement.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        ;*/

            Cases_Statementp.Rule
                                        = Cases_Statement
                                        | Case_Else_Statement
                                        | Epsilon
                                        ;

            Case_Else_Statement.Rule
                                        = Else + Begin + Instrucciones_Bodyp + End + Pt_Coma      //Varias Instrucciones
                                        | Else + Instruccion_Body                       //Una Instruccion
                                        ;

            /*Case_Else_Statement.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        ;*/

            For_Statement.Rule
                                        = For + Identificador + Ds_Pts + Igual + Expresion_Numerica + To + Expresion_Numerica + Do + Begin + Instrucciones_Bodyp + End + Pt_Coma
                                        ;

            /*For_Statement.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        ;*/

            While_Statement.Rule
                                        = While + Expresion_Logica + Do + Begin + Instrucciones_Bodyp + End + Pt_Coma
                                        | While + Par_Izq + Expresion_Logica + Par_Der + Do + Begin + Instrucciones_Bodyp + End + Pt_Coma
                                        ;

            /*While_Statement.ErrorRule
                                        = SyntaxError + End + Pt_Coma
                                        ;*/


            Repeat_Statement.Rule
                                        = Repeat + Instrucciones_Bodyp + Until + Expresion_Logica + Pt_Coma
                                        ;

            /*Repeat_Statement.ErrorRule
                                        =SyntaxError + Pt_Coma
                                        ;*/



            Variables.Rule
                                        = Const + Nueva_Asignacion_Constante
                  /*Deshabilitar si habilito la de la ambiguedad*/ //| Var + Asignacion_Variable
                                                                   //| Var + Begin + Nueva_Asignacion_Variable + End + Pt_Coma
                  /*Tiene Ambiguedad*/  | Var + Nueva_Asignacion_Variable
                                        ;

            /*Variables.ErrorRule
                                        = SyntaxError + Pt_Coma
                                        ;*/


            Asignacion_Variable.Rule
                                        = Identificador + Ds_Pts + Tipo_Variable + Igual + Expresion_Cadena + Pt_Coma
                                        | Identificador + Ds_Pts + Tipo_Variable + Pt_Coma
                                        | Identificador + Coma + Lista_Variables + Ds_Pts + Tipo_Variable + Pt_Coma
                                        | Identificador + Ds_Pts + Array + Cor_Izq + Dimensiones + Cor_Der + Of + Tipo_Variable + Pt_Coma
                                        ;

            Dimensiones.Rule
                                        = Dimensiones + Coma + Dimensiones
                                        | Entero + Pt + Pt + Entero
                                        ;


            Asignacion_Constante.Rule
                                        = Identificador + Ds_Pts + Tipo_Variable + Igual + Expresion_Cadena + Pt_Coma
                                        ;

            Nueva_Asignacion_Constante.Rule
                                        = Asignacion_Constante + Nueva_Asignacion_Constantep
                                        ;

            Nueva_Asignacion_Constantep.Rule
                                        = Nueva_Asignacion_Constante
                                        | Epsilon
                                        ;


            Nueva_Asignacion_Variable.Rule
                                        = Asignacion_Variable + Nueva_Asignacion_Variablep
                                        ;
                                        


            Nueva_Asignacion_Variablep.Rule
                                        = Nueva_Asignacion_Variable
                                        | Epsilon
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
                                        | Identificador
                                        ;



            Expresion_Cadena.Rule
                                        = Expresion_Cadena + Mas + Expresion_Cadena
                                        | Expresion_Numerica
                                        | Cadena
                                        ;

            Expresion_Numerica.Rule
                                        = Expresion_Numerica + Mas + Expresion_Numerica
                                        | Expresion_Numerica + Menos + Expresion_Numerica
                                        | Expresion_Numerica + Por + Expresion_Numerica
                                        | Expresion_Numerica + Div + Expresion_Numerica
                                        | Expresion_Numerica + Mod + Expresion_Numerica
                                        | Entero
                                        | Decimal
                                        | Identificador
                                        | Llamada
                                        | Valor_Arreglo
                                        | True
                                        | False
                                        | Identificador + Cor_Izq + Indices_Array + Cor_Der
                                        ;

            Indices_Array.Rule
                                        = Indices_Array + Coma + Indices_Array
                                        | Entero
                                        ;
            

            Valor.Rule
                                        = Entero
                                        | Decimal
                                        | Identificador
                                        | Llamada
                                        | Valor_Arreglo
                                        | True
                                        | False
                                        /*Tiene Ambiguedad*/ // | Par_Izq + Expresion_Numerica + Par_Der + Expresion_Numericap
                                        ;


            Expresion_Relacional.Rule
                                        = Expresion_Numerica + May_Que + Expresion_Numerica
                                        | Expresion_Numerica + Men_Que + Expresion_Numerica
                                        | Expresion_Numerica + May_Ig_Que + Expresion_Numerica
                                        | Expresion_Numerica + Men_Ig_Que + Expresion_Numerica
                                        | Expresion_Numerica + No_Igual + Expresion_Numerica
                                        | Expresion_Cadena + Igual + Expresion_Cadena
                                        | Identificador
                                        | Llamada
                                        ;

            Expresion_Logica.Rule
                                        = Expresion_Logica + And + Expresion_Logica
                                        | Expresion_Logica + Or + Expresion_Logica
                                        | Not + Expresion_Logica
                                        | Expresion_Relacional
                                        ;

            Pt_Comas.Rule
                                        = Pt_Coma
                                        | Epsilon
                                        ;

            Parametros_Entrada.Rule
                                        = Expresion_Cadena + Coma + Parametros_Entrada
                                        | Expresion_Cadena
                                        | Epsilon
                                        ;

            Parametros_Asignacion.Rule
                                        = Lista_Variables + Ds_Pts + Tipo_Variable + ParametrosAsignacionp
                                        | Var + Identificador + Ds_Pts + Tipo_Variable + ParametrosAsignacionp
                                        | Epsilon
                                        ;

            ParametrosAsignacionp.Rule
                                        = Pt_Coma + Parametros_Asignacion
                                        | Epsilon
                                        ;


            Llamada.Rule
                                        = Identificador + Par_Izq + Parametros_Entrada + Par_Der
                                        ;

            Valor_Arreglo.Rule
                                        = Identificador + Cor_Izq + Indices_Array + Cor_Der
                                        ;

            


            #endregion

            #region Preferencias
            this.Root = Raiz;
            this.RegisterOperators(1, Associativity.Left, Mas, Menos);
            this.RegisterOperators(2, Associativity.Left, Por, Div);
            this.RegisterOperators(3, Associativity.Left, Mod);
            this.RegisterOperators(4, Associativity.Left, Then, Else);
            this.RegisterOperators(5, Associativity.Left, Var, Identificador);
            this.RegisterOperators(6, Associativity.Left, End, Pt_Coma);
            this.RegisterOperators(7, Associativity.Left, Or, And, Not);
            this.RegisterOperators(8, Associativity.Left, Expresion_Cadena,Par_Der);
            this.RegisterOperators(9, Associativity.Left, Coma);
            #endregion
        }
    }
}
