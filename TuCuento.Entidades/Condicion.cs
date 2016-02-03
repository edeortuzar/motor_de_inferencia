using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Condicion {

		private int _nCod_Condicion;
		private string _sNombre;
		private string _sDescripcion;
		private int _nHab;
		private int _nCod_Entidad;
		private int _nCod_Atributo;
        private List<TuCuento.Entidades.CondicionValPosible> _lstValPosible;
        private int _nCod_Dominio;

		// constructor
		public Condicion () {
		}

		public int nCod_Condicion {get {return _nCod_Condicion;} set {_nCod_Condicion = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
		public int nCod_Entidad {get {return _nCod_Entidad;} set {_nCod_Entidad = value;}}
		public int nCod_Atributo {get {return _nCod_Atributo;} set {_nCod_Atributo = value;}}
        public List<TuCuento.Entidades.CondicionValPosible> lstValPosible { get { return _lstValPosible; } set { _lstValPosible = value; } }
        public int nCod_Dominio { get { return _nCod_Dominio; } set { _nCod_Dominio = value; } }

	}
}
