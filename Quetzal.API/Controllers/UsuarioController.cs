using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Implementacoes;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;


namespace SenacFlix.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioServico _usuarioServico;

    public UsuariosController(UsuarioServico usuarioServico)
    {
        _usuarioServico = usuarioServico;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var users = await _usuarioServico.ObterTodosAsync();
        var dtos = new List<UsuarioDto>();

        foreach (var user in )
        {
            var roles = await _usuarioServico.ObterPorIdAsync(user.Id);
            dtos.Add(new UsuarioDto
            {
                Id = user.Id,
                NomeCompleto = user.NomeCompleto,
                Email = user.Email!,
                Ativo = user.Ativo,
                DataCadastro = user.DataCadastro,
                Perfis = roles.ToList()
            });
        }
        return Ok(ApiResposta<IEnumerable<UsuarioDto>>.Ok(dtos));
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] UsuarioDto dto)
    {
        var resposta = await _usuarioServico.CadastrarAsync(new RegistrarUserDto
        {
            NomeCompleto = dto.NomeCompleto,
            Email = dto.Email,
            Senha = dto.Senha
        });
        if (!resposta.Sucesso) return BadRequest(resposta);

        return StatusCode(201, resposta);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var user = await _usuarioServico.ObterPorIdAsync(id);
        if (user == null) return NotFound(ApiResposta<bool>.Falha("Usuario nao encontrado."));

        user.Ativo = false;
        await _usuarioServico.AtualizarAsync(id, new AtualizarPerfilDto());

        return Ok(ApiResposta<bool>.Ok(true, "Usuario desativado com sucesso."));
    }

    [HttpPut("{id}/ativar")]
    public async Task<IActionResult> Ativar(int id)
    {
        var user = await _usuarioServico.ObterPorIdAsync(id);
        if (user == null) return NotFound(ApiResposta<bool>.Falha("Usuario nao encontrado."));

        user.Ativo = true;
        await _usuarioServico.AtualizarAsync(id, new AtualizarPerfilDto());

        return Ok(ApiResposta<bool>.Ok(true, "Usuario ativado com sucesso."));
    }


}
