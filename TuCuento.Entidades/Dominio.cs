using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades
{
    public class Dominio
    {

        private int _nCod_Dominio;
		private string _sDescripcion;
        private int _nHab;

		// constructor
        public Dominio()
        {
		}

        public int nCod_Dominio { get { return _nCod_Dominio; } set { _nCod_Dominio = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public int nHab { get { return _nHab; } set { _nHab = value; } }

    }
}
