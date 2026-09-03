using SenacFlix.Desktop.ApiClientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Desktop.ApiClientes
{
        
        public class LoginDto
    {

        public string Email { get; set; }

        public string Senha { get; set; }
    }
    public class LoginRespostaDto
    {
        // Token JWT gerado pelo servidor para autorizar requisicoes futuras
        public string Token { get; set; }

        // Data e hora de expiracao do token JWT
        public string Expiracao { get; set; }

        public string NomeUsuario { get; set; }

        public string Email { get; set; }

        // Lista de perfis/roles do usuario (ex: "Admin", "Operador")
        public List<string> Perfis { get; set; }

    }
    public class ApiRespostaSimples<T>
    {
        
        public bool Sucesso { get; set; }

       
        public string Mensagem { get; set; }

        public T Dados { get; set; }
    }

    public class AuthApiUsuario : UsuarioHttp
    {
        // --------------------------------------------------------
        // Rotas da API de autenticacao
        // --------------------------------------------------------

        // Rota do endpoint de login da API
        private const string RotaLogin = "/api/autenticacao/login";

        public async Task<ApiRespostaSimples<LoginRespostaDto>> LoginAsync(string email, string senha)
        {
            // Monta o objeto DTO com as credenciais do usuario
            var credenciais = new LoginDto
            {
                Email = email,  // Define o e-mail para autenticacao
                Senha = senha   // Define a senha para autenticacao
            };

            // Chama o metodo POST sem autenticacao (rota publica de login)
            // Retorna o envelope ApiRespostaSimples contendo o token e dados do usuario
            return await PostSemAutenticacaoAsync<ApiRespostaSimples<LoginRespostaDto>>(
                RotaLogin,      // Rota do endpoint de login
                credenciais     // Corpo da requisicao com as credenciais
            );
        }
    }

}
