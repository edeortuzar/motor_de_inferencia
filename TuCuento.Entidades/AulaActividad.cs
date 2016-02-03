using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class AulaActividad {

		private int _nCod_Aula;
		private string _sCod_Usuario;
		private int _nCod_Actividad;
		private string _dFechaFin;
		private int _nNota;
		private string _sObservacion;

		// constructor
		public AulaActividad () {
		}

		public int nCod_Aula {get {return _nCod_Aula;} set {_nCod_Aula = value;}}
		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
		public int nCod_Actividad {get {return _nCod_Actividad;} set {_nCod_Actividad = value;}}
		public string dFechaFin {get {return _dFechaFin;} set {_dFechaFin = value;}}
		public int nNota {get {return _nNota;} set {_nNota = value;}}
		public string sObservacion {get {return _sObservacion;} set {_sObservacion = value;}}
	}
}
