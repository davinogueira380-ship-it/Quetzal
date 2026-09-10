using Quetzal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Application.Servicos.Interfaces
{
    internal interface IUsuarioServico
    {
        Task<ApiResposta<UsuarioDto>> CadastrarAsync(RegistrarUserDto dto);
        Task<ApiResposta<IEnumerable<UsuarioDto>>> ObterTodosAsync(bool incluirInativas = false);
        Task<ApiResposta<UsuarioDto>> AtualizarAsync(string id, AtualizarPerfilDto dto);
        Task<ApiResposta<bool>> DesativarAsync(string id);
        Task<ApiResposta<bool>> ReativarAsync(string id);
        Task<ApiResposta<bool>> ExcluirPermanentementeAsync(string id);

    }
}
