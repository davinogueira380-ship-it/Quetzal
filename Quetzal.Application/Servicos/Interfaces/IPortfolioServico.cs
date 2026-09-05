using System.Collections.Generic;
using System.Threading.Tasks;
using Quetzal.Application.DTOs;

namespace Quetzal.Application.Servicos.Interfaces
{
    public interface IPortfolioServico
    {
        Task<ApiResposta<IEnumerable<PortfolioDto>>> ObterTodosAsync(bool incluirInativos = false);
        Task<ApiResposta<PortfolioDto>> ObterPorIdAsync(int id);
        Task<ApiResposta<IEnumerable<PortfolioDto>>> FiltrarPorAmbienteAsync(string? termo, int? ambienteId = null);

        // Código para uso futuro - se necessário ↓

        //Task<ApiResposta<IEnumerable<PortfolioDto>>> ObterPorAmbienteAsync(int ambienteId);
        Task<ApiResposta<PortfolioDto>> CadastrarAsync(CriarPortfolioDto dto);
        Task<ApiResposta<PortfolioDto>> AtualizarAsync(int id, AtualizarPortfolioDto dto);
        Task<ApiResposta<bool>> DesativarAsync(int id);
        Task<ApiResposta<bool>> ExcluirPermanentementeAsync(int id);
        Task<ApiResposta<bool>> ReativarAsync(int id);
    }
}