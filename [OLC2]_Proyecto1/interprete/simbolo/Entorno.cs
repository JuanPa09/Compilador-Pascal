using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.instruccion;
using System.Diagnostics;
using _OLC2__Proyecto1.reportes;
using System.IO;
using System.Globalization;

namespace _OLC2__Proyecto1.interprete.simbolo
{
    class Entorno
    {
        string nombre;
        public Dictionary<string, Simbolo> variables /*= new Dictionary<string,Simbolo>()*/;
        public Dictionary<string, Simbolo> constantes;
        public Dictionary<string, Funcion> funciones;
        public Dictionary<string, Procedimiento> procedimiento;
        public Dictionary<string, Tipos> tipoArreglo;
        public Dictionary<string, Simbolo> types;
        Dictionary<string, object> structs;

        Dictionary<string, int> linea;
        Dictionary<string, int> columna;

        public Entorno padre;
        public Reporte reporte;

        int noReporte = 1; //Para llevar el control de los reportes de simbolos


        public Entorno(string nombreEntorno,Entorno padre,Reporte reporte)
        {
            this.nombre = nombreEntorno;
            this.padre = padre;
            this.variables = new Dictionary<string, Simbolo>();
            this.constantes = new Dictionary<string, Simbolo>();
            this.funciones = new Dictionary<string, Funcion>();
            this.procedimiento = new Dictionary<string, Procedimiento>();
            this.tipoArreglo = new Dictionary<string, Tipos>();
            this.types = new Dictionary<string, Simbolo>();
            this.linea = new Dictionary<string, int>();
            this.columna = new Dictionary<string, int>();
            this.reporte = reporte;

        }

        public void declararType(string id, Simbolo variable,int linea,int columna)
        {
            if (types.Count == 0 || !types.ContainsKey(id))
            {
                this.types.Add(id, variable);
                this.columna.Add(id, columna);
                this.linea.Add(id,linea);
                this.reporte.nuevoSimbolo(id, variable.tipo.tipo.ToString(), this.nombre, linea, columna);

            }
            else
            {
                throw new util.ErrorPascal(0, 0, "El type \"" + id + "\" ya existe en este ambito", "semántico",reporte);
            }
        }


        public Simbolo obtenerType(string id)
        {
            Entorno actual = this;
            while (actual != null)
            {
                if (actual.types.ContainsKey(id))
                    return actual.types[id];
                actual = actual.padre;              //Busca de padre en padre
            }
            throw new util.ErrorPascal(0, 0, "No se puede obtener el valor de la variable \"" + id + "\" porque no esta declarada", "Semantico",reporte);
        }


        public void declararVariables(string id, Simbolo variable,int linea,int columna)
        {
            if (variables.Count==0 || !variables.ContainsKey(id))
            {
                this.variables.Add(id, variable);
                this.linea.Add(id,linea);
                this.columna.Add(id,columna);
                this.reporte.nuevoSimbolo(id, variable.tipo.tipo.ToString(), this.nombre, linea, columna);
            }
            else
            {
                throw new util.ErrorPascal(0,0,"La variable \"" + id + "\" ya existe en este ambito","semántico",reporte);
            }
        }

        public void declararConstante(string id, Simbolo variable,int linea,int columna)
        {
            if (constantes.Count == 0 || !constantes.ContainsKey(id))
            {
                this.constantes.Add(id, variable);
                this.linea.Add(id, linea);
                this.columna.Add(id,columna);
                this.reporte.nuevoSimbolo(id, variable.tipo.tipo.ToString(), this.nombre, linea, columna);
            }
            else
            {
                throw new util.ErrorPascal(0, 0, "La constante \"" + id + "\" ya existe en este ambito", "semántico",reporte);
            }
        }

        public Simbolo obtenerVariable(string id)
        {
            Entorno actual = this;
            while(actual!=null)
            {
                if(actual.variables.ContainsKey(id))
                    return actual.variables[id];
                actual = actual.padre;              //Busca de padre en padre
            }
            throw new util.ErrorPascal(0,0,"No se puede obtener el valor de la variable \"" + id + "\" porque no esta declarada","Semantico",reporte);
        }

        public Simbolo obtenerConstane(string id)
        {
            Entorno actual = this;
            while (actual != null)
            {
                if (actual.constantes.ContainsKey(id))
                    return actual.constantes[id];
                actual = actual.padre;              //Busca de padre en padre
            }
            throw new util.ErrorPascal(0, 0, "No se puede obtener el valor de la variable \"" + id + "\" porque no esta declarada", "Semantico",reporte);
        }

        public object modificarVariable(string id,object valor,Tipos tipo,string idVariable)
        {

            /*Entorno actual = this;
            if (actual.variables.ContainsKey(id))
            {
                Simbolo simbolo = actual.variables[id];
                if (simbolo.tipo.tipo != tipo)
                    throw new util.ErrorPascal(0,0,"No se pudo asignar a la variable \""+id.ToString()+"\" el valor \""+valor.ToString()+"\" porque los datos no coinciden","semántico",reporte);
                if (simbolo.tipo.tipo == Tipos.ARRAY)
                    actual.tipoArreglo.Add(id,getTipoArray(idVariable));
                simbolo.valor = valor;
                actual.variables[id] = simbolo;
                if (id == nombre) // Si es un retorno de funcion
                    return new Simbolo(valor, new Tipo(tipo,null), null);
                return null;
            }
            throw new util.ErrorPascal(0, 0, "La variable \"" + id + "\" no ha sido declarada", "semántico",reporte);*/

            Entorno actual = this;

            while (actual!=null)
            {
                if (actual.variables.ContainsKey(id))
                {
                    Simbolo simbolo = actual.variables[id];
                    if (simbolo.tipo.tipo != tipo)
                        throw new util.ErrorPascal(0, 0, "No se pudo asignar a la variable \"" + id.ToString() + "\" el valor \"" + valor.ToString() + "\" porque los datos no coinciden", "semántico", reporte);
                    /*if (simbolo.tipo.tipo == Tipos.ARRAY)
                        actual.tipoArreglo.Add(id, getTipoArray(idVariable));*/
                    simbolo.valor = valor;
                    actual.variables[id] = simbolo;
                    if (id == nombre) // Si es un retorno de funcion
                        return new Simbolo(valor, new Tipo(tipo, null), null);
                    return null;
                }
                actual = actual.padre;
            }

            throw new util.ErrorPascal(0, 0, "La variable \"" + id + "\" no ha sido declarada", "semántico", reporte);


        }

        public Entorno buscarEntornoVariable(string id)
        {
            Entorno actual = this;
            while (actual!=null)
            {
                if (actual.variables.ContainsKey(id))
                    return actual;
                actual = actual.padre;
            }
            throw new util.ErrorPascal(0, 0, "La variable \"" + id + "\" no ha sido declarada", "semántico",reporte);
        }


        public void declararFuncion(string nombre, Funcion funcion, int linea, int columna)
        {
            Debug.WriteLine("Nombre Entorno -> "+ this.nombre.ToString() + " Funcion -> "+funcion.nombre);
            this.funciones.Add(nombre, funcion);
            this.linea.Add(nombre,linea);
            this.columna.Add(nombre,columna);
            this.reporte.nuevoSimbolo(nombre, "Funcion", this.nombre, linea, columna);
        }

        public Funcion existeFuncion(string id)
        {
            Entorno actual = this;
            Debug.WriteLine("Nombre Entorno -> "+actual.nombre.ToString());
            while (actual != null)
            {
                if (actual.funciones.ContainsKey(id))
                    return actual.funciones[id];
                actual = actual.padre;
            }
            return null;
        }



        public void declararProcedimiento(string nombre, Procedimiento procedimiento,int linea,int columna)
        {
            Debug.WriteLine("Nombre Entorno -> " + this.nombre.ToString() + " Funcion -> " + procedimiento.nombre);
            this.linea.Add(nombre, linea);
            this.columna.Add(nombre,columna);
            this.procedimiento.Add(nombre, procedimiento);
            this.reporte.nuevoSimbolo(nombre, "Procedimiento", this.nombre, linea, columna);
        }

        public Procedimiento existeProcedimiento(string id)
        {
            Entorno actual = this;
            Debug.WriteLine("Nombre Entorno -> " + actual.nombre.ToString());
            while (actual != null)
            {
                if (actual.procedimiento.ContainsKey(id))
                    return actual.procedimiento[id];
                actual = actual.padre;
            }
            return null;
        }

        public Tipos getTipoArray(string id)
        {
            Entorno actual = this;
            while (actual != null)
            {
                if (actual.tipoArreglo.ContainsKey(id))
                    return actual.tipoArreglo[id];
                actual = actual.padre;
            }
            return Tipos.NULLL;
        }



        public bool existeVariable(string id)
        {
            Entorno actual = this;
            //while (actual != null)
            //{
                if (actual.variables.ContainsKey(id))
                    return true;
                //actual = actual.padre;
            //}
            return false;
        }


        public bool existeConstante(string id)
        {
            Entorno actual = this;
            /*while (actual != null)
            {*/
                if (actual.constantes.ContainsKey(id))
                    return true;
                //actual = actual.padre;
            /*}*/
            return false;
        }


        public Simbolo existeLaVariable(string id)
        {
            Entorno actual = this;
            while (actual != null)
            {
                if (actual.variables.ContainsKey(id))
                    return actual.variables[id];
                actual = actual.padre;              //Busca de padre en padre
            }
            return null;
        }


        public Tipos buscarTipoArreglo(string nombre)
        {
            Entorno actual = this;

            while (actual != null)
            {
                if (actual.tipoArreglo.ContainsKey(nombre))
                    return actual.tipoArreglo[nombre];
                actual = actual.padre;
            }
            throw new util.ErrorPascal(0,0,"No se pudo encontrar el arreglo","semantico",null);

        }



        public void graficarSimbolos()
        {
            

            Entorno actual = this;

            string path = "C:\\compiladores2\\"+this.noReporte+"_"+this.nombre+"_graficar_Ts().html";

            string reporte = "<html><title>Errores Lexicos</title><body><center><h1>Reporte De Errores</h1></center><br><br><center>";
            reporte += "<table style=\"width: 100%\">";
            reporte += "<tr>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Nombre</th>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Tipo</th>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Ambito</th>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Fila</th>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Columna</th>";
            reporte += "</tr>";

            while (actual != null)
            {
                foreach(KeyValuePair<string,Simbolo> value in variables)
                {
                    reporte += "<tr>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Key + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Value.tipo.tipo.ToString() + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.nombre + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.linea[value.Key] + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.columna[value.Key] + "</th>";
                    reporte += "</tr>";
                }

                foreach (KeyValuePair<string, Simbolo> value in constantes)
                {
                    reporte += "<tr>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Key + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Value.tipo.tipo.ToString() + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.nombre + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.linea[value.Key] + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.columna[value.Key] + "</th>";
                    reporte += "</tr>";
                }

                foreach (KeyValuePair<string, Simbolo> value in types)
                {
                    reporte += "<tr>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Key + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Value.tipo.tipo.ToString() + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.nombre + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.linea[value.Key] + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.columna[value.Key] + "</th>";
                    reporte += "</tr>";
                }

                foreach (KeyValuePair<string, Funcion> value in funciones)
                {
                    reporte += "<tr>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Key + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \"> Funcion </th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.nombre + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.linea[value.Key] + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.columna[value.Key] + "</th>";
                    reporte += "</tr>";
                }

                foreach (KeyValuePair<string, Procedimiento> value in procedimiento)
                {
                    reporte += "<tr>";
                    reporte += "<th style=\"border: 1px solid black; \">" + value.Key + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \"> Procedimiento </th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.nombre + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.linea[value.Key] + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + actual.columna[value.Key] + "</th>";
                    reporte += "</tr>";
                }

                actual = actual.padre;
            }

            reporte += "</table></center></body></html>";

            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(reporte);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
            }

        }



       

        


    }
}
