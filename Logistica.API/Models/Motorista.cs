namespace Logistica.API.Models
{
    public class Motorista : Usuario
    {
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
        public string? CNH { get; set; }
        public string? Categoria { get; set; }

        public override string ToString()
        {
            return $"Motorista {Nome} - CNH: {CNH}";
        }
    }
}