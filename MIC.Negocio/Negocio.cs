using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MIC.Negocio
{
    public class Negocio
    {
        private XmlDocument doc;
        private XmlNode docNode;
        private XmlNode RootNode;

        public string EjecutarRegla(byte[] Regla, MIC.Entidades.Termino[] oTerminos, ref string sXMLTerminos, ref string sXMLSalida)
        {
            string sXMLResultante;
            int nCodSesion = 0;
            MIC.DAL.Datos oDatos = new MIC.DAL.Datos();
            bool bResp;
            string sResp;
            string sCopeteXML = @"<?xml version=""1.0"" encoding=""UTF-8""?>";
           
            //Traigo el número de sesión
            nCodSesion = oDatos.NumerodeSesion();

            //Armo el XML final
            sXMLResultante = ProcesoXML(Regla, oTerminos, nCodSesion);

            sXMLResultante = sXMLResultante.Replace(sCopeteXML,"");

            //Ejecutar la regla
            bResp = oDatos.EjecutarRegla(sXMLResultante, ref sXMLTerminos, ref sXMLSalida);

            if (bResp)
                sResp = "Ok";
            else
                sResp = "ERROR";
                 
            return sResp;
        }

        private void ProcesarArchivo(byte[] Regla, int nCodSesion)
        {
            //XmlDocument doc = new XmlDocument();
            
            MemoryStream memStream = new MemoryStream(Regla);
            try
            {
                StreamReader fic = new StreamReader(memStream);
                string linea = null;

                linea = fic.ReadLine();
                bool bCondicion = false;
                bool bAccion = false;
                bool bEntro = false;
                int nCodRegla = 0;
                string sValor;
                int nCodCondicion = 0;
                int nCondicionDetalle = 0;
                int nCodAccion = 0;
                int nCodTipoAccion = 0;

                while ((linea != null))
                {
                    bEntro = true;

                    #region Regla
                    if (linea.Contains("regla "))
                    {
                        nCodRegla++;
                        bCondicion = false;
                        bAccion = false;

                        //NODO miConocimiento
                        XmlNode oNode = doc.CreateElement("miConocimiento");
                        XmlAttribute oAttribute = doc.CreateAttribute("nCodSesion");
                        oAttribute.Value = nCodSesion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodRegla");
                        oAttribute.Value = nCodRegla.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("sNombreRegla");
                        sValor = linea.Replace("\"", "");
                        sValor = sValor.Replace("regla ", "");
                        oAttribute.Value = sValor;
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodEstado");
                        oAttribute.Value = "0";
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nOrden");
                        oAttribute.Value = nCodRegla.ToString();
                        oNode.Attributes.Append(oAttribute);

                        RootNode.AppendChild(oNode);
                    }
                    #endregion

                    #region Condiciones
                    if (linea.Trim().Equals("cuando"))
                    {
                        bCondicion = true;
                        bAccion = false;
                        bEntro = false;

                        nCodCondicion++;

                        XmlNode oNode = doc.CreateElement("miCondicion");
                        XmlAttribute oAttribute = doc.CreateAttribute("nCodSesion");
                        oAttribute.Value = nCodSesion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodRegla");
                        oAttribute.Value = nCodRegla.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodCondicion");
                        oAttribute.Value = nCodCondicion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodEstado");
                        oAttribute.Value = "0";
                        oNode.Attributes.Append(oAttribute);

                        RootNode.AppendChild(oNode);

                    }
                    #endregion

                    #region Acciones
                    if (linea.Trim().Equals("entonces"))
                    {
                        bCondicion = false;
                        bAccion = true;
                        bEntro = false;

                    }
                    #endregion

                    #region Fin Regla
                    if (linea.Trim().Equals("finregla"))
                    {
                        bCondicion = false;
                        bAccion = false;
                        nCodCondicion = 0;
                        nCondicionDetalle = 0;
                        nCodAccion = 0;
                    }
                    #endregion

                    #region Condiciones

                    if (bCondicion && bEntro)
                    {
                        nCondicionDetalle++;

                        XmlNode oNode = doc.CreateElement("miCondicionDetalle");
                        XmlAttribute oAttribute = doc.CreateAttribute("nCodSesion");
                        oAttribute.Value = nCodSesion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodRegla");
                        oAttribute.Value = nCodRegla.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodCondicion");
                        oAttribute.Value = nCodCondicion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodEstado");
                        oAttribute.Value = "0";
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nDetalle");
                        oAttribute.Value = nCondicionDetalle.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("sCondicion");
                        oAttribute.Value = linea.Trim();
                        oNode.Attributes.Append(oAttribute);

                        RootNode.AppendChild(oNode);
                    }

                    #endregion

                    #region Acciones

                    if (bAccion && bEntro)
                    {
                        nCodAccion++;

                        XmlNode oNode = doc.CreateElement("miAccion");
                        XmlAttribute oAttribute = doc.CreateAttribute("nCodAccion");
                        oAttribute.Value = nCodAccion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodSesion");
                        oAttribute.Value = nCodSesion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodRegla");
                        oAttribute.Value = nCodRegla.ToString();
                        oNode.Attributes.Append(oAttribute);

                        oAttribute = doc.CreateAttribute("nCodCondicion");
                        oAttribute.Value = nCodCondicion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        sValor = linea.Trim().Replace(";", "");

                        oAttribute = doc.CreateAttribute("sAccion");
                        oAttribute.Value = sValor;
                        oNode.Attributes.Append(oAttribute);

                        if (sValor.Contains("MIC."))
                            nCodTipoAccion = 2;
                        else
                            nCodTipoAccion = 1;

                        oAttribute = doc.CreateAttribute("nCodTipoAccion");
                        oAttribute.Value = nCodTipoAccion.ToString();
                        oNode.Attributes.Append(oAttribute);

                        RootNode.AppendChild(oNode);

                    }

                    #endregion

                    linea = fic.ReadLine();
                }
            }
            catch (Exception ex)
            {
                string sError = ex.Message;
            }
            finally
            {
                memStream.Close();
                memStream.Dispose();
            }

        }

        private string ProcesoXML(byte[] Regla, MIC.Entidades.Termino[] oTerminos, int nCodSesion)
        {
            string sXML = "";

            doc = new XmlDocument();
            docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            RootNode = doc.CreateElement("ROOT");
            doc.AppendChild(RootNode);

            //NODO miSesion
            XmlNode oNode = doc.CreateElement("miSesion");
            XmlAttribute oAttribute = doc.CreateAttribute("nCodSesion");
            oAttribute.Value = nCodSesion.ToString();
            oNode.Attributes.Append(oAttribute);
            RootNode.AppendChild(oNode);

            //Proceso el archivo e incorporo el resultado
            ProcesarArchivo(Regla, nCodSesion);

            //NODO miTermino
            for (int nI = 0; nI < oTerminos.Length; nI++)
            {
                oNode = doc.CreateElement("miTermino");
                oAttribute = doc.CreateAttribute("nCodSesion");
                oAttribute.Value = nCodSesion.ToString();
                oNode.Attributes.Append(oAttribute);

                oAttribute = doc.CreateAttribute("nCodTermino");
                oAttribute.Value = oTerminos[nI].nCodTermino.ToString();
                oNode.Attributes.Append(oAttribute);

                oAttribute = doc.CreateAttribute("sDescTermino");
                oAttribute.Value = oTerminos[nI].sDescTermino;
                oNode.Attributes.Append(oAttribute);

                RootNode.AppendChild(oNode);

                //Por cada termino busco sus atributos (hechos)
                for (int nH = 0; nH < oTerminos[nI].Hecho.Length; nH++)
                {
                    oNode = doc.CreateElement("miHecho");
                    oAttribute = doc.CreateAttribute("nCodSesion");
                    oAttribute.Value = nCodSesion.ToString();
                    oNode.Attributes.Append(oAttribute);

                    oAttribute = doc.CreateAttribute("nCodTermino");
                    oAttribute.Value = oTerminos[nI].nCodTermino.ToString();
                    oNode.Attributes.Append(oAttribute);

                    oAttribute = doc.CreateAttribute("nCodHecho");
                    oAttribute.Value = oTerminos[nI].Hecho[nH].nCodHecho.ToString();
                    oNode.Attributes.Append(oAttribute);

                    oAttribute = doc.CreateAttribute("sDescHecho");
                    oAttribute.Value = oTerminos[nI].Hecho[nH].sDescHecho;
                    oNode.Attributes.Append(oAttribute);

                    oAttribute = doc.CreateAttribute("nCodTipoDato");
                    oAttribute.Value = oTerminos[nI].Hecho[nH].nCodTipoDato.ToString();
                    oNode.Attributes.Append(oAttribute);

                    oAttribute = doc.CreateAttribute("nModificado");
                    oAttribute.Value = oTerminos[nI].Hecho[nH].nModificado.ToString();
                    oNode.Attributes.Append(oAttribute);

                    if (oTerminos[nI].Hecho[nH].sValorInicial != null)
                    {
                        oAttribute = doc.CreateAttribute("sValorInicial");
                        oAttribute.Value = oTerminos[nI].Hecho[nH].sValorInicial;
                        oNode.Attributes.Append(oAttribute);
                    }

                    RootNode.AppendChild(oNode);
                }

            }
            

            sXML = doc.InnerXml;

            return sXML;
        }

    }
}
