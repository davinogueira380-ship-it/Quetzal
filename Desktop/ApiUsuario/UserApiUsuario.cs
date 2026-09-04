using Quetzal.Desktop.ApiClientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.ApiUsuario
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
    public class UserApiUsuario : UsuarioHttp
    {
        private const string RotaBase = "/api/usuarios";
    
    public async Task<List<UsuarioDto>> ObterTodosAsync()
        {
            var resposta = await GetAsync<ApiRespostaSimples<List<UsuarioDto>>>(RotaBase);
            return resposta?.Dados ?? new List<UsuarioDto>();
        }
        public async Task<ApiRespostaSimples<object>> DesativarAsync(string id)
        {
            // Chama DELETE /api/usuarios/{id} para desativar o usuario na API
            return await DeleteAsync<ApiRespostaSimples<object>>($"{RotaBase}/{id}");
        }

    } }