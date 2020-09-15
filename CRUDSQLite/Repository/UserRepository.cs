using CRUDSQLite.Model;
using System;
using System.Configuration;
using System.Data.SQLite;
using System.Windows.Forms;

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

        public DataGridView GetData(DataGridView gridView)
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
                        gridView.Rows.Add(new object[]
                        {
                            reader.GetValue(reader.GetOrdinal("ID")),
                            reader.GetValue(reader.GetOrdinal("Nome")),
                            reader.GetValue(reader.GetOrdinal("Nascimento")),
                            reader.GetValue(reader.GetOrdinal("RU")),
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
        public User GetUser(int Id)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(_strConn))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    User userDB = null;
                    cmd.CommandText = $@"SELECT ID, Nome, Nascimento, RU, Sexo, Obs FROM User Where ID = {Id}";
                    cmd.Connection = con;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userDB = new User()
                        {
                            Id = int.Parse(reader.GetInt32(reader.GetOrdinal("ID")).ToString()),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Nascimento = reader.GetString(reader.GetOrdinal("Nascimento")),
                            RU = reader.GetString(reader.GetOrdinal("RU")),
                            Genero = reader.GetString(reader.GetOrdinal("Sexo")),
                            Obs = reader.GetString(reader.GetOrdinal("Obs"))
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
                    cmd.Parameters.Add(new SQLiteParameter("nome", user.Nome));
                    cmd.Parameters.Add(new SQLiteParameter("nascimento", user.Nascimento));
                    cmd.Parameters.Add(new SQLiteParameter("ru", user.RU));
                    cmd.Parameters.Add(new SQLiteParameter("sexo", user.Genero));
                    cmd.Parameters.Add(new SQLiteParameter("obs", user.Obs));
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
                    cmd.Parameters.Add(new SQLiteParameter("nome", user.Nome));
                    cmd.Parameters.Add(new SQLiteParameter("nascimento", user.Nascimento));
                    cmd.Parameters.Add(new SQLiteParameter("ru", user.RU));
                    cmd.Parameters.Add(new SQLiteParameter("sexo", user.Genero));
                    cmd.Parameters.Add(new SQLiteParameter("obs", user.Obs));
                    cmd.Parameters.Add(new SQLiteParameter("id", user.Id));
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
                    cmd.Parameters.Add(new SQLiteParameter("id", id));
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
