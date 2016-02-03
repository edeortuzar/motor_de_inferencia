using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class Familia_NEG {

        public static DataTable ListarFamilias()
        {
            DataTable oFamilias = new DataTable();
            TuCuento.DAL.Familia_DAL.TraerFamilia(-1, -1, oFamilias);
            return oFamilias;
        }

        public static DataTable ListarFamilias(int nCod_Familia, int nHab)
        {
            DataTable dtFamilias = new DataTable();
            TuCuento.DAL.Familia_DAL.TraerFamilia(nCod_Familia, nHab, dtFamilias);
            return dtFamilias;
        }

        public static Boolean Persistir(TuCuento.Entidades.Familia oFamilia)
        {
            Boolean bResp = false;
            DataTable dtFamilia = new DataTable();
            TuCuento.DAL.Familia_DAL oDAL = new TuCuento.DAL.Familia_DAL();

            TuCuento.DAL.Familia_DAL.TraerFamilia(oFamilia.nCod_Flia, -1, dtFamilia);

            if (dtFamilia.Rows.Count == 0)
            {
                if (oDAL.AgregarFamilia(oFamilia))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarFamilia(oFamilia))
                    bResp = true;
            }

            return bResp;
        }

        public static DataTable TraerFamiliaPatente(int nCod_Flia)
        {
            DataTable oPatentes = new DataTable();
            TuCuento.DAL.Familia_DAL.TraerFamiliaPatente(nCod_Flia, oPatentes);
            return oPatentes;
        }

	}
}
