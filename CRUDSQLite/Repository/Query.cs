namespace CRUDSQLite.Classes.DB
{
    public static class Query
    {
        public static string CreateTable { get; } = @"CREATE Table User 
                                                (ID INTEGER PRIMARY KEY AUTOINCREMENT, Nome NVARCHAR(50), Nascimento NVARCHAR(10), RU NVARCHAR(10), Sexo CHAR(1), Obs TEXT(200))";
        public static string SelectAll { get; } = @"SELECT * FROM User";
        public static string Insert { get; } = @"INSERT INTO User (Nome, Nascimento, RU, Sexo, Obs) VALUES (@nome, @nascimento,@ru,@sexo,@obs)";
    }
}
