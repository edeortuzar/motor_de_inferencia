using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

    public class Backup_NEG
    {

        public static Boolean HacerBKBD(string sPath)
        {
            return TuCuento.DAL.Backup_DAL.HacerBKBD(sPath);
        }

        public static Boolean HacerRestoreBD(string sPath)
        {
            return TuCuento.DAL.Backup_DAL.HacerRestore(sPath);
        }
        
	}
}
