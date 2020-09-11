using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDSQLite.Classes.DB
{
    public static class Query
    {
        public static readonly string queryCreateTable = @"CREATE Table User 
                                                (ID INTEGER PRIMARY KEY AUTOINCREMENT, Nome NVARCHAR(50), Nascimento NVARCHAR(10), RG NVARCHAR(10), Sexo CHAR(1), Obs TEXT(200))";
        public static readonly string querySelectAll = @"SELECT * FROM User";
        public static readonly string queryInsert = @"INSERT INTO User (Nome, Nascimento, RG, Sexo, Obs) VALUES (@nom, @nas,@rg,@s,@obs)";
    }
}
