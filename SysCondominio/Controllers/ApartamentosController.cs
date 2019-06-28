using SysCondominio.Aplicacao;
using SysCondominio.Models;
using System.Linq;
using System.Web.Mvc;

namespace SysCondominio.Controllers
{
    public class ApartamentosController : Controller
    {
        private readonly ApartamentoAplicacao _apartamentoAplicacao = new ApartamentoAplicacao();
        private readonly ComumAplicacao _comumAplicacao = new ComumAplicacao();

        public ApartamentosController()
        {
            ViewBag.RetornaBlocos = _comumAplicacao.RetornaListaDeBlocos();
        }

        public ActionResult Index()
        {
            var apartamento = _apartamentoAplicacao.List().OrderBy(x => x.Bloco.Torre).ThenBy(x => x.Bloco.Nome).ThenBy(x => x.Numero);

            return View(apartamento.ToList());
        }

        public ActionResult Detalhes(int? id)
        {           

            if(id != null)
            {
                var apartamento = _apartamentoAplicacao.Get((int)id);
                return View(apartamento);
            }
            else
            {
                return View(new Apartamento());
            }            
        }

        public ActionResult Criar()
        {
            return View(new Apartamento());
        }

        [HttpPost]
        public ActionResult Criar(Apartamento apartamento)
        {
            _apartamentoAplicacao.Insert(apartamento);
            
            return RedirectToAction("Index", "Apartamentos");
        }
    }
}