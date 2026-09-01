using Quetzal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Application.Servicos.Interfaces
{
    public interface IAmbienteServico
    {
        Task<ApiResposta<IEnumerable<AmbienteDto>>> ObterTodasAsync(bool incluirInativas = false);
        Task<ApiResposta<AmbienteDto>> ObterPorIdAsync(int id);
        Task<ApiResposta<IEnumerable<AmbienteDto>>> FiltrarPorAsync(string? termo, int? categoriaId = null);
        Task<ApiResposta<AmbienteDto>> CadastrarAsync(CriarAmbienteDto dto);
        Task<ApiResposta<AmbienteDto>> AtualizarAsync(int id, CriarAmbienteDto dto);
        Task<ApiResposta<bool>> DesativarAsync(int id);
        Task<ApiResposta<bool>> ReativarAsync(int id);
        Task<ApiResposta<bool>> ExcluirPermanentementeAsync(int id);
    }
}
