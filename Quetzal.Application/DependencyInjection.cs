//Objetivo: Classe de extensao para registro de dependencias da camada Application
//Facilira a configuracao do container de DI no Program.cs da API

using Microsoft.Extensions.DependencyInjection;
using Quetzal.Application.Mapeamentos;
using Quetzal.Application.Servicos.Implementacoes;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Domain.Interfaces;

namespace Quetzal.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AdicionarServicosAplicacao(this IServiceCollection services)
        {
            //registra os perfis do AutoMapper
            services.AddAutoMapper(opt => opt.AddProfile<PerfilMapeamento>());

            //registra os servicos da camada Application
            services.AddScoped<IAmbienteServico, AmbienteServico>();
            services.AddScoped<IPortfolioServico, PortfolioServico>();
            services.AddScoped<IProjetoCServico, ProjetoCServico>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            
            return services;

        }
    }
}

// Voltar aqui Sthefanny ↑