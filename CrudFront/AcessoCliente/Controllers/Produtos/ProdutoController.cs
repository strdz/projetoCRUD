using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace AcessoCliente.Controllers
{
    public class ProdutoController : Controller
    {
        public string Id_Usuario = "";
        public string TokenUsuario = "";
        public string nomeUsuario = "";

        public bool ValidaUsuarioLogado()
        {
            bool valida = false;
            if (Request.Cookies["token"] != null && Request.Cookies["usuario"] != null && Request.Cookies["id"] != null)
            {
                Id_Usuario = Request.Cookies["id"];
                TokenUsuario = Request.Cookies["token"];
                nomeUsuario = Request.Cookies["usuario"];

                valida = true;
            }
            return valida;
        }

        public IActionResult Produto()
        {
            if (ValidaUsuarioLogado())
            {
                ViewData["Usuario"] = nomeUsuario;
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Route("/Produto/Produto/Adiciona")]
        public string Adicionar(ProdutoModel produto)
        {
            string retorno = "";
            try
            {
                if (ValidaUsuarioLogado())
                {
                    produto.Id_Usuario = Convert.ToInt32(Id_Usuario);
                    if (produto.Id > 0)
                    {
                        retorno = produto.post_Alterar();
                    }
                    else
                    {
                        retorno = produto.post_Adiciona();
                    }
                } else
                {
                    retorno = "Falha ao autenticar usuario, faça o login novamente.";
                }
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        [Route("/Produto/Produto/Remover")]
        public string Remover(ProdutoModel produto)
        {
            string retorno = "";
            try
            {
                if (ValidaUsuarioLogado())
                {
                    produto.Id_Usuario = Convert.ToInt32(Id_Usuario);
                    if (produto.Id > 0)
                    {
                        retorno = produto.post_Remover();
                    }
                    else
                    {
                        retorno = "Erro ao capturar ID, tente novamente";
                    }
                }
                else
                {
                    retorno = "Falha ao autenticar usuario, faça o login novamente.";
                }
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        [HttpGet]
        [Route("/Produto/Produto/Listar")]
        public string Listar(ProdutoModel produto)
        {
            string retorno = "";
            try
            {
                retorno = produto.get_Listar();
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        [HttpGet]
        [Route("/Produto/Produto/CarregaDepartamentos")]
        public string CarregaDepartamentos()
        {

            var cliente = new WebClient();
            var resultado = cliente.DownloadString(string.Format("https://talentoshumanos-selecao.solucoesmaxima.com.br/Departamento"));

            return resultado;
        }

    }
}
