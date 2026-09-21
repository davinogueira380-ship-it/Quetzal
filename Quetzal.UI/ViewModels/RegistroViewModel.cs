using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Conta/Registro
    // Mapeia para: RegistrarUserDto (Quetzal.Application)
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome não pode exceder 200 caracteres.")]
        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O telefone deve conter 11 dígitos incluindo DDD.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Digite apenas números, incluindo DDD.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        public string ConfirmarSenha { get; set; } = string.Empty;

    }
}
