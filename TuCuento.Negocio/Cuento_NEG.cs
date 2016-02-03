using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace TuCuento.Negocio {

	public class Cuento_NEG {

        public static bool GenerarCuento(string sSessionID, string sPath, int nCod_Dominio, string sDesc_Dominio, List<TuCuento.Entidades.HistoriaDetalle> oHistoria, List<TuCuento.Entidades.Condicion> oCondiciones, ref string sNombreArchivo)
        {
            bool bResp = true;

            try
            {
                string sIndicadorTexto = "";
                string sNombreEntidad = "";
                string sNombreAtributo = "";
                int nCod_TipoAtributo;
                int nCod_Entidad;
                int nCod_Atributo;
                string sValor = "";
                DataTable oTabla;
                sNombreArchivo = sPath + @"\Cuentos\MIC\" + sSessionID + DateTime.Now.Day.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".mic";

                #region Wrapper

                int nRegla = 0;

                /* Creo el archivo */
                StreamWriter writer = File.CreateText(sNombreArchivo);

                foreach (TuCuento.Entidades.HistoriaDetalle oDetalle in oHistoria)
                {
                    if (oDetalle.Inferencia.Condiciones.Count > 0)
                    {
                        nRegla++;
                        writer.WriteLine("");
                        writer.WriteLine("regla '" + sDesc_Dominio + "_" + nRegla.ToString() + "'");

                        //Agrego las condiciones
                        writer.WriteLine("	cuando");

                        foreach (TuCuento.Entidades.Condicion oCondicion in oDetalle.Inferencia.Condiciones)
                        {
                            sIndicadorTexto = "";
                            //Busco la entidad y el atributo
                            oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(oCondicion.nCod_Entidad, oCondicion.nCod_Atributo, -1);

                            nCod_TipoAtributo = Convert.ToInt32(oTabla.Rows[0]["nCod_TipoAtributo"].ToString());
                            
                            sNombreEntidad = oTabla.Rows[0]["sNombreEntidad"].ToString();
                            sNombreEntidad = sNombreEntidad.Replace(" ", "_");
                            sNombreAtributo = oTabla.Rows[0]["sNombre"].ToString();
                            sNombreAtributo = sNombreAtributo.Replace(" ", "_");
                            
                            if (nCod_TipoAtributo == 2) //Si es de tipo Texto va con las comillas
                                sIndicadorTexto = "'";

                            sValor = oCondicion.lstValPosible[0].sValor;

                            if (nCod_TipoAtributo == 5) //Si es booleano cambio por true o false
                                if (sValor == "VERDADERO")
                                    sValor = "true";
                                else
                                    sValor = "false";

                            writer.WriteLine("		" + sNombreEntidad + "." + sNombreAtributo + "(" + oCondicion.lstValPosible[0].sOperadorLogico.PadRight(2,Convert.ToChar(" ")) + sIndicadorTexto + sValor + sIndicadorTexto + ")");

                        }

                        //Agrego las acciones
                        writer.WriteLine("	entonces");

                        foreach (TuCuento.Entidades.Accion oAccion in oDetalle.Inferencia.Accion)
                        {
                            oTabla = TuCuento.Negocio.Accion_NEG.ListarAcciones(oAccion.nCod_Accion, -1, nCod_Dominio);

                            //Modifica un hecho
                            if (oAccion.nCod_TipoAccion == 1)
                            {
                                //Traigo los codigos de entidad y el atributo    
                                nCod_Entidad = Convert.ToInt32(oTabla.Rows[0]["nCod_Entidad"].ToString());
                                nCod_Atributo = Convert.ToInt32(oTabla.Rows[0]["nCod_Atributo"].ToString());
                                sValor = oTabla.Rows[0]["sValor"].ToString();

                                sIndicadorTexto = "";

                                //Busco la entidad y el atributo
                                oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(nCod_Entidad, nCod_Atributo, -1);

                                nCod_TipoAtributo = Convert.ToInt32(oTabla.Rows[0]["nCod_TipoAtributo"].ToString());
                                sNombreEntidad = oTabla.Rows[0]["sNombreEntidad"].ToString();
                                sNombreEntidad = sNombreEntidad.Replace(" ", "_");
                                sNombreAtributo = oTabla.Rows[0]["sNombre"].ToString();
                                sNombreAtributo = sNombreAtributo.Replace(" ", "_");

                                if (nCod_TipoAtributo == 2) //Si es de tipo Texto va con las comillas
                                    sIndicadorTexto = "'";

                                if (nCod_TipoAtributo == 5) //Si es booleano cambio por true o false
                                    if (sValor == "VERDADERO")
                                        sValor = "true";
                                    else
                                        sValor = "false";

                                writer.WriteLine("		" + sNombreEntidad + "." + sNombreAtributo + "(" + sIndicadorTexto + sValor + sIndicadorTexto + ");");
                            }

                            //Ejecuta una historia
                            if (oAccion.nCod_TipoAccion == 2)
                            {
                                DataTable oHistoriaAEjecutar = TuCuento.Negocio.Historia_NEG.ListarHistorias(Convert.ToInt32(oTabla.Rows[0]["nCod_Historia"].ToString()), -1, nCod_Dominio);
                                writer.WriteLine("		MIC.ejecuta('" + oHistoriaAEjecutar.Rows[0]["sNombre"].ToString() + "')");
                            }

                        }

                        writer.WriteLine("finregla");

                    }

                }

                writer.Close();

                #endregion

            }
            catch (Exception ex)
            {
                bResp = false;
            }

            return bResp;
        }

        public static DataTable ListarCuentos()
        {
            DataTable oCuentos = new DataTable();
            TuCuento.DAL.Cuento_DAL.ListarCuentos(oCuentos);
            return oCuentos;
        }

        public static DataTable ListarCuentos(int nCod_Cuento)
        {
            DataTable dtCuentos = new DataTable();
            TuCuento.DAL.Cuento_DAL.TraerCuento(nCod_Cuento, dtCuentos);
            return dtCuentos;
        }

        public static DataTable ListarCuentos(int nCod_Cuento, string sCod_Usuario)
        {
            DataTable dtCuentos = new DataTable();
            TuCuento.DAL.Cuento_DAL.TraerCuento(nCod_Cuento, sCod_Usuario, dtCuentos);
            return dtCuentos;
        }

        public static Boolean Persistir(TuCuento.Entidades.Cuento oCuento)
        {
            Boolean bResp = false;
            DataTable dtCuento = new DataTable();
            TuCuento.DAL.Cuento_DAL oDAL = new TuCuento.DAL.Cuento_DAL();

            if (oDAL.AgregarCuento(oCuento))
                bResp = true;
            
            return bResp;
        }

	}
}
