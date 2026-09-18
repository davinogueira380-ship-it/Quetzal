using Quetzal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Application.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        Task<ApiResposta<UsuarioDto>> CadastrarAsync(RegistrarUserDto dto);
        Task<ApiResposta<IEnumerable<UsuarioDto>>> ObterTodosAsync(bool incluirInativas = false);
        Task<ApiResposta<UsuarioDto>> AtualizarAsync(int id, AtualizarPerfilDto dto);
        Task<ApiResposta<bool>> DesativarAsync(int id);
        Task<ApiResposta<bool>> ReativarAsync(int id);
        Task<ApiResposta<bool>> ExcluirPermanentementeAsync(int id);

    }
}
