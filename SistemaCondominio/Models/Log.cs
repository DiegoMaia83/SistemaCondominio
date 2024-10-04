using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaCondominio.Models
{
    [Table("logs")]
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public int ApartamentoId { get; set; }
        public int MoradorId { get; set; }
        public int UsuarioId { get; set; }
        public string Ip { get; set; }
        public DateTime DataLog { get; set; }
        public string Descricao { get; set; }

        [ForeignKey("ApartamentoId")]
        public virtual Apartamento Apartamento { get; set; }

        [ForeignKey("MoradorId")]
        public virtual Morador Morador { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}