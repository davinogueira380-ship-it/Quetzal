using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.ApiClientes
{
    public class UsuarioDto
    {
        public string Id { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        // Lista de papeis (roles) do usuario
        public List<string> Perfis { get; set; } = new List<string>();
    }
    public class UsuarioApiCliente : ClienteHttp
    {

        private const string RotaBase = "/api/usuarios";
        public async Task<List<UsuarioDto>> ObterTodosAsync()
        {
            var resposta = await GetAsync<ApiRespostaSimples<List<UsuarioDto>>>(RotaBase);
            return resposta?.Dados ?? new List<UsuarioDto>();
        }
        // Metodo de desativacao (remocao logica)

        public async Task<ApiRespostaSimples<object>> DesativarAsync(string id)
        {
            return await DeleteAsync<ApiRespostaSimples<object>>($"{RotaBase}/{id}");
        }
    }

}
