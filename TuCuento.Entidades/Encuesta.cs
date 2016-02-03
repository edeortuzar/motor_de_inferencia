using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades
{

    public class Encuesta
    {

        private int _nId;
        private string _dFechaAlta;
        private string _dFechaFin;
        private int _nHab;
        private string _sTitulo;
        private string _sPregunta;
        private List<EncuestaDetalle> _lstDetalle = new List<EncuestaDetalle>();
        private List<Usuario> _lstUsuario = new List<Usuario>();

        // constructor
        public Encuesta()
        {
        }

        public int nId { get { return _nId; } set { _nId = value; } }
        public string dFechaAlta { get { return _dFechaAlta; } set { _dFechaAlta = value; } }
        public string dFechaFin { get { return _dFechaFin; } set { _dFechaFin = value; } }
        public int nHab { get { return _nHab; } set { _nHab = value; } }
        public string sTitulo { get { return _sTitulo; } set { _sTitulo = value; } }
        public string sPregunta { get { return _sPregunta; } set { _sPregunta = value; } }
        public List<EncuestaDetalle> lstDetalle { get { return _lstDetalle; } set { _lstDetalle = value; } }
        public List<Usuario> lstUsuario { get { return _lstUsuario; } set { _lstUsuario = value; } }
    }
}