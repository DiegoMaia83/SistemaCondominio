using SysCondominio.Aplicacao;
using SysCondominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysCondominio.Controllers
{
    public class MoradoresController : Controller
    {
        private readonly MoradorAplicacao _moradorAplicacao = new MoradorAplicacao();
        private readonly ComumAplicacao _comumAplicacao = new ComumAplicacao();

        public MoradoresController()
        {
            ViewBag.RetornaApartamentos = _comumAplicacao.RetornaListaDeApartamentos();
        }

        // GET: Moradores
        public ActionResult Index()
        {
            var morador = _moradorAplicacao.List();

            return View(morador.ToList());
        }

        public ActionResult Criar()
        {
            return View(new Morador());
        }

        [HttpPost]
        public ActionResult Criar(Morador morador)
        {
            _moradorAplicacao.Insert(morador);

            return RedirectToAction("Index", "Moradores");
        }
    }
}