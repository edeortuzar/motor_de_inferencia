using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

public partial class Reportes_ReporteFinanciero : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable oDatos = (DataTable)Session["DatosBitacora"];

        string sFechaDesde = Request.QueryString["dFechaDesde"].ToString();
        string sFechaHasta = Request.QueryString["dFechaHasta"].ToString();
        lblFechaDesde.Text = sFechaDesde.Substring(0, 2) + "/" + sFechaDesde.Substring(2, 2) + "/" + sFechaDesde.Substring(4);
        lblFechaHasta.Text = sFechaHasta.Substring(0, 2) + "/" + sFechaHasta.Substring(2, 2) + "/" + sFechaHasta.Substring(4);
        lblUsuario.Text = Request.QueryString["sUsuario"].ToString();
        lblPatente.Text = Request.QueryString["sPatente"].ToString();

        gvDatos.DataSource = oDatos;
        gvDatos.DataBind();

    }

}
