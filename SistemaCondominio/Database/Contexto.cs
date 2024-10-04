using SistemaCondominio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaCondominio.Database
{
    public class Contexto : DbContext
    {
        public Contexto()
           : base("name=MySql")
        {
            //Arquivo ConnectionStrings.config
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Apartamento> Apartamento { get; set; }
        public DbSet<ApartamentoBloco> Bloco { get; set; }
        public DbSet<ApartamentoAndamento> ApartamentoAndamento { get; set; }
        public DbSet<ApartamentoVaga> Vaga { get; set; }
        public DbSet<Morador> Morador { get; set; }
        public DbSet<MoradorOcupacao> Ocupacao { get; set; }
        public DbSet<Log> Logs { get; set; }       

    }
}