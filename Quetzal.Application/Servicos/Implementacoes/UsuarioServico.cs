using AutoMapper;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quetzal.Application.Servicos.Implementacoes;


public class UsuarioServico : IUsuarioServico
{
    private readonly IUsuarioRepositorio _repositorio;
    private readonly IMapper _mapper;

    public UsuarioServico(IUsuarioRepositorio repositorio, IMapper mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }
    public async Task<ApiResposta<IEnumerable<UsuarioDto>>> ObterTodosAsync(bool incluirInativos = false)
    {
        try
        {
            var usuarios = await _repositorio.ObterTodosAsync(incluirInativos);
            var dtos = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return ApiResposta<IEnumerable<UsuarioDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return ApiResposta<IEnumerable<UsuarioDto>>.Falha($"Erro ao obter usuários: {ex.Message}");
        }
    }
    public async Task<ApiResposta<UsuarioDto>> ObterPorIdAsync(int id)
    {
        try
        {
            var usuario = await _repositorio.ObterPorIdAsync(id.ToString());
            if (usuario == null)
                return ApiResposta<UsuarioDto>.Falha("Usuário não encontrado.");
            var dto = _mapper.Map<UsuarioDto>(usuario);
            return ApiResposta<UsuarioDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return ApiResposta<UsuarioDto>.Falha($"Erro ao obter o usuário: {ex.Message}");
        }
    }
    public async Task<ApiResposta<UsuarioDto>> AdicionarAsync(RegistrarUserDto dto)
    {
        try
        {
            var usuario = _mapper.Map<ApplicationUser>(dto);
            var usuarioAdicionado = await _repositorio.AdicionarAsync(usuario);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuarioAdicionado);
            return ApiResposta<UsuarioDto>.Ok(usuarioDto, "Usuário adicionado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResposta<UsuarioDto>.Falha($"Erro ao adicionar o usuário: {ex.Message}");
        }

    }
    public async Task<ApiResposta<UsuarioDto>> AtualizarAsync(int id, AtualizarPerfilDto dto)
    {
        try
        {
            var usuarioExistente = await _repositorio.ObterPorIdAsync(id.ToString());
            if (usuarioExistente == null)
                return ApiResposta<UsuarioDto>.Falha("Usuário não encontrado.");
            _mapper.Map(dto, usuarioExistente);

            var usuarioAtualizado = await _repositorio.AtualizarAsync(usuarioExistente);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuarioAtualizado);
            return ApiResposta<UsuarioDto>.Ok(usuarioDto, "Usuário atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResposta<UsuarioDto>.Falha($"Erro ao atualizar o usuário: {ex.Message}");
        }
    }
    public async Task<ApiResposta<UsuarioDto>> CadastrarAsync(RegistrarUserDto dto)
    {
        try
        {
            var usuario = await _repositorio.ObterPorIdAsync(dto.Id.ToString());
            if (usuario != null)
                return ApiResposta<UsuarioDto>.Falha("Usuário já existe.");

            var novoUsuario = _mapper.Map<ApplicationUser>(dto);
            var usuarioAdicionado = await _repositorio.AdicionarAsync(novoUsuario);

            var usuarioCadastrado = _mapper.Map<UsuarioDto>(usuarioAdicionado);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuarioCadastrado);
            return ApiResposta<UsuarioDto>.Ok(usuarioDto, "Usuário cadastrado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResposta<UsuarioDto>.Falha($"Erro ao cadastrar o usuário: {ex.Message}");
        }
    }
    public async Task<ApiResposta<bool>> DesativarAsync(int id)
    {
        try
        {
            await _repositorio.DesativarAsync(id.ToString());
            return ApiResposta<bool>.Ok(true, "Usuário desativado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResposta<bool>.Falha($"Erro ao desativar o usuário: {ex.Message}");
        }
    }
    public async Task<ApiResposta<bool>> ExcluirPermanentementeAsync(int id)
    {
        try
        {
            await _repositorio.ExcluirPermanentementeAsync(id.ToString());
            return ApiResposta<bool>.Ok(true, "Usuário excluído permanentemente com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResposta<bool>.Falha($"Erro ao excluir o usuário: {ex.Message}");
        }
    }
    public async Task<ApiResposta<bool>> ReativarAsync(int id)
    {
        try
        {
            var usuario = await _repositorio.ObterPorIdAsync(id.ToString());
            if (usuario == null && _repositorio.ObterPorIdAsync(id.ToString()) != null)
            {
                await _repositorio.ReativarAsync(id.ToString());
                return ApiResposta<bool>.Ok(true, "Usuário reativado com sucesso.");
            }
            if (usuario == null)
                return ApiResposta<bool>.Falha("Usuário não encontrado.");

            await _repositorio.ReativarAsync(id.ToString());
            return ApiResposta<bool>.Ok(true, "Usuário reativado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResposta<bool>.Falha($"Erro ao reativar o usuário: {ex.Message}");
        }
    }



}





