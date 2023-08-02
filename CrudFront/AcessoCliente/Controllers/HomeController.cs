using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcessoCliente.Models;

namespace AcessoCliente.Controllers
{
    public class HomeController : Controller
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
        public IActionResult Index(string mensagem)
        {
            if (mensagem != "")
            {
                ViewData["Retorno"] = mensagem;
            }
            if (ValidaUsuarioLogado())
            {
                return RedirectToAction("Dashboard", "Cliente");
            }
            else
            {
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
