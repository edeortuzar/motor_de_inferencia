using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Entidad {

		private int _nCod_Entidad;
		private string _sNombre;
		private string _sDescripcion;
		private int _nHab;
        private List<EntidadAtributo> _Atributos;

		// constructor
		public Entidad () {
		}

		public int nCod_Entidad {get {return _nCod_Entidad;} set {_nCod_Entidad = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
        public List<EntidadAtributo> Atributos {get {return _Atributos;} set {_Atributos = value;}}
	}
}
