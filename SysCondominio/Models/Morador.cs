using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysCondominio.Models
{
    [Table("Sys_Moradores")]
    public class Morador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Apartamento")]
        public int ApartamentoId { get; set; }

        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        public string UsuCriacao { get; set; }

        public DateTime? DataCriacao { get; set; }

        [ForeignKey("ApartamentoId")]
        public virtual Apartamento Apartamento { get; set; }
    }
}