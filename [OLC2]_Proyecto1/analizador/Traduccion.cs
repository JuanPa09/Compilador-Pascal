using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.traductor.Simbolo;
using Irony.Parsing;
using System.Windows.Forms;

namespace _OLC2__Proyecto1.analizador
{
    class Traduccion
    {
        ParseTreeNode nodoRaiz;
        RichTextBox consola;

        public Traduccion(ParseTreeNode nodoRaiz, RichTextBox consola)
        {
            this.nodoRaiz = nodoRaiz;
            this.consola = consola;
        }

        public void iniciar()
        {
            consola.Text = instrucciones(nodoRaiz);
        }

        


        public string instrucciones(ParseTreeNode actual)
        {

            string instrs = "";
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {

                if (nodo.ChildNodes.Count == 2)
                {
                    instrs = instruccionesMultiples(nodo);
                }
                else
                {
                    instrs +=instruccion(nodo)+"\n";
                }
            }
            return instrs;
        }

        public string instruccionesMultiples(ParseTreeNode actual)
        {
            //LLEGA A INSTRUCCIONES QUE PUEDEN TENER MAS INSTRUCCIONES
            string instrs = "";
            foreach (ParseTreeNode nodo in actual.ChildNodes)
            {
                //Aca se van a hacer los 2 ciclos
                if (nodo.ChildNodes.Count == 2 && nodo.ChildNodes[1].Term.ToString() != "Pt_Comas")
                {
                    instrs += instruccionesMultiples( nodo);

                    continue;
                }

                if (nodo.ChildNodes.Count != 0)
                {
                    string instr = instruccion(nodo);
                    if (instr != null)
                    {
                        instrs += instr + "\n";
                    }
                    else
                    {
                        instrs += instruccionesMultiples(nodo);
                    }
                }
            }
            return instrs;

        }



        public string instruccion(ParseTreeNode actual)
        {
            string nombreNodo = actual.ChildNodes[0].Term.ToString();
            if (actual.ChildNodes.Count == 0) { nombreNodo = actual.ChildNodes[0].Token.Text; }

            switch (nombreNodo)
            {
                case "program":
                    //Obtener Instrucciones Head e Instrucciones Body
                    return "program " + actual.ChildNodes[1].Token.Text + "; \n " + instrucciones(actual.ChildNodes[3]) + "\nbegin \n" + instrucciones(actual.ChildNodes[5]) + "\nend.";
                case "Writes":
                    if (actual.ChildNodes[0].ChildNodes[0].Token.Text == "writeln")
                        return "writeln("+evaluarExpresionCadena(actual.ChildNodes[0].ChildNodes[2], actual.ChildNodes[0].ChildNodes[3])+");";
                    return "write(" + evaluarExpresionCadena(actual.ChildNodes[0].ChildNodes[2], actual.ChildNodes[0].ChildNodes[3]) + ");";
                case "Variables":
                    return evaluarVarConst(actual.ChildNodes[0]);
                case "Types":
                    return evaluarType(actual.ChildNodes[0]);
                case "Asignacion":
                    //return nuevaAsignacion(actual.ChildNodes[0]);
                case "If_Statement":
                    return evaluarIf(actual.ChildNodes[0]);
                case "For_Statement":
                    actual = actual.ChildNodes[0];
                    return "for " + actual.ChildNodes[1].Token.Text + ":=" + evaluarExpresionNumerica(actual.ChildNodes[4]) + " to " + evaluarExpresionNumerica(actual.ChildNodes[6]) + "do \n begin" + instrucciones(actual.ChildNodes[9]) + "\n end;";
                case "While_Statement":
                    return evaluarWhile(actual.ChildNodes[0]);
                case "Repeat_Statement":
                    actual = actual.ChildNodes[0];
                    return "repeat \n " + instrucciones(actual.ChildNodes[1]) + " \n until  " + evaluarExpresionLogica(actual.ChildNodes[3]) + ";";
                case "Case_Statement":
                    return evaluarCase(actual.ChildNodes[0]);
                case "Funcion":
                    //return evaluarFuncion(actual.ChildNodes[0]);
                case "Procedimiento":
                    //return evaluarProcedimiento(actual.ChildNodes[0]);
                case "Llamada":
                    return evaluarNuevaLlamada(actual.ChildNodes[0]);
                case "break":
                    return "\n break;";
                case "continue":
                    return "continue;";
                case "exit":
                    if (actual.ChildNodes.Count == 5)
                        return "exit("+expresionCadena(actual.ChildNodes[2]) + ")";
                    return "exit;";


            }

            return null;
        }

        /* ------------------------ Evaluacion Variables -------------------------- */

        public string evaluarType(ParseTreeNode actual)
        {
            switch (actual.ChildNodes[0].Term.ToString())
            {
                case "Objeto":
                    return nuevoObjeto(actual.ChildNodes[0]);
                case "Tipo_Array":
                    return null;
            }
            return null;
        }

        public string nuevoObjeto(ParseTreeNode actual)
        {

            return "Type \n" + actual.ChildNodes[1].Token.Text + " = object \n" + evaluarVarConst(actual.ChildNodes[4]);
        }

        public string evaluarVarConst(ParseTreeNode actual)
        {
            if (actual.ChildNodes[0].Term.ToString() == "var")
            {
                return "var " + evaluarVariable(actual.ChildNodes[1], true); //Es variable
            }
            else
            {
                return "const" + evaluarVariable(actual.ChildNodes[1], false) + ";"; //Es constante
            }

        }

        public string evaluarVariable(ParseTreeNode actual, bool isVariable)
        {
            //Estoy en var Nueva_Asignacion_variable
            string variables = "";
            if (actual.ChildNodes.Count == 0)
                return "";


            //Ir a Asignacion Variable
            variables += "\n" + declaracionVariable(actual.ChildNodes[0], isVariable);

            if (actual.ChildNodes[1].ChildNodes.Count != 0)
                variables += "\n" + evaluarVariable(actual.ChildNodes[1].ChildNodes[0], isVariable);
            return variables;
        }

        public string declaracionVariable(ParseTreeNode actual, bool isVariable)
        {
            // Estoy en ID : Tipo ......
            int cantidad = actual.ChildNodes.Count;
            string variables = "";
            switch (cantidad)
            {
                case 4:
                    variables+= actual.ChildNodes[0].Token.Text + ":" + getTipo(actual.ChildNodes[2]) + ";";
                    break;
                case 6:
                    if (actual.ChildNodes[1].Token.Text != ",")
                    {
                        variables += actual.ChildNodes[0].Token.Text + ":" + getTipo(actual.ChildNodes[2]) + " = " + expresionCadena(actual.ChildNodes[4]) + ";";
                    }
                    else
                    {
                        //Tiene ,
                        variables += variosIds(actual.ChildNodes[2], actual.ChildNodes[0].Token.Text, getTipo(actual.ChildNodes[4])) + ";";
                    }
                    break;
                case 9:
                    variables += actual.ChildNodes[0].Token.Text + ": array[" + getDimensiones(actual.ChildNodes[4]) + "] of " + getTipo(actual.ChildNodes[7]) + ";"; 
                    break;
            }
            return variables;

        }

        /*
        public Instruccion nuevaAsignacion(ParseTreeNode actual)
        {
            switch (actual.ChildNodes.Count)
            {
                case 7:
                    return new AsignacionObjeto(actual.ChildNodes[0].Token.Text, actual.ChildNodes[2].Token.Text, expresionCadena(actual.ChildNodes[5]));
                default:
                    switch (actual.ChildNodes[0].Term.ToString())
                    {
                        case "ID":
                            return new NuevaAsignacion(actual.ChildNodes[0].Token.Text, expresionCadena(actual.ChildNodes[3]));
                        case "Valor_Arreglo":
                            Expresion expresion = expresionCadena(actual.ChildNodes[3]);
                            actual = actual.ChildNodes[0];
                            LinkedList<Expresion> indices = new LinkedList<Expresion>();
                            return new AsignacionArreglo(actual.ChildNodes[0].Token.Text, getIndicesArray(actual.ChildNodes[2], indices), expresion);
                        default:
                            return null;
                    }
            }
        }
        */
        public string variosIds(ParseTreeNode actual, string id, string tipo)
        {
            // listaDeclaraciones.AddLast(NuevaDeclaracion(expresionCadena(actual.ChildNodes)))
            LinkedList<string> variables = listaVariables(actual, new LinkedList<string>());
            string ids = id;


            foreach (string identificador in variables)
            {
                ids += "," + identificador;
            }
            return ids+= ":" + tipo; 
        }

        public LinkedList<string> listaVariables(ParseTreeNode actual, LinkedList<string> lista)
        {
            switch (actual.ChildNodes.Count)
            {
                case 2:
                    lista.AddLast(actual.ChildNodes[0].Token.Text);
                    if (actual.ChildNodes[1].ChildNodes.Count == 0)
                        return lista;
                    return listaVariables(actual.ChildNodes[1], lista);
                default: // 3
                    lista.AddLast(actual.ChildNodes[1].Token.Text);
                    if (actual.ChildNodes[2].ChildNodes.Count == 0)
                        return lista;
                    return listaVariables(actual.ChildNodes[2], lista);
            }
        }

        public string getDimensiones(ParseTreeNode actual)
        {
            string dimensiones = "";
            if (actual.ChildNodes.Count == 4)
            {
                dimensiones += actual.ChildNodes[0].Token.Text + ".." + actual.ChildNodes[3].Token.Text;
            }
            else
            {
                dimensiones+=getDimensiones(actual.ChildNodes[0]);
                dimensiones+=","+ getDimensiones(actual.ChildNodes[2]);

            }
            return dimensiones;
        }

        public string getIndicesArray(ParseTreeNode actual)
        {
            string indices;
            switch (actual.ChildNodes.Count)
            {
                case 3:
                    //indices =  getIndicesArray(actual.ChildNodes[0],indices);
                    //indices =  getIndicesArray(actual.ChildNodes[2], indices);
                    indices = getIndicesArray(actual.ChildNodes[0]);
                    return indices += "," + getIndicesArray(actual.ChildNodes[2]);
                default:
                    return expresionCadena(actual.ChildNodes[0]);
            }
        }


        /* ---------------------------------- Evaluaciones Sentencias De Control ----------------------------------- */

        public string evaluarIf(ParseTreeNode actual)
        {
            int cantidad = actual.ChildNodes.Count;
            switch (cantidad)
            {
                case 10:
                    return "If (" +  evaluarExpresionLogica(actual.ChildNodes[2]) + ") then \n begin \n" +  instrucciones(actual.ChildNodes[6]) + "\n end" + evaluarElse(actual.ChildNodes[8]) + ";";
                case 8:
                    return "If " + evaluarExpresionLogica(actual.ChildNodes[1]) + "then \n begin \n" + instrucciones(actual.ChildNodes[4]) + "\n end" + evaluarElse(actual.ChildNodes[6]) + ";";
                case 7:
                    return "If ( " + evaluarExpresionLogica(actual.ChildNodes[2]) + ") then \n" + instruccion(actual.ChildNodes[5]) + evaluarElse(actual.ChildNodes[6]);
                default:
                    return "If " + evaluarExpresionLogica(actual.ChildNodes[1]) + "\n then \n" + instruccion(actual.ChildNodes[3]) + evaluarElse(actual.ChildNodes[4]);
            }
        }

        public string evaluarElse(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 0)
                return "";
            int cantidad = actual.ChildNodes.Count;
            switch (cantidad)
            {
                case 4:
                    return "\n else \n begin \n " + instrucciones(actual.ChildNodes[2]) + "\nend";
                default:
                    // 1 Instruccion
                    return "\n else \n " + instruccion(actual.ChildNodes[1]);
            }
        }

        public string evaluarWhile(ParseTreeNode actual)
        {
            switch (actual.ChildNodes.Count)
            {
                case 7:
                    return "while " + evaluarExpresionLogica(actual.ChildNodes[1]) + "do \n begin \n" + instrucciones(actual.ChildNodes[4]) + "\n end;";
                default:
                    //9
                    return "while ( " + evaluarExpresionLogica(actual.ChildNodes[2]) + ") do \n begin \n" + instrucciones(actual.ChildNodes[6]) + "\n end;";
            }
        }

        public string evaluarCase(ParseTreeNode actual)
        {
            
            switch (actual.ChildNodes.Count)
            {
                case 8:
                    return "case (" + expresionCadena(actual.ChildNodes[2]) + ") of \n" +  actual.ChildNodes[5] + " \n end;";
                default:
                    //Son 6
                    return "case " + expresionCadena(actual.ChildNodes[1]) + " of \n" + actual.ChildNodes[3] + "\n end;";
            }
        }

        public string cases(ParseTreeNode actual)
        {
            string casos = "";
            switch (actual.ChildNodes.Count)
            {
                case 4:
                    
                    casos += expresionCadena(actual.ChildNodes[0]) + " : " + instruccion(actual.ChildNodes[2]) + "\n";
                    casos += casePrima(actual.ChildNodes[3]);
                    return casos;
                default:
                    casos += expresionCadena(actual.ChildNodes[0]) + " : \n begin " + instrucciones(actual.ChildNodes[3]) + "end;";
                    return casos;
            }
        }

        public string casePrima(ParseTreeNode actual)
        {
            switch (actual.ChildNodes[0].Term.ToString())
            {
                case "Cases_Statement":
                    return cases(actual.ChildNodes[0]);
                case "Case_Else_Statement":
                    return CaseElse(actual.ChildNodes[0]);
            }
            return null;
        }

        public string CaseElse(ParseTreeNode actual)
        {
            switch (actual.ChildNodes.Count)
            {
                case 5:
                    return "else \n begin " + instrucciones(actual.ChildNodes[2]) + "end;";
                default: // 2
                    return "else " + instruccion(actual.ChildNodes[1]);
            }
        }
        /*
        public NuevaFuncion evaluarFuncion(ParseTreeNode actual,Entorno padre)
        {
            Dictionary<string, Instruccion> paramsValor = new Dictionary<string, Instruccion>();
            Dictionary<string, Instruccion> paramsRef = new Dictionary<string, Instruccion>();
            Dictionary<int, string> orden = new Dictionary<int, string>();
            LinkedList<Tipo> paramsTipos = new LinkedList<Tipo>();


            Entorno entorno = new Entorno(padre,actual.ChildNodes[1].Token.Text);

            switch (actual.ChildNodes.Count)
            {
                case 13:
                    parametrosFuncion(actual.ChildNodes[3],entorno);
                    return new NuevaFuncion(actual.ChildNodes[1].Token.Text, crearFuncion(actual.ChildNodes[1].Token.Text, getTipo(actual.ChildNodes[6]), paramsValor, paramsRef, instrucciones(actual.ChildNodes[8]), instrucciones(actual.ChildNodes[10]), paramsTipos, orden));
                default:
                    return new NuevaFuncion(actual.ChildNodes[1].Token.Text, crearFuncion(actual.ChildNodes[1].Token.Text, getTipo(actual.ChildNodes[3]), paramsValor, paramsRef, instrucciones(actual.ChildNodes[5]), instrucciones(actual.ChildNodes[7]), paramsTipos, orden));
            }
        }

        public Funcion crearFuncion(string nombre, Tipo tipo, Dictionary<string, Instruccion> paramsValor, Dictionary<string, Instruccion> paramsRef, LinkedList<Instruccion> head, LinkedList<Instruccion> body, LinkedList<Tipo> paramsTipos, Dictionary<int, string> orden)
        {

            foreach (Instruccion instruccion in body)
            {
                head.AddLast(instruccion);
            }
            return new Funcion(nombre, tipo, paramsValor, paramsRef, head, paramsTipos, orden);
        }
        */
        /*
        public void parametrosFuncion(ParseTreeNode actual, Entorno entorno)
        {
            switch (actual.ChildNodes.Count)
            {
                case 4:
                    LinkedList<string> ids = new LinkedList<string>();
                    listaVariables(actual.ChildNodes[0], ids);
                    foreach (string variable in ids)
                    {
                        string nuevoNombre 
                    }
                    if (actual.ChildNodes[3].ChildNodes.Count == 0)
                        return;
                    parametrosFuncion(actual.ChildNodes[3].ChildNodes[1], ref paramsValor, ref paramsRef, ref paramsTipos, ref orden, pos);
                    return;
                case 5:
                    paramsRef.Add(actual.ChildNodes[1].Token.Text, new NuevaDeclaracion(null, actual.ChildNodes[1].Token.Text, getTipo(actual.ChildNodes[3]), true));
                    paramsTipos.AddLast(getTipo(actual.ChildNodes[3]));
                    orden.Add(pos, actual.ChildNodes[1].Token.Text);
                    if (actual.ChildNodes[4].ChildNodes.Count == 0)
                        return;
                    parametrosFuncion(actual.ChildNodes[4].ChildNodes[1], ref paramsValor, ref paramsRef, ref paramsTipos, ref orden, pos + 1);
                    return;

                default:
                    return;
            }
        }
        */
        public string evaluarNuevaLlamada(ParseTreeNode actual)
        {
            
            
            return entradaFuncion(actual.ChildNodes[2]);
        }

        public string entradaFuncion(ParseTreeNode actual)
        {

            switch (actual.ChildNodes.Count)
            {
                case 3:
                    
                    return expresionCadena(actual.ChildNodes[0]) + "," + entradaFuncion(actual.ChildNodes[2]);
                case 1:
                    if (actual.ChildNodes[0].Term.ToString() != "Expresion_Cadena")
                        return "";
                    return expresionCadena(actual.ChildNodes[0]); ;
            }

            return "";
        }

        
        /*
        public NuevoProcedimiento evaluarProcedimiento(ParseTreeNode actual)
        {
            Dictionary<string, Instruccion> paramsValor = new Dictionary<string, Instruccion>();
            Dictionary<string, Instruccion> paramsRef = new Dictionary<string, Instruccion>();
            Dictionary<int, string> orden = new Dictionary<int, string>();
            LinkedList<Tipo> paramsTipos = new LinkedList<Tipo>();

            switch (actual.ChildNodes.Count)
            {
                case 11:
                    parametrosFuncion(actual.ChildNodes[3], ref paramsValor, ref paramsRef, ref paramsTipos, ref orden, 1);
                    return new NuevoProcedimiento(actual.ChildNodes[1].Token.Text, crearProcedimiento(actual.ChildNodes[1].Token.Text, paramsValor, paramsRef, instrucciones(actual.ChildNodes[6]), instrucciones(actual.ChildNodes[8]), paramsTipos, orden));
                default:
                    return new NuevoProcedimiento(actual.ChildNodes[1].Token.Text, crearProcedimiento(actual.ChildNodes[1].Token.Text, paramsValor, paramsRef, instrucciones(actual.ChildNodes[3]), instrucciones(actual.ChildNodes[5]), paramsTipos, orden));
            }
        }

        public Procedimiento crearProcedimiento(string nombre, Dictionary<string, Instruccion> paramsValor, Dictionary<string, Instruccion> paramsRef, LinkedList<Instruccion> head, LinkedList<Instruccion> body, LinkedList<Tipo> paramsTipos, Dictionary<int, string> orden)
        {
            foreach (Instruccion instruccion in body)
            {
                head.AddLast(instruccion);
            }
            return new Procedimiento(nombre, paramsValor, paramsRef, head, paramsTipos, orden);
        }

        */

        /* ------------------------ EVALUACION EXPRESIONES ------------------------ */

        public string evaluarExpresionCadena(ParseTreeNode expresionCadena, ParseTreeNode masTexto)
        {
            string ExpresionCadena = "";
            ExpresionCadena = this.expresionCadena(expresionCadena);
            string MasTexto = "";
            MasTexto = this.masTexto(masTexto);

            if (MasTexto != null)
            {
                return ExpresionCadena + "+" + MasTexto;
            }
            else
            {
                return ExpresionCadena;
            }
        }

        public string evaluarExpresionNumerica(ParseTreeNode actual)
        {

            if (actual.ChildNodes.Count == 3)
            {
                string operador = actual.ChildNodes[1].Token.Text;
                switch (operador)
                {
                    case "+":
                        return evaluarExpresionNumerica(actual.ChildNodes[0]) + "+" + evaluarExpresionNumerica(actual.ChildNodes[2]);
                    case "-":
                        return evaluarExpresionNumerica(actual.ChildNodes[0]) + "-" + evaluarExpresionNumerica(actual.ChildNodes[2]);
                    case "*":
                        return evaluarExpresionNumerica(actual.ChildNodes[0]) + "*" + evaluarExpresionNumerica(actual.ChildNodes[2]);
                    case "/":
                        return evaluarExpresionNumerica(actual.ChildNodes[0]) + "/" + evaluarExpresionNumerica(actual.ChildNodes[2]);
                    case ".":
                        return evaluarExpresionNumerica(actual.ChildNodes[0]) + "." + evaluarExpresionNumerica(actual.ChildNodes[2]);
                    default:
                        return evaluarExpresionNumerica(actual.ChildNodes[0]) + "%" + evaluarExpresionNumerica(actual.ChildNodes[2]);
                }
            }
            else
            {
                switch (actual.ChildNodes[0].Term.ToString())
                {
                    case "DOUBLE":
                        return actual.ChildNodes[0].Token.Text;

                    case "ID":
                        return actual.ChildNodes[0].Token.Text;
                    case "Llamada":
                        return evaluarNuevaLlamada(actual.ChildNodes[0]);
                    case "Valor_Arreglo":
                        actual = actual.ChildNodes[0];
                        return actual.ChildNodes[0].Token.Text + "[" + getIndicesArray(actual.ChildNodes[2]) + "]"; ;
                    case "true":
                        return "true";
                    case "false":
                        return "false";
                    default:
                        // Es INT
                        return actual.ChildNodes[0].Token.Text;
                }

            }
        }

        public string evaluarExpresionLogica(ParseTreeNode actual)
        {
            int cantidad = actual.ChildNodes.Count;
            switch (cantidad)
            {
                case 3:
                    // Llevan Or o And
                    return evaluarExpresionLogica(actual.ChildNodes[0]) + actual.ChildNodes[1].Token.Text + evaluarExpresionLogica(actual.ChildNodes[2]);
                case 2:
                    // Tiene operador Not
                    return "Not "+evaluarExpresionLogica(actual.ChildNodes[1]);
                case 1:
                    // Tiene Expresion Relacional
                    return evaluarExpresionRelacional(actual.ChildNodes[0]);
            }
            return null;
        }

        public string evaluarExpresionRelacional(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 3)
            {
                //string operador = actual.ChildNodes[1].Toke
                if (actual.ChildNodes[0].Term.ToString() == "Expresion_Numerica")
                    return evaluarExpresionNumerica(actual.ChildNodes[0]) + actual.ChildNodes[1].Token.Text + evaluarExpresionNumerica(actual.ChildNodes[2]);
                return expresionCadena(actual.ChildNodes[0]) + actual.ChildNodes[1].Token.Text + expresionCadena(actual.ChildNodes[2]);

            }
            else
            {
                //Buscar Identificador
                if (actual.ChildNodes.Count != 0)
                    return actual.ChildNodes[0].Token.Text;
                if (actual.Token.Text.ToLower() == "true")
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
        }


        /* ------------------------ Otras Evaluaciones ----------------------------- */

        public string expresionCadena(ParseTreeNode expresionCadena)
        {
            if (expresionCadena.ChildNodes.Count == 1)
            {
                if (expresionCadena.ChildNodes[0].Term.ToString() != "Expresion_Numerica")
                {
                    //Sintetizar
                    return expresionCadena.ChildNodes[0].Token.Text;
                }
                else
                {
                    //Es una expresion Numerica
                    return evaluarExpresionNumerica(expresionCadena.ChildNodes[0]);
                }
            }
            else
            {
                //Tiene 3 nodos (Expresion_Cadena Simbolo(+,-...) Expresion_Cadena
                string ExpresionCadena1 = null;
                string ExpresionCadena2 = null;

                ExpresionCadena1 = this.expresionCadena(expresionCadena.ChildNodes[0]);
                ExpresionCadena2 = this.expresionCadena(expresionCadena.ChildNodes[2]);

                if (ExpresionCadena1 != null && ExpresionCadena2 != null)
                {
                    return ExpresionCadena1 + getSignoAritmetica(expresionCadena.ChildNodes[1]) + ExpresionCadena2;
                }
                else
                {
                    return ExpresionCadena1;
                }
            }
        }

        public string masTexto(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 0)
            {
                return null;
            }
            else
            {
                //Tiene 3 hijos (, Expresion_Cadena Mas_Texto)
                string expresionCadena = this.expresionCadena(actual.ChildNodes[1]);
                string masTexto = this.masTexto(actual.ChildNodes[2]);

                if (expresionCadena != null && masTexto != null)
                {
                    return expresionCadena + "+" + masTexto;
                }
                else
                {
                    return expresionCadena;
                }

            }
        }

        /* ------------------------ GETS ---------------------------*/

        public char getSignoAritmetica(ParseTreeNode actual)
        {
            switch (actual.Token.Text)
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

        public string getTipo(ParseTreeNode actual)
        {
            switch (actual.ChildNodes[0].Term.ToString())
            {
                case "integer":
                    return "integer";
                case "string":
                    return "string";
                case "boolean":
                    return "boolean";
                case "real":
                    return "real";
                case "array":
                    return "array";
                case "ID":
                    return actual.ChildNodes[0].Token.Text;
                default:
                    return "error"; ;
            }
        }

        public char Literales(ParseTreeNode actual)
        {
            switch (actual.ChildNodes[0].Term.ToString())
            {


                case "INT":
                    return 'N';

                case "CADENA":
                    return 'S';
                case "Array":
                    return 'A';
                case "true":
                    return 'T';
                case "false":
                    return 'F';
            }
            return 'E';
        }
    }
}
