using CRUDSQLite.Model;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace CRUDSQLite.Classes.DB
{
    /*
     * CLASSE RESPONSÁVEL POR GERENCIAR A LÓGICA DE CRUD.
     */
    public class UserRepository
    {
        private readonly string _strConn = ConfigurationManager.AppSettings["strConnection"];
        private SQLiteCommand cmd;
        private bool tableIsAvailable;
        public DataTable GetData()
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    DataTable dataTable = new DataTable();
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.CommandText = Query.SelectAll;
                    cmd.Connection = con;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    con.Close();
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro em GetData: " + ex.Message);
            }
        }
        public User GetUser(int Id)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    User userDB = null;
                    cmd.CommandText = Query.GetUser(Id);
                    cmd.Connection = con;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userDB = new User()
                        {
                            Id = int.Parse(reader.GetInt32(reader.GetOrdinal(nameof(User.Id))).ToString()),
                            Nome = reader.GetString(reader.GetOrdinal(nameof(User.Nome))),
                            Nascimento = reader.GetString(reader.GetOrdinal(nameof(User.Nascimento))),
                            RU = reader.GetString(reader.GetOrdinal(nameof(User.RU))),
                            Genero = reader.GetString(reader.GetOrdinal(nameof(User.Genero))),
                            Obs = reader.GetString(reader.GetOrdinal(nameof(User.Obs)))
                        };
                    }
                    con.Close();
                    return userDB;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro em GetUser: " + ex.Message);
            }
        }
        public void NewUser(User user)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.Connection = con;
                    cmd.CommandText = Query.Insert;
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Nome), user.Nome));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Nascimento), user.Nascimento));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.RU), user.RU));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Genero), user.Genero));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Obs), user.Obs));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método InsertData: "+ex.Message);
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.Connection = con;
                    cmd.CommandText = Query.Update;
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Nome), user.Nome));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Nascimento), user.Nascimento));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.RU), user.RU));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Genero), user.Genero));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Obs), user.Obs));
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Id), user.Id));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método Update: " + ex.Message);
            }
        }
        public void DeleteData(int id)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    cmd.Connection = con;
                    cmd.CommandText = Query.Delete;
                    cmd.Parameters.Add(new SQLiteParameter(nameof(User.Id), id));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método Delete: " + ex.Message);
            }
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
                    cmd.CommandText = Query.CreateTable;
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
                    cmd.CommandText = Query.SelectAll;
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
