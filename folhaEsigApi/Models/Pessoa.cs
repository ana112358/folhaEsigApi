using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolhaEsigAPI.Models
{
    [Table("pessoa")]
    public class Pessoa
    {
        [Key]
        [Column("pessoa_id")]
        public int PessoaId { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("cep")]
        public string Cep { get; set; }

        [Column("endereco")]
        public string Endereco { get; set; }

        [Column("pais")]
        public string Pais { get; set; }

        [Column("usuario")]
        public string Usuario { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("data_nascimento")]
        public DateTime? DataNascimento { get; set; }

        [ForeignKey("Cargo")]
        [Column("cargo_id")]
        public int CargoId { get; set; }

        public Cargo Cargo { get; set; }
    }
}
