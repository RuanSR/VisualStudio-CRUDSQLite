using System;
using System.IO;
using CRUDSQLite.Classes.DB;
using System.Data.SQLite;

namespace CRUDSQLite.Classes
{
    public class Init
    {
        private Util util = new Util();
        private DBManager dbManager;
        public Init()
        {
            CreateRootPathFolder();
            CreateDBPathFolder();
            CreateDBFile();
        }
        private void CreateRootPathFolder()
        {
            try
            {
                if (!util.CheckRootPathFolder())
                {
                    Directory.CreateDirectory(PathSys.RootPathFolder);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CreateDBPathFolder()
        {
            try
            {
                if (!util.CheckDBPathFolder())
                {
                    Directory.CreateDirectory(PathSys.DBPathFolder);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CreateDBFile()
        {
            dbManager = new DBManager();
            try
            {
                if (!util.CheckDBFile())
                {
                    dbManager.CreateDataBase();
                }
                if (!dbManager.CheckDB())
                {
                    dbManager.CreateTable();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
