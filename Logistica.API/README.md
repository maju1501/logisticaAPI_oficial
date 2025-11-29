# Logistica.API
Projeto de exemplo para disciplina — API Web em C# (.NET 8) usando:
- POO: herança, polimorfismo, encapsulamento
- Tratamento de exceções
- Persistência: Entity Framework Core + SQLite
- Swagger para documentação / testes
- Endpoints prévios para Usuário, Cliente, Motorista, Veículo, Caminhão, Rota, Entrega

Instruções rápidas:
1. Abra o terminal na pasta Logistica.API
2. Execute: dotnet restore
3. Execute: dotnet build
4. (Opcional) Para criar migrações use EF Tools: dotnet ef migrations add InitialCreate
5. Execute: dotnet run
6. Abra: https://localhost:5001 para ver o Swagger UI
