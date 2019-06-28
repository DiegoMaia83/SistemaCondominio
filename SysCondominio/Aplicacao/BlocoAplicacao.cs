using SysCondominio.Models;
using SysCondominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysCondominio.Aplicacao
{
    public class BlocoAplicacao
    {
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
    }
}