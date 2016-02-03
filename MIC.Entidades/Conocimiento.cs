using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIC.Entidades
{
    public class Conocimiento
    {

        public Sesion Sesion { get; set; }
        public int nCodRegla { get; set; }
        public string sDescRegla { get; set; }
        public int nCodEstado { get; set; }
        public int nOrden { get; set; }

    }
}
