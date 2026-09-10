using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Domain.Entidades;

namespace Quetzal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioServico _portfolioServico;

    public PortfolioController(IPortfolioServico portfolioServico)
    {
        _portfolioServico = portfolioServico;
    }

    // Endpoint publico para visitantes verem o catalogo (apenas ativos)
    [HttpGet]
    public async Task<IActionResult> ObterAtivos()
    {
        var resposta = await _portfolioServico.ObterTodosAsync(incluirInativos: false);
        return Ok(resposta);
    }

    // Endpoint administrativo para listar todos os portfolios
    [HttpGet("todos")]
    public async Task<IActionResult> ObterTodos()
    {
        var resposta = await _portfolioServico.ObterTodosAsync(incluirInativos: true);
        return Ok(resposta);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var resposta = await _portfolioServico.ObterPorIdAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);
        return Ok(resposta);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> Buscar([FromQuery] string? termo, [FromQuery] int? ambienteId = null)
    {
        var resposta = await _portfolioServico.FiltrarPorAmbienteAsync(termo, ambienteId);
        return Ok(resposta);
    }


    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CriarPortfolioDto dto)
    {
        var resposta = await _portfolioServico.CadastrarAsync(dto);
        if (!resposta.Sucesso) return BadRequest(resposta);

        return StatusCode(201, resposta);
    }

    [HttpPut("{id}/atualizar")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarPortfolioDto dto)
    {
        var resposta = await _portfolioServico.AtualizarAsync(id, dto);
        if (!resposta.Sucesso) return BadRequest(resposta);

        return Ok(resposta);
    }

    [HttpDelete("{id}/desativar")]
    public async Task<IActionResult> Desativar(int id)
    {
        var resposta = await _portfolioServico.DesativarAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);

        return Ok(resposta);
    }

    [HttpPut("{id}/reativar")]
    public async Task<IActionResult> Reativar(int id)
    {
        var resposta = await _portfolioServico.ReativarAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);

        return Ok(resposta);
    }

    [HttpDelete("{id}/permanente")]
    public async Task<IActionResult> ExcluirPermanente(int id)
    {
        var resposta = await _portfolioServico.ExcluirPermanentementeAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);

        return Ok(resposta);
    }
}