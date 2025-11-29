namespace Logistica.API.Models
{
    public class Cliente : Usuario
    {
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }

        public string ObterResumo()
        {
            return $"Cliente: {Nome} - {Email}";
        }
    }
}
