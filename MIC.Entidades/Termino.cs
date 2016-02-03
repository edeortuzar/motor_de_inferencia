using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIC.Entidades
{
    public class Termino
    {

        public Sesion Sesion { get; set; }
        public int nCodTermino { get; set; }
        public string sDescTermino { get; set; }
        public Hecho[] Hecho { get; set; }

    }
}
