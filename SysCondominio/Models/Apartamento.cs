using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysCondominio.Models
{
    [Table("Sys_Apartamentos")]
    public class Apartamento
    {
        [Key]
        public int ApartamentoId { get; set; }

        [Required]
        [Display(Name = "Número")]
        public int? Numero { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Required]
        [Display(Name = "Bloco")]
        public int BlocoId { get; set; }

        public string UsuCriacao { get; set; }

        public DateTime? DataCriacao { get; set; }

        [ForeignKey("BlocoId")]
        public virtual Bloco Bloco { get; set; }

        
        public virtual ICollection<Morador> Moradores { get; set; }
    }
}