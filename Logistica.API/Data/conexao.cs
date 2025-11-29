namespace Logistica.API.Data
{
    public static class Conexao
    {
        private static IConfiguration _configuration;

        // Método chamado no Program.cs para carregar a configuração
        public static void Inicializar(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Propriedade pública e estática para uso em todo o sistema
        public static string ConnectionString =>
            _configuration.GetConnectionString("DefaultConnection");
    }
}
