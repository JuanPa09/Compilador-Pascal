using System;
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
            Entorno global = new Entorno("global",null);
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
                if (nodo.ChildNodes.Count == 2 && nodo.ChildNodes[1].Term.ToString()!="Pt_Comas")
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
                case "Variables":
                    LinkedList<Instruccion> listaDeclaraciones = new LinkedList<Instruccion>();
                    evaluarVariable(actual.ChildNodes[0].ChildNodes[1],ref listaDeclaraciones);
                    return new DeclararVariable(listaDeclaraciones);
                case "Asignacion":
                    return nuevaAsignacion(actual.ChildNodes[0]);
                case "If_Statement":
                    return evaluarIf(actual.ChildNodes[0]);
                case "For_Statement":
                    actual = actual.ChildNodes[0];
                    return new For(evaluarExpresionNumerica(actual.ChildNodes[4]), evaluarExpresionNumerica(actual.ChildNodes[6]), actual.ChildNodes[1].Token.Text, instrucciones(actual.ChildNodes[9]));
                case "While_Statement":
                    return evaluarWhile(actual.ChildNodes[0]);
                case "Repeat_Statement":
                    actual = actual.ChildNodes[0];
                    return new Repeat(evaluarExpresionLogica(actual.ChildNodes[3]), instrucciones(actual.ChildNodes[1]));
                case "Case_Statement":
                    return evaluarCase(actual.ChildNodes[0]);
                case "Funcion":
                    return evaluarFuncion(actual.ChildNodes[0]);
                case "Procedimiento":
                    return evaluarProcedimiento(actual.ChildNodes[0]);
                case "Llamada":
                    return evaluarNuevaLlamada(actual.ChildNodes[0]);
                case "break":
                    return new Break();
                case "continue":
                    return new Continue();
                case "exit":
                    return new Exit(expresionCadena(actual.ChildNodes[2]));
                
                

            }

            return null; 
        }

        /* ------------------------ Evaluacion Variables -------------------------- */

        public LinkedList<Instruccion> evaluarVariable(ParseTreeNode actual, ref LinkedList<Instruccion> listaDeclaraciones)
        {
            Debug.WriteLine("nodo -> "+actual.Term.ToString());
            //Estoy en var Nueva_Asignacion_variable
            if (actual.ChildNodes.Count==0)
                return null;

            
            //Ir a Asignacion Variable
            listaDeclaraciones.AddLast(declaracionVariable(actual.ChildNodes[0],ref listaDeclaraciones));

            if (actual.ChildNodes[1].ChildNodes.Count != 0)
                evaluarVariable(actual.ChildNodes[1].ChildNodes[0],ref listaDeclaraciones);
            return null;
        }

        public Instruccion declaracionVariable(ParseTreeNode actual, ref LinkedList<Instruccion> listaDeclaraciones)
        {
            // Estoy en ID : Tipo ......
            int cantidad = actual.ChildNodes.Count;

            switch (cantidad)
            {
                case 4:
                    listaDeclaraciones.AddLast(new NuevaDeclaracion(null,actual.ChildNodes[0].Token.Text,getTipo(actual.ChildNodes[2])));
                    break;
                case 6:
                    if (actual.ChildNodes[1].Token.Text != ",")
                    {
                        listaDeclaraciones.AddLast(new NuevaDeclaracion(expresionCadena(actual.ChildNodes[4]), actual.ChildNodes[0].Token.Text, getTipo(actual.ChildNodes[2])));
                    }
                    else
                    {
                        //Tiene ,
                        listaDeclaraciones = variosIds(actual.ChildNodes[2], actual.ChildNodes[0].Token.Text, getTipo(actual.ChildNodes[4]), ref listaDeclaraciones);
                    }
                    break;
            }
            return null;

        }

        public Instruccion nuevaAsignacion(ParseTreeNode actual)
        {
            switch(actual.ChildNodes[0].Term.ToString())
            {
                case "ID":
                    return new NuevaAsignacion(actual.ChildNodes[0].Token.Text, expresionCadena(actual.ChildNodes[3]));

                default:
                    return null;

            }
        }

        public  LinkedList<Instruccion> variosIds(ParseTreeNode actual, string id, Tipos tipo, ref LinkedList<Instruccion> listaDeclaraciones)
        {
            // listaDeclaraciones.AddLast(NuevaDeclaracion(expresionCadena(actual.ChildNodes)))
            LinkedList<string> variables = listaVariables(actual, new LinkedList<string>()) ;

            listaDeclaraciones.AddLast(new NuevaDeclaracion(null,id,tipo));

            foreach(string identificador in variables)
            {
                listaDeclaraciones.AddLast(new NuevaDeclaracion(null,identificador,tipo));
            }

           return listaDeclaraciones;
        }

        public LinkedList<string> listaVariables(ParseTreeNode actual,LinkedList<string> lista)
        {
            Debug.WriteLine(actual.Term.ToString());
            switch(actual.ChildNodes.Count)
            {
                case 2:
                    lista.AddLast(actual.ChildNodes[0].Token.Text);
                    if (actual.ChildNodes[1].ChildNodes.Count == 0)
                        return lista;
                    return listaVariables(actual.ChildNodes[1],lista);
                default: // 3
                    lista.AddLast(actual.ChildNodes[1].Token.Text);
                    if (actual.ChildNodes[2].ChildNodes.Count == 0)
                        return lista;
                    return listaVariables(actual.ChildNodes[2], lista);
            }
        }

        /* ---------------------------------- Evaluaciones Sentencias De Control ----------------------------------- */

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

        public While evaluarWhile(ParseTreeNode actual)
        {
            switch (actual.ChildNodes.Count)
            {
                case 7:
                    return new While(instrucciones(actual.ChildNodes[4]), evaluarExpresionLogica(actual.ChildNodes[1]));
                default:
                    //9
                    return new While(instrucciones(actual.ChildNodes[6]), evaluarExpresionLogica(actual.ChildNodes[2]));
            }
        }

        public Instruccion evaluarCase(ParseTreeNode actual)
        {
            Dictionary<Expresion, LinkedList<Instruccion>> casos = new Dictionary<Expresion, LinkedList<Instruccion>>();
            switch (actual.ChildNodes.Count)
            {
                case 8:
                    return cases(expresionCadena(actual.ChildNodes[2]),casos,actual.ChildNodes[5]);
                default:
                    //Son 6
                    return cases(expresionCadena(actual.ChildNodes[1]), casos, actual.ChildNodes[3]);
            }
        }

        public Case cases(Expresion valor, Dictionary<Expresion, LinkedList<Instruccion>> casos, ParseTreeNode actual)
        {

            switch (actual.ChildNodes.Count)
            {
                case 4:
                    LinkedList<Instruccion> instr = new LinkedList<Instruccion>();
                    instr.AddLast(instruccion(actual.ChildNodes[2]));
                    casos.Add(expresionCadena(actual.ChildNodes[0]), instr);
                    return casePrima(valor,casos,actual.ChildNodes[3]);
                default:
                    casos.Add(expresionCadena(actual.ChildNodes[0]), instrucciones(actual.ChildNodes[3]));
                    return casePrima(valor,casos,actual.ChildNodes[6]);
            }
        }

        public Case casePrima(Expresion valor, Dictionary<Expresion, LinkedList<Instruccion>> casos, ParseTreeNode actual)
        {
            switch (actual.ChildNodes[0].Term.ToString())
            {
                case "Cases_Statement":
                    return cases(valor,casos,actual.ChildNodes[0]);
                case "Case_Else_Statement":
                    return new Case(valor,casos,CaseElse(actual.ChildNodes[0]));
                default:
                    return new Case(valor, casos, new LinkedList<Instruccion>());
            }
        }

        public LinkedList<Instruccion> CaseElse(ParseTreeNode actual)
        {
            switch(actual.ChildNodes.Count)
            {
                case 5:
                    return instrucciones(actual.ChildNodes[2]);
                default: // 2
                    LinkedList<Instruccion> instr = new LinkedList<Instruccion>();
                    instr.AddLast(instruccion(actual.ChildNodes[1]));
                    return instr;
            }
        }

        public NuevaFuncion evaluarFuncion(ParseTreeNode actual)
        {
            Dictionary<string, Instruccion> paramsValor = new Dictionary<string, Instruccion>();
            Dictionary<string, Instruccion> paramsRef = new Dictionary<string, Instruccion>();
            Dictionary<int, string> orden = new Dictionary<int, string>();
            LinkedList<Tipos> paramsTipos = new LinkedList<Tipos>();

            switch (actual.ChildNodes.Count)
            {
                case 13:
                    parametrosFuncion(actual.ChildNodes[3],ref paramsValor,ref paramsRef,ref paramsTipos,ref orden,1);
                    return new NuevaFuncion(actual.ChildNodes[1].Token.Text,crearFuncion(actual.ChildNodes[1].Token.Text, getTipo(actual.ChildNodes[6]),paramsValor,paramsRef, instrucciones(actual.ChildNodes[8]), instrucciones(actual.ChildNodes[10]),paramsTipos,orden));
                default:
                    return new NuevaFuncion(actual.ChildNodes[1].Token.Text, crearFuncion(actual.ChildNodes[1].Token.Text, getTipo(actual.ChildNodes[3]), paramsValor, paramsRef, instrucciones(actual.ChildNodes[5]), instrucciones(actual.ChildNodes[7]),paramsTipos,orden));
            }
        }

        public Funcion crearFuncion(string nombre, Tipos tipo, Dictionary<string,Instruccion> paramsValor, Dictionary<string, Instruccion> paramsRef, LinkedList<Instruccion> head, LinkedList<Instruccion> body, LinkedList<Tipos> paramsTipos, Dictionary<int, string> orden)
        {
            
            foreach(Instruccion instruccion in body)
            {
                head.AddLast(instruccion);
            }
            return new Funcion(nombre, tipo, paramsValor, paramsRef, head, paramsTipos,orden);
        }

        public void parametrosFuncion(ParseTreeNode actual, ref Dictionary<string, Instruccion> paramsValor, ref Dictionary<string, Instruccion> paramsRef,ref LinkedList<Tipos> paramsTipos,ref Dictionary<int, string> orden, int pos)
        {
            switch (actual.ChildNodes.Count)
            {
                case 4:
                    LinkedList<string> ids = new LinkedList<string>();
                    listaVariables(actual.ChildNodes[0], ids);
                    Tipos tipoVar = getTipo(actual.ChildNodes[2]);
                    foreach(string variable in ids)
                    {
                        paramsValor.Add(variable, new NuevaDeclaracion(null,variable,tipoVar));
                        orden.Add(pos, variable);
                        paramsTipos.AddLast(tipoVar);
                        pos++;
                    }
                    if (actual.ChildNodes[3].ChildNodes.Count == 0)
                        return;
                    parametrosFuncion(actual.ChildNodes[3].ChildNodes[1],ref paramsValor, ref paramsRef, ref paramsTipos,ref orden , pos);
                    return;
                case 5:
                    paramsRef.Add(actual.ChildNodes[1].Token.Text,new NuevaDeclaracion(null,actual.ChildNodes[1].Token.Text,getTipo(actual.ChildNodes[3])));
                    paramsTipos.AddLast(getTipo(actual.ChildNodes[3]));
                    orden.Add(pos, actual.ChildNodes[1].Token.Text);
                    if (actual.ChildNodes[4].ChildNodes.Count == 0)
                        return;
                    parametrosFuncion(actual.ChildNodes[4].ChildNodes[1], ref paramsValor, ref paramsRef, ref paramsTipos, ref orden , pos +1);
                    return;

                default:
                    return;
            }
        }

        public Instruccion evaluarNuevaLlamada(ParseTreeNode actual)
        {
            LinkedList<Expresion> valores = new LinkedList<Expresion>();
            entradaFuncion(actual.ChildNodes[2], ref valores);
            return new NuevaLlamada(actual.ChildNodes[0].Token.Text,valores);
        }
        
        public void entradaFuncion(ParseTreeNode actual, ref LinkedList<Expresion>valores)
        {

            switch(actual.ChildNodes.Count)
            {
                case 3:
                    valores.AddLast(expresionCadena(actual.ChildNodes[0]));
                    entradaFuncion(actual.ChildNodes[2],ref valores);
                    return ;
                case 1:
                    if (actual.ChildNodes[0].Term.ToString() != "Expresion_Cadena")
                        return ;
                    valores.AddLast(expresionCadena(actual.ChildNodes[0]));
                    return ;
            }

            return ;
        }

        public void entradFuncionp(ParseTreeNode actual, ref LinkedList<Expresion> valores)
        {
            switch(actual.ChildNodes.Count)
            {
                case 1: return;

                default: entradaFuncion(actual.ChildNodes[1], ref valores);
                    break;
            }
        }

        public NuevoProcedimiento evaluarProcedimiento(ParseTreeNode actual)
        {
            Dictionary<string, Instruccion> paramsValor = new Dictionary<string, Instruccion>();
            Dictionary<string, Instruccion> paramsRef = new Dictionary<string, Instruccion>();
            Dictionary<int, string> orden = new Dictionary<int, string>();
            LinkedList<Tipos> paramsTipos = new LinkedList<Tipos>();

            switch (actual.ChildNodes.Count)
            {
                case 11:
                    parametrosFuncion(actual.ChildNodes[3], ref paramsValor, ref paramsRef, ref paramsTipos, ref orden, 1);
                    return new NuevoProcedimiento(actual.ChildNodes[1].Token.Text, crearProcedimiento(actual.ChildNodes[1].Token.Text, paramsValor, paramsRef, instrucciones(actual.ChildNodes[6]), instrucciones(actual.ChildNodes[8]), paramsTipos, orden));
                default:
                    return new NuevoProcedimiento(actual.ChildNodes[1].Token.Text, crearProcedimiento(actual.ChildNodes[1].Token.Text, paramsValor, paramsRef, instrucciones(actual.ChildNodes[3]), instrucciones(actual.ChildNodes[5]), paramsTipos, orden));
            }
        }

        public Procedimiento crearProcedimiento(string nombre, Dictionary<string, Instruccion> paramsValor, Dictionary<string, Instruccion> paramsRef, LinkedList<Instruccion> head, LinkedList<Instruccion> body, LinkedList<Tipos> paramsTipos, Dictionary<int, string> orden)
        {
            foreach (Instruccion instruccion in body)
            {
                head.AddLast(instruccion);
            }
            return new Procedimiento(nombre, paramsValor, paramsRef, head, paramsTipos, orden);
        }



        /* ------------------------ EVALUACION EXPRESIONES ------------------------ */

        public Expresion evaluarExpresionCadena(ParseTreeNode expresionCadena, ParseTreeNode masTexto)
        {
            Expresion ExpresionCadena = null;
            ExpresionCadena = this.expresionCadena(expresionCadena);
            Expresion MasTexto = null;
            MasTexto = this.masTexto(masTexto);

            if (MasTexto != null)
            {
                return new Aritmetica(ExpresionCadena, MasTexto, '+');
            }
            else
            {
                return ExpresionCadena;
            }
        }

        public Expresion evaluarExpresionNumerica(ParseTreeNode actual)
        {

            if (actual.ChildNodes.Count == 3)
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
            }
            else
            {
                Debug.WriteLine(actual.ChildNodes[0].Term.ToString());
                //Verificar el tipo y no solo poner la n porque pueden venir strings o bools
                switch (actual.ChildNodes[0].Term.ToString())
                {
                    case "DOUBLE":
                        return new Literal('D', actual.ChildNodes[0].Token.Text);

                    case "ID":
                        return new ObtenerVariable(actual.ChildNodes[0].Token.Text);
                    case "Llamada":
                        return new ObtenerLlamada(evaluarNuevaLlamada(actual.ChildNodes[0]));
                    case "Valor_Arreglo":
                        return null;
                    case "true":
                        return new Literal('T', true);
                    case "false":
                        return new Literal('F',false);
                    default:
                        // Es INT
                        return new Literal('N', actual.ChildNodes[0].Token.Text);
                }
                
            }
        }

        public Expresion evaluarExpresionLogica(ParseTreeNode actual)
        {
            int cantidad = actual.ChildNodes.Count;
            switch (cantidad)
            {
                case 3:
                    // Llevan Or o And
                    return new RelacionalMultiple(evaluarExpresionLogica(actual.ChildNodes[0]),evaluarExpresionLogica(actual.ChildNodes[2]),actual.ChildNodes[1].Token.Text);
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
            if (actual.ChildNodes.Count == 3)
            {
                //string operador = actual.ChildNodes[1].Toke
                if (actual.ChildNodes[0].Term.ToString() == "Expresion_Numerica")
                    return new Relacional(evaluarExpresionNumerica(actual.ChildNodes[0]), evaluarExpresionNumerica(actual.ChildNodes[2]), actual.ChildNodes[1].Token.Text);             
                return new Relacional(expresionCadena(actual.ChildNodes[0]), expresionCadena(actual.ChildNodes[2]), actual.ChildNodes[1].Token.Text);
                
            }
            else
            {
                //Buscar Identificador
                return new Relacional(new ObtenerVariable(actual.ChildNodes[0].Token.Text),null,"unica");
            }
        }


        /* ------------------------ Otras Evaluaciones ----------------------------- */

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
            }
            else
            {
                //Tiene 3 nodos (Expresion_Cadena Simbolo(+,-...) Expresion_Cadena
                Expresion ExpresionCadena1 = null;
                Expresion ExpresionCadena2 = null;

                ExpresionCadena1 = this.expresionCadena(expresionCadena.ChildNodes[0]);
                ExpresionCadena2 = this.expresionCadena(expresionCadena.ChildNodes[2]);

                if (ExpresionCadena1 != null && ExpresionCadena2 != null)
                {
                    return new Aritmetica(ExpresionCadena1, ExpresionCadena2, getSignoAritmetica(expresionCadena.ChildNodes[1]));
                }
                else
                {
                    return ExpresionCadena1;
                }
            }
        }

        public Expresion masTexto(ParseTreeNode actual)
        {
            if (actual.ChildNodes.Count == 0)
            {
                return null;
            }
            else
            {
                //Tiene 3 hijos (, Expresion_Cadena Mas_Texto)
                Expresion expresionCadena = this.expresionCadena(actual.ChildNodes[1]);
                Expresion masTexto = this.masTexto(actual.ChildNodes[2]);

                if (expresionCadena != null && masTexto != null)
                {
                    return new Aritmetica(expresionCadena, masTexto, '+');
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

        public Tipos getTipo(ParseTreeNode actual)
        {
            Debug.WriteLine(actual.ChildNodes[0].Term.ToString());
            switch(actual.ChildNodes[0].Term.ToString())
            {
                case "integer":
                    return Tipos.NUMBER;
                case "string":
                    return Tipos.STRING;
                case "boolean":
                    return Tipos.BOOLEAN;
                default:
                    return Tipos.NULLL;
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

                case "true":
                    return 'T';
                case "false":
                    return 'F';
            }
            return 'E';
        }












    }
}
