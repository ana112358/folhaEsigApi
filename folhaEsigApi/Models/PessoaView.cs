using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace folhaEsigApi.Models;
public class PessoaView
{
    [Key]
    [Column("pessoa_id")]
    public int PessoaId { get; set; }

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
    public string DataNascimento { get; set; }

    [Column("cargo_nome")]
    public string CargoNome { get; set; }

    [Column("salario")]
    public decimal? Salario { get; set; }
}
