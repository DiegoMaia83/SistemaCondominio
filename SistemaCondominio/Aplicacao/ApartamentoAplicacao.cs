using SistemaCondominio.Models;
using SistemaCondominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCondominio.Aplicacao
{
    public class ApartamentoAplicacao
    {
        public List<ApartamentoBloco> ListarBlocos()
        {
            try
            {
                using(var blocos = new ApartamentoBlocoRepositorio())
                {
                    var lista = blocos.GetAll().ToList();

                    return lista;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Apartamento RetornarPorId(int apartamentoId)
        {
            try
            {
                using(var apartamentos = new ApartamentoRepositorio())
                {
                    return apartamentos.GetAll().Where(x => x.ApartamentoId == apartamentoId).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Apartamento> RetornarApartamentos()
        {
            try
            {
                using(var apartamentos = new ApartamentoRepositorio())
                {
                    return apartamentos.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Apartamento> ListarApartamentos(string blocoId, string ocupado)
        {
            int srcBloco = blocoId != "" ? int.Parse(blocoId) : 0;
            bool srcOcupado = ocupado != "" ? bool.Parse(ocupado) : true;

            try
            {
                using(var apartamentos = new ApartamentoRepositorio())
                {
                    var lista = apartamentos.GetAll();

                    if (blocoId != "") lista = lista.Where(x => x.BlocoId == srcBloco);

                    if (ocupado != "") lista = lista.Where(x => x.Ocupado == srcOcupado);

                    return lista.ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Salvar(Apartamento apartamento)
        {
            try
            {
                using(var apartamentos = new ApartamentoRepositorio())
                {
                    apartamentos.Adicionar(apartamento);
                    apartamentos.SalvarTodos();

                    return apartamento.ApartamentoId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Alterar(Apartamento apartamento)
        {
            try
            {
                using(var apartamentos = new ApartamentoRepositorio())
                {
                    apartamentos.Atualizar(apartamento);
                    apartamentos.SalvarTodos();

                    return apartamento.ApartamentoId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // Andamentos
        public IEnumerable<ApartamentoAndamento> ListarAndamentos()
        {
            try
            {
                using(var andamentos = new ApartamentoAndamentoRepositorio())
                {
                    return andamentos.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int SalvarAndamento(ApartamentoAndamento andamento)
        {
            try
            {
                using(var andamentos = new ApartamentoAndamentoRepositorio())
                {
                    andamentos.Adicionar(andamento);
                    andamentos.SalvarTodos();

                    return andamento.AndamentoId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // Vagas
        public IEnumerable<ApartamentoVaga> ListarVagas()
        {
            try
            {
                using(var vagas = new ApartamentoVagaRepositorio())
                {
                    return vagas.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ApartamentoVaga RetornarVagaPorId(int vagaId)
        {
            try
            {
                using(var vaga = new ApartamentoVagaRepositorio())
                {
                    return vaga.GetAll().Where(x => x.VagaId == vagaId).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ApartamentoVaga> RetornarVagas()
        {
            try
            {
                using(var vagas = new ApartamentoVagaRepositorio())
                {
                    return vagas.GetAll().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ApartamentoVaga AlterarVaga(ApartamentoVaga vaga)
        {
            try
            {
                using (var vagas = new ApartamentoVagaRepositorio())
                {
                    var vagaDb = vagas.GetAll().Where(x => x.VagaId == vaga.VagaId).FirstOrDefault();
                    vagaDb.ApartamentoId = vaga.ApartamentoId;

                    vagas.Atualizar(vagaDb);
                    vagas.SalvarTodos();

                    return vagaDb;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}