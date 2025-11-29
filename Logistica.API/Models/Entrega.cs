namespace Logistica.API.Models
{
    public class Entrega : BaseEntity
    {
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int MotoristaId { get; set; }
        public Motorista? Motorista { get; set; }

        public int CaminhaoId { get; set; }
        public Caminhao? Caminhao { get; set; }

        public int RotaId { get; set; }
        public Rota? Rota { get; set; }

        public decimal PesoCargaKg { get; set; }
        public DateTime DataPrevista { get; set; }
        public bool Concluida { get; set; }

        public string Descricao()
        {
            return $"Entrega para {Cliente?.Nome} via {Rota?.Resumo()} - Peso: {PesoCargaKg}kg";
        }
    }
}
