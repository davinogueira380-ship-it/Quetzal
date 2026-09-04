//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Quetzal.Application.DTOs;
//using Quetzal.Domain.Entidades;

//namespace Quetzal.API.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class PortfolioController : ControllerBase
//{
//    private readonly IPortfolioServico _portfolioServico;
//    private readonly IAuditoriaServico _auditoriaServico;

//    public PortfolioController(IPortfolioServico portfolioServico, IAuditoriaServico auditoriaServico)
//    {
//        _portfolioServico = portfolioServico;
//        _auditoriaServico = auditoriaServico;
//    }

//    // Endpoint publico para visitantes verem o catalogo (apenas ativos)
//    [HttpGet]
//    public async Task<IActionResult> ObterAtivos()
//    {
//        var resposta = await _filmeServico.ObterTodosAsync(incluirInativos: false);
//        return Ok(resposta);
//    }

//    // Endpoint administrativo para listar todos os filmes
//    [HttpGet("todos")]
//    public async Task<IActionResult> ObterTodos()
//    {
//        var resposta = await _filmeServico.ObterTodosAsync(incluirInativos: true);
//        return Ok(resposta);
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> ObterPorId(int id)
//    {
//        var resposta = await _filmeServico.ObterPorIdAsync(id);
//        if (!resposta.Sucesso) return NotFound(resposta);
//        return Ok(resposta);
//    }

//    [HttpGet("buscar")]
//    public async Task<IActionResult> Buscar([FromQuery] string? termo, [FromQuery] int? categoriaId = null)
//    {
//        var resposta = await _filmeServico.BuscarAsync(termo, categoriaId);
//        return Ok(resposta);
//    }

//    [HttpGet("destaque")]
//    public async Task<IActionResult> ObterDestaque()
//    {
//        var resposta = await _filmeServico.ObterFilmeDestaqueAsync();
//        if (!resposta.Sucesso) return NotFound(resposta);
//        return Ok(resposta);
//    }

//    [HttpGet("categoria/{categoriaId}")]
//    public async Task<IActionResult> ObterPorCategoria(int categoriaId)
//    {
//        var resposta = await _filmeServico.ObterPorCategoriaAsync(categoriaId);
//        return Ok(resposta);
//    }

//    [HttpPost]
//    public async Task<IActionResult> Cadastrar([FromBody] CriarFilmeDto dto)
//    {
//        var resposta = await _filmeServico.CadastrarAsync(dto);
//        if (!resposta.Sucesso) return BadRequest(resposta);

//        return StatusCode(201, resposta);
//    }

//    [HttpPut("{id}")]
//    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarFilmeDto dto)
//    {
//        var resposta = await _filmeServico.AtualizarAsync(id, dto);
//        if (!resposta.Sucesso) return BadRequest(resposta);

//        return Ok(resposta);
//    }

//    [HttpDelete("{id}/desativar")]
//    public async Task<IActionResult> Desativar(int id)
//    {
//        var resposta = await _filmeServico.DesativarAsync(id);
//        if (!resposta.Sucesso) return NotFound(resposta);

//        return Ok(resposta);
//    }

//    [HttpPut("{id}/reativar")]
//    public async Task<IActionResult> Reativar(int id)
//    {
//        var resposta = await _filmeServico.ReativarAsync(id);
//        if (!resposta.Sucesso) return NotFound(resposta);

//        return Ok(resposta);
//    }

//    [HttpDelete("{id}/permanente")]
//    public async Task<IActionResult> ExcluirPermanente(int id)
//    {
//        var resposta = await _filmeServico.ExcluirPermanentementeAsync(id);
//        if (!resposta.Sucesso) return NotFound(resposta);

//        return Ok(resposta);
//    }
//}