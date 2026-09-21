using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    public class ContatoViewModel
    {
        [Required(ErrorMessage = "Informe seu nome.")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe seu e-mail.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe seu telefone.")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Digite apenas números, com DDD.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Conte um pouco sobre o que você precisa.")]
        [StringLength(1000, ErrorMessage = "A mensagem deve ter no máximo 1000 caracteres.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Como podemos ajudar")]
        public string Mensagem { get; set; } = string.Empty;

        // Checkboxes múltiplos: o model binder preenche a lista sozinho
        // desde que todos os inputs tenham name="FormasContato"
        [MinLength(1, ErrorMessage = "Escolha ao menos uma forma de contato.")]
        [Display(Name = "Forma de contato preferida")]
        public List<string> FormasContato { get; set; } = new();
    }
}