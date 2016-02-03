using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class CuentoPermiso {

		private int _nCod_Cuento;
		private string _sCod_Usuario;

		// constructor
		public CuentoPermiso () {
		}

		public int nCod_Cuento {get {return _nCod_Cuento;} set {_nCod_Cuento = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
	}
}
