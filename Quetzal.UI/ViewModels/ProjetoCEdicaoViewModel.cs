using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    public class ProjetoCEdicaoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do Projeto é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do Projeto não pode ter mais que 200 caracteres.")]
        [Display(Name = "Nome do Projeto")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Imagem do Projeto")]
        public IFormFile? ImagemArquivo { get; set; }
        public string? ImagemAtualUrl { get; set; }

        // Antes: int AmbienteId
        // Agora: lista de ids selecionados pelo admin
        [Required(ErrorMessage = "Selecione ao menos um ambiente.")]
        [Display(Name = "Ambientes")]
        public List<int> AmbientesSelecionadosIds { get; set; } = new();

        // Todas as opções disponíveis pra montar os checkboxes/multi-select
        public List<SelectListItem> AmbientesDisponiveis { get; set; } = new();

        [Display(Name = "Cliente")]
        public string ClienteNome { get; set; } = string.Empty;
    }
}

