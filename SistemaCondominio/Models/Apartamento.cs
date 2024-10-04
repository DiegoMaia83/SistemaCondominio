using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaCondominio.Models
{
    [Table("apartamento")]
    public class Apartamento
    {
        [Key]
        public int ApartamentoId { get; set; }
        public int Numero { get; set; }
        public int Andar { get; set; }
        public int BlocoId { get; set; }
        public bool Ocupado { get; set; }
        public string UsuCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UsuAlteracao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Observacao { get; set; }

        [ForeignKey("BlocoId")]
        public virtual ApartamentoBloco Bloco { get; set; }

        public virtual ICollection<Log> Logs { get; set; }

        public virtual ICollection<MoradorOcupacao> Ocupacao { get; set; }

        public virtual ICollection<ApartamentoAndamento> Andamento { get; set; }

        public virtual ICollection<ApartamentoVaga> Vaga { get; set; }
    }

    [Table("apartamento_bloco")]
    public class ApartamentoBloco
    {
        [Key]
        public int BlocoId { get; set; }
        public string Nome { get; set; }
        public string Torre { get; set; }

        public virtual ICollection<Apartamento> Apartamentos { get; set; }
    }

    [Table("apartamento_andamento")]
    public class ApartamentoAndamento
    {
        [Key]
        public int AndamentoId { get; set; }
        public int ApartamentoId { get; set; }
        public string UsuAndamento { get; set; }
        public DateTime DataAndamento { get; set; }
        public string Descricao { get; set; }

        [ForeignKey("ApartamentoId")]
        public virtual Apartamento Apartamento { get; set; }
    }

    [Table("apartamento_vaga")]
    public class ApartamentoVaga
    {
        [Key]
        public int VagaId { get; set; }
        public int Numero { get; set; }
        public string Piso { get; set; }
        public int? ApartamentoId { get; set; }

        [ForeignKey("ApartamentoId")]
        public virtual Apartamento Apartamento { get; set; }
    }
}