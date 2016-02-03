using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class Historia_NEG {

        public static DataTable ListarHistorias()
        {
            DataTable oHistorias = new DataTable();
            TuCuento.DAL.Historia_DAL.ListarHistorias(oHistorias);
            return oHistorias;
        }

        public static DataTable ListarHistorias(int nCod_Historia, int nHab, int nCod_Dominio)
        {
            DataTable dtHistorias = new DataTable();
            TuCuento.DAL.Historia_DAL.TraerHistoria(nCod_Historia, nHab, nCod_Dominio, dtHistorias);
            return dtHistorias;
        }

        public static DataTable ListarHistorias(int nCod_Historia, int nHab, string sNombre, int nCod_Dominio)
        {
            DataTable dtHistorias = new DataTable();
            TuCuento.DAL.Historia_DAL.TraerHistoria(nCod_Historia, nHab, nCod_Dominio, sNombre, dtHistorias);
            return dtHistorias;
        }

        public static bool Persistir(TuCuento.Entidades.Historia oHistoria)
        {
            Boolean bResp = false;
            DataTable dtHistoria = new DataTable();
            TuCuento.DAL.Historia_DAL oDAL = new TuCuento.DAL.Historia_DAL();

            TuCuento.DAL.Historia_DAL.TraerHistoria(oHistoria.nCod_Historia, -1, -1, dtHistoria);

            if (dtHistoria.Rows.Count == 0)
            {
                if (oDAL.AgregarHistoria(oHistoria))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarHistoria(oHistoria))
                    bResp = true;
            }

            return bResp;
        }

        public static DataTable ValHistoria(int nCod_Historia, string sNombre, int nCod_Dominio)
        {
            DataTable dtHistoria = new DataTable();
            TuCuento.DAL.Historia_DAL.ValHistoria(nCod_Historia, sNombre, nCod_Dominio, dtHistoria);
            return dtHistoria;
        }

	}
}
