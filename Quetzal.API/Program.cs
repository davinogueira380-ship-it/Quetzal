using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quetzal.Application.Mapeamentos;
using Quetzal.Domain.Entidades;
using Quetzal.Infrastructure;
using Quetzal.Infrastructure.Dados;

var builder = WebApplication.CreateBuilder(args);

// 1. Registra os serviços da infraestrutura (DbContext, Identity, Jwt)
builder.Services.AdicionarServicosDeInfraestrutura(builder.Configuration);

// 3. Configura a Autenticação via JWT
var chaveJwt = builder.Configuration["Jwt:Chave"] ?? "QuetzalChaveSecretaSuperSegura2026!";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(PerfilMapeamento));

builder.Services.AddDbContext<QuetzalContexto>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<QuetzalContexto>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Criação dos perfis padrão: deve ficar antes do app.Run()
using (var scope = app.Services.CreateScope())
{
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