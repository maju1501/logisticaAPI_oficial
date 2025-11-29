using Logistica.API.Data;
using Logistica.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Logistica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly string _connectionString;

        public ClientesController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // ================================================================
        // GET ALL CLIENTES
        // ================================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            var lista = new List<Cliente>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"SELECT * FROM usuarios WHERE Discriminator = 'Cliente'";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Cliente
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader["Nome"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Senha = reader["Senha"]?.ToString(),
                    Endereco = reader["Endereco"]?.ToString(),
                    Telefone = reader["Telefone"]?.ToString()
                });
            }

            return Ok(lista);
        }

        // ================================================================
        // GET BY ID
        // ================================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            Cliente cliente = null!;

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"SELECT * FROM usuarios WHERE Id = @Id AND Discriminator = 'Cliente'";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                cliente = new Cliente
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader["Nome"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Senha = reader["Senha"]?.ToString(),
                    Endereco = reader["Endereco"]?.ToString(),
                    Telefone = reader["Telefone"]?.ToString()
                };
            }

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // ================================================================
        // POST - CRIAR CLIENTE
        // ================================================================
        [HttpPost]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                INSERT INTO usuarios 
                (Nome, Email, Senha, Endereco, Telefone, Discriminator)
                VALUES (@Nome, @Email, @Senha, @Endereco, @Telefone, 'Cliente')";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Email", cliente.Email);
            cmd.Parameters.AddWithValue("@Senha", cliente.Senha);
            cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

            await cmd.ExecuteNonQueryAsync();

            return Ok(new { message = "Cliente cadastrado com sucesso!" });
        }

        // ================================================================
        // PUT - ATUALIZAR CLIENTE
        // ================================================================
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Cliente cliente)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                UPDATE usuarios SET
                    Nome = @Nome,
                    Email = @Email,
                    Senha = @Senha,
                    Endereco = @Endereco,
                    Telefone = @Telefone
                WHERE Id = @Id AND Discriminator = 'Cliente'";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            cmd.Parameters.AddWithValue("@Email", cliente.Email);
            cmd.Parameters.AddWithValue("@Senha", cliente.Senha);
            cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Cliente atualizado com sucesso!" });
        }

        // ================================================================
        // DELETE
        // ================================================================
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "DELETE FROM usuarios WHERE Id = @Id AND Discriminator = 'Cliente'";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Cliente removido com sucesso!" });
        }
    }
 }


    

