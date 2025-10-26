using Microsoft.EntityFrameworkCore;
using FolhaEsigAPI.Models;

namespace FolhaEsigAPI.Data
{
    public class FolhaEsigContext : DbContext
    {
        public FolhaEsigContext(DbContextOptions<FolhaEsigContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<PessoaSalario> PessoaSalarios { get; set; }
    }
}
