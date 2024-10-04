using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCondominio.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int TipoId { get; set; }
        public bool Bloqueio { get; set; }

        public virtual ICollection<Log> Logs { get; set; }
    }
}