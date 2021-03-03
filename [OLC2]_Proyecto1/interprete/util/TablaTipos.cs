using System;
using System.Collections.Generic;
using System.Text;
using _OLC2__Proyecto1.interprete.simbolo;

namespace _OLC2__Proyecto1.interprete.util
{
    class TablaTipos
    {
        public static Tipos[,] tipos = new Tipos[7,7]
        {
            { Tipos.NUMBER,Tipos.STRING,Tipos.NUMBER,Tipos.NULLL,Tipos.NULLL,Tipos.NULLL,Tipos.DOUBLE},
            { Tipos.STRING,Tipos.STRING,Tipos.STRING,Tipos.STRING,Tipos.STRING,Tipos.STRING,Tipos.STRING},
            { Tipos.NUMBER,Tipos.STRING,Tipos.BOOLEAN,Tipos.NULLL,Tipos.NULLL,Tipos.NULLL,Tipos.DOUBLE},
            { Tipos.NULLL,Tipos.STRING,Tipos.NULLL,Tipos.NULLL,Tipos.NULLL,Tipos.NULLL,Tipos.NULLL},
            { Tipos.NULLL,Tipos.STRING,Tipos.NULLL,Tipos.NULLL,Tipos.ARRAY,Tipos.NULLL,Tipos.NULLL},
            { Tipos.NULLL,Tipos.STRING,Tipos.NULLL,Tipos.NULLL,Tipos.NULLL,Tipos.OBJECT,Tipos.NULLL},
            { Tipos.DOUBLE,Tipos.STRING,Tipos.DOUBLE,Tipos.NULLL,Tipos.NULLL,Tipos.NULLL,Tipos.DOUBLE}
        };

        public static Tipos getTipo(Tipo izquierda,Tipo derecha)
        {
            return tipos[(int)izquierda.tipo,(int)derecha.tipo];
        }

    }
}
