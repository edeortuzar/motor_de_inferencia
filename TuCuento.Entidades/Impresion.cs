using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Impresion {

		private int _nCod_Impresion;
		private string _dFechaSolicitud;
		private string _dFechaFinEstimada;
		private string _dFechaFin;
		private string _sCod_Usuario;
		private int _nCod_Cuento;

		// constructor
		public Impresion () {
		}

		public int nCod_Impresion {get {return _nCod_Impresion;} set {_nCod_Impresion = value;}}
		public string dFechaSolicitud {get {return _dFechaSolicitud;} set {_dFechaSolicitud = value;}}
		public string dFechaFinEstimada {get {return _dFechaFinEstimada;} set {_dFechaFinEstimada = value;}}
		public string dFechaFin {get {return _dFechaFin;} set {_dFechaFin = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
		public int nCod_Cuento {get {return _nCod_Cuento;} set {_nCod_Cuento = value;}}
	}
}
