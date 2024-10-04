using SistemaCondominio.Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCondominio.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsuarioAplicacao _usuarioAplicacao = new UsuarioAplicacao();

        public ActionResult Index()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            var lista = _usuarioAplicacao.RetornarTodos();

            return View(lista);
        }

    }
}