using AutoMapper;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quetzal.Application.Servicos.Implementacoes
{
    public class AmbienteServico : IAmbienteServico
    {
        private readonly IAmbienteRepositorio _repositorio;
        private readonly IMapper _mapper;

        public AmbienteServico(IAmbienteRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<ApiResposta<IEnumerable<AmbienteDto>>> ObterTodasAsync(bool incluirInativas = false)
        {
            try
            {
                var ambiente = await _repositorio.ObterTodasAsync(incluirInativas);
                var dtos = _mapper.Map<IEnumerable<AmbienteDto>>(ambiente);
                return ApiResposta<IEnumerable<AmbienteDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<AmbienteDto>>.Falha($"Erro ao encontrar o ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<AmbienteDto>> ObterPorIdAsync(int id)
        {
            try
            {
                var ambiente = await _repositorio.ObterPorIdAsync(id);
                if (ambiente == null)
                    return ApiResposta<AmbienteDto>.Falha("Ambiente nao encontrado.");

                var dto = _mapper.Map<AmbienteDto>(ambiente);
                return ApiResposta<AmbienteDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResposta<AmbienteDto>.Falha($"Erro ao encontrar o ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<IEnumerable<AmbienteDto>>> FiltrarPorAsync(string? termo, int? categoriaId = null)
        {
            try
            {
                var projetos = await _repositorio.FiltrarPorAsync(termo, categoriaId);
                var dtos = _mapper.Map<IEnumerable<AmbienteDto>>(projetos);
                return ApiResposta<IEnumerable<AmbienteDto>>.Ok(dtos, "Filtro realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<AmbienteDto>>.Falha($"Erro ao buscar os ambientes: {ex.Message}");
            }
        }

        public async Task<ApiResposta<AmbienteDto>> CadastrarAsync(CriarAmbienteDto dto)
        {
            try
            {
                var todas = await _repositorio.ObterTodasAsync(true);
                foreach (var a in todas)
                {
                    if (a.Nome.Equals(dto.Nome, StringComparison.OrdinalIgnoreCase))
                        return ApiResposta<AmbienteDto>.Falha("Já existe um Ambiente com este nome.");
                }

                var ambiente = _mapper.Map<Ambiente>(dto);
                var ambienteCadastrada = await _repositorio.AdicionarAsync(ambiente);
                var ambienteDto = _mapper.Map<AmbienteDto>(ambienteCadastrada);

                return ApiResposta<AmbienteDto>.Ok(ambienteDto, "Ambiente cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<AmbienteDto>.Falha($"Erro ao cadastrar o ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<AmbienteDto>> AtualizarAsync(int id, CriarAmbienteDto dto)
        {
            try
            {
                var ambienteExistente = await _repositorio.ObterPorIdAsync(id);
                if (ambienteExistente == null)
                    return ApiResposta<AmbienteDto>.Falha("Ambiente nao encontrado.");

                var todas = await _repositorio.ObterTodasAsync(true);
                foreach (var a in todas)
                {
                    if (a.Id != id && a.Nome.Equals(dto.Nome, StringComparison.OrdinalIgnoreCase))
                        return ApiResposta<AmbienteDto>.Falha("Já existe outra um ambiente com este nome.");
                }

                _mapper.Map(dto, ambienteExistente);
                ambienteExistente.DataAtualizacao = DateTime.UtcNow;

                await _repositorio.AtualizarAsync(ambienteExistente);

                var ambienteDto = _mapper.Map<AmbienteDto>(ambienteExistente);
                return ApiResposta<AmbienteDto>.Ok(ambienteDto, "Ambiente atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<AmbienteDto>.Falha($"Erro ao atualizar o ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> DesativarAsync(int id)
        {
            try
            {
                var ambiente = await _repositorio.ObterPorIdAsync(id);
                if (ambiente == null)
                    return ApiResposta<bool>.Falha("Ambiente nao encontrado.");

                await _repositorio.DesativarAsync(id);
                return ApiResposta<bool>.Ok(true, "Ambiente foi desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha($"Erro ao desativar o ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> ReativarAsync(int id)
        {
            try
            {
                var ambiente = await _repositorio.ObterPorIdAsync(id);
                if (ambiente == null)
                    return ApiResposta<bool>.Falha("Ambiente nao encontrada.");

                await _repositorio.ReativarAsync(id);
                return ApiResposta<bool>.Ok(true, "Ambiente reativada com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha($"Erro ao reativar o ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> ExcluirPermanentementeAsync(int id)
        {
            try
            {
                var ambiente = await _repositorio.ObterPorIdAsync(id);
                if (ambiente == null)
                    return ApiResposta<bool>.Falha("Ambiente nao encontrado.");

                if (ambiente.Portfolio != null && ambiente.Portfolio.Any(p => !p.Ativo || p.Ativo)) //Obs
                {
                    return ApiResposta<bool>.Falha("Não é possível excluir um ambiente que esteja sendo utilizado por algum portfolio.");
                }

                await _repositorio.ExcluirPermanentementeAsync(id);
                return ApiResposta<bool>.Ok(true, "Ambiente excluido permanentemente com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha($"Erro ao excluir um ambiente: {ex.Message}");
            }
        }
    }
}
