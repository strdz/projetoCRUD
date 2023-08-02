using Microsoft.AspNetCore.Mvc;
using Servicos;
using System;
using System.Net;

namespace AcessoCliente.Controllers
{
    public class ProdutoModel : Controller
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Decimal Preco { get; set; }
        public int Id_Departamento { get; set; }
        public bool Status { get; set; }
        public int Id_Usuario { get; set; }

        public string post_Adiciona()
        {
            var retorno = "";
            try
            {
                retorno = Servico.PostAsync($"api/Produto/Produto/Adicionar", this).Result;
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        public string post_Alterar()
        {
            var retorno = "";
            try
            {
                retorno = Servico.PostAsync($"api/Produto/Produto/Alterar", this).Result;
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        public string post_Remover()
        {
            var retorno = "";
            try
            {
                retorno = Servico.PostAsync($"api/Produto/Produto/Remover", this).Result;
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        public string get_Listar()
        {
            var retorno = "";
            try
            {
                retorno = Servico.GetAsync($"api/Produto/Produto/Listar").Result;
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

    }
}
