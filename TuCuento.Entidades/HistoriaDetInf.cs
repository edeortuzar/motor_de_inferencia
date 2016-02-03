using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class HistoriaDetInf {

		private List<TuCuento.Entidades.Condicion> _Condiciones = new List<Condicion>();
		private List<TuCuento.Entidades.Accion> _Accion = new List<Accion>();

		// constructor
		public HistoriaDetInf () {
		}

        public List<TuCuento.Entidades.Condicion> Condiciones { get { return _Condiciones; } set { _Condiciones = value; } }
        public List<TuCuento.Entidades.Accion> Accion { get { return _Accion; } set { _Accion = value; } }
	}
}
