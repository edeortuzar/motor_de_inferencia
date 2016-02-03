using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuCuento.Entidades {

	public class HistoriaDetTexto {

		private string _sTexto;

		// constructor
		public HistoriaDetTexto () {
		}

		public string sTexto {get {return _sTexto;} set {_sTexto = value;}}
        public string sTextoCorto { 
            get 
            {
                if (_sTexto != null)
                {
                    if (_sTexto.Length < 20)
                    {
                        return _sTexto + "...";
                    }
                    else
                    {
                        return _sTexto.Substring(0, 20) + "...";
                    }
                }
                else
                {
                    return "";
                }
            } 
        }
	}
}
