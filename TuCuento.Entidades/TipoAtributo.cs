using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class TipoAtributo {

		private int _nCod_TipoAtributo;
		private string _sNombre;

		// constructor
		public TipoAtributo () {
		}

		public int nCod_TipoAtributo {get {return _nCod_TipoAtributo;} set {_nCod_TipoAtributo = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
	}
}
