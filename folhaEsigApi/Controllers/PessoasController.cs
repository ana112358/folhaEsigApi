using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FolhaEsigAPI.Data;
using FolhaEsigAPI.Models;

namespace FolhaEsigAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly FolhaEsigContext _context;

        public PessoasController(FolhaEsigContext context)
        {
            _context = context;
        }

        // GET: api/Pessoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaSalario>>> GetPessoas()
        {
            // Retorna a view vw_ListaPessoas
            try
            {
               
                var cargos = await _context.Cargos
                    .FromSqlRaw("SELECT * FROM vw_ListarPessoas")
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(cargos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar cargos: {ex.Message}");
            }
        }
        // POST: api/Pessoa/inserir
        [HttpPost("inserir")]
        public async Task<IActionResult> InserirPessoa([FromBody] Pessoa pessoa)
        {
            if (pessoa == null || string.IsNullOrEmpty(pessoa.Nome))
                return BadRequest("Dados inválidos.");

            try
            {
                // Chamada da procedure InserirPessoa
                var sql = @"
                    CALL InserirPessoa(
                        {0}, {1}, {2}, {3}, {4}, 
                        {5}, {6}, {7}, {8}, {9}
                    );
                ";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    pessoa.Nome,
                    pessoa.Cidade,
                    pessoa.Email,
                    pessoa.Cep,
                    pessoa.Endereco,
                    pessoa.Pais,
                    pessoa.Usuario,
                    pessoa.Telefone,
                    pessoa.DataNascimento,
                    pessoa.CargoId
                );

                return Ok("Pessoa inserida com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inserir pessoa: {ex.Message}");
            }
        }

        // DELETE: api/Pessoas/Deletar/{id}
        [HttpDelete("/Deletar/{id}")]
        public async Task<IActionResult> RemoverPessoa(int id)
        {
            // Chama a procedure "RemoverPessoa"
            var result = await _context.Database.ExecuteSqlRawAsync(
                "CALL RemoverPessoa({0})", id
            );

            if (result == 0)
            {
                return NotFound(new { message = "Pessoa não encontrada ou não foi possível remover." });
            }

            return Ok(new { message = "Pessoa removida com sucesso." });
        }

        // POST: api/Pessoas/AtualizarPessoaSalario
        [HttpPost("AtualizarPessoaSalario")]
        public async Task<IActionResult> RecalcularSalarios()
        {
            // Chama a procedure "AtualizarPessoaSalario"
            await _context.Database.ExecuteSqlRawAsync("CALL AtualizarPessoaSalario()");

            return Ok(new { message = "Salários recalculados com sucesso." });
        }
    }
}
