using AutoMapper;
using Quetzal.Application.DTOs;
using Quetzal.Application.Servicos.Interfaces;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using System.Linq;

namespace Quetzal.Application.Servicos.Implementacoes
{
    public class PortfolioServico : IPortfolioServico
    {
        private readonly IPortfolioRepositorio _repositorio;
        private readonly IAmbienteRepositorio _ambienteRepositorio;
        private readonly IProjetoCRepositorio _projetoCRepositorio;
        private readonly IMapper _mapper;

        public PortfolioServico(
            IPortfolioRepositorio repositorio,
            IAmbienteRepositorio ambienteRepositorio,
            IProjetoCRepositorio projetoCRepositorio,
            IMapper mapper)
        {
            _repositorio = repositorio;
            _ambienteRepositorio = ambienteRepositorio;
            _projetoCRepositorio = projetoCRepositorio;
            _mapper = mapper;
        }

        public async Task<ApiResposta<IEnumerable<PortfolioDto>>> ObterTodosAsync(
            bool incluirInativos = false)
        {
            try
            {
                var portfolios =
                    await _repositorio.ObterTodosAsync(incluirInativos);

                var dtos =
                    _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);

                return ApiResposta<IEnumerable<PortfolioDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<PortfolioDto>>.Falha(
                    $"Erro ao obter portfolios: {ex.Message}");
            }
        }

        public async Task<ApiResposta<PortfolioDto>> ObterPorIdAsync(int id)
        {
            try
            {
                var portfolio =
                    await _repositorio.ObterPorIdAsync(id);

                if (portfolio == null)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Portfolio não encontrado.");
                }

                var dto =
                    _mapper.Map<PortfolioDto>(portfolio);

                return ApiResposta<PortfolioDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto>.Falha(
                    $"Erro ao obter o portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<IEnumerable<PortfolioDto>>>
            FiltrarPorAmbienteAsync(
                string? termo,
                int? ambienteId = null)
        {
            try
            {
                var portfolios =
                    await _repositorio.FiltrarPorAmbienteAsync(
                        termo,
                        ambienteId);

                var dtos =
                    _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);

                return ApiResposta<IEnumerable<PortfolioDto>>.Ok(
                    dtos,
                    "Busca realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<PortfolioDto>>.Falha(
                    $"Erro ao buscar portfolios: {ex.Message}");
            }
        }

        public async Task<ApiResposta<IEnumerable<PortfolioDto>>>
            ObterPorAsync(int ambienteId)
        {
            try
            {
                var portfolios =
                    await _repositorio.ObterPorAsync(ambienteId);

                var dtos =
                    _mapper.Map<IEnumerable<PortfolioDto>>(portfolios);

                return ApiResposta<IEnumerable<PortfolioDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ApiResposta<IEnumerable<PortfolioDto>>.Falha(
                    $"Erro ao obter portfolios por ambiente: {ex.Message}");
            }
        }

        public async Task<ApiResposta<PortfolioDto>> CadastrarAsync(
            CriarPortfolioDto dto)
        {
            try
            {
                // --------------------------------------------------------
                // VALIDAR AMBIENTE
                // --------------------------------------------------------

                var ambiente =
                    await _ambienteRepositorio.ObterPorIdAsync(
                        dto.AmbienteId);

                if (ambiente == null)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Ambiente inválido.");
                }

                // --------------------------------------------------------
                // VALIDAR PROJETO
                // --------------------------------------------------------

                if (!dto.ProjetoCId.HasValue)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "É necessário selecionar um projeto.");
                }

                var projeto =
                    await _projetoCRepositorio.ObterPorIdAsync(
                        dto.ProjetoCId.Value);

                if (projeto == null)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Projeto selecionado não foi encontrado.");
                }

                // --------------------------------------------------------
                // VALIDAR FOTOS
                // --------------------------------------------------------

                var fotosIds =
                    (dto.ProjetoCFotosIds ?? new List<int>())
                    .Distinct()
                    .ToList();

                if (fotosIds.Count == 0)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Selecione pelo menos uma foto para o portfólio.");
                }

                var fotosDoProjeto =
                    projeto.Fotos
                        .Where(f => fotosIds.Contains(f.Id))
                        .ToList();

                if (fotosDoProjeto.Count != fotosIds.Count)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Uma ou mais fotos selecionadas não pertencem ao projeto informado.");
                }

                // --------------------------------------------------------
                // CRIAR PORTFOLIO
                // --------------------------------------------------------

                var portfolio =
                    _mapper.Map<Portfolio>(dto);

                // Não armazenamos a imagem novamente.
                portfolio.ImagemUpload = null;

                // Criamos somente os relacionamentos com as fotos originais.
                portfolio.Fotos =
                    fotosDoProjeto
                        .Select(f => new PortfolioFoto
                        {
                            ProjetoCFotoId = f.Id
                        })
                        .ToList();

                var portfolioCadastrado =
                    await _repositorio.AdicionarAsync(portfolio);

                var portfolioCompleto =
                    await _repositorio.ObterPorIdAsync(
                        portfolioCadastrado.Id);

                var portfolioDto =
                    _mapper.Map<PortfolioDto>(portfolioCompleto);

                return ApiResposta<PortfolioDto>.Ok(
                    portfolioDto,
                    "Portfolio cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto>.Falha(
                    $"Erro ao cadastrar portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<PortfolioDto>> AtualizarAsync(
            int id,
            AtualizarPortfolioDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "O Id informado na URL é diferente do Id no corpo da requisição.");
                }

                var portfolioExistente =
                    await _repositorio.ObterPorIdAsync(id);

                if (portfolioExistente == null)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Portfolio não encontrado.");
                }

                // --------------------------------------------------------
                // VALIDAR AMBIENTE
                // --------------------------------------------------------

                var ambiente =
                    await _ambienteRepositorio.ObterPorIdAsync(
                        dto.AmbienteId);

                if (ambiente == null)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Ambiente inválido.");
                }

                // --------------------------------------------------------
                // VALIDAR PROJETO
                // --------------------------------------------------------

                if (!dto.ProjetoCId.HasValue)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "É necessário selecionar um projeto.");
                }

                var projeto =
                    await _projetoCRepositorio.ObterPorIdAsync(
                        dto.ProjetoCId.Value);

                if (projeto == null)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Projeto selecionado não foi encontrado.");
                }

                // --------------------------------------------------------
                // VALIDAR FOTOS
                // --------------------------------------------------------

                var fotosIds =
                    (dto.ProjetoCFotosIds ?? new List<int>())
                    .Distinct()
                    .ToList();

                if (fotosIds.Count == 0)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Selecione pelo menos uma foto para o portfólio.");
                }

                var fotosDoProjeto =
                    projeto.Fotos
                        .Where(f => fotosIds.Contains(f.Id))
                        .ToList();

                if (fotosDoProjeto.Count != fotosIds.Count)
                {
                    return ApiResposta<PortfolioDto>.Falha(
                        "Uma ou mais fotos selecionadas não pertencem ao projeto informado.");
                }

                // --------------------------------------------------------
                // ATUALIZAR DADOS DO PORTFOLIO
                // --------------------------------------------------------

                _mapper.Map(dto, portfolioExistente);

                portfolioExistente.ImagemUpload = null;

                // Substitui a seleção anterior pela nova seleção.
                portfolioExistente.Fotos.Clear();

                foreach (var foto in fotosDoProjeto)
                {
                    portfolioExistente.Fotos.Add(
                        new PortfolioFoto
                        {
                            PortfolioId = portfolioExistente.Id,
                            ProjetoCFotoId = foto.Id
                        });
                }

                portfolioExistente.DataAtualizacao =
                    DateTime.UtcNow;

                await _repositorio.AtualizarAsync(
                    portfolioExistente);

                var portfolioAtualizado =
                    await _repositorio.ObterPorIdAsync(id);

                var portfolioDto =
                    _mapper.Map<PortfolioDto>(
                        portfolioAtualizado);

                return ApiResposta<PortfolioDto>.Ok(
                    portfolioDto,
                    "Portfolio atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<PortfolioDto>.Falha(
                    $"Erro ao atualizar portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> DesativarAsync(int id)
        {
            try
            {
                var portfolio =
                    await _repositorio.ObterPorIdAsync(id);

                if (portfolio == null)
                {
                    return ApiResposta<bool>.Falha(
                        "Portfolio não encontrado.");
                }

                await _repositorio.DesativarAsync(id);

                return ApiResposta<bool>.Ok(
                    true,
                    "Portfolio desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha(
                    $"Erro ao desativar portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>>
            ExcluirPermanentementeAsync(int id)
        {
            try
            {
                var portfolio =
                    await _repositorio.ObterPorIdAsync(id);

                if (portfolio == null)
                {
                    return ApiResposta<bool>.Falha(
                        "Portfolio não encontrado.");
                }

                await _repositorio.ExcluirPermanentementeAsync(id);

                return ApiResposta<bool>.Ok(
                    true,
                    "Portfolio excluído permanentemente com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha(
                    $"Erro ao excluir portfolio: {ex.Message}");
            }
        }

        public async Task<ApiResposta<bool>> ReativarAsync(int id)
        {
            try
            {
                var portfolio =
                    await _repositorio.ObterPorIdAsync(id);

                if (portfolio == null)
                {
                    return ApiResposta<bool>.Falha(
                        "Portfolio não encontrado.");
                }

                await _repositorio.ReativarAsync(id);

                return ApiResposta<bool>.Ok(
                    true,
                    "Portfolio reativado com sucesso.");
            }
            catch (Exception ex)
            {
                return ApiResposta<bool>.Falha(
                    $"Erro ao reativar portfolio: {ex.Message}");
            }
        }
    }
}