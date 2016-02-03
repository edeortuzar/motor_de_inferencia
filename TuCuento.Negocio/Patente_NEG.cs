using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace TuCuento.Negocio {

	public class Patente_NEG {

        public static DataTable ListarPatentes()
        {
            DataTable oPatentes = new DataTable();
            TuCuento.DAL.Patente_DAL.TraerPatente(-1,-1,oPatentes);
            return oPatentes;
        }

        public static DataTable ListarPatentes(int nCod_Patente, int nHab)
        {
            DataTable dtPatentes = new DataTable();
            TuCuento.DAL.Patente_DAL.TraerPatente(nCod_Patente, nHab, dtPatentes);
            return dtPatentes;
        }

        public static Boolean Persistir(TuCuento.Entidades.Patente oPatente)
        {
            Boolean bResp = false;
            DataTable dtPatente = new DataTable();
            TuCuento.DAL.Patente_DAL oDAL = new TuCuento.DAL.Patente_DAL();

            TuCuento.DAL.Patente_DAL.TraerPatente(oPatente.nCod_Patente, -1, dtPatente);

            if (dtPatente.Rows.Count == 0)
            {
                if (oDAL.AgregarPatente(oPatente))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarPatente(oPatente))
                    bResp = true;
            }

            return bResp;
        }

	}
}
