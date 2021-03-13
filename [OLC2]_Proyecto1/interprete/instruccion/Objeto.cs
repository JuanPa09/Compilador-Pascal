using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.simbolo;
using _OLC2__Proyecto1.interprete.util;
using _OLC2__Proyecto1.reportes;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Objeto
    {
        string nombre;
        Dictionary<string, Simbolo> variables;
        Dictionary<string, Tipo> tipoVariable;
        Reporte reporte;

        public Objeto(string nombre, Dictionary<string,Simbolo> variables, Dictionary<string,Tipo> tipoVariable,Reporte reporte)
        {
            this.nombre = nombre;
            this.variables = variables;
            this.tipoVariable = tipoVariable;
            variables = new Dictionary<string, Simbolo>();
            tipoVariable = new Dictionary<string, Tipo>();
            this.reporte = reporte;

        }

        public Objeto(Objeto objeto)
        {
            this.nombre = objeto.nombre;
            variables = new Dictionary<string, Simbolo>(objeto.variables);
            this.tipoVariable = new Dictionary<string,Tipo>(objeto.tipoVariable);
        }

        public void setValor(string propiedad, Simbolo valor)
        {
            if (!variables.ContainsKey(propiedad))
                throw new ErrorPascal(0,0,"El objeto \""+nombre+"\" no contiene la propiedad "+propiedad,"",reporte);

            if (tipoVariable[propiedad].tipo != valor.tipo.tipo && tipoVariable[propiedad].tipoAuxiliar != valor.tipo.tipoAuxiliar)
                throw new ErrorPascal(0,0,"No se puede asignar el valor \""+valor.valor+"\" a \""+nombre+"."+propiedad+"\" porque los datos no coinciden","semantico",reporte);

            variables[propiedad] = valor;
        }

        public Simbolo getValor(string propiedad)
        {
            if (!variables.ContainsKey(propiedad))
                throw new ErrorPascal(0, 0, "El objeto \"" + nombre + "\" no contiene la propiedad " + propiedad, "",reporte);
            return variables[propiedad];
        }

        public void agregarPropiedad(string propiedad,Tipo tipo)
        {
            variables[propiedad] = new Simbolo(null,tipo,null);
            tipoVariable[propiedad] = tipo;
        }


    }
}
