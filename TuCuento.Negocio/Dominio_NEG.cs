using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio
{
    public class Dominio_NEG
    {

        public static DataTable ListarDominios()
        {
            DataTable oDominios = new DataTable();
            TuCuento.DAL.Dominio_DAL.ListarDominios(oDominios);
            return oDominios;
        }

        public static DataTable ListarDominios(int nCod_Dominio, int nHab)
        {
            DataTable dtDominios = new DataTable();
            TuCuento.DAL.Dominio_DAL.TraerDominio(nCod_Dominio, nHab, dtDominios);
            return dtDominios;
        }

        public static Boolean Persistir(TuCuento.Entidades.Dominio oDominio)
        {
            Boolean bResp = false;
            DataTable dtDominio = new DataTable();
            TuCuento.DAL.Dominio_DAL oDAL = new TuCuento.DAL.Dominio_DAL();

            TuCuento.DAL.Dominio_DAL.TraerDominio(oDominio.nCod_Dominio, -1, dtDominio);

            if (dtDominio.Rows.Count == 0)
            {
                if (oDAL.AgregarDominio(oDominio))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarDominio(oDominio))
                    bResp = true;
            }

            return bResp;
        }

        public static DataTable ValDominio(int nCod_Dominio, string sDesc_Dominio)
        {
            DataTable dtDominios = new DataTable();
            TuCuento.DAL.Dominio_DAL.ValDominio(nCod_Dominio, sDesc_Dominio, dtDominios);
            return dtDominios;
        }

    }
}
