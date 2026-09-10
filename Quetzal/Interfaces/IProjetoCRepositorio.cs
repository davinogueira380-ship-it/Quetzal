using Quetzal.Domain.Entidades;

namespace Quetzal.Domain.Interfaces
{
    public interface IProjetoCRepositorio
    {
        Task<IEnumerable<ProjetoC>> ObterTodosAsync(bool incluirInativos = false);
        Task<ProjetoC?> ObterPorIdAsync(int id);
        Task<IEnumerable<ProjetoC>> FiltrarPorAmbienteAsync(string? termo, int? ambienteId = null);

        Task<IEnumerable<ProjetoC>> ObterPorAsync(int ambienteId);

        Task<ProjetoC> AdicionarAsync(ProjetoC filme);

        Task AtualizarAsync(ProjetoC projeto);

        Task DesativarAsync(int id);

        Task ExcluirPermanentementeAsync(int id);

        Task ReativarAsync(int id);
    }
}
