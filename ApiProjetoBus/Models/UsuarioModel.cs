using ConexaoSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiProjeto.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Nome { get; set; }
        public string ValidaCaptcha { get; set; }

        public string Adicionar()
        {
            var retorno = "";
            try
            {
                Dictionary<string, dynamic> parametros = new Dictionary<string, dynamic>();


                Conexao conexao = new Conexao();

                var Id = $"'{this.Id}'";
                var Usuario = $"'{this.Usuario}'";
                var Senha = $"'{this.Senha}'";
                var Nome = $"'{this.Nome}'";
                var Email = $"'{this.Email}'";

                if (conexao.InserirDados("INSERT INTO USUARIOS (ID, LOGIN, SENHA, NOME, EMAIL) VALUES (" + Id + ", " + Usuario + ", " + Senha + ", " + Nome + ", " + Email + ")") == 0)
                {

                    retorno = "SUCESSO";
                } else
                {
                    retorno = "ATENCAO! Falha ao cadastrar usuario, tente novamente.";
                }
            }

            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }
  
        public IEnumerable<Dictionary<string, dynamic>> Login() 
        {

            string sql = " SELECT LOGIN AS LOGIN, ID AS ID, SENHA AS SENHA, NOME AS NOME ";
            sql += @" FROM USUARIOS ";

            sql += $" WHERE LOGIN = '{this.Usuario}' OR EMAIL = '{this.Usuario}' ";

            Conexao conexao = new Conexao();
            return conexao.CarregaLista(sql);

        }

    }
}