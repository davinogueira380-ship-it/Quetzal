using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quetzal.Application.Mapeamentos;
using Quetzal.Application.Servicos;
using Quetzal.Application.Servicos.Implementacoes;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Infrastructure;
using Quetzal.Infrastructure.Dados;

var builder = WebApplication.CreateBuilder(args);

// Registra DbContext, Identity e demais serviços da Infrastructure
builder.Services.AdicionarServicosDeInfraestrutura(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(PerfilMapeamento));

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAmbienteServico, AmbienteServico>();
builder.Services.AddScoped<IPortfolioServico, PortfolioServico>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Cria o banco e aplica as migrations pendentes
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<QuetzalContexto>();

    await context.Database.MigrateAsync();

    // Cria os perfis padrão
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Cliente", "Administrador" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();