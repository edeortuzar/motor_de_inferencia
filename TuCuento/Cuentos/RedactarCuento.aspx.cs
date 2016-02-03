using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;

public partial class Cuentos_RedactarCuento : System.Web.UI.Page
{
    #region Propiedades

    private string _sMensajeError;

    private List<TuCuento.Entidades.HistoriaDetalle> lstDetalleHistoria
    {
        get { return (List<TuCuento.Entidades.HistoriaDetalle>)Session["lstDetalleHistoria"]; }
        set
        {
            Session["lstDetalleHistoria"] = value;
        }
    }

    private List<Inferencia> lstInferencia
    {
        get { return (List<Inferencia>)Session["lstInferencia"]; }
        set
        {
            Session["lstInferencia"] = value;
        }
    }

    string nl = Environment.NewLine;

    private string sMensajeError
    {
        get { return _sMensajeError; }
        set
        {
            if (_sMensajeError == "")
            {
                _sMensajeError = value;
            }
            else
            {
                _sMensajeError = _sMensajeError + nl + value;
            }
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MostrarPanel(1);
            HfnCod_Dominio.Value = Request.QueryString["nCod_Dominio"].ToString();
            HfsDesc_Dominio.Value = Request.QueryString["sDesc_Dominio"].ToString();
            CargarGrilla();
        }
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        if (Validar())
        {
            txtTexto.Text = "";

            #region Declaración Variables
            List<TuCuento.Entidades.Condicion> oCondiciones = new List<TuCuento.Entidades.Condicion>();
            DropDownList ddlVPosible;
            string sNombreArchivo = "";
            DataTable oTabla;
            int nCod_EntidadAnt = 0;
            int nCod_Hecho = 0;
            int nCod_TipoAtributo = 0;
            int nCod_Dominio = Convert.ToInt32(HfnCod_Dominio.Value);
            int nCod_Entidad = 0;
            int nCod_Atributo = 0;
            int nCod_Historia = 0;
            string sValor;
            int nValor = 0;
            string sOperador = "";
            DateTime dValor;
            bool bEsta;
            XmlDocument xmlDocTerminos = new XmlDocument();
            XmlDocument xmlDocSalida = new XmlDocument();
            bool bHistoriaPrincipal = true;
            string sAccion;
            MICServicio.MICServicio oServicio = new MICServicio.MICServicio();
            List<MICServicio.Termino> oTermino = new List<MICServicio.Termino>();
            List<MICServicio.Hecho> olstHecho = new List<MICServicio.Hecho>();
            MICServicio.Hecho oHecho = new MICServicio.Hecho();
            MICServicio.Termino unTermino = new MICServicio.Termino();
            string[] sRespuesta;
            int[] nCod_HistoriaPrincipal;
            int nPos = 0;
            int nMaxVector = 200;
            
            #endregion

            #region Recorro la grilla para cargar la lista de condiciones (entidad y atributo) y el valor seleccionado (valores posibles)
            foreach (GridViewRow grdRow in gvCondiciones.Rows)
            {
                TuCuento.Entidades.Condicion oCond = new TuCuento.Entidades.Condicion();
                TuCuento.Entidades.CondicionValPosible oValPos = new TuCuento.Entidades.CondicionValPosible();
                
                oCond.nCod_Entidad = Convert.ToInt32(Server.HtmlDecode(grdRow.Cells[0].Text.Trim()));
                oCond.nCod_Atributo = Convert.ToInt32(Server.HtmlDecode(grdRow.Cells[1].Text.Trim()));

                oCond.lstValPosible = new List<TuCuento.Entidades.CondicionValPosible>();

                //Obtengo el combo
                ddlVPosible = (DropDownList)(gvCondiciones.Rows[grdRow.RowIndex].Cells[1].FindControl("ddlValPosible"));

                oValPos.nCod_ValPosible = Convert.ToInt32(ddlVPosible.SelectedValue);
                
                sValor = ddlVPosible.SelectedItem.Text.Substring(2);
                sOperador = ddlVPosible.SelectedItem.Text.Substring(0, 2);

                //Para asignar el valor veo si es el == sino que tipo de datos es y sobore eso asigno el valor
                if (sOperador == "==")
                    oValPos.sValor = sValor;
                else
                {
                    oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(oCond.nCod_Entidad, oCond.nCod_Atributo, -1);

                    nCod_TipoAtributo = Convert.ToInt32(oTabla.Rows[0]["nCod_TipoAtributo"].ToString());

                    //Si es texto y no es igual a ==, entonces es !=
                    if ((Funciones.TipoDato)nCod_TipoAtributo == Funciones.TipoDato.Texto)
                    {
                        //Agrego el mismo texto pero sin la primer letra
                        oValPos.sValor = sValor.Substring(1);
                    }

                    //Si es número
                    if ((Funciones.TipoDato)nCod_TipoAtributo == Funciones.TipoDato.Número)
                    {
                        nValor = Convert.ToInt32(sValor);
                        
                        switch (sOperador.Trim())
                        {
                            case ">":
                                nValor = nValor + 2;
                                break;
                            case ">=":
                            case "<=":
                                break;
                            case "!=":
                            case "<":
                                nValor = nValor - 2;
                                break;
                        }

                        oValPos.sValor = nValor.ToString();
                    }

                    //Si es fecha
                    if ((Funciones.TipoDato)nCod_TipoAtributo == Funciones.TipoDato.Fecha)
                    {
                        dValor = new DateTime(Convert.ToInt32(sValor.Substring(6, 4)), Convert.ToInt32(sValor.Substring(3, 2)), Convert.ToInt32(sValor.Substring(0, 2)));

                        switch (sOperador.Trim())
                        {
                            case ">":
                                dValor = dValor.AddDays(2);
                                break;
                            case ">=":
                            case "<=":
                                break;
                            case "!=":
                            case "<":
                                dValor = dValor.AddDays(-2);
                                break;
                        }

                        oValPos.sValor = dValor.Day.ToString().PadLeft(2, Convert.ToChar("0")) + "/" + dValor.Month.ToString().PadLeft(2, Convert.ToChar("0")) + "/" + dValor.Year.ToString().PadLeft(4, Convert.ToChar("0"));
                    }

                }
                oCond.lstValPosible.Add(oValPos);

                oCondiciones.Add(oCond);

            }
            #endregion

            #region Completo las entidades y atributos a utilizar

            //Traigo todas las entidades y atributos que son usados en las reglas y completo la lista
            oTabla = TuCuento.Negocio.Condicion_NEG.ListarCondiciones(-1, 1, nCod_Dominio);
            foreach (DataRow oFila in oTabla.Rows)
            {
                nCod_Entidad = Convert.ToInt32(oFila["nCod_Entidad"].ToString());
                nCod_Atributo = Convert.ToInt32(oFila["nCod_Atributo"].ToString());

                bEsta = false;

                //veo que si no esta en la lista
                foreach (TuCuento.Entidades.Condicion oCond in oCondiciones)
                {
                    if (oCond.nCod_Entidad == nCod_Entidad && oCond.nCod_Atributo == nCod_Atributo)
                    {
                        bEsta = true;
                        break;
                    }
                }

                //Si no esta lo agrego a la lista
                if (!bEsta)
                {
                    TuCuento.Entidades.Condicion oCondicionNueva = new TuCuento.Entidades.Condicion();
                    TuCuento.Entidades.CondicionValPosible oValPos = new TuCuento.Entidades.CondicionValPosible();

                    oCondicionNueva.nCod_Entidad = nCod_Entidad;
                    oCondicionNueva.nCod_Atributo = nCod_Atributo;

                    oValPos.sValor = "";

                    //oCondicionNueva.lstValPosible = new List<TuCuento.Entidades.CondicionValPosible>();
                    //oCondicionNueva.lstValPosible.Add(oValPos);

                    oCondiciones.Add(oCondicionNueva);
                }

            }

            #endregion

            //Genero el archivo y llamo al motor de inferencia
            if (TuCuento.Negocio.Cuento_NEG.GenerarCuento(Session.SessionID, Request.PhysicalApplicationPath, nCod_Dominio, HfsDesc_Dominio.Value, lstDetalleHistoria, oCondiciones, ref sNombreArchivo))
            {
                #region Armo la lista de terminos y hechos

                foreach (TuCuento.Entidades.Condicion oCondicion in oCondiciones)
                {

                    if (nCod_EntidadAnt != oCondicion.nCod_Entidad)
                    {
                        if (nCod_EntidadAnt != 0) //Si no es la primera vez que pasa
                        {
                            unTermino.Hecho = olstHecho.ToArray();
                            oTermino.Add(unTermino);
                            unTermino = new MICServicio.Termino();
                            olstHecho = new List<MICServicio.Hecho>();
                        }

                        //Termino-Hechos
                        unTermino.nCodTermino = oCondicion.nCod_Entidad;
                        //Traigo el nombre de la entidad
                        oTabla = TuCuento.Negocio.Entidad_NEG.ListarEntidades(unTermino.nCodTermino, -1);
                        unTermino.sDescTermino = oTabla.Rows[0]["sNombre"].ToString();
                        unTermino.sDescTermino = unTermino.sDescTermino.Replace(" ", "_");
                        nCod_EntidadAnt = oCondicion.nCod_Entidad;
                        nCod_Hecho = 0;
                    }

                    oHecho = new MICServicio.Hecho();

                    nCod_Hecho++;

                    //Traigo los datos del atributo
                    oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(oCondicion.nCod_Entidad, oCondicion.nCod_Atributo, -1);

                    oHecho.nCodHecho = nCod_Hecho;
                    oHecho.sDescHecho = oTabla.Rows[0]["sNombre"].ToString();
                    oHecho.sDescHecho = oHecho.sDescHecho.Replace(" ", "_");
                    oHecho.nCodTipoDato = Convert.ToInt32(oTabla.Rows[0]["nCod_TipoAtributo"].ToString());

                    if (oCondicion.lstValPosible != null)
                    {
                        if (oHecho.nCodTipoDato != 5)
                            oHecho.sValorInicial = oCondicion.lstValPosible[0].sValor;
                        else
                        {
                            if (oCondicion.lstValPosible[0].sValor == "VERDADERO")
                                oHecho.sValorInicial = "true";
                            else
                                oHecho.sValorInicial = "false";
                        }
                    }

                    oHecho.nModificado = 0;

                    olstHecho.Add(oHecho);

                }


                //Agrego el último que paso
                unTermino.Hecho = olstHecho.ToArray();
                oTermino.Add(unTermino);

                #endregion

                #region Levanto el archivo

                FileStream reglaStream = new FileStream(sNombreArchivo, FileMode.Open, FileAccess.Read);
                //Stream reglaStream = UploadFile.PostedFile.InputStream;
                //int reglaLen = UploadFile.PostedFile.ContentLength;
                long reglaLen = reglaStream.Length;
                //string reglaContentType = UploadFile.PostedFile.ContentType;
                BinaryReader reglaBinaria = new BinaryReader(reglaStream);
                
                //byte[] reglaBinaryData = new byte[reglaLen];
                byte[] reglaBinaryData = reglaBinaria.ReadBytes(Convert.ToInt32(reglaLen));

                reglaBinaria.Close();
                //int n = reglaStream.Read(reglaBinaryData,0,reglaLen);
                
                #endregion

                sRespuesta = oServicio.EjecutarRegla(reglaBinaryData, oTermino.ToArray());

                if (sRespuesta[0] == "Ok")
                {
                    #region Proceso el resultado de la regla

                    xmlDocTerminos.LoadXml("<ROOT>" + sRespuesta[1] + "</ROOT>");
                    xmlDocSalida.LoadXml("<ROOT>" + sRespuesta[2] + "</ROOT>");

                    nCod_HistoriaPrincipal = new int[nMaxVector];

                    //Pongo el resultado en el textbox a partir de la lista lstDetalleHistoria
                    for (int nI=0; nI < lstDetalleHistoria.Count;nI++)
                    {
                        bHistoriaPrincipal = true;

                        TuCuento.Entidades.HistoriaDetalle oDetalle = lstDetalleHistoria[nI];

                        //Busco la historia principal, la que no es disparada por ninguna acción
                        foreach (TuCuento.Entidades.HistoriaDetalle oHistoria in lstDetalleHistoria)
                        {
                            
                            foreach (TuCuento.Entidades.Accion oAccion in oHistoria.Inferencia.Accion)
                            {
                                if (oAccion.Historia != null)
                                {
                                    if (oDetalle.nCod_Historia == oAccion.Historia.nCod_Historia)
                                    {
                                        //busco si esta en la lista y lo quito

                                        for (int nII = 0; nII < nPos; nII++)
                                        {
                                            if (nCod_HistoriaPrincipal[nII] == oDetalle.nCod_Historia)
                                                nCod_HistoriaPrincipal[nII] = 0;
                                        }

                                        bHistoriaPrincipal = false;
                                        break;
                                    }
                                }
                            }
                            if (!bHistoriaPrincipal)
                                break;
                    
                        }

                        if (bHistoriaPrincipal)
                        {
                            bool bEstaHistoria = false;
                            
                            for (int nII = 0; nII < nPos; nII++)
                            {
                                if (nCod_HistoriaPrincipal[nII] == oDetalle.nCod_Historia)
                                {
                                    bEstaHistoria = true;
                                    break;
                                }
                            }

                            if (!bEstaHistoria)
                            {
                                //La agrego a la lista de historias a generar el texto
                                nCod_HistoriaPrincipal[nPos] = oDetalle.nCod_Historia;
                                nPos++;
                            }
                        }

                    }

                    //Agrego el texto de la historia al textbox
                    for (int nII = 0; nII < nPos; nII++)
                    {
                        if (nCod_HistoriaPrincipal[nII] != 0)
                            AgregarTexto(nCod_HistoriaPrincipal[nII]);
                    }
                    

                    System.Xml.XmlNodeList oLstAcciones = xmlDocSalida.SelectNodes("./ROOT/Accion");

                    foreach (System.Xml.XmlNode unaAccion in oLstAcciones)
                    {
                        //Obtengo el nombre de la historia a ejecutar
                        sAccion = unaAccion.Attributes["sAccion"].InnerText;

                        sAccion = sAccion.Replace("MIC.ejecuta('", "").Replace("')", "");

                        //Busco el código de la historia
                        oTabla = TuCuento.Negocio.Historia_NEG.ListarHistorias(-1, -1, sAccion, nCod_Dominio);

                        nCod_Historia = Convert.ToInt32(oTabla.Rows[0]["nCod_Historia"].ToString());

                        //Recoorro la lista y agrego el texto
                        foreach (TuCuento.Entidades.HistoriaDetalle oDetalle in lstDetalleHistoria)
                        {
                            if (oDetalle.nCod_Historia == nCod_Historia)
                            {
                                AgregarTexto(nCod_Historia);
                                break;
                            }
                        }

                    }

                    MostrarPanel(2);

                    #endregion

                }
                else
                {
                    Master.MensajeError = "Se produjo un error en la inferencia. Por favor, intente más tardes.";
                }
            }

        }
    }

    protected void gvCondiciones_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 0)
            e.Row.Cells[0].Visible = false;

        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

    }

    protected void btnAtras_Click(object sender, EventArgs e)
    {
        Response.Redirect("Redaccion.aspx", false);
    }

    protected void btnAtrasCuento_Click(object sender, EventArgs e)
    {
        MostrarPanel(1);
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        //Grabar el resultado
        TuCuento.Entidades.Cuento oCuento = new TuCuento.Entidades.Cuento();
        TuCuento.Entidades.Usuario oUnUsuario = new TuCuento.Entidades.Usuario();
        oUnUsuario = (TuCuento.Entidades.Usuario)Session["oUsuarioInternet"];

        oCuento.nCod_Dominio = Convert.ToInt32(HfnCod_Dominio.Value);
        oCuento.sCod_Usuario = oUnUsuario.sCod_Usuario;
        oCuento.sTexto = txtTexto.Text;

        if (TuCuento.Negocio.Cuento_NEG.Persistir(oCuento))
        {
            Response.Redirect("Redaccion.aspx?nResultado=1",false);
        }
        else
        {
            Master.MensajeError = "Ocurrio un error al grabar el cuento, por favor, intente mas tarde.";
        }

    }

    #endregion

    #region Metodos

    private void AgregarTexto(int nCod_Historia)
    {
        foreach (TuCuento.Entidades.HistoriaDetalle oDetalle in lstDetalleHistoria)
        {
            if (oDetalle.nCod_Historia == nCod_Historia)
            {
                if (oDetalle.Texto.sTexto != null)
                    txtTexto.Text = txtTexto.Text + oDetalle.Texto.sTexto;
            }
        }
    }

    private void MostrarPanel(int nPaso)
    {
        switch (nPaso)
        {
            case 1:
                txtTexto.ReadOnly = false;
                PanelCuento.Visible = false;
                PanelRedaccion.Visible = true;
                break;
            case 2:
                txtTexto.ReadOnly = true;
                PanelCuento.Visible = true;
                PanelRedaccion.Visible = false;
                break;
        }
    }

    private void CargarGrilla()
    {
        //Traigo las historias que componen el dominio
        DataTable oTabla = TuCuento.Negocio.Historia_NEG.ListarHistorias(-1, 1, Convert.ToInt32(HfnCod_Dominio.Value));
        DropDownList ddlVPosible;

        lstInferencia = new List<Inferencia>();
        lstDetalleHistoria = new List<TuCuento.Entidades.HistoriaDetalle>();

        foreach (DataRow oHistoria in oTabla.Rows)
        {
            CargarListaDetalle(Convert.ToInt32(oHistoria["nCod_Historia"].ToString()));
        }

        //Cargo la grilla
        gvCondiciones.DataSource = lstInferencia;
        gvCondiciones.DataBind();
        
        //Recorro la grilla para cargar el combo
        foreach (GridViewRow grdRow in gvCondiciones.Rows)
        {
            //Obtengo el combo
            ddlVPosible = (DropDownList)(gvCondiciones.Rows[grdRow.RowIndex].Cells[1].FindControl("ddlValPosible"));

            //Cargo el combo
            ddlVPosible.DataSource = lstInferencia[grdRow.RowIndex].lstValPosible;
            ddlVPosible.DataTextField = "ValorPosible";
            ddlVPosible.DataValueField = "nCod_ValPosible";
            ddlVPosible.DataBind();

            ddlVPosible.Items.Add(new ListItem("Elija", "0"));
            
            Funciones.OrdenarCombo(ddlVPosible);

            ddlVPosible.SelectedValue = "0";
        }

    }

    private void CargarListaDetalle(int nCod_Historia)
    {
        DataTable oTabla = TuCuento.Negocio.HistoriaDetalle_NEG.ListarDetalleHistoria(nCod_Historia);
        List<TuCuento.Entidades.EntidadAtributo> lstEntidadNoMostrar = new List<TuCuento.Entidades.EntidadAtributo>();
        
        foreach (DataRow oFila in oTabla.Rows)
        {
            TuCuento.Entidades.HistoriaDetalle oDetalle = new TuCuento.Entidades.HistoriaDetalle();

            oDetalle.nCod_Historia = Convert.ToInt32(oFila["nCod_Historia"].ToString());
            oDetalle.nCod_TipoHistoriaDetalle = Convert.ToInt32(oFila["nCod_TipoHistoriaDetalle"].ToString());
            oDetalle.nOrden = Convert.ToInt32(oFila["nOrden"].ToString());

            Funciones.TipoHistoriaDetalle oTipo = (Funciones.TipoHistoriaDetalle)oDetalle.nCod_TipoHistoriaDetalle;

            if (Funciones.TipoHistoriaDetalle.Texto == oTipo)
            {
                DataTable oTexto = TuCuento.Negocio.HistoriaDetalle_NEG.TraerTexto(oDetalle.nCod_Historia, oDetalle.nOrden);

                oDetalle.Texto.sTexto = oTexto.Rows[0]["sTexto"].ToString();

            }
            else
            {
                TuCuento.Entidades.HistoriaDetInf oInferencia = new TuCuento.Entidades.HistoriaDetInf();
                //Traigo las condiciones
                DataTable oTablaInferencia = TuCuento.Negocio.HistoriaDetalle_NEG.TraerCondiciones(nCod_Historia, oDetalle.nOrden);

                foreach (DataRow oFilaInf in oTablaInferencia.Rows)
                {
                    TuCuento.Entidades.Condicion oCondicion = new TuCuento.Entidades.Condicion();
                    TuCuento.Entidades.CondicionValPosible oValPosible = new TuCuento.Entidades.CondicionValPosible();

                    oCondicion.nCod_Condicion = Convert.ToInt32(oFilaInf["nCod_Condicion"].ToString());
                    oCondicion.nCod_Entidad = Convert.ToInt32(oFilaInf["nCod_Entidad"].ToString());
                    oCondicion.nCod_Atributo = Convert.ToInt32(oFilaInf["nCod_Atributo"].ToString());

                    bool bEstaValPosible = false;
                    bool bEstaEntidadAtributo = false;

                    foreach (Inferencia oDato in lstInferencia)
                    {
                        if (oDato.nCod_Entidad == oCondicion.nCod_Entidad && oDato.nCod_Atributo == oCondicion.nCod_Atributo)
                        {
                            bEstaEntidadAtributo = true;
                            foreach (ValPosible oDatoVal in oDato.lstValPosible)
                            {
                                if (oDatoVal.nCod_ValPosible == Convert.ToInt32(oFilaInf["nCod_ValPosible"].ToString()))
                                {
                                    bEstaValPosible = true;
                                    break;
                                }
                            }
                            if (!bEstaValPosible)
                            {
                                ValPosible oCValPos = new ValPosible();
                                oCValPos.nCod_ValPosible = Convert.ToInt32(oFilaInf["nCod_ValPosible"].ToString());
                                oCValPos.sOperadorLogico = oFilaInf["sOperadorLogico"].ToString();
                                oCValPos.sValor = oFilaInf["sValor"].ToString();

                                if (oDato.lstValPosible == null)
                                {
                                    List<ValPosible> oValPos = new List<ValPosible>();
                                    oValPos.Add(oCValPos);
                                    oDato.lstValPosible = oValPos;
                                }
                                else
                                {
                                    oDato.lstValPosible.Add(oCValPos);
                                }
                                
                            }
                        }
                        if (bEstaEntidadAtributo)
                            break;
                    }

                    
                    if (!bEstaEntidadAtributo)
                    {
                        Inferencia oInf = new Inferencia(oCondicion.nCod_Entidad, oCondicion.nCod_Atributo, oFilaInf["sAtributo"].ToString());
                        List<ValPosible> oValPos = new List<ValPosible>();

                        ValPosible oCValPos = new ValPosible();
                        oCValPos.nCod_ValPosible = Convert.ToInt32(oFilaInf["nCod_ValPosible"].ToString());
                        oCValPos.sOperadorLogico = oFilaInf["sOperadorLogico"].ToString();
                        oCValPos.sValor = oFilaInf["sValor"].ToString();
                        oValPos.Add(oCValPos);
                        oInf.lstValPosible = oValPos;

                        lstInferencia.Add(oInf);
                    }
                                    
                    oValPosible.nCod_ValPosible = Convert.ToInt32(oFilaInf["nCod_ValPosible"].ToString());
                    oValPosible.sOperadorLogico = oFilaInf["sOperadorLogico"].ToString();
                    oValPosible.sValor = oFilaInf["sValor"].ToString();

                    oCondicion.lstValPosible = new List<TuCuento.Entidades.CondicionValPosible>();
                    oCondicion.lstValPosible.Add(oValPosible);

                    oInferencia.Condiciones.Add(oCondicion);
                }

                //Traigo las acciones
                oTablaInferencia = TuCuento.Negocio.HistoriaDetalle_NEG.TraerAccion(nCod_Historia, oDetalle.nOrden);

                foreach (DataRow oFilaInf in oTablaInferencia.Rows)
                {
                    TuCuento.Entidades.Accion oAccion = new TuCuento.Entidades.Accion();
                    oAccion.nCod_Accion = Convert.ToInt32(oFilaInf["nCod_Accion"].ToString());
                    oAccion.nCod_TipoAccion = Convert.ToInt32(oFilaInf["nCod_TipoAccion"].ToString());

                    //Si modifica un hecho busco cual es el hecho y lo agrego a la lista de hechos que no tengo que mostrar
                    if ((Funciones.TipoAccion)oAccion.nCod_TipoAccion == Funciones.TipoAccion.ModHecho)
                    {
                        DataTable oAccionModHecho = TuCuento.Negocio.Accion_NEG.ListarAcciones(oAccion.nCod_Accion, -1, -1);

                        TuCuento.Entidades.EntidadAtributo oEntAtrr = new TuCuento.Entidades.EntidadAtributo();
                        oEntAtrr.nCod_Entidad = Convert.ToInt32(oAccionModHecho.Rows[0]["nCod_Entidad"].ToString());
                        oEntAtrr.nCod_Atributo = Convert.ToInt32(oAccionModHecho.Rows[0]["nCod_Atributo"].ToString());

                        lstEntidadNoMostrar.Add(oEntAtrr);
                    }
                    else
                    {
                        oAccion.Historia = new TuCuento.Entidades.AccionHistoria();
                        oAccion.Historia.nCod_Historia = Convert.ToInt32(oFilaInf["nCod_HistoriaAccion"].ToString());
                    }

                    oInferencia.Accion.Add(oAccion);
                }

                oDetalle.Inferencia = oInferencia;

            }

            lstDetalleHistoria.Add(oDetalle);
        }

        //Quito las entidades y atributos que van a ser inferidos
        foreach (TuCuento.Entidades.EntidadAtributo oEnt in lstEntidadNoMostrar) 
        {
            foreach (Inferencia oInf in lstInferencia)
            {
                if (oEnt.nCod_Entidad == oInf.nCod_Entidad && oEnt.nCod_Atributo == oInf.nCod_Atributo)
                {
                    lstInferencia.Remove(oInf);
                    break;
                }
            }
        }

    }

    private bool Validar()
    {
        bool bResp = true;

        //Recorro los combos para ver que en todos haya seleccionado una opción
        DropDownList ddlVPosible;
        
        foreach (GridViewRow grdRow in gvCondiciones.Rows)
        {
            //Obtengo el combo
            ddlVPosible = (DropDownList)(gvCondiciones.Rows[grdRow.RowIndex].Cells[1].FindControl("ddlValPosible"));

            if (ddlVPosible.SelectedValue == "0")
            {
                sMensajeError = "Debe seleccionar un valor posible para cada condición.";
                bResp = false;
                break;
            }
            
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    #endregion

   
}
