using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Quetzal.UI.ViewModels
{
    // Telas: /Admin/Portfolio/Criar e /Admin/Portfolio/Editar/{id}
    public class PortfolioEdicaoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do projeto é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do projeto não pode ter mais que 200 caracteres.")]
        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        // Mantido por compatibilidade com outros pontos da UI.
        // Portfolio não faz mais upload de imagem; usa fotos de ProjetoC.
        [Display(Name = "Imagem do Projeto")]
        public IFormFile? ImagemArquivo { get; set; }

        public string? ImagemAtualUrl { get; set; }

        [Required(ErrorMessage = "O ambiente é obrigatório.")]
        [Display(Name = "Ambiente")]
        public int AmbienteId { get; set; }

        [Required(ErrorMessage = "O projeto do cliente é obrigatório.")]
        [Display(Name = "Projeto do Cliente")]
        public int? ProjetoCId { get; set; }

        // IDs das fotos já cadastradas em ProjetoC que serão publicadas.
        [MinLength(1, ErrorMessage = "Selecione pelo menos uma foto para publicar no portfólio.")]
        public List<int> ProjetoCFotosIds { get; set; } = new();

        public List<SelectListItem> AmbientesDisponiveis { get; set; } = new();

        public List<ProjetoCPortfolioViewModel> ProjetosDisponiveis { get; set; } = new();

        public bool EhEdicao => Id.HasValue && Id.Value > 0;

        public string TituloPagina => EhEdicao
            ? "Editar projeto do portfólio"
            : "Novo projeto no portfólio";
    }

    public class ProjetoCPortfolioViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public List<ProjetoCFotoPortfolioViewModel> Fotos { get; set; } = new();
    }

    public class ProjetoCFotoPortfolioViewModel
    {
        public int Id { get; set; }

        public string Foto { get; set; } = string.Empty;

        public int Ordem { get; set; }
    }
}
