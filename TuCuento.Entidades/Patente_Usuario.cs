using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Patente_Usuario {

		private int _nCod_Patente;
		private string _sCod_Usuario;

		// constructor
		public Patente_Usuario () {
		}

		public int nCod_Patente {get {return _nCod_Patente;} set {_nCod_Patente = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
	}
}
