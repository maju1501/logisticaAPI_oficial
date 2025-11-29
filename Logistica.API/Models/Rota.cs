namespace Logistica.API.Models
{
    public class Rota : BaseEntity
    {
        public string? Origem { get; set; }
        public string? Destino { get; set; }
        public double DistanciaKm { get; set; }

        public string Resumo()
        {
            return $"{Origem} -> {Destino} ({DistanciaKm} km)";
        }
    }
}
