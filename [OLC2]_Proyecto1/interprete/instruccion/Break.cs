﻿using _OLC2__Proyecto1.interprete.simbolo;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2__Proyecto1.interprete.instruccion
{
    class Break : Instruccion
    {
        public override object ejecutar(Entorno entorno)
        {
            return "break";
        }
    }
}