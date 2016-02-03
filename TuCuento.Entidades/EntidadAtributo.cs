using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class EntidadAtributo {

        public enum TipoAtributo
        {
            Elija = 0,
            Entidad = 1,
            Texto = 2,
            Número = 3,
            Fecha = 4,
            Booleano = 5
        }

		private int _nCod_Entidad;
		private int _nCod_Atributo;
		private string _sNombre;
		private string _sDescripcion;
		private int _nHab;
		private int _nCod_TipoAtributo;
		private TuCuento.Entidades.Entidad _TipoEntidad;
        
		// constructor
		public EntidadAtributo () {
		}

		public int nCod_Entidad {get {return _nCod_Entidad;} set {_nCod_Entidad = value;}}
		public int nCod_Atributo {get {return _nCod_Atributo;} set {_nCod_Atributo = value;}}
		public string sNombre {get {return _sNombre;} set {_sNombre = value;}}
		public string sDescripcion {get {return _sDescripcion;} set {_sDescripcion = value;}}
		public int nHab {get {return _nHab;} set {_nHab = value;}}
		public int nCod_TipoAtributo {get {return _nCod_TipoAtributo;} set {_nCod_TipoAtributo = value;}}
        public TuCuento.Entidades.Entidad TipoEntidad { get { return _TipoEntidad; } set { _TipoEntidad = value; } }
        public string sDesc_TipoAtributo
        {
            get
            {
                string sRespuesta = "";
                if (!TipoAtributo.IsDefined(typeof(TipoAtributo), nCod_TipoAtributo))
                    sRespuesta = "Sin definir";
                else
                {
                    TipoAtributo oTipo = (TipoAtributo)nCod_TipoAtributo;
                    sRespuesta = oTipo.ToString();
                }

                return sRespuesta;

            }
        }


	}
}
