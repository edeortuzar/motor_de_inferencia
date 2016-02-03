using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class HistoriaDetalle_NEG {

        public static DataTable ListarDetalleHistoria(int nCod_Historia)
        {
            DataTable oDetalle = new DataTable();
            TuCuento.DAL.HistoriaDetalle_DAL.ListarDetalleHistoria(nCod_Historia, oDetalle);
            return oDetalle;
        }

        public static DataTable TraerTexto(int nCod_Historia, int nOrden)
        {
            DataTable dtTexto = new DataTable();
            TuCuento.DAL.HistoriaDetalle_DAL.TraerTexto(nCod_Historia, nOrden, dtTexto);
            return dtTexto;
        }

        public static DataTable TraerCondiciones(int nCod_Historia, int nOrden)
        {
            DataTable dtTabla = new DataTable();
            TuCuento.DAL.HistoriaDetalle_DAL.TraerCondiciones(nCod_Historia, nOrden, dtTabla);
            return dtTabla;
        }

        public static DataTable TraerAccion(int nCod_Historia, int nOrden)
        {
            DataTable dtTabla = new DataTable();
            TuCuento.DAL.HistoriaDetalle_DAL.TraerAccion(nCod_Historia, nOrden, dtTabla);
            return dtTabla;
        }

	}
}
