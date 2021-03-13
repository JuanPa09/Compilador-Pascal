using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace _OLC2__Proyecto1.reportes
{
    class Reporte
    {
        List<formato> lista;
        RichTextBox debugger;

        public Reporte(RichTextBox debugger)
        {
            this.debugger = debugger;
            lista = new List<formato>();
        }

        public void limpiarLista()
        {
            lista.Clear();
        }

        public void nuevoError(int fila, int columna, string tipo, string mensaje)
        {
            lista.Add(new formato(fila,columna,tipo,mensaje));
        }

        public void generarReporte()
        {

            string path = "C:\\compiladores2\\reporte.html";

            string reporte = "<html><title>Errores Lexicos</title><body><center><h1>Reporte De Errores</h1></center><br><br><center>";
            reporte+= "<table style=\"width: 100%\">";
            reporte += "<tr>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Tipo</th>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Descripcion</th>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Linea</th>";
            reporte += "<th style=\"border: 1px solid black; background-color:#DFC93F \">Columna</th>";
            reporte += "</tr>";
            foreach (formato error in lista)
            {
                    

                    reporte += "<tr>";
                    reporte += "<th style=\"border: 1px solid black; \">" + error.tipo + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + error.mensaje + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + error.fila + "</th>";
                    reporte += "<th style=\"border: 1px solid black; \">" + error.columna + "</th>";
                    reporte += "</tr>";
            }
            reporte += "</table></center></body></html>";


            try
            {
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(reporte);
                    fs.Write(info, 0, info.Length);
                }
                debugger.AppendText("\n Reporte Generado Con Exito!");
            }
            catch (Exception ex)
            {
                debugger.AppendText("\n " + ex.ToString());
            }


        }





    }
}
