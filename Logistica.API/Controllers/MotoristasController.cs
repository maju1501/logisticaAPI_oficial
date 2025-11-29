using Logistica.API.Data;
using Logistica.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;

namespace Logistica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoristasController : ControllerBase
    {
        private readonly string _connectionString;

        public MotoristasController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // =========================================================
        // GET ALL MOTORISTAS
        // =========================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorista>>> GetAll()
        {
            var lista = new List<Motorista>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"SELECT * FROM usuarios WHERE Discriminator = 'Motorista'";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Motorista
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader["Nome"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Senha = reader["Senha"]?.ToString(),
                    Endereco = reader["Endereco"]?.ToString(),
                    Telefone = reader["Telefone"]?.ToString(),
                    CNH = reader["CNH"]?.ToString(),
                    Categoria = reader["Categoria"]?.ToString()
                });
            }

            return Ok(lista);
        }

        // =========================================================
        // GET BY ID
        // =========================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorista>> GetById(int id)
        {
            Motorista? motorista = null;

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"SELECT * FROM usuarios WHERE Id = @Id AND Discriminator = 'Motorista'";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                motorista = new Motorista
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader["Nome"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Senha = reader["Senha"]?.ToString(),
                    Endereco = reader["Endereco"]?.ToString(),
                    Telefone = reader["Telefone"]?.ToString(),
                    CNH = reader["CNH"]?.ToString(),
                    Categoria = reader["Categoria"]?.ToString()
                };
            }

            if (motorista == null)
                return NotFound();

            return Ok(motorista);
        }

        // =========================================================
        // POST - CRIAR MOTORISTA
        // =========================================================
        [HttpPost]
        public async Task<ActionResult> Create(Motorista motorista)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                INSERT INTO usuarios 
                (Nome, Email, Senha, Endereco, Telefone, CNH, Categoria, Discriminator)
                VALUES (@Nome, @Email, @Senha, @Endereco, @Telefone, @CNH, @Categoria, 'Motorista')";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Nome", motorista.Nome);
            cmd.Parameters.AddWithValue("@Email", motorista.Email);
            cmd.Parameters.AddWithValue("@Senha", motorista.Senha);
            cmd.Parameters.AddWithValue("@Endereco", motorista.Endereco);
            cmd.Parameters.AddWithValue("@Telefone", motorista.Telefone);
            cmd.Parameters.AddWithValue("@CNH", motorista.CNH);
            cmd.Parameters.AddWithValue("@Categoria", motorista.Categoria);

            await cmd.ExecuteNonQueryAsync();

            return Ok(new { message = "Motorista cadastrado com sucesso!" });
        }

        // =========================================================
        // PUT - ATUALIZAR MOTORISTA
        // =========================================================
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Motorista motorista)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                UPDATE usuarios SET
                    Nome = @Nome,
                    Email = @Email,
                    Senha = @Senha,
                    Endereco = @Endereco,
                    Telefone = @Telefone,
                    CNH = @CNH,
                    Categoria = @Categoria
                WHERE Id = @Id AND Discriminator = 'Motorista'";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nome", motorista.Nome);
            cmd.Parameters.AddWithValue("@Email", motorista.Email);
            cmd.Parameters.AddWithValue("@Senha", motorista.Senha);
            cmd.Parameters.AddWithValue("@Endereco", motorista.Endereco);
            cmd.Parameters.AddWithValue("@Telefone", motorista.Telefone);
            cmd.Parameters.AddWithValue("@CNH", motorista.CNH);
            cmd.Parameters.AddWithValue("@Categoria", motorista.Categoria);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Motorista atualizado com sucesso!" });
        }

        // =========================================================
        // DELETE
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "DELETE FROM usuarios WHERE Id = @Id AND Discriminator = 'Motorista'";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Motorista removido com sucesso!" });
        }
    }
}
