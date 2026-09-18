using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Conta/Login
    // Mapeia para: LoginDto (Quetzal.Application)
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; } = string.Empty;

        // Só existe na UI — não vai para a API.
        // Controla se o cookie/token é persistido além da sessão do navegador.
        //[Display(Name = "Manter-me conectado")]
        public bool LembrarMe { get; set; }

        // Só existe na UI — para onde redirecionar após o login.
        // Ex: usuário tentou acessar /Admin/Dashboard sem estar logado,
        // foi mandado para o login, e depois deve voltar para lá.
        //public string? RetornoUrl { get; set; }
    }
}
}
