using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Patente {

		private int _nCod_Patente;
		private string _sDesc_Patente;
		private int _nHab;
		private int _nNodo;
		private int _nNodo_Padre;
		private string _sUrl;
		private string _sParametro;
		private string _sToolTip;

		// constructor
		public Patente () {
		}

		public int nCod_Patente {get {return _nCod_Patente;} set {_nCod_Patente = value;}}
		public string sDesc_Patente {get {return _sDesc_Patente;} set {_sDesc_Patente = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
		public int nNodo {get {return _nNodo;} set {_nNodo = value;}}
		public int nNodo_Padre {get {return _nNodo_Padre;} set {_nNodo_Padre = value;}}
		public string sUrl {get {return _sUrl;} set {_sUrl = value;}}
		public string sParametro {get {return _sParametro;} set {_sParametro = value;}}
		public string sToolTip {get {return _sToolTip;} set {_sToolTip = value;}}
	}
}
