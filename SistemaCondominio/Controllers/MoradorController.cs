using SistemaCondominio.Aplicacao;
using SistemaCondominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCondominio.Controllers
{
    public class MoradorController : Controller
    {
        private readonly MoradorAplicacao _moradorAplicacao = new MoradorAplicacao();
        private readonly LogAplicacao _logAplicacao = new LogAplicacao();

        public ActionResult Home()
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            return View();
        }

        public ActionResult Detalhes(int moradorId = 0)
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            var morador = new Morador();   
            
            if(moradorId > 0)
            {
                morador = _moradorAplicacao.RetornarPorId(moradorId);
            }

            return View(morador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Detalhes(Morador morador)
        {
            if (!UsuarioSessao.ValidaToken()) Response.Redirect("/Login/Index");

            try
            {
                var log = new Log()
                {
                    ApartamentoId = 0,
                    UsuarioId = UsuarioSessao.Logado.UsuarioId,
                    Ip = Request.ServerVariables["REMOTE_ADDR"],
                    DataLog = DateTime.Now
                };

                // Se já existir um cpf cadastrado não deixa prosseguir
                if (_moradorAplicacao.ExisteCpf(morador.Cpf, morador.MoradorId)) return Json(new ResultadoPost { Sucesso = false, Mensagem = "Já existe um morador cadastrado com esse CPF!" });

                if (morador.MoradorId == 0)
                {
                    morador.UsuCriacao = UsuarioSessao.Logado.Nome;
                    morador.DataCriacao = DateTime.Now;

                    var moradorInserido = _moradorAplicacao.Salvar(morador);

                    log.MoradorId = moradorInserido;
                    log.Descricao = "Cadastrou o morador";
                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost { Id = moradorInserido, Sucesso = true, Mensagem = "Morador adicionado com sucesso!" });
                } 
                else
                {
                    morador.UsuAlteracao = UsuarioSessao.Logado.Nome;
                    morador.DataAlteracao = DateTime.Now;

                    _moradorAplicacao.Alterar(morador);

                    log.MoradorId = morador.MoradorId;
                    log.Descricao = "Alterou o cadastro do morador";
                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost { Id = morador.MoradorId, Sucesso = true, Mensagem = "Morador alterado com sucesso!" });
                }                
            }
            catch
            {
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });
            }
        }

        [HttpGet]
        public ActionResult ListarMoradores(string input)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            var listaMoradores = _moradorAplicacao.ListarMoradores(input);

            return View("_ListarMoradores", listaMoradores);
        }

        [HttpGet]
        public ActionResult AdicionarMorador(int apartamentoId, string input)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            var listaMoradores = _moradorAplicacao.ListarMoradores(input);

            ViewBag.ApartamentoId = apartamentoId;

            return View("_AdicionarMorador", listaMoradores);
        }

        [HttpPost]
        public JsonResult AddMoradorOcupacao(int moradorId, int apartamentoId)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            try
            {
                if(moradorId > 0 && apartamentoId > 0)
                {
                    _moradorAplicacao.AddMoradorOcupacao(moradorId, apartamentoId);

                    var log = new Log()
                    {
                        MoradorId = moradorId,
                        ApartamentoId = apartamentoId,
                        UsuarioId = UsuarioSessao.Logado.UsuarioId,
                        Ip = Request.ServerVariables["REMOTE_ADDR"],
                        DataLog = DateTime.Now,
                        Descricao = "Adicionou o morador ao apartamento"
                    };

                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost { Id = apartamentoId, Sucesso = true, Mensagem = "Morador adicionado com sucesso!" });
                }
                else
                {
                    return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });
                }
            }
            catch
            {
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });
            }
            
        }

        [HttpPost]
        public JsonResult DelMoradorOcupacao(int ocupacaoId)
        {
            if (!UsuarioSessao.ValidaToken())
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação. Tente efetuar o login novamente." });

            try
            {
                if(ocupacaoId > 0)
                {                   

                    var ocupacao = _moradorAplicacao.DelMoradorOcupacao(ocupacaoId);

                    var log = new Log()
                    {
                        MoradorId = ocupacao.MoradorId,
                        ApartamentoId = ocupacao.ApartamentoId,
                        UsuarioId = UsuarioSessao.Logado.UsuarioId,
                        Ip = Request.ServerVariables["REMOTE_ADDR"],
                        DataLog = DateTime.Now,
                        Descricao = "Removeu o morador do apartamento"
                    };

                    _logAplicacao.GravarLog(log);

                    return Json(new ResultadoPost { Id = ocupacao.ApartamentoId, Sucesso = true, Mensagem = "Morador removido com sucesso!" });
                } 
                else
                {
                    return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });
                }
            }
            catch(Exception ex)
            {                
                return Json(new ResultadoPost { Id = 0, Sucesso = false, Mensagem = "Houve um erro ao efetuar a operação!" });
            }
        }
    }
}