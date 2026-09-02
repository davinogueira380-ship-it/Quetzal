using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quetzal.Application.DTOs;
using Quetzal.Domain.Entidades;

namespace Quetzal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetosController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ProjetosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var projetos = new List<Projeto>
            {
                new Projeto
                {
                    Id = 1,
                    NomeProjeto = "Sistema Quetzal",
                    Descricao = "Projeto de exemplo"
                }
            };

            var projetosDto = _mapper.Map<List<ProjetoDto>>(projetos);

            return Ok(projetosDto);
        }
    }
}