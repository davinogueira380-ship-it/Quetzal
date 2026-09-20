using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Telas: /Admin/Ambientes/Criar e /Admin/Ambientes/Editar/{id}
    // Mapeia para: CriarAmbienteDto (Quetzal.Application)
    public class AmbienteEdicaoViewModel
    {
        // Nulo na criação, preenchido na edição
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do ambiente é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        [Display(Name = "Nome do ambiente")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        // Upload — existe só na UI, nunca no DTO.
        // O Controller salva o arquivo e envia apenas o caminho para a API.
        [Display(Name = "Imagem do ambiente")]
        public IFormFile? ImagemArquivo { get; set; }

        // Caminho da imagem já salva, para exibir o preview na edição
        public string? ImagemAtualUrl { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;

        // Só de exibição — o título da página muda conforme o modo
        public bool EhEdicao => Id.HasValue && Id.Value > 0;

        public string TituloPagina => EhEdicao ? "Editar ambiente" : "Novo ambiente";
    }
}
