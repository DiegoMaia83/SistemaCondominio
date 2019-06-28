using SysCondominio.Models;
using SysCondominio.Repositorio;
using SysCondominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysCondominio.Aplicacao
{
    public class ComumAplicacao
    {
        public List<SelectListItem> RetornaListaDeApartamentos()
        {
            var listaDeApartamentos = RetornaApartamentos().ToList().OrderBy(x => x.Bloco.Nome).ThenBy(x => x.Numero);

            var listSelectListItems = new List<SelectListItem>();

            // Insere uma linha em branco
            var selectList = new SelectListItem { Text = "", Value = "", Selected = false };
            listSelectListItems.Add(selectList);

            foreach (Apartamento ap in listaDeApartamentos)
            {
                selectList = new SelectListItem
                {
                    Text = ap.Bloco.Nome + " - " + ap.Numero.ToString(),
                    Value = ap.ApartamentoId.ToString()
                };

                listSelectListItems.Add(selectList);
            }
            return listSelectListItems;
        }

        public List<SelectListItem> RetornaListaDeBlocos()
        {
            var listaDeBlocos = RetornaBlocos().ToList();

            var listSelectListItems = new List<SelectListItem>();

            //Insere uma linha em branco
            var selectList = new SelectListItem { Text = "", Value = "", Selected = false };
            listSelectListItems.Add(selectList);

            foreach (Bloco a in listaDeBlocos)
            {
                selectList = new SelectListItem
                {
                    Text = a.Nome,
                    Value = a.BlocoId.ToString()
                };

                listSelectListItems.Add(selectList);
            }
            return listSelectListItems;
        }

        public IEnumerable<Bloco> RetornaBlocos()
        {
            using (var bloco = new BlocoRepositorio())
            {
                return bloco.GetAll();
            }
        }

        public IEnumerable<Apartamento> RetornaApartamentos()
        {
            using (var apartamento = new ApartamentoRepositorio())
            {
                return apartamento.GetAll();
            }
        }
    }
}