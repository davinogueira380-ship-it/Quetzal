using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Implementacoes;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Application.Servicos;
using Quetzal.Application.Servicos.Interfaces;

namespace Quetzal.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioServico _usuarioServico;// incluido I no Usuario K 16-09
    private readonly UserManager<ApplicationUser> _userManager; 


    public UsuariosController(IUsuarioServico usuarioServico , UserManager<ApplicationUser> userManager) // incluido I no Usuario k 16-09
    {
        _usuarioServico = usuarioServico;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var users = await _userManager.Users.ToListAsync();
        var dtos = new List<UsuarioDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            dtos.Add(new UsuarioDto
            {
                Id = user.Id,
                NomeCompleto = user.NomeCompleto,
                Email = user.Email!,
                Telefone = user.Telefone,
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
    public async Task<IActionResult> Desativar(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound(ApiResposta<bool>.Falha("Usuario nao encontrado."));

        user.Ativo = false;
        await _userManager.UpdateAsync(user);

        return Ok(ApiResposta<bool>.Ok(true, "Usuario desativado com sucesso."));
    }

    [HttpPut("{id}/ativar")]
    public async Task<IActionResult> Ativar(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound(ApiResposta<bool>.Falha("Usuario nao encontrado."));

        user.Ativo = true;
        await _userManager.UpdateAsync(user);

        return Ok(ApiResposta<bool>.Ok(true, "Usuario ativado com sucesso."));
    }


}
