using Microsoft.EntityFrameworkCore;
using Logistica.API.Models;

namespace Logistica.API.Data
{
    public class LogisticaContext : DbContext
    {
        public LogisticaContext(DbContextOptions<LogisticaContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Motorista> Motoristas { get; set; } = null!;
        public DbSet<Veiculo> Veiculos { get; set; } = null!;
        public DbSet<Caminhao> Caminhoes { get; set; } = null!;
        public DbSet<Rota> Rotas { get; set; } = null!;
        public DbSet<Entrega> Entregas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().ToTable("usuario");


            // Configure TPH for Veiculo (store Caminhao in Caminhoes table)
            modelBuilder.Entity<Veiculo>()
                .HasDiscriminator<string>("VeiculoTipo")
                .HasValue<Caminhao>("Caminhao");
        }
    }
}
