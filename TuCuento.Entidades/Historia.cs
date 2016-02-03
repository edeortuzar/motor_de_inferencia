using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Historia {

		private int _nCod_Historia;
		private string _sNombre;
		private string _sDescripcion;
		private int _nHab;
        private List<TuCuento.Entidades.HistoriaDetalle> _Detalle;
        private int _nCod_Dominio;

		// constructor
		public Historia () {
		}

		public int nCod_Historia {get {return _nCod_Historia;} set {_nCod_Historia = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
        public List<TuCuento.Entidades.HistoriaDetalle> Detalle { get { return _Detalle; } set { _Detalle = value; } }
        public int nCod_Dominio { get { return _nCod_Dominio; } set { _nCod_Dominio = value; } }
	}
}
