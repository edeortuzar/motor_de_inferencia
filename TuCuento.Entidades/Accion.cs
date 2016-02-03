using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Accion {

		private int _nCod_Accion;
		private string _sNombre;
		private string _sDescripcion;
		private int _nHab;
		private int _nCod_TipoAccion;
        private TuCuento.Entidades.AccionHecho _Hecho;
        private TuCuento.Entidades.AccionHistoria _Historia;
        private int _nCod_Dominio;

		// constructor
		public Accion () {
		}

		public int nCod_Accion {get {return _nCod_Accion;} set {_nCod_Accion = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
		public int nCod_TipoAccion {get {return _nCod_TipoAccion;} set {_nCod_TipoAccion = value;}}
        public TuCuento.Entidades.AccionHecho Hecho { get { return _Hecho; } set { _Hecho = value; } }
        public TuCuento.Entidades.AccionHistoria Historia { get { return _Historia; } set { _Historia = value; } }
        public int nCod_Dominio { get { return _nCod_Dominio; } set { _nCod_Dominio = value; } }

	}
}
