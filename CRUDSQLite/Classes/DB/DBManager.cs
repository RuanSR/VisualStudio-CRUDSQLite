using System;
using System.Data.SQLite;

namespace CRUDSQLite.Classes.DB
{
    /*
     * CLASSE RESPONSÁVEL POR GERENCIAR A LÓGICA DE CRUD.
     */
    public class DBManager
    {
        private bool tableIsAvailable;
        SQLiteCommand cmd;
        private static string queryCreateTable = @"CREATE Table User 
                                                (ID INTEGER PRIMARY KEY AUTOINCREMENT, Nome NVARCHAR(50), Nascimento NVARCHAR(8), Sexo CHAR(1))";
        private static string querySelectAll = @"SELECT * FROM User";
        private string GetConnection()
        {
            try
            {
                return @"Data Source=" + PathSys.DBFile+"; Version=3;";
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método GetConnection: "+ex.Message);
            }
        }

        public bool GetData()
        {
            return false;
        }
        public bool InsertData()
        {
            return false;
        }
        public bool UpdateData()
        {
            return false;
        }
        public bool DeleteData()
        {
            return false;
        }

        public void CreateDataBase()
        {
            try
            {
                SQLiteConnection.CreateFile(PathSys.DBFile);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método CreateDataBase: "+ex.Message);
            }
        }
        public void CreateTable()
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(GetConnection()))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.CommandText = queryCreateTable;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método CreateTable da Classe DBManager: "+ex.Message);
            }
        }
        public bool CheckDB()
        {
            using (SQLiteConnection con = new SQLiteConnection(GetConnection()))
            {
                try
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.CommandText = querySelectAll;
                    cmd.Connection = con;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tableIsAvailable = true;
                    }
                }
                catch (Exception)
                {
                    tableIsAvailable = false;
                }
                return tableIsAvailable;
            }
        }

    }
}
