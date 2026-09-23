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
    // ATUALIZAR USUÁRIO

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(
        string id,
        [FromBody] UsuarioDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound(
                ApiResposta<UsuarioDto>.Falha(
                    "Usuário não encontrado."));
        }

        // Atualiza os dados básicos
        user.NomeCompleto = dto.NomeCompleto;
        user.Email = dto.Email;
        user.UserName = dto.Email;
        user.Telefone = dto.Telefone;

        var resultado = await _userManager.UpdateAsync(user);

        if (!resultado.Succeeded)
        {
            var erros = string.Join(
                " | ",
                resultado.Errors.Select(e => e.Description));

            return BadRequest(
                ApiResposta<UsuarioDto>.Falha(
                    $"Não foi possível atualizar o usuário. {erros}"));
        }

        // Atualizar os perfis (Roles)
        if (dto.Perfis != null)
        {
            var perfisAtuais = await _userManager.GetRolesAsync(user);

            // Remove os perfis atuais
            if (perfisAtuais.Any())
            {
                var resultadoRemocao =
                    await _userManager.RemoveFromRolesAsync(
                        user,
                        perfisAtuais);

                if (!resultadoRemocao.Succeeded)
                {
                    var erros = string.Join(
                        " | ",
                        resultadoRemocao.Errors
                            .Select(e => e.Description));

                    return BadRequest(
                        ApiResposta<UsuarioDto>.Falha(
                            $"Usuário atualizado, mas não foi possível alterar os perfis. {erros}"));
                }
            }

            // Adiciona os novos perfis
            if (dto.Perfis.Any())
            {
                var resultadoPerfis =
                    await _userManager.AddToRolesAsync(
                        user,
                        dto.Perfis);

                if (!resultadoPerfis.Succeeded)
                {
                    var erros = string.Join(
                        " | ",
                        resultadoPerfis.Errors
                            .Select(e => e.Description));

                    return BadRequest(
                        ApiResposta<UsuarioDto>.Falha(
                            $"Usuário atualizado, mas não foi possível adicionar os perfis. {erros}"));
                }
            }
        }

        // Busca novamente os perfis atualizados
        var perfisAtualizados =
            await _userManager.GetRolesAsync(user);

        var usuarioAtualizado = new UsuarioDto
        {
            Id = user.Id,
            NomeCompleto = user.NomeCompleto,
            Email = user.Email ?? string.Empty,
            Telefone = user.Telefone,
            Ativo = user.Ativo,
            DataCadastro = user.DataCadastro,
            Perfis = perfisAtualizados.ToList()
        };

        return Ok(
            ApiResposta<UsuarioDto>.Ok(
                usuarioAtualizado,
                "Usuário atualizado com sucesso."));
    }
    // DESATIVAR USUÁRIO

    [HttpPut("{id}/desativar")]
    public async Task<IActionResult> Desativar(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound(
                ApiResposta<bool>.Falha("Usuário não encontrado."));
        }

        user.Ativo = false;

        var resultado = await _userManager.UpdateAsync(user);

        if (!resultado.Succeeded)
        {
            return BadRequest(
                ApiResposta<bool>.Falha(
                    "Não foi possível desativar o usuário."));
        }

        return Ok(
            ApiResposta<bool>.Ok(
                true,
                "Usuário desativado com sucesso."));
    }


    // EXCLUIR USUÁRIO

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound(
                ApiResposta<bool>.Falha("Usuário não encontrado."));
        }

        var resultado = await _userManager.DeleteAsync(user);

        if (!resultado.Succeeded)
        {
            var erros = string.Join(
                " | ",
                resultado.Errors.Select(e => e.Description));

            return BadRequest(
                ApiResposta<bool>.Falha(
                    $"Não foi possível excluir o usuário. {erros}"));
        }

        return Ok(
            ApiResposta<bool>.Ok(
                true,
                "Usuário excluído com sucesso."));
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
