using Logistica.API.Data;
using Logistica.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Logistica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {

        private readonly string _connection;

        public UsuariosController(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }

        // =========================================================
        // GET ALL USUÁRIOS
        // =========================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var lista = new List<Usuario>();

            using var conn = new MySqlConnection(_connection);
            await conn.OpenAsync();

            string sql = "SELECT * FROM usuarios";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Usuario
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader["Nome"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Senha = reader["Senha"]?.ToString(),
                });
            }

            return Ok(lista);
        }

        // =========================================================
        // GET BY ID
        // =========================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            Usuario usuario = null!;

            using var conn = new MySqlConnection(_connection);
            await conn.OpenAsync();

            string sql = "SELECT * FROM usuarios WHERE Id = @Id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                usuario = new Usuario
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader["Nome"]?.ToString(),
                    Email = reader["Email"]?.ToString(),
                    Senha = reader["Senha"]?.ToString(),
                };
            }

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // =========================================================
        // POST - CRIAR USUÁRIO
        // =========================================================
        [HttpPost]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            using var conn = new MySqlConnection(_connection);
            await conn.OpenAsync();

            string sql = @"
                INSERT INTO usuarios 
                (Nome, Email, Senha, Discriminator)
                VALUES (@Nome, @Email, @Senha, 'Usuario')";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

            await cmd.ExecuteNonQueryAsync();

            return Ok(new { message = "Usuário criado com sucesso!" });
        }

        // =========================================================
        // PUT - ATUALIZAR USUÁRIO
        // =========================================================
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Usuario usuario)
        {
            using var conn = new MySqlConnection(_connection);
            await conn.OpenAsync();

            string sql = @"
                UPDATE usuarios SET
                    Nome = @Nome,
                    Email = @Email,
                    Senha = @Senha
                WHERE Id = @Id";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Usuário atualizado com sucesso!" });
        }

        // =========================================================
        // DELETE
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var conn = new MySqlConnection(_connection);
            await conn.OpenAsync();

            string sql = "DELETE FROM usuarios WHERE Id = @Id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            int result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound();

            return Ok(new { message = "Usuário excluído com sucesso!" });
        }


    }
}
