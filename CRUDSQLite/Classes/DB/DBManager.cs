﻿using System;
using System.Data.SQLite;
using System.Windows.Forms;

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
                                                (ID INTEGER PRIMARY KEY AUTOINCREMENT, Nome NVARCHAR(50), Nascimento NVARCHAR(10), RU NVARCHAR(10), Sexo CHAR(1), Obs TEXT(200))";
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

        public DataGridView GetData(DataGridView gridView)
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
        public void InsertData(string nom, string nas, string ru, char s, string obs)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(GetConnection()))
                {
                    con.Open();
                    cmd = new SQLiteCommand();
                    string query = @"INSERT INTO User (Nome, Nascimento, RU, Sexo, Obs) VALUES (@nom, @nas,@ru,@s,@obs)";
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SQLiteParameter("nom", nom));
                    cmd.Parameters.Add(new SQLiteParameter("nas", nas));
                    cmd.Parameters.Add(new SQLiteParameter("ru", ru));
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
