using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    public class PortfolioEdicaoViewModel
    {
        // Só existe quando é edição (Create não tem Id ainda)
        public int? Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(200, ErrorMessage = "O Título não pode ter mais que 200 caracteres.")]
        [Display(Name = "Título do Projeto")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        // Upload de arquivo -> isso NUNCA existe no DTO, é 100% coisa de View
        [Display(Name = "Imagem do Projeto")]
        public IFormFile? ImagemArquivo { get; set; }

        // Guarda o caminho já salvo (pra mostrar preview na edição)
        public string? ImagemAtualUrl { get; set; }

        [Required(ErrorMessage = "O ambiente é obrigatório.")]
        [Display(Name = "Ambiente")]
        public int AmbienteId { get; set; }

        // Só existe pra popular o <select asp-for="AmbienteId" asp-items="Model.Ambientes">
        public List<SelectListItem> Ambientes { get; set; } = new();
    }
}