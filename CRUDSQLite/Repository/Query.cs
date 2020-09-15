using CRUDSQLite.Model;

namespace CRUDSQLite.Classes.DB
{
    public static class Query
    {
        public static string CreateTable { get; } = $@"CREATE Table {nameof(User)} 
                                                ({nameof(User.Id)} INTEGER PRIMARY KEY AUTOINCREMENT, {nameof(User.Nome)} NVARCHAR(50), {nameof(User.Nascimento)} NVARCHAR(10), {nameof(User.RU)} NVARCHAR(10), {nameof(User.Genero)} CHAR(1), {nameof(User.Obs)} TEXT(200))";
        public static string SelectAll { get; } = $@"SELECT * FROM {nameof(User)}";
        public static string Insert { get; } = $@"INSERT INTO {nameof(User)} ({nameof(User.Nome)}, {nameof(User.Nascimento)}, {nameof(User.RU)}, {nameof(User.Genero)}, {nameof(User.Obs)}) VALUES (@{nameof(User.Nome)}, @{nameof(User.Nascimento)},@{nameof(User.RU)},@{nameof(User.Genero)},@{nameof(User.Obs)})";
        public static string Update { get; } = $@"UPDATE {nameof(User)} SET {nameof(User.Nome)}=@{nameof(User.Nome)}, {nameof(User.Nascimento)}=@{nameof(User.Nascimento)}, {nameof(User.RU)}=@{nameof(User.RU)}, {nameof(User.Genero)}=@{nameof(User.Genero)}, {nameof(User.Obs)}=@{nameof(User.Obs)} WHERE {nameof(User.Id)}=@{nameof(User.Id)}";
        public static string Delete { get; } = $@"Delete FROM {nameof(User)} WHERE {nameof(User.Id)}=@{nameof(User.Id)}";
        public static string GetUser(int id)
        {
            return $@"SELECT {nameof(User.Id)}, {nameof(User.Nome)}, {nameof(User.Nascimento)}, {nameof(User.RU)}, {nameof(User.Genero)}, {nameof(User.Obs)} FROM {nameof(User)} Where {nameof(User.Id)} = {id}";
        }
    }
}
