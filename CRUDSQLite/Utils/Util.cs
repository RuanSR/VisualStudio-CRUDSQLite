using System;
using System.IO;

namespace CRUDSQLite.Classes.DB
{
    /*
     * CLASSE RESPONSÁVEL POR FAZER A VERIFICAÇÃO
     * DE ARQUIVOS NECESSÁRIOS PARA O FUNCIONAMENTO
     * DO SISTEMA.
     */
    public class Util
    {
        public bool CheckRootPathFolder()
        {
            try
            {
                if (File.Exists(PathSys.RootPathFolder))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método CheckRootPathFolder: "+ex.Message);
            }
        }
        public bool CheckDBPathFolder()
        {
            try
            {
                if (File.Exists(PathSys.DBPathFolder))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método CheckDBPathFolder: "+ex);
            }
        }
        public bool CheckDBFile()
        {
            try
            {
                if (File.Exists(PathSys.DBFile))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método CheckDBFile: "+ex.Message);
            }
        }
    }
}
