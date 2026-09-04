using Microsoft.AspNetCore.Mvc;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;

namespace Quetzal.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AmbienteController : ControllerBase
{
    //Serviços Injetados
    private readonly IAmbienteServico _ambienteServico;

    public AmbienteController(IAmbienteServico ambienteServico)
    {
        _ambienteServico = ambienteServico;
    }


    [HttpGet]
    public async Task<IActionResult> ObterAtivas()
    {
        var resposta = await _ambienteServico.ObterTodasAsync(incluirInativas: false);
        return Ok(resposta);
    }

    [HttpGet("todas")]
    public async Task<IActionResult> ObterTodas()
    {
        var resposta = await _ambienteServico.ObterTodasAsync(incluirInativas: true);
        return Ok(resposta);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var resposta = await _ambienteServico.ObterPorIdAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);
        return Ok(resposta);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CriarAmbienteDto dto)
    {
        var resposta = await _ambienteServico.CadastrarAsync(dto);
        if (!resposta.Sucesso) return BadRequest(resposta);
        return StatusCode(201, resposta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarAmbienteDto dto)
    {
        var resposta = await _ambienteServico.AtualizarAsync(id, dto);
        if (!resposta.Sucesso) return BadRequest(resposta);
        return Ok(resposta);
    }

    [HttpDelete("{id}/desativar")]
    public async Task<IActionResult> Desativar(int id)
    {
        var resposta = await _ambienteServico.DesativarAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);
        return Ok(resposta);
    }

    [HttpPut("{id}/reativar")]
    public async Task<IActionResult> Reativar(int id)
    {
        var resposta = await _ambienteServico.ReativarAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);
        return Ok(resposta);
    }

    [HttpDelete("{id}/permanente")]
    public async Task<IActionResult> ExcluirPermanente(int id)
    {
        var resposta = await _ambienteServico.ExcluirPermanentementeAsync(id);
        if (!resposta.Sucesso) return NotFound(resposta);
        return Ok(resposta);
    }
}
