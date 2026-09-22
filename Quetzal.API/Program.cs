
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quetzal.Application.Mapeamentos;
using Quetzal.Application.Servicos.Implementacoes;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Infrastructure;
using Quetzal.Infrastructure.Dados;
using Quetzal.Application.Servicos;

var builder = WebApplication.CreateBuilder(args);

// ================================================================
// INFRASTRUCTURE
// ================================================================

builder.Services.AdicionarServicosDeInfraestrutura(
    builder.Configuration);

builder.Services.AddDataProtection();

// ================================================================
// CONTROLLERS
// ================================================================

builder.Services.AddControllers();

builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();

// ================================================================
// SWAGGER
// ================================================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ================================================================
// AUTOMAPPER
// ================================================================

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(PerfilMapeamento));

// ================================================================
// AUTHORIZATION
// ================================================================

builder.Services.AddAuthorization();

// ================================================================
// SERVIÇOS DA APPLICATION
// ================================================================

builder.Services.AddScoped<IAmbienteServico, AmbienteServico>();
builder.Services.AddScoped<IPortfolioServico, PortfolioServico>();
builder.Services.AddScoped<IProjetoCServico, ProjetoCServico>();

// ================================================================
// CONSTRUÇÃO DA APLICAÇÃO
// ================================================================

var app = builder.Build();

// ================================================================
// SWAGGER
// ================================================================

app.UseSwagger();
app.UseSwaggerUI();

// ================================================================
// HTTPS
// ================================================================

app.UseHttpsRedirection();

// ================================================================
// AUTENTICAÇÃO E AUTORIZAÇÃO
// ================================================================

app.UseAuthentication();
app.UseAuthorization();

// ================================================================
// CONTROLLERS
// ================================================================

app.MapControllers();

// ================================================================
// BANCO DE DADOS E SEED
// ================================================================

using (var scope = app.Services.CreateScope())
{
    await SeedDados.InicializarAsync(scope.ServiceProvider);
}

// ================================================================
// EXECUÇÃO DA APLICAÇÃO
// ================================================================

app.Run();

