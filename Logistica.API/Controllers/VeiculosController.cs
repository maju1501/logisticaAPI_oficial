using Logistica.API.Data;
using Logistica.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Logistica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly string _connection;

        public VeiculosController(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection") ??
                         throw new ArgumentNullException("DefaultConnection não configurada");
        }

        // ==================================================================
        // GET - Lista todos os veículos
        // ==================================================================
        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = new List<Veiculo>();

            using (var con = new MySqlConnection(_connection))
            {
                con.Open();

                string sql = "SELECT Id, Placa, Marca, Capacidade, Discriminator FROM veiculos";
                using var cmd = new MySqlCommand(sql, con);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Veiculo()
                    {
                        Id = reader.GetInt32("Id"),
                        Placa = reader["Placa"]?.ToString(),
                        Marca = reader["Marca"]?.ToString(),
                        Capacidade = reader["Capacidade"] != DBNull.Value ? Convert.ToInt32(reader["Capacidade"]) : 0,
                        Discriminator = reader["Discriminator"]?.ToString()
                    });
                }
            }

            return Ok(lista);
        }

        // ==================================================================
        // GET por ID
        // ==================================================================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Veiculo? veiculo = null;

            using (var con = new MySqlConnection(_connection))
            {
                con.Open();

                string sql = "SELECT Id, Placa, Marca, Capacidade, Discriminator FROM veiculos WHERE Id=@id LIMIT 1";
                using var cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    veiculo = new Veiculo()
                    {
                        Id = reader.GetInt32("Id"),
                        Placa = reader["Placa"]?.ToString(),
                        Marca = reader["Marca"]?.ToString(),
                        Capacidade = reader["Capacidade"] != DBNull.Value ? Convert.ToInt32(reader["Capacidade"]) : 0,
                        Discriminator = reader["Discriminator"]?.ToString()
                    };
                }
            }

            if (veiculo == null)
                return NotFound();

            return Ok(veiculo);
        }

        // ==================================================================
        // POST - Criar veículo
        // ==================================================================
        [HttpPost]
        public IActionResult Create(Veiculo veiculo)
        {
            int novoId;

            using (var con = new MySqlConnection(_connection))
            {
                con.Open();

                string sql = @"INSERT INTO veiculos (Placa, Marca, Capacidade, Discriminator)
                               VALUES (@placa, @marca, @capacidade, @discriminator);
                               SELECT LAST_INSERT_ID();";

                using var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@placa", veiculo.Placa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@marca", veiculo.Marca ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@capacidade", veiculo.Capacidade);
                cmd.Parameters.AddWithValue("@discriminator", veiculo.Discriminator ?? (object)DBNull.Value);

                novoId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            veiculo.Id = novoId;
            return CreatedAtAction(nameof(GetById), new { id = novoId }, veiculo);
        }

        // ==================================================================
        // PUT - Atualizar veículo
        // ==================================================================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Veiculo veiculo)
        {
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();

                string sql = @"UPDATE veiculos 
                               SET Placa=@placa, Marca=@marca, Capacidade=@capacidade, Discriminator=@discriminator
                               WHERE Id=@id";

                using var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@placa", veiculo.Placa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@marca", veiculo.Marca ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@capacidade", veiculo.Capacidade);
                cmd.Parameters.AddWithValue("@discriminator", veiculo.Discriminator ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id", id);

                int afetados = cmd.ExecuteNonQuery();

                if (afetados == 0)
                    return NotFound();
            }

            return NoContent();
        }

        // ==================================================================
        // DELETE - Excluir veículo
        // ==================================================================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();

                string sql = "DELETE FROM veiculos WHERE Id=@id";
                using var cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", id);

                int afetados = cmd.ExecuteNonQuery();

                if (afetados == 0)
                    return NotFound();
            }

            return NoContent();
            }
        }
    }


