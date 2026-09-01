using Quetzal.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Domain.Interfaces
{
    public interface IAmbienteRepositorio
    {
        Task<IEnumerable<Ambiente>> ObterTodasAsync(bool incluirInativas = false);

        Task<Ambiente?> ObterPorIdAsync(int id);

        Task<IEnumerable<Ambiente>> FiltrarPorAsync(string? termo, int? categoriaId = null);

        Task<Ambiente> AdicionarAsync(Ambiente ambiente);

        Task AtualizarAsync(Ambiente ambiente);

        Task DesativarAsync(int id);

        Task ReativarAsync(int id);

        Task ExcluirPermanentementeAsync(int id);


    }
}
