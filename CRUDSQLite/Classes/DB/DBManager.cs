using System;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CRUDSQLite.Classes.DB
{
    /*
     * CLASSE RESPONSÁVEL POR GERENCIAR A LÓGICA DE CRUD.
     */
    public class DBManager
    {
        private SQLiteCommand cmd;
        private readonly string _strConn = ConfigurationManager.AppSettings["strConnection"];
        private bool tableIsAvailable;

        public DataGridView GetData(DataGridView gridView)
        {
            using (SQLiteConnection con = new SQLiteConnection(_strConn))
            {
                try
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.CommandText = Query.querySelectAll;
                    cmd.Connection = con;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        gridView.Rows.Add(new object[]
                        {
                            reader.GetValue(reader.GetOrdinal("ID")),
                            reader.GetValue(reader.GetOrdinal("Nome")),
                            reader.GetValue(reader.GetOrdinal("Nascimento")),
                            reader.GetValue(reader.GetOrdinal("RG")),
                            reader.GetValue(reader.GetOrdinal("Sexo")),
                            reader.GetValue(reader.GetOrdinal("Obs"))
                        });
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro em GetData: " + ex.Message);
                }
            }
            return gridView;
        }
        public void InsertData(string nom, string nas, string rg, char s, string obs)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.Connection = con;
                    cmd.CommandText = Query.queryInsert;
                    cmd.Parameters.Add(new SQLiteParameter("nom", nom));
                    cmd.Parameters.Add(new SQLiteParameter("nas", nas));
                    cmd.Parameters.Add(new SQLiteParameter("rg", rg));
                    cmd.Parameters.Add(new SQLiteParameter("s", s));
                    cmd.Parameters.Add(new SQLiteParameter("obs", obs));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método InsertData: "+ex.Message);
            }
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
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.CommandText = Query.queryCreateTable;
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
            using (SQLiteConnection con = new SQLiteConnection(_strConn))
            {
                try
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.CommandText = Query.querySelectAll;
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
