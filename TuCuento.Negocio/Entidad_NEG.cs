using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class Entidad_NEG {

        public static DataTable ListarEntidades()
        {
            DataTable oEntidades = new DataTable();
            TuCuento.DAL.Entidad_DAL.ListarEntidades(oEntidades);
            return oEntidades;
        }

        public static DataTable ListarEntidades(int nCod_Entidad, int nHab)
        {
            DataTable dtEntidades = new DataTable();
            TuCuento.DAL.Entidad_DAL.TraerEntidad(nCod_Entidad, nHab, dtEntidades);
            return dtEntidades;
        }

        public static DataTable TraerAtributos(int nCod_Entidad, int nCod_Atributo, int nHab)
        {
            DataTable oAtributos = new DataTable();
            TuCuento.DAL.Entidad_DAL.TraerAtributos(nCod_Entidad, nCod_Atributo, nHab, oAtributos);
            return oAtributos;
        }

        public static Boolean Persistir(TuCuento.Entidades.Entidad oEntidad)
        {
            Boolean bResp = false;
            DataTable dtEntidad = new DataTable();
            TuCuento.DAL.Entidad_DAL oDAL = new TuCuento.DAL.Entidad_DAL();

            TuCuento.DAL.Entidad_DAL.TraerEntidad(oEntidad.nCod_Entidad, -1, dtEntidad);

            if (dtEntidad.Rows.Count == 0)
            {
                if (oDAL.AgregarEntidad(oEntidad))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarEntidad(oEntidad))
                    bResp = true;
            }

            return bResp;
        }

        public static DataTable ValEntidad(int nCod_Entidad, string sNombre)
        {
            DataTable dtEntidad = new DataTable();
            TuCuento.DAL.Entidad_DAL.ValEntidad(nCod_Entidad, sNombre, dtEntidad);
            return dtEntidad;
        }
	}
}
