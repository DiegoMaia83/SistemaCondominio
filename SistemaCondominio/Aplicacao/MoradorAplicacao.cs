using SistemaCondominio.Models;
using SistemaCondominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCondominio.Aplicacao
{
    public class MoradorAplicacao
    {
        public Morador RetornarPorId(int moradorId)
        {
            try
            {
                using(var moradores = new MoradorRepositorio())
                {
                    return moradores.GetAll().Where(x => x.MoradorId == moradorId).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Morador> ListarMoradores()
        {
            try
            {
                using(var moradores = new MoradorRepositorio())
                {
                    return moradores.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Morador> ListarMoradores(string input)
        {
            try
            {
                using (var moradores = new MoradorRepositorio())
                {
                    return moradores.GetAll().Where(x => x.Nome.Contains(input) || x.Cpf.Contains(input)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteCpf(string cpf, int moradorId)
        {
            try
            {
                using (var moradores = new MoradorRepositorio())
                {
                    return moradores.GetAll().Where(x => x.Cpf == cpf && x.MoradorId != moradorId).FirstOrDefault() != null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Salvar(Morador morador)
        {
            try
            {
                using(var moradores = new MoradorRepositorio())
                {
                    moradores.Adicionar(morador);
                    moradores.SalvarTodos();

                    return morador.MoradorId;
                }
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Alterar(Morador morador)
        {
            try
            {
                using(var moradores = new MoradorRepositorio())
                {
                    moradores.Atualizar(morador);
                    moradores.SalvarTodos();

                    return morador.MoradorId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public MoradorOcupacao RetornarOcupacaoPorId(int ocupacaoId)
        {
            try
            {
                using(var moradorOcupacao = new MoradorOcupacaoRepositorio())
                {
                    return moradorOcupacao.GetAll().Where(x => x.OcupacaoId == ocupacaoId).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void AddMoradorOcupacao(int moradorId, int apartamentoId)
        {
            try
            {
                var ocupacao = new MoradorOcupacao()
                {
                    MoradorId = moradorId,
                    ApartamentoId = apartamentoId,
                    UsuInclusao = UsuarioSessao.Logado.Nome,
                    DataInclusao = DateTime.Now
                };

                using(var moradorOcupacao = new MoradorOcupacaoRepositorio())
                {
                    moradorOcupacao.Adicionar(ocupacao);
                    moradorOcupacao.SalvarTodos();                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public MoradorOcupacao DelMoradorOcupacao(int ocupacaoId)
        {
            try
            {  
                using(var moradorOcupacao = new MoradorOcupacaoRepositorio())
                {
                    var ocupacao = moradorOcupacao.GetAll().Where(x => x.OcupacaoId == ocupacaoId).FirstOrDefault();
                    ocupacao.UsuExclusao = UsuarioSessao.Logado.Nome;
                    ocupacao.DataExclusao = DateTime.Now;
                    ocupacao.Excluido = true;

                    moradorOcupacao.Atualizar(ocupacao);
                    moradorOcupacao.SalvarTodos();

                    return ocupacao;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}