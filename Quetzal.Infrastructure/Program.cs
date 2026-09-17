using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Infrastructure.Dados;
using Quetzal.Infrastructure.Repositorios;

namespace Quetzal.Infrastructure
{
    public static class InfrastructureExtensoes
    {
        public static IServiceCollection AdicionarServicosDeInfraestrutura(this IServiceCollection services, IConfiguration configuration)
        {
            //Registra o contexto do banco de dados usando SQL Server
            services.AddDbContext<QuetzalContexto>(options => options.UseSqlServer(configuration.GetConnectionString("QuetzalDB")));

     //       services.AddIdentityCore<ApplicationUser>(options =>
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Configurações de senha
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

            })

                .AddEntityFrameworkStores<QuetzalContexto>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAmbienteRepositorio, AmbienteRepositorio>();
            services.AddScoped<IPortfolioRepositorio, PortfolioRepositorio>();
            services.AddScoped<IProjetoCRepositorio, ProjetoCRepositorio>();
            

            return services;
        }


    }
}