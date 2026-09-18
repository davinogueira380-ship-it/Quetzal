using Quetzal.Domain.Entidades;

namespace Quetzal.Domain.Interfaces
{
    public interface IUsuarioRepositorio 
    {
        Task<IEnumerable<ApplicationUser>> ObterTodosAsync(bool incluirInativos = false);
        Task<ApplicationUser?> ObterPorIdAsync(int id);
        Task<ApplicationUser> AdicionarAsync(ApplicationUser usuario);
        Task<ApplicationUser> AtualizarAsync(int id, ApplicationUser usuario);
        Task DesativarAsync(int id);
        Task ExcluirPermanentementeAsync(int id);
        Task ReativarAsync(int id);
        Task<ApplicationUser> AtualizarAsync(ApplicationUser usuarioExistente);
    }
}

// Voltar aqui STHEFANNY ↑
