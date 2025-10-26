using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolhaEsigAPI.Models
{
    [Table("cargo")]
    public class Cargo
    {
        [Key]
        [Column("cargo_id")]
        public int CargoId { get; set; }

        [Required]
        [Column("cargo_nome")]
        public string CargoNome { get; set; }

        [Required]
        [Column("salario_base")]
        public decimal SalarioBase { get; set; }
    }
}
