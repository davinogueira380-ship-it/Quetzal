using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;

namespace Quetzal.Application.Servicos.Implementacoes
{
    public class PortfolioServico : IPortfolioServico
    {
        private readonly IPortfolioRepositorio _repositorio;
        private readonly IAmbienteRepositorio _ambienteRepositorio;
        private readonly IMapper _mapper;

        public PortfolioServico(IPortfolioRepositorio repositorio, IAmbienteRepositorio ambienteRepositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _ambienteRepositorio = ambienteRepositorio;
            _mapper = mapper;
        }

        public async Task<ApiResposta<PortfolioDto?>> ObterProjetoDestaqueAsync()
        {
            try
            {
                var todosPortfolio = await _repositorio.ObterTodosAsync(false);
                var portfolioDestaque = new List<Portfolio>();
                var portfolioElegiveis = new List<Portfolio>();
                foreach (var = p in todosPortfolio)
                {
                    if (!string.IsNullOrEmpty(p.ImagemBannerUrl) && !string.IsNullOrEmpty(p.TrailerYoutubeUrl))
                    {
                        portfolioElegiveis.Add(p);
                        if (p.DestaqueHome)
                        {
                            portfolioDestaque.Add(p);
                        }
                    }
                }

                var listaParaSorteio = portfolioDestaque.Count > 0 ? portfolioDestaque : portfolioElegiveis;

                if (listaParaSorteio.Count == 0)
                {
                    return ApiResposta<PortfolioDto?>.Ok(null);
                }

                var random = new Random();
                var index = random.Next(listaParaSorteio.Count);
                var projetoEscolhido = listaParaSorteio[index];

                var dto = _mapper.Map<PortfolioDto>(projetoEscolhido);
                return ApiResposta<PortfolioDto?>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto?>.Falha($"Erro ao obter portfolio destaque: {ex.Message}");
            }
        }

        public async Task<ApiResposta<IEnumerable<PortfolioDto>>> ObterTodosAsync(bool incluirInativos = false)
        {
            try
            {
                var projetos = await _repositorio.ObterTodosAsync(incluirInativos);
                var dtos = _mapper.Map<IEnumerable<PortfolioDto>>(projetos);
                return ApiResposta<IEnumerable<PortfolioDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<PortfolioDto>>.Falha($"Erro ao obter portfolios: {ex.Message}");
            }
        }

        public async Task<ApiResposta<PortfolioDto>> ObterPorIdAsync(int id)
        {
            try
            {
                var projeto = await _repositorio.ObterPorIdAsync(id);
                if (projeto == null)
                    return ApiResposta<PortfolioDto>.Falha("Portfolio nao encontrado.");

                var dto = _mapper.Map<PortfolioDto>(projeto);
                return ApiResposta<PortfolioDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto>.Falha($"Erro ao obter o portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<IEnumerable<PortfolioDto>>> FiltrarPorAsync(string? termo, int? ambienteId = null)
        {
            try
            {
                var projetos = await _repositorio.FiltrarPorAsync(termo, ambienteId);
                var dtos = _mapper.Map<IEnumerable<PortfolioDto>>(projetos);
                return ApiResposta<IEnumerable<PortfolioDto>>.Ok(dtos, "Busca realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<PortfolioDto>>.Falha($"Erro ao buscar portfolios: {ex.Message}");
            }
        }

        public async Task<ApiResposta<IEnumerable<PortfolioDto>>> ObterPorAmbienteAsync(int ambienteId)
        {
            try
            {
                var portfolios = await _repositorio.ObterPorAmbienteAsync(ambienteId);
                var dtos = _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);
                return ApiResposta<IEnumerable<PortfolioDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<PortfolioDto>>.Falha($"Erro ao obter portfolios por ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<PortfolioDto>> CadastrarAsync(CriarPortfolioDto dto)
        {
            try
            {
                // Verifica se a ambiente informada existe
                var ambiente = await _ambienteRepositorio.ObterPorIdAsync(dto.AmbienteId);
                if (ambiente == null)
                    return ApiResposta<PortfolioDto>.Falha("Ambiente invalida.");

                var projeto = _mapper.Map<Portfolio>(dto);
                var projetoCadastrado = await _repositorio.AdicionarAsync(projeto);
                // Buscar novamente para carregar os relacionamentos corretamente para o DTO de retorno
                var projetoCompleto = await _repositorio.ObterPorIdAsync(projetoCadastrado.Id);
                var projetoDto = _mapper.Map<PortfolioDto>(projetoCompleto);
                return ApiResposta<PortfolioDto>.Ok(projetoDto, "Portfolio cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto>.Falha($"Erro ao cadastrar portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<PortfolioDto>> AtualizarAsync(int id, AtualizarPortfolioDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return ApiResposta<PortfolioDto>.Falha("O Id informado na URL e diferente do Id no corpo da requisicao.");

                var projetoExistente = await _repositorio.ObterPorIdAsync(id);
                if (projetoExistente == null)
                    return ApiResposta<PortfolioDto>.Falha("Portfolio nao encontrado.");

                var ambiente = await _ambienteRepositorio.ObterPorIdAsync(dto.AmbienteId);
                if (ambiente == null)
                    return ApiResposta<PortfolioDto>.Falha("Ambiente invalida.");

                _mapper.Map(dto, projetoExistente);
                projetoExistente.DataAtualizacao = DateTime.UtcNow;

                await _repositorio.AtualizarAsync(projetoExistente);
                var projetoDto = _mapper.Map<PortfolioDto>(projetoExistente);
                return ApiResposta<PortfolioDto>.Ok(projetoDto, "Portfolio atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto>.Falha($"Erro ao atualizar portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> DesativarAsync(int id)
        {
            try
            {
                var projeto = await _repositorio.ObterPorIdAsync(id);
                if (projeto == null)
                    return ApiResposta<bool>.Falha("Portfolio nao encontrado.");

                await _repositorio.DesativarAsync(id);
                return ApiResposta<bool>.Ok(true, "Portfolio desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha($"Erro ao desativar portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> ExcluirPermanentementeAsync(int id)
        {
            try
            {
                var projeto = await _repositorio.ObterPorIdAsync(id);
                if (projeto == null)
                    return ApiResposta<bool>.Falha("Portfolio nao encontrado.");

                await _repositorio.ExcluirPermanentementeAsync(id);
                return ApiResposta<bool>.Ok(true, "Portfolio excluido permanentemente com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha($"Erro ao excluir portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> ReativarAsync(int id)
        {
            try
            {
                var projeto = await _repositorio.ObterPorIdAsync(id);
                if (projeto == null && await _repositorio.ObterPorIdAsync(id) == null) // We actually need to include inativos to find it if ObterPorIdAsync only gets ativos. 
                {
                    // Wait, ObterPorIdAsync currently doesn't have `incluirInativos` parameter in the repository. Let's fix that too or just call ReativarAsync.
                }
                // Em ProjetoRepositorio, ObterPorIdAsync traz o projeto mesmo inativo porque não há filtro lá.
                if (projeto == null)
                    return ApiResposta<bool>.Falha("Projeto nao encontrado.");

                await _repositorio.ReativarAsync(id);
                return ApiResposta<bool>.Ok(true, "Projeto reativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha($"Erro ao reativar projeto: {ex.Message}");
            }
        }
    }
}