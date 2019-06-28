using SysCondominio.Models;
using SysCondominio.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace SysCondominio.Aplicacao
{
    public class ApartamentoAplicacao
    {
        public Apartamento Get(int id)
        {
            using(var apartamento = new ApartamentoRepositorio())
            {
                return apartamento.GetAll().FirstOrDefault(x => x.ApartamentoId == id);
            }
        }

        public IEnumerable<Apartamento> List()
        {
            using(var apartamentos = new ApartamentoRepositorio())
            {
                return apartamentos.GetAll();
            }
        }

        public int Insert(Apartamento obj)
        {
            using(var apartamento = new ApartamentoRepositorio())
            {
                apartamento.Adicionar(obj);
                apartamento.SalvarTodos();

                return obj.ApartamentoId;
            }
        }
    }
}