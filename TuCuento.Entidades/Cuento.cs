using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Cuento {

		private int _nCod_Cuento;
		private string _sCod_Usuario;
        private string _sTexto;
        private int _nCod_Dominio;

		// constructor
		public Cuento () {
		}

		public int nCod_Cuento {get {return _nCod_Cuento;} set {_nCod_Cuento = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
        public string sTexto { get { return _sTexto; } set { _sTexto = value; } }
        public int nCod_Dominio { get { return _nCod_Dominio; } set { _nCod_Dominio = value; } }
	}
}
