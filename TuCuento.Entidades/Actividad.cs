using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Actividad {

		private int _nCod_Actividad;
		private string _sNombre;
		private string _sDescripcion;

		// constructor
		public Actividad () {
		}

		public int nCod_Actividad {get {return _nCod_Actividad;} set {_nCod_Actividad = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
	}
}
