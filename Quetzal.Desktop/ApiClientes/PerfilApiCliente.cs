using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.ApiClientes
{
    public class AtualizarPerfilDto
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

    }

    public class AlterarSenhaDto
    {
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }

    public class PerfilApiCliente : ClienteHttp
    {
        private const string RotaBase = "/api/perfil";
        public async Task<UsuarioDto> ObterPerfilAsync()
        {
            var resposta = await GetAsync<ApiRespostaSimples<UsuarioDto>>(RotaBase);
            return resposta?.Dados;
        }
        public async Task<ApiRespostaSimples<object>> AtualizarPerfilAsync(AtualizarPerfilDto dados)
        {
            return await PutAsync<ApiRespostaSimples<object>>(RotaBase, dados);
        }
        public async Task<ApiRespostaSimples<object>> AlterarSenhaAsync(AlterarSenhaDto dados)
        {
            return await PutAsync<ApiRespostaSimples<object>>($"{RotaBase}/senha", dados);
        }
    }


}
