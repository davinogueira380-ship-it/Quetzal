using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Quetzal.UI.ViewModels
{
    // Telas: /Admin/Portfolio/Criar e /Admin/Portfolio/Editar/{id}
    // Mapeia para: CriarPortfolioDto / AtualizarPortfolioDto (via PortfolioController)
    public class PortfolioEdicaoViewModel
    {
        // Nulo na criação, preenchido na edição
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do projeto é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do projeto não pode ter mais que 200 caracteres.")]
        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        // Upload -- existe só na UI, nunca é enviado como IFormFile para a API.
        // O Controller salva o arquivo em wwwroot e manda só o caminho (string).
        [Display(Name = "Imagem do Projeto")]
        public IFormFile? ImagemArquivo { get; set; }

        // Caminho da imagem já salva, para mostrar o preview na edição
        public string? ImagemAtualUrl { get; set; }

        [Required(ErrorMessage = "O ambiente é obrigatório.")]
        [Display(Name = "Ambiente")]
        public int AmbienteId { get; set; }

        // Populado pelo Controller a partir de GET api/Ambiente,
        // para montar o <select asp-for="AmbienteId" asp-items="Model.AmbientesDisponiveis">
        public List<SelectListItem> AmbientesDisponiveis { get; set; } = new();

        // Só de exibição -- o título da página muda conforme o modo
        public bool EhEdicao => Id.HasValue && Id.Value > 0;

        public string TituloPagina => EhEdicao ? "Editar projeto do portfólio" : "Novo projeto no portfólio";
    }
}
