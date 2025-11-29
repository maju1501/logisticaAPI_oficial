namespace Logistica.API.Models
{
    public class Veiculo : BaseEntity
    {
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public int Capacidade { get; set; } // em 
        public string? Discriminator { get; set; }
        public virtual string Informacoes()
        {
            return $"{Marca} - {Placa} (capacidade: {Capacidade}kg)";
        }
    }
}