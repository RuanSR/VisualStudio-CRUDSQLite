using System;

namespace CRUDSQLite.Model
{
    public class User
    {
        public User()
        {

        }
        public User(string nome, string nascimento, string RU, string genero, string obs)
        {
            try
            {
                ValidaDados(nome);
                this.Nome = nome;
                this.Nascimento = nascimento;
                this.RU = RU;
                this.Genero = genero;
                this.Obs = obs;
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro inesperado! {e.Message}");
            }
        }

        private void ValidaDados(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Campo Nome precisa ter no mínimo 6 caracteres.");
            }
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Nascimento { get; set; }
        public string RU { get; set; }
        public string Genero { get; set; }
        public string Obs { get; set; }

    }
}
