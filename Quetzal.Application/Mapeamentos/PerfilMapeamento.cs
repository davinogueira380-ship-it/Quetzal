using AutoMapper;
using Quetzal.Domain.Entidades;
using Quetzal.Application.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quetzal.Application.Mapeamentos;

public class PerfilMapeamento : Profile
{
    public PerfilMapeamento()
    {
        CreateMap<Ambiente, AmbienteDto>().ReverseMap();
        CreateMap<Projeto, PortfolioDto>().ReverseMap();
        CreateMap<ApplicationUser, UsuarioDto>().ReverseMap();

        // Só inclua se existir uma entidade Usuario no Domain.
    }
}