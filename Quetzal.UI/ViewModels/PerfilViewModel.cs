using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Conta/Perfil (usuário logado edita os próprios dados)
    // Mapeia para: AtualizarPerfilDto (Quetzal.Application)
    public class PerfilViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres.")]
        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O telefone deve conter 11 dígitos incluindo DDD.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        // Somente leitura — contexto para o usuário, não é editável
        [Display(Name = "Cliente desde")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }
    }

    // Tela: /Conta/AlterarSenha
    // Mapeia para: AlterarSenhaDto (Quetzal.Application)
    //
    // Separado do PerfilViewModel de proposito: sao dois formularios
    // independentes, com POSTs e validacoes distintas. Juntar os dois numa
    // unica ViewModel faria o [Required] da senha bloquear a simples
    // atualizacao de nome/telefone.
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string SenhaAtual { get; set; } = string.Empty;

        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação da nova senha é obrigatória.")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }
}
