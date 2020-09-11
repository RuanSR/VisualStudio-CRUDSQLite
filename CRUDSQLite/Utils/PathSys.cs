using System;
using System.Configuration;

namespace CRUDSQLite.Classes.DB
{
    /* 
     * CLASSE RESPONSÁVEL POR GUARDAR AS
     * LOCALIZAÇÕES DE ARQUIVOS NECESSÁRIOS NO
     * COMPUTADOR.
     */
    public static class PathSys
    {
        //public static string RootPathFolder = @"C:\R.S.R Software\LabPro\CRUDSQLite";
        public static string RootPathFolder = ConfigurationManager.AppSettings["RootPathFolder"];
        public static string DBPathFolder = ConfigurationManager.AppSettings["DBPathFolder"];
        public static string DBFile = ConfigurationManager.AppSettings["DBFile"];
    }
}
