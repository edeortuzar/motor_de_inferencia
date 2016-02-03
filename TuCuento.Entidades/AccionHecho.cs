using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class AccionHecho {

		private int _nCod_Accion;
		private int _nCod_Entidad;
		private int _nCod_Atributo;
		private int _nValor;
		private string _sValor;

		// constructor
		public AccionHecho () {
		}

		public int nCod_Accion {get {return _nCod_Accion;} set {_nCod_Accion = value;}}
		public int nCod_Entidad {get {return _nCod_Entidad;} set {_nCod_Entidad = value;}}
		public int nCod_Atributo {get {return _nCod_Atributo;} set {_nCod_Atributo = value;}}
		public int nValor {get {return _nValor;} set {_nValor = value;}}
		public string sValor {get {return _sValor;} set {_sValor = value;}}
	}
}
