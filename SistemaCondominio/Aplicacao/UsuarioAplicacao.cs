using SistemaCondominio.Models;
using SistemaCondominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCondominio.Aplicacao
{
    public class UsuarioAplicacao
    {
        public List<Usuario> RetornarTodos()
        {
            using(var usuarios = new UsuarioRepositorio())
            {
                var i = usuarios.GetAll().ToList();

                return i;
            }
        }

        public Usuario Retornar(int usuarioId)
        {
            try
            {
                using (var usuarios = new UsuarioRepositorio())
                {
                    return usuarios.GetAll().Where(x => x.UsuarioId == usuarioId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}