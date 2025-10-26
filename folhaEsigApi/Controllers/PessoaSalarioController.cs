using FolhaEsigAPI.Data;
using FolhaEsigAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;

namespace FolhaEsigAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaSalarioController : ControllerBase
    {
        private readonly FolhaEsigContext _context;
        private readonly string _connStr;

        public PessoaSalarioController(FolhaEsigContext context, IConfiguration configuration)
        {
            _context = context;
            _connStr = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string 'DefaultConnection' não encontrada no appsettings.json");
        }

        // GET: api/PessoaSalario/listar?ordem=nome
        [HttpGet("listar")]
        public async Task<IActionResult> ListarPessoaSalario([FromQuery] string ordem = "nome")
        {
            try
            {
                using (var conn = new MySqlConnection(_connStr))
                {
                    await conn.OpenAsync();

                    using (var cmd = new MySqlCommand("CALL ListarPessoaSalario(@ordem);", conn))
                    {
                        cmd.Parameters.AddWithValue("@ordem", ordem);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);

                            // Converte o DataTable em lista para JSON
                            var lista = new List<Dictionary<string, object>>();
                            foreach (DataRow row in dt.Rows)
                            {
                                var item = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                    item[col.ColumnName] = row[col];
                                lista.Add(item);
                            }

                            return Ok(lista);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar salários: {ex.Message}");
            }
        }

        // POST: api/PessoaSalario/calcular
        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularSalarios()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("CALL AtualizarPessoaSalario();");
                return Ok("Salários calculados com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao calcular salários: {ex.Message}");
            }
        }
    }
}
