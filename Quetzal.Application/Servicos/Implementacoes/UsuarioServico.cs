using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;
using AutoMapper;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Application.Servicos.Implementacoes;

internal class UsuarioServico : IUsuarioServico
{

    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public async Task<ApiResposta<UsuarioDto>> CadastrarAsync(RegistrarUserDto dto)
    {
        {
            try
            // Verifica se o Usuario informado existe
            var usuario = await _usuarioRepositorio.ObterPorIdAsync(dto.Email);
            if (usuario == null)
                return ApiResposta<UsuarioDto>.Falha("Usuario invalido.");

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return ApiResposta<UsuarioDto>.Ok(usuarioDto, "Usuario obtido com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<UsuarioDto>.Falha($"Erro ao obter usuario: {ex.Message}");
            }
        }

    }
}
    



