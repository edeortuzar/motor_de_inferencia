using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Bitacora {

		private int _nRegistro;
		private string _dFecha;
		private string _sCod_Usuario;
		private int _nCod_Cliente;
		private int _nCod_Patente;

		// constructor
		public Bitacora () {
		}

		public int nRegistro {get {return _nRegistro;} set {_nRegistro = value;}}
		public string dFecha {get {return _dFecha;} set {_dFecha = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
		public int nCod_Cliente {get {return _nCod_Cliente;} set {_nCod_Cliente = value;}}
		public int nCod_Patente {get {return _nCod_Patente;} set {_nCod_Patente = value;}}
	}
}
