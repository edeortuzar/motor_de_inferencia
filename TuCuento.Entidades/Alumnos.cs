using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Alumnos {

		private int _nCod_Aula;
		private string _sCod_Usuario;

		// constructor
		public Alumnos () {
		}

		public int nCod_Aula {get {return _nCod_Aula;} set {_nCod_Aula = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
	}
}
