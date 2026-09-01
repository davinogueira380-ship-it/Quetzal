using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quetzal.Application.DTOs
{
    // Dto para exibir informações do usuário
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

    public class RegistrarUsuarioDto
    {
        [Required(ErrorMessage = "O Nome Completo é obrigatorio.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O E-mail e obrigatorio.")]
        [EmailAddress(ErrorMessage = "E-mail em formato invalido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O telefone deve conter 11 dígitos incluindo DDD.")]
        public string Telefone { get; set; } = string.Empty;


        [Required(ErrorMessage = "A Senha e obrigatoria.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmacao de senha e obrigatoria.")]
        [Compare("Senha", ErrorMessage = "As senhas nao coincidem.")]
        public string ConfirmarSenha { get; set; } = string.Empty;
        
    }
    public class LoginDto
    {
        // Criar regra para ativação do admin e usuário ATIVO
        [Required(ErrorMessage = "O E-mail e obrigatorio.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha e obrigatoria.")]
        public string Senha { get; set; } = string.Empty;
    }

    // Dto para atualizar dados do perfil do usuário LOGADO/ATIVO
    public class AtualizarPerfilDto
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O E-mail e obrigatorio.")]
        [EmailAddress(ErrorMessage = "E-mail em formato invalido.")]
        public string Email { get; set; } = string.Empty;

        public string? Telefone { get; set; }
    }
    // Dto para alteração de senha
    public class AlterarSenhaDto
    {
        [Required(ErrorMessage = "A Senha Atual é obrigatória.")]
        public string SenhaAtual { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Nova Senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres.")]
        public string NovaSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação da nova senha é obrigatória.")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }

}
