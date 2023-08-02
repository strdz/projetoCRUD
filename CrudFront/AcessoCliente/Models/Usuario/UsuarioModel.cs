using System;
using Servicos;

namespace AcessoCliente.Models
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

        public string post_Registra(UsuarioModel usuario)
        {
            var retorno = "";
            try
            {
                retorno = Servico.PostAsync($"api/Usuario/Adicionar", this).Result;
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        public string post_Login(UsuarioModel usuario)
        {
            var retorno = "";
            try
            {
                retorno = Servico.PostAsync($"api/Usuario/Login", this).Result;
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }
    }
}
