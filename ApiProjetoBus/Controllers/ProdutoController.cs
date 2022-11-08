using ApiProjetoBus.Models.Produto;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ApiProjeto.Controllers
{
    public class ProdutoController : ApiController
    {
        [HttpPost]
        [Route("api/Produto/Adicionar")]
        public string Adicionar(ProdutoModel produto)
        {
            string retorno = "";
            try
            {
                retorno = produto.Adicionar();
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        [HttpPatch]
        [Route("api/Produto/Alterar")]
        public string Alterar(ProdutoModel produto)
        {
            string retorno = "";
            try
            {
                retorno = produto.Alterar();
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        [HttpDelete]
        [Route("api/Produto/Remover")]
        public string Remover(ProdutoModel produto)
        {
            string retorno = "";
            try
            {
                retorno = produto.Remover();
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        [HttpGet]
        [Route("api/Produto/Produto/Listar")]
        public IEnumerable<Dictionary<string, dynamic>> Listar()
        {
            return new ProdutoModel().Listar();
        }
    }
}
