using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Denuncia {

		private int _nCod_Cuento;
		private int _nComentario;
		private int _nCod_Denuncia;
		private string _sCod_Usuario;
		private string _dFechaDenuncia;
		private string _sObservacion;

		// constructor
		public Denuncia () {
		}

		public int nCod_Cuento {get {return _nCod_Cuento;} set {_nCod_Cuento = value;}}
		public int nComentario {get {return _nComentario;} set {_nComentario = value;}}
		public int nCod_Denuncia {get {return _nCod_Denuncia;} set {_nCod_Denuncia = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
		public string dFechaDenuncia {get {return _dFechaDenuncia;} set {_dFechaDenuncia = value;}}
		public string sObservacion {get {return _sObservacion;} set {_sObservacion = value;}}
	}
}
