using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Familia {

		private int _nCod_Flia;
		private string _sDesc_Flia;
		private int _nHab;
        private List<TuCuento.Entidades.Patente> _lstPatentes;

		// constructor
		public Familia () {
		}

		public int nCod_Flia {get {return _nCod_Flia;} set {_nCod_Flia = value;}}
		public string sDesc_Flia {get {return _sDesc_Flia;} set {_sDesc_Flia = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
        public List<TuCuento.Entidades.Patente> lstPatentes { get { return _lstPatentes; } set { _lstPatentes = value; } }

	}
}
