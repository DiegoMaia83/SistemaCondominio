using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCondominio.Models
{
    [Table("morador")]
    public class Morador
    {
        [Key]
        public int MoradorId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UsuCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string UsuAlteracao { get; set; }        

        public virtual ICollection<Log> Logs { get; set; }

        public virtual ICollection<MoradorOcupacao> Ocupacao { get; set; }
    }

    [Table("morador_ocupacao")]
    public class MoradorOcupacao
    {
        [Key]
        public int OcupacaoId { get; set; }
        public int MoradorId { get; set; }
        public int ApartamentoId { get; set; }
        public string UsuInclusao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string UsuExclusao { get; set; }
        public DateTime DataExclusao { get; set; }
        public bool Excluido { get; set; }

        [ForeignKey("MoradorId")]
        public virtual Morador Morador { get; set; }

        [ForeignKey("ApartamentoId")]
        public virtual Apartamento Apartamento { get; set; }
    }
}