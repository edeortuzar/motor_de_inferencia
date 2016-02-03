using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIC.Entidades
{
    public class Condicion
    {
        public Conocimiento Conocimiento { get; set; }
        public int nCodCondicion { get; set; }
        public int nCodEstado { get; set; }
        public CondicionDetalle[] CondicionDetalle { get; set; }
        public Accion[] Accion { get; set; }
    }
}
