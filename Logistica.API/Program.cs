using Logistica.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------
// 1. Controllers
// -----------------------------------------------------------
builder.Services.AddControllers();

// -----------------------------------------------------------
// 2. Banco de Dados - MySQL (banco: logistica)
// -----------------------------------------------------------
// Necessário: Pomelo.EntityFrameworkCore.MySql
builder.Services.AddDbContext<LogisticaContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 40))
    )
);

// -----------------------------------------------------------
// 3. CORS
// -----------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// -----------------------------------------------------------
// 4. Swagger
// -----------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Logistica API",
        Version = "v1",
        Description = "API de Logística desenvolvida em C# .NET"
    });
});

var app = builder.Build();

// Ativa CORS
app.UseCors("AllowAll");

// -----------------------------------------------------------
// 5. Swagger em Desenvolvimento
// -----------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logistica API v1");
        c.RoutePrefix = string.Empty;
    });
}

// -----------------------------------------------------------
// 6. Pipeline
// -----------------------------------------------------------
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();