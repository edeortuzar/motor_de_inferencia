using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class Usuario {

		private string _sCod_Usuario;
		private string _sNombre;
		private string _sApellido;
		private string _sPSW;
		private int _nHab;
		private string _sEmail;
        private List<Patente> _lstPatentes = new List<Patente>();
        private List<Familia> _lstFamilias = new List<Familia>();
        private PreguntaClave _Pregunta = new PreguntaClave();
        private string _sRespuesta;

		// constructor
		public Usuario () {
		}

		public string sCod_Usuario {get {return _sCod_Usuario;} set {_sCod_Usuario = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sApellido {get {return _sApellido;} set {_sApellido = value;}}
		public string sPSW {get {return _sPSW;} set {_sPSW = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
		public string sEmail {get {return _sEmail;} set {_sEmail = value;}}
        public Boolean bHab { 
            get {
                if (this.nHab == 1)
                    return true;
                else
                    return false;
            } 
            set {
                if (value)
                    this.nHab = 1;
                else
                    this.nHab = 0;
            } 
        }

        public List<Patente> lstPatentes { get { return _lstPatentes; } set { _lstPatentes = value; } }
        public List<Familia> lstFamilias { get { return _lstFamilias; } set { _lstFamilias = value; } }

        public PreguntaClave Pregunta { get { return _Pregunta; } set { _Pregunta = value; } }
        public string sRespuesta { get { return _sRespuesta; } set { _sRespuesta = value; } }

	}
}
