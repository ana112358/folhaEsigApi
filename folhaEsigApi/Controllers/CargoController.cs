using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FolhaEsigAPI.Data;
using FolhaEsigAPI.Models;

namespace FolhaEsigAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly FolhaEsigContext _context;

        public CargoController(FolhaEsigContext context)
        {
            _context = context;
        }

    
        [HttpPost("inserir")]
        public async Task<IActionResult> InserirCargo([FromBody] Cargo cargo)
        {
            if (cargo == null || string.IsNullOrEmpty(cargo.CargoNome))
                return BadRequest("Dados inválidos.");

            try
            {
                var sql = "CALL InserirCargo({0}, {1});";
                await _context.Database.ExecuteSqlRawAsync(sql, cargo.CargoNome, cargo.SalarioBase);

                return Ok("Cargo inserido com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inserir cargo: {ex.Message}");
            }
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Cargo>>> ListarCargos()
        {
            try
            {
            
                var cargos = await _context.Cargos
                    .FromSqlRaw("SELECT * FROM vw_listarcargos")
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(cargos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar cargos: {ex.Message}");
            }
        }
    }
}
