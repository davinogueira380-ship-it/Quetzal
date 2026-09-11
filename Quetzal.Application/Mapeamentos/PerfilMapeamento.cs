using AutoMapper;
using Quetzal.Application.DTOs;
using Quetzal.Domain.Entidades;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quetzal.Application.Mapeamentos;

public class PerfilMapeamento : Profile
{
    public PerfilMapeamento()
    {
        //=======================================================================================
        // Criação de mapeamentos para o Portfolio e DTOs relacionados a projetos
        //=======================================================================================

        CreateMap<Ambiente, AmbienteDto>().ReverseMap();
        CreateMap<Portfolio, PortfolioDto>()
                // Mapeia o nome da categoria vindo do relacionamento
                .ForMember(dest => dest.NomeProjeto, opt => opt.MapFrom(src => src.Ambiente != null ? src.Ambiente.Nome : string.Empty))
                // DataAtualizacao e DataExclusao sao mapeados diretamente (mesmos nomes)
                .ForMember(dest => dest.DataAtualizacao, opt => opt.MapFrom(src => src.DataExclusao)); // int -> int, formatacao feita na ViewModel/View

        // DTO -> Entidade (Criacao)
        CreateMap<CriarPortfolioDto, Portfolio>()
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => true))
            // Ignora campos que nao vem do DTO e sao gerados/gerenciados automaticamente
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Ambiente, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataExclusao, opt => opt.Ignore());

        // DTO -> Entidade (Atualizacao)
        CreateMap<AtualizarPortfolioDto, Portfolio>()
            .ForMember(dest => dest.Ambiente, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataExclusao, opt => opt.Ignore())
            .ForMember(dest => dest.Ativo, opt => opt.Ignore());

        //=======================================================================================
        // Criação de mapeamentos para o Ambiente e DTOs relacionados a ambientes
        //=======================================================================================

        CreateMap<Ambiente, AmbienteDto>()
                // Conta o numero de filmes ativos vinculados a esta categoria
                .ForMember(dest => dest.TotalProjetos, opt => opt.MapFrom(src => src.Portfolios != null ? src.Portfolios.Count : 0));

        CreateMap<CriarAmbienteDto, Ambiente>()
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Portfolios, opt => opt.Ignore())
            .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.DataExclusao, opt => opt.Ignore());

        //=======================================================================================
        // Criação de mapeamentos para o ApplicationUser e DTOs relacionados a usuários
        //=======================================================================================
        CreateMap<ApplicationUser, UsuarioDto>()
              // Perfis deverao ser preenchidos manualmente pelo servico, pois o Identity gerencia as roles de forma complexa
              .ForMember(dest => dest.Perfis, opt => opt.Ignore());

        CreateMap<RegistrarUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // UserName no Identity sera o Email
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => false));

        //=======================================================================================
        //
        //=======================================================================================
        public class ProjetoCProfile : Profile
    {
        public ProjetoCProfile()
        {
            CreateMap<ProjetoC, ProjetoCDto>()
                .ForMember(dest => dest.Nome,
                    opt => opt.MapFrom(src => src.NomeProjeto ?? string.Empty))
                .ForMember(dest => dest.AmbienteId,
                    opt => opt.MapFrom(src => src.Ambientes
                        .Select(a => a.Id)
                        .FirstOrDefault()))
                .ForMember(dest => dest.AmbienteNome,
                    opt => opt.MapFrom(src => src.Ambientes
                        .Select(a => a.Nome)
                        .FirstOrDefault() ?? string.Empty));

            CreateMap<CriarProjetoCDto, ProjetoC>()
                .ForMember(dest => dest.NomeProjeto,
                    opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Ambientes,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Usuario,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioId,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao,
                    opt => opt.Ignore())
                .ForMember(dest => dest.DataExclusao,
                    opt => opt.Ignore());

            CreateMap<AtualizarProjetoCDto, ProjetoC>()
                .ForMember(dest => dest.NomeProjeto,
                    opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Ambientes,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Usuario,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioId,
                    opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao,
                    opt => opt.Ignore())
                .ForMember(dest => dest.DataExclusao,
                    opt => opt.Ignore());
        }
    }
}
