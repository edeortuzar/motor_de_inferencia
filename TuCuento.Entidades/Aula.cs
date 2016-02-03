using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Aula {

		private int _nCod_Aula;
		private string _sNombre;
		private string _sDescripcion;
		private string _sCod_Usuario;

		// constructor
		public Aula () {
		}

		public int nCod_Aula {get {return _nCod_Aula;} set {_nCod_Aula = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
	}
}
