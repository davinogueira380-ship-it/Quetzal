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

        public async Task<ApiResposta<IEnumerable<PortfolioDto>>> ObterTodosAsync(bool incluirInativos = false)
        {
            try
            {
                var portfolios = await _repositorio.ObterTodosAsync(incluirInativos);
                var dtos = _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);
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
                var portfolios = await _repositorio.ObterPorIdAsync(id);
                if (portfolios == null)
                    return ApiResposta<PortfolioDto>.Falha("Portfolio nao encontrado.");

                var dto = _mapper.Map<PortfolioDto>(portfolios);
                return ApiResposta<PortfolioDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto>.Falha($"Erro ao obter o portfolio: {ex.Message}");
            }
        }
        
        public async Task<ApiResposta<IEnumerable<PortfolioDto>>> FiltrarPorAmbienteAsync(string? termo, int? ambienteId = null)
        {
            try
            {
                var portfolios = await _repositorio.FiltrarPorAsync(termo, ambienteId);
                var dtos = _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);
                return ApiResposta<IEnumerable<PortfolioDto>>.Ok(dtos, "Busca realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<PortfolioDto>>.Falha($"Erro ao buscar portfolios: {ex.Message}");
            }
        }
        
        // Código para uso futuro - se necessário ↓


        //public async Task<ApiResposta<IEnumerable<PortfolioDto>>> ObterPorAmbienteAsync(int ambienteId)
        //{
        //    try
        //    {
        //        var portfolios = await _repositorio.ObterPorAmbienteAsync(ambienteId);
        //        var dtos = _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);
        //        return ApiResposta<IEnumerable<PortfolioDto>>.Ok(dtos);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResposta<IEnumerable<PortfolioDto>>.Falha($"Erro ao obter portfolios por ambiente: {ex.Message}");
        //    }
        //}

        public async Task<ApiResposta<PortfolioDto>> CadastrarAsync(CriarPortfolioDto dto)
        {
            try
            {
                // Verifica se o aAMBIENTE informado existe
                var ambiente = await _ambienteRepositorio.ObterPorIdAsync(dto.AmbienteId);
                if (ambiente == null)
                    return ApiResposta<PortfolioDto>.Falha("Ambiente invalido.");

                var portfolio = _mapper.Map<Portfolio>(dto);
                var portfolioCadastrado = await _repositorio.AdicionarAsync(portfolio);

                // Buscar novamente para carregar os relacionamentos corretamente para o DTO de retorno
                var portfolioCompleto = await _repositorio.ObterPorIdAsync(portfolioCadastrado.Id);
                var portfolioDto = _mapper.Map<PortfolioDto>(portfolioCompleto);
                return ApiResposta<PortfolioDto>.Ok(portfolioDto, "Portfolio cadastrado com sucesso.");
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
                    return ApiResposta<PortfolioDto>.Falha("O Id informado na URL é diferente do Id no corpo da requisição.");

                var portfolioExistente = await _repositorio.ObterPorIdAsync(id);
                if (portfolioExistente == null)
                    return ApiResposta<PortfolioDto>.Falha("Portfolio não encontrado.");

                var ambiente = await _ambienteRepositorio.ObterPorIdAsync(dto.AmbienteId);
                if (ambiente == null)
                    return ApiResposta<PortfolioDto>.Falha("Ambiente invalido.");

                _mapper.Map(dto, portfolioExistente);
                portfolioExistente.DataAtualizacao = DateTime.UtcNow;

                await _repositorio.AtualizarAsync(portfolioExistente);
                var portfolioDto = _mapper.Map<PortfolioDto>(portfolioExistente);
                return ApiResposta<PortfolioDto>.Ok(portfolioDto, "Portfolio atualizado com sucesso.");
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
                var portfolio = await _repositorio.ObterPorIdAsync(id);
                if (portfolio == null)
                    return ApiResposta<bool>.Falha("Portfolio não encontrado.");

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
                var portfolio = await _repositorio.ObterPorIdAsync(id);
                if (portfolio == null)
                    return ApiResposta<bool>.Falha("Portfolio não encontrado.");

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
                if (projeto == null && await _repositorio.ObterPorIdAsync(id) == null)  
                {
                    //não possui o parâmetro
                }
                // Em PortfolioRepositorio, ObterPorIdAsync traz o portfolio mesmo inativo porque não há filtro lá.
                if (projeto == null)
                    return ApiResposta<bool>.Falha("Portfolio nao encontrado.");

                await _repositorio.ReativarAsync(id);
                return ApiResposta<bool>.Ok(true, "Portfolio reativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha($"Erro ao reativar portfolio: {ex.Message}");
            }
        }
    }
}