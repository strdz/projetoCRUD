using ApiProjeto.Models;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ApiProjeto.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("api/Usuario/Adicionar")]
        public string Adicionar(UsuarioModel usuario)
        {
            string retorno = "";
            try
            {
                retorno = usuario.Adicionar();
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

        [HttpPost]
        [Route("api/Usuario/Login")]
        public IEnumerable<Dictionary<string, dynamic>> Login(UsuarioModel usuario)
        {
            return usuario.Login();
        }
    }
}