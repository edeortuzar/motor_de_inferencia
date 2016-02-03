using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ValPosible
/// </summary>
public class ValPosible
{
    public ValPosible()
    {
        
    }

    private int _nCod_ValPosible;
	private string _sValor;
	private string _sOperadorLogico;
    
	public int nCod_ValPosible {get {return _nCod_ValPosible;} set {_nCod_ValPosible = value;}}
	public string sValor {get {return _sValor;} set {_sValor = value;}}
	public string sOperadorLogico {get {return _sOperadorLogico;} set {_sOperadorLogico = value;}}
    public string ValorPosible { get { 
        return _sOperadorLogico.PadRight(2, Convert.ToChar(" ")) + _sValor; } 
    }

}
