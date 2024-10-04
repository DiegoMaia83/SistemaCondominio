using SistemaCondominio.Aplicacao;
using SistemaCondominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCondominio.Controllers
{
    public class ApartamentoController : Controller
    {
        private readonly ApartamentoAplicacao _apartamentoAplicacao = new ApartamentoAplicacao();
        private readonly LogAplicacao _logAplicacao = new LogAplicacao();
        
        public ActionResult Home()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            ViewBag.ListaBlocos = _apartamentoAplicacao.ListarBlocos();

            return View();
        }

        public ActionResult Detalhes(int apartamentoId = 0)
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            try
            {
                var apartamento = new Apartamento();

                if (apartamentoId > 0)
                {
                    apartamento = _apartamentoAplicacao.RetornarPorId(apartamentoId);
                }

                ViewBag.ListaBlocos = _apartamentoAplicacao.ListarBlocos();

                return View(apartamento);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Detalhes(Apartamento apartamento)
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            try
            {
                var log = new Log
                {                    
                    MoradorId = 0,
                    UsuarioId = UsuarioSessao.Logado.UsuarioId,
                    Ip = Request.ServerVariables["REMOTE_ADDR"],
                    DataLog = DateTime.Now                    
                };

                if (apartamento.ApartamentoId == 0)
                {
                    apartamento.UsuCriacao = UsuarioSessao.Logado.Nome;
                    apartamento.DataCriacao = DateTime.Now;
                    apartamento.ApartamentoId = _apartamentoAplicacao.Salvar(apartamento);

                    log.ApartamentoId = apartamento.ApartamentoId;
                    log.Descricao = "Cadastrou o apartamento";

                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost { Id = apartamento.ApartamentoId, Sucesso = true, Mensagem = "Apartamento adicionado com sucesso!" });
                } 
                else
                {
                    apartamento.UsuAlteracao = UsuarioSessao.Logado.Nome;
                    apartamento.DataAlteracao = DateTime.Now;

                    _apartamentoAplicacao.Alterar(apartamento);

                    log.ApartamentoId = apartamento.ApartamentoId;
                    log.Descricao = "Alterou o cadastro do apartamento";

                    _logAplicacao.GravarLog(log);
                    
                    return Json(new ResultadoPost { Id = apartamento.ApartamentoId, Sucesso = true, Mensagem = "Apartamento alterado com sucesso!" });
                }                
                
            }
            catch
            {                
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao salvar. Verifique os dados e tente novamente." });                
            }
        }

        [HttpGet]
        public ActionResult ListarApartamentos(string blocoId, string ocupado)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            var listaApartamentos = _apartamentoAplicacao.ListarApartamentos(blocoId, ocupado);            

            return View("_ListarApartamentos", listaApartamentos);
        }

        [HttpGet]
        public ActionResult ListarAndamentos(int apartamentoId)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            var lista = _apartamentoAplicacao.ListarAndamentos().Where(x => x.ApartamentoId == apartamentoId);

            return View("_ListarAndamentos", lista);
        }

        [HttpPost]
        public JsonResult AdicionarAndamento(ApartamentoAndamento andamento)
        {
            if (!UsuarioSessao.ValidaToken()) 
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            try
            {
                if (andamento.ApartamentoId > 0)
                {
                    andamento.DataAndamento = DateTime.Now;
                    andamento.UsuAndamento = UsuarioSessao.Logado.Nome;

                    _apartamentoAplicacao.SalvarAndamento(andamento);

                    var log = new Log
                    {
                        MoradorId = 0,
                        ApartamentoId = andamento.ApartamentoId,
                        UsuarioId = UsuarioSessao.Logado.UsuarioId,
                        Ip = Request.ServerVariables["REMOTE_ADDR"],
                        DataLog = DateTime.Now,
                        Descricao = "Inseriu um registro de andamento"
                    };

                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost { Id = andamento.ApartamentoId, Sucesso = true, Mensagem = "Andamento inserido com sucesso!" });
                }
                else
                {
                    return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação." });
                }
            }
            catch
            {
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação." });
            }
        }        



        // ----- Vagas ----- //

        public ActionResult Vagas()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            var lista = _apartamentoAplicacao.ListarVagas();

            return View(lista);
        }

        [HttpGet]
        public ActionResult ListarVagas()
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            var lista = _apartamentoAplicacao.ListarVagas();

            return View("_ListarVagas", lista);
        }

        [HttpGet]
        public ActionResult RetornarVaga(int id = 0)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            var vaga = new ApartamentoVaga();

            if (id > 0)
            {
                vaga = _apartamentoAplicacao.RetornarVagaPorId(id);
            }

            ViewBag.ListaApartamentos = _apartamentoAplicacao.RetornarApartamentos();

            return View("_RetornarVaga", vaga);
        }

        [HttpPost]
        public ActionResult AlterarVaga(ApartamentoVaga vaga)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            try
            {
                if (vaga.VagaId > 0)
                {
                    var result = _apartamentoAplicacao.AlterarVaga(vaga);

                    var log = new Log
                    {
                        MoradorId = 0,
                        ApartamentoId = Convert.ToInt32(result.ApartamentoId),
                        UsuarioId = UsuarioSessao.Logado.UsuarioId,
                        Ip = Request.ServerVariables["REMOTE_ADDR"],
                        DataLog = DateTime.Now,
                        Descricao = "Alterou a vaga do apartamento"
                    };

                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost() { Id = result.VagaId, Sucesso = true, Mensagem = "Operação realizada com sucesso" });
                }
                else
                {
                    return Json(new ResultadoPost() { Sucesso = false, Mensagem = "Vaga não encontrada!" });
                }
            }
            catch
            {
                return Json(new ResultadoPost() { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" });
            }
        }
    }
}