using AutoMapper;
using Quetzal.Application.DTOs;
using Quetzal.Domain.Entidades;

namespace Quetzal.Application.Mapeamentos
{
    public class PerfilMapeamento : Profile
    {
        public PerfilMapeamento()
        {
            CreateMap<Portfolio, PortfolioDto>()
                .ForMember(
                    dest => dest.NomeProjeto,
                    opt => opt.MapFrom(
                        src => src.Ambiente != null
                            ? src.Ambiente.Nome
                            : string.Empty))
                .ForMember(
                    dest => dest.DataAtualizacao,
                    opt => opt.MapFrom(src => src.DataExclusao));

            CreateMap<CriarPortfolioDto, Portfolio>()
                .ForMember(
                    dest => dest.DataCriacao,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                    dest => dest.Ativo,
                    opt => opt.MapFrom(src => true))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Ambiente,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataAtualizacao,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataExclusao,
                    opt => opt.Ignore());

            CreateMap<AtualizarPortfolioDto, Portfolio>()
                .ForMember(
                    dest => dest.Ambiente,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataCriacao,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataAtualizacao,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataExclusao,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Ativo,
                    opt => opt.Ignore());

            CreateMap<Ambiente, AmbienteDto>()
                .ForMember(
                    dest => dest.TotalProjetos,
                    opt => opt.MapFrom(
                        src => src.Portfolios != null
                            ? src.Portfolios.Count
                            : 0));

            CreateMap<CriarAmbienteDto, Ambiente>()
                .ForMember(
                    dest => dest.DataCadastro,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                    dest => dest.Ativo,
                    opt => opt.MapFrom(src => true))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Portfolios,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataAtualizacao,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataExclusao,
                    opt => opt.Ignore());

            CreateMap<ApplicationUser, UsuarioDto>()
                .ForMember(
                    dest => dest.Perfis,
                    opt => opt.Ignore());

            CreateMap<RegistrarUserDto, ApplicationUser>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(
                    dest => dest.DataCadastro,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(
                    dest => dest.Ativo,
                    opt => opt.MapFrom(src => false));

            CreateMap<ProjetoC, ProjetoCDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(
                        src => src.NomeProjeto ?? string.Empty))
                .ForMember(
                    dest => dest.UsuarioId,
                    opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(
                    dest => dest.UsuarioNome,
                    opt => opt.MapFrom(
                        src => src.Usuario != null
                            ? src.Usuario.NomeCompleto
                            : string.Empty))
                .ForMember(
                    dest => dest.Fotos,
                    opt => opt.MapFrom(
                        src => src.Fotos
                            .OrderBy(f => f.Ordem)
                            .Select(f => f.Foto)
                            .ToList()));

            CreateMap<CriarProjetoCDto, ProjetoC>()
                .ForMember(
                    dest => dest.NomeProjeto,
                    opt => opt.MapFrom(src => src.Nome))
                .ForMember(
                    dest => dest.Usuario,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.UsuarioId,
                    opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(
                    dest => dest.Fotos,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataAtualizacao,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataExclusao,
                    opt => opt.Ignore());

            CreateMap<AtualizarProjetoCDto, ProjetoC>()
                .ForMember(
                    dest => dest.NomeProjeto,
                    opt => opt.MapFrom(src => src.Nome))
                .ForMember(
                    dest => dest.Usuario,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.UsuarioId,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Fotos,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataAtualizacao,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DataExclusao,
                    opt => opt.Ignore());
        }
    }
}