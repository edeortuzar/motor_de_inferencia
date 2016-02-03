using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades
{

    public class PreguntaClave
    {

        private int _nCod_Pregunta;
        private string _sPregunta;
        private int _nHab;

        // constructor
        public PreguntaClave()
        {
        }

        public int nCod_Pregunta { get { return _nCod_Pregunta; } set { _nCod_Pregunta = value; } }
        public string sPregunta { get { return _sPregunta; } set { _sPregunta = value; } }
        public int nHab { get { return _nHab; } set { _nHab = value; } }
    }
}