using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class TipoHistoriaDetalle {

		private int _nCod_TipoHistoriaDetalle;
		private string _sDescripcion;

		// constructor
		public TipoHistoriaDetalle () {
		}

		public int nCod_TipoHistoriaDetalle {get {return _nCod_TipoHistoriaDetalle;} set {_nCod_TipoHistoriaDetalle = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
	}
}
