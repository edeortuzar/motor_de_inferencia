using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class HistoriaDetalle {

        public enum TipoHistoriaDetalle
        {
            Texto = 1,
            Inferencia = 2
        }

		private int _nCod_Historia;
		private int _nOrden;
		private int _nCod_TipoHistoriaDetalle;
        private string _sDesc_TipoDetalle;

        /*Inferencia*/
        private TuCuento.Entidades.HistoriaDetInf _Inferencia = new TuCuento.Entidades.HistoriaDetInf();

        /*Texto*/
        private TuCuento.Entidades.HistoriaDetTexto _Texto = new TuCuento.Entidades.HistoriaDetTexto();
                
		// constructor
		public HistoriaDetalle () {
		}

		public int nCod_Historia {get {return _nCod_Historia;} set {_nCod_Historia = value;}}
		public int nOrden {get {return _nOrden;} set {_nOrden = value;}}
		public int nCod_TipoHistoriaDetalle {get {return _nCod_TipoHistoriaDetalle;} set {_nCod_TipoHistoriaDetalle = value;}}

        /* Inferencia */
        public TuCuento.Entidades.HistoriaDetInf Inferencia { get { return _Inferencia; } set { _Inferencia = value; } }

        /* Texto */
        public TuCuento.Entidades.HistoriaDetTexto Texto { get { return _Texto; } set { _Texto = value; } }

        public string sDesc_TipoDetalle
        {
            get
            {
                string sRespuesta = "";
                if (!TipoHistoriaDetalle.IsDefined(typeof(TipoHistoriaDetalle), nCod_TipoHistoriaDetalle))
                    sRespuesta = "Sin definir";
                else
                {
                    TipoHistoriaDetalle oTipo = (TipoHistoriaDetalle)nCod_TipoHistoriaDetalle;
                    sRespuesta = oTipo.ToString();
                }

                return sRespuesta;

            }
        }

	}
}
