using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Productos : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rdSimple.Checked = true;
            rdAvanzada.Checked = false;
            pnlAvanzada.Visible = false;
            pnlSimple.Visible = true;

            Funciones.LlenarGrillaVacia(gvDatos);

        }
    }

    protected void rdSimple_CheckedChanged(object sender, EventArgs e)
    {
        pnlAvanzada.Visible = false;
        pnlSimple.Visible = true;
    }
    protected void rdAvanzada_CheckedChanged(object sender, EventArgs e)
    {
        pnlAvanzada.Visible = true;
        pnlSimple.Visible = false;
    }

    #endregion
    
}
