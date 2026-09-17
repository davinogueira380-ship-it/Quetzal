
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quetzal.Application.Mapeamentos;
using Quetzal.Application.Servicos.Implementacoes;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Infrastructure;
using Quetzal.Infrastructure.Dados;

var builder = WebApplication.CreateBuilder(args);

// INFRASTRUCTURE

builder.Services.AdicionarServicosDeInfraestrutura(builder.Configuration);
builder.Services.AddDataProtection(); //Adicionado Kelly 16-09

// CONTROLLERS

builder.Services.AddControllers();


// SWAGGER

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// AUTOMAPPER

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(PerfilMapeamento));


// AUTHORIZATION

builder.Services.AddAuthorization();


// SERVIÇOS DA APPLICATION

builder.Services.AddScoped<IAmbienteServico, AmbienteServico>();
builder.Services.AddScoped<IPortfolioServico, PortfolioServico>();


// CONSTRUÇÃO DA APLICAÇÃO

var app = builder.Build();


// SWAGGER

app.UseSwagger();
app.UseSwaggerUI();


// HTTPS

app.UseHttpsRedirection();


// AUTENTICAÇÃO E AUTORIZAÇÃO

app.UseAuthentication();
app.UseAuthorization();


// CONTROLLERS

app.MapControllers();


// BANCO DE DADOS E ROLES

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<QuetzalContexto>();

    await context.Database.MigrateAsync();


    // Cria os perfis padrão
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[]
    {
        "Cliente",
        "Administrador"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(
                new IdentityRole(role));
        }
    }
}


app.Run();

