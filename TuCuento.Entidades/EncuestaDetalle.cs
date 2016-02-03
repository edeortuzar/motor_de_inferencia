using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades
{

    public class EncuestaDetalle
    {

        private int _nId;
        private int _nIdRespuesta;
        private string _sRespuesta;

        // constructor
        public EncuestaDetalle()
        {
        }

        public int nId { get { return _nId; } set { _nId = value; } }
        public int nIdRespuesta { get { return _nIdRespuesta; } set { _nIdRespuesta = value; } }
        public string sRespuesta { get { return _sRespuesta; } set { _sRespuesta = value; } }
    }
}
