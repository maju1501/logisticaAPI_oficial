namespace Logistica.API.Models
{
    public class Caminhao : Veiculo
    {
        public string? TipoCarroceria { get; set; }

        public override string Informacoes()
        {
            return $"Caminhão {Marca} {Placa} - Carroceria: {TipoCarroceria} - Capacidade: {Capacidade}kg";
        }
    }
}
