using SistemaCondominio.Aplicacao;
using SistemaCondominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCondominio.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioAplicacao _usuarioAplicacao = new UsuarioAplicacao();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UsuarioLogin usuarioLogin)
        {
            try
            {
                if (UsuarioSessao.Login(usuarioLogin))
                {
                    var usuario = _usuarioAplicacao.Retornar(UsuarioSessao.Logado.UsuarioId);

                    // Registra Logs

                    return RedirectToAction("Home", "Apartamento");
                }
                else
                {
                    ViewBag.Mensagem = "Usuário não localizado!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro ao tentar efetuar o login." + ex.Message;
            }

            return View();
        }

        public ActionResult Logoff()
        {
            UsuarioSessao.Logoff();

            return RedirectToAction("Index", "Login");
        }
    }
}