using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class TipoAccion {

		private int _nCod_TipoAccion;
		private string _sDescripcion;

		// constructor
		public TipoAccion () {
		}

		public int nCod_TipoAccion {get {return _nCod_TipoAccion;} set {_nCod_TipoAccion = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
	}
}
