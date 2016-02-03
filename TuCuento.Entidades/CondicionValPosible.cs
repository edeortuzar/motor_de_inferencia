using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class CondicionValPosible {

		private int _nCod_ValPosible;
		private string _sValor;
		private string _sOperadorLogico;

		// constructor
		public CondicionValPosible () {
		}

		public int nCod_ValPosible {get {return _nCod_ValPosible;} set {_nCod_ValPosible = value;}}
		public string sValor {get {return _sValor;} set {_sValor = value;}}
		public string sOperadorLogico {get {return _sOperadorLogico;} set {_sOperadorLogico = value;}}
	}
}
