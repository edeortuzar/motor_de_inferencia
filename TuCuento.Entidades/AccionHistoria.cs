using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class AccionHistoria {

		private int _nCod_Accion;
		private int _nCod_Historia;

		// constructor
		public AccionHistoria () {
		}

		public int nCod_Accion {get {return _nCod_Accion;} set {_nCod_Accion = value;}}
		public int nCod_Historia {get {return _nCod_Historia;} set {_nCod_Historia = value;}}
	}
}
