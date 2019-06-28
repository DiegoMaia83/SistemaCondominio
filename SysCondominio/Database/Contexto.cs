using SysCondominio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SysCondominio.Database
{
    public class Contexto : DbContext
    {
        public Contexto() : base("name=Sys_DbConnection")
        {

        }

        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<Bloco> Blocos { get; set; }
        public DbSet<Morador> Moradores { get; set; }
    }
}