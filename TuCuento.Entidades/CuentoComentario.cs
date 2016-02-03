using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class CuentoComentario {

		private int _nComentario;
		private string _sCod_Usuario;
		private int _nCod_Cuento;
		private string _sComentario;
		private string _dFecha;

		// constructor
		public CuentoComentario () {
		}

		public int nComentario {get {return _nComentario;} set {_nComentario = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
		public int nCod_Cuento {get {return _nCod_Cuento;} set {_nCod_Cuento = value;}}
		public string sComentario {get {return _sComentario;} set {_sComentario = value;}}
		public string dFecha {get {return _dFecha;} set {_dFecha = value;}}
	}
}
