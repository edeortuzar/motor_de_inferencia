using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;

/// <summary>
/// Estan las funciones genericas del proyecto
/// </summary>
public class Funciones : System.Web.UI.Page
{
    public Funciones()
    {
    }

    public static string sRegExpSoloNumeros = "^[0-9]*$";

    public enum TipoDato
    {
        Elija = 0,
        Entidad = 1,
        Texto = 2,
        Número = 3,
        Fecha = 4,
        Booleano = 5
    }

    public enum TipoAccion
    {
        ModHecho = 1,
        EjecutaHistoria = 2
    }

    public enum TipoHistoriaDetalle
    {
        Texto = 1,
        Inferencia = 2
    }

    public static void CargarComboTipoDato(System.Web.UI.WebControls.DropDownList ddlCombo)
    {
        ddlCombo.Items.Clear();
        foreach (int nval in TipoDato.GetValues(typeof(TipoDato)))
        {
            ddlCombo.Items.Add(new System.Web.UI.WebControls.ListItem(Enum.GetName(typeof(TipoDato), nval),nval.ToString()));
        }
    }

    public static int CodigoTipoAccion(string sDato)
    {
        int nReturn = 0;
        foreach (int nval in TipoAccion.GetValues(typeof(TipoAccion)))
        {
            if (Enum.GetName(typeof(TipoAccion), nval) == sDato)
            {
                nReturn = nval;
                break;
            }
        }


        return nReturn;
    }

    public static int CodigoTipoHistoriaDetalle(string sDato)
    {
        int nReturn = 0;
        foreach (int nval in TipoHistoriaDetalle.GetValues(typeof(TipoHistoriaDetalle)))
        {
            if (Enum.GetName(typeof(TipoHistoriaDetalle), nval) == sDato)
            {
                nReturn = nval;
                break;
            }
        }


        return nReturn;
    }

    public static void LlenarGrillaVacia(System.Web.UI.WebControls.GridView grdView)
    {
        
        if (grdView.Rows.Count == 0 &&
            grdView.DataSource != null)
        {
            DataTable dt = null;

            if (grdView.DataSource is DataSet)
            {
                dt = ((DataSet)grdView.DataSource).Tables[0].Clone();
            }
            else if (grdView.DataSource is DataTable)
            {
                dt = ((DataTable)grdView.DataSource).Clone();
            }

            if (dt == null)
            {
                return;
            }

            dt.Rows.Add(dt.NewRow()); //Agrego una fila nueva

            grdView.DataSource = dt;
            grdView.DataBind();

            // Oculto la fila

            grdView.Rows[0].Visible = false;
            grdView.Rows[0].Controls.Clear();
        }

        if (grdView.Rows.Count == 1 &&
            grdView.DataSource == null)
        {
            bool bIsGridEmpty = true;

            //Verifico que todas las celdas estan vacias

            for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
            {
                if (grdView.Rows[0].Cells[i].Text != string.Empty)
                {
                    bIsGridEmpty = false;
                }
            }
            //Oculto la grilla

            if (bIsGridEmpty)
            {
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }
        }
    }

    public static void OrdenarCombo(System.Web.UI.WebControls.DropDownList objDDL)
    {
        ArrayList textList = new ArrayList();
        ArrayList valueList = new ArrayList();


        foreach (System.Web.UI.WebControls.ListItem li in objDDL.Items)
        {
            textList.Add(li.Text);
        }

        textList.Sort();


        foreach (object item in textList)
        {
            string value = objDDL.Items.FindByText(item.ToString()).Value;
            valueList.Add(value);
        }
        objDDL.Items.Clear();

        for (int i = 0; i < textList.Count; i++)
        {
            System.Web.UI.WebControls.ListItem objItem = new System.Web.UI.WebControls.ListItem(textList[i].ToString(), valueList[i].ToString());
            objDDL.Items.Add(objItem);
        }
    }

    public bool EnviarMail(string sTo, string sFrom, string sBody, string sSubject)
    {
        bool bResp = true;

        try
        {
            string bodyHTML = string.Empty;
            string bodyPlain = string.Empty;
            MailAddress mailTo;

            MailMessage correo;
            MailAddress mailAddress;
            SmtpClient client;

            mailTo = new MailAddress(sTo);
            correo = new MailMessage();
            mailAddress = new MailAddress(sFrom);
            client = new SmtpClient();

            //Armo el email a enviar
            correo.Subject = sSubject;
            correo.From = mailAddress;
            correo.To.Add(mailTo);
            correo.IsBodyHtml = true;
            bodyHTML = sBody;

            bodyPlain = @"// TuCuento.com.ar // //Copyright (c) 2010. TuCuento.com.ar //";
            correo.Body = bodyHTML + bodyPlain;

            //Mando el mail
            client.Send(correo);

            bResp = true;

        }
        catch(Exception ex)
        {
            bResp = false;
        }
        
        return bResp;
    }

}
