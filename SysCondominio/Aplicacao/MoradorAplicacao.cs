using SysCondominio.Models;
using SysCondominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysCondominio.Aplicacao
{
    public class MoradorAplicacao
    {
        public IEnumerable<Morador> List()
        {
            using(var moradores = new MoradorRepositorio())
            {
                return moradores.GetAll();
            }
        }

        public int Insert (Morador obj)
        {
            using(var morador = new MoradorRepositorio())
            {
                morador.Adicionar(obj);
                morador.SalvarTodos();

                return obj.Id;
            }
        }
    }
}