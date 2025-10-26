using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolhaEsigAPI.Models
{
    [Table("pessoa_salario")]
    public class PessoaSalario
    {
        [Key]
        [ForeignKey("Pessoa")]
        [Column("pessoa_id")]
        public int PessoaId { get; set; }

        [Required]
        [Column("pessoa_nome")]
        public string PessoaNome { get; set; }

        [Required]
        [Column("cargo_nome")]
        public string CargoNome { get; set; }

        [Required]
        [Column("salario")]
        public decimal Salario { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
