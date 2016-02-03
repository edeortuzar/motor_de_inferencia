using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Inferencia
/// </summary>
public class Inferencia
{
    public Inferencia()
    {
        
    }

    public Inferencia(int nCod_Entidad, int nCod_Atributo, string sAtributo)
    {
        this.nCod_Atributo = nCod_Atributo;
        this.nCod_Entidad = nCod_Entidad;
        this.sAtributo = sAtributo;
    }

    public int nCod_Entidad { get; set; }

    public int nCod_Atributo { get; set; }

    public string sAtributo { get; set; }

    public List<ValPosible> lstValPosible { get; set; }

}
