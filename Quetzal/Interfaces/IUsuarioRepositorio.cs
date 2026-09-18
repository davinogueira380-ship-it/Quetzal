using Quetzal.Domain.Entidades;

namespace Quetzal.Domain.Interfaces
{
    public interface IUsuarioRepositorio 
    {
        Task<IEnumerable<ApplicationUser>> ObterTodosAsync(bool incluirInativos = false);
        Task<ApplicationUser?> ObterPorIdAsync(string id);
        Task<ApplicationUser> AdicionarAsync(ApplicationUser usuario);
        Task<ApplicationUser> AtualizarAsync(string id, ApplicationUser usuario);
        Task DesativarAsync(string id);
        Task ExcluirPermanentementeAsync(string id);
        Task ReativarAsync(string id);
        Task<ApplicationUser> AtualizarAsync(ApplicationUser usuarioExistente);
    }
}

// Voltar aqui STHEFANNY ↑
