using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysCondominio.Models
{
    [Table("Sys_Blocos")]
    public class Bloco
    {
        [Key]
        public int BlocoId { get; set; }
        public string Nome { get; set; }
        public string Torre { get; set; }

        [ForeignKey("BlocoId")]
        public virtual ICollection<Apartamento> Apartamentos { get; set; }

    }
}