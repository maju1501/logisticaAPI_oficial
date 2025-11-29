using Logistica.API.Data;
using Logistica.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Logistica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RotasController : ControllerBase
    {
        private readonly string _connectionString;

        public RotasController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // =========================================================
        // GET ALL ROTAS
        // =========================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rota>>> GetAll()
        {
            var lista = new List<Rota>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "SELECT * FROM rotas";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Rota
                {
                    Id = reader.GetInt32("Id"),
                    Origem = reader["Origem"]?.ToString(),
                    Destino = reader["Destino"]?.ToString(),
                    DistanciaKm = reader["DistanciaKm"] != DBNull.Value ? Convert.ToDouble(reader["DistanciaKm"]) : 0,
                });
            }

            return Ok(lista);
        }

        // =========================================================
        // GET BY ID
        // =========================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Rota>> GetById(int id)
        {
            Rota? rota = null;

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "SELECT * FROM rotas WHERE Id = @Id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                rota = new Rota
                {
                    Id = reader.GetInt32("Id"),
                    Origem = reader["Origem"]?.ToString(),
                    Destino = reader["Destino"]?.ToString(),
                    DistanciaKm = reader["DistanciaKm"] != DBNull.Value ? Convert.ToDouble(reader["DistanciaKm"]) : 0,
                };
            }

            if (rota == null)
                return NotFound();

            return Ok(rota);
        }

        // =========================================================
        // POST - CRIAR ROTA
        // =========================================================
        [HttpPost]
        public async Task<ActionResult> Create(Rota rota)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                INSERT INTO rotas 
                (Origem, Destino, DistanciaKm)
                VALUES (@Origem, @Destino, @DistanciaKm)";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Origem", rota.Origem);
            cmd.Parameters.AddWithValue("@Destino", rota.Destino);
            cmd.Parameters.AddWithValue("@DistanciaKm", rota.DistanciaKm);

            await cmd.ExecuteNonQueryAsync();

            return Ok(new { message = "Rota criada com sucesso!" });
        }

        // =========================================================
        // PUT - ATUALIZAR ROTA
        // =========================================================
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Rota rota)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                UPDATE rotas SET
                    Origem = @Origem,
                    Destino = @Destino,
                    DistanciaKm = @DistanciaKm
                WHERE Id = @Id";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Origem", rota.Origem);
            cmd.Parameters.AddWithValue("@Destino", rota.Destino);
            cmd.Parameters.AddWithValue("@DistanciaKm", rota.DistanciaKm);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Rota atualizada com sucesso!" });
        }

        // =========================================================
        // DELETE
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "DELETE FROM rotas WHERE Id = @Id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Rota excluída com sucesso!" });
        }
    }   
  }
