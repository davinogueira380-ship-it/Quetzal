using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Quetzal.UI.ViewModels
{
    // Telas: /Admin/Projetos/Editar/{id}
    // o projeto nasce vinculado ao cliente em outro lugar (fora do admin) -- o admin
    // só edita o conteúdo de um projeto já existente.
    public class ProjetoCEdicaoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "O nome do Projeto é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do Projeto não pode ter mais que 200 caracteres.")]
        [Display(Name = "Nome do Projeto")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Imagem do Projeto")]
        public IFormFile? ImagemArquivo { get; set; }

        public string? ImagemAtualUrl { get; set; }

        // Vários ambientes por projeto (N:N) -- ids marcados nos checkboxes.
        // Mapeia para CriarProjetoCDto/AtualizarProjetoCDto.AmbientesIds
        [Required(ErrorMessage = "Selecione ao menos um ambiente.")]
        [MinLength(1, ErrorMessage = "Selecione ao menos um ambiente.")]
        [Display(Name = "Ambientes")]
        public List<int> AmbientesSelecionadosIds { get; set; } = new();

        public List<SelectListItem> AmbientesDisponiveis { get; set; } = new();

        // Somente leitura -- agora a API expõe UsuarioNome, não precisa
        // mais mostrar só o Id cru
        [Display(Name = "Cliente")]
        public string ClienteNome { get; set; } = string.Empty;

        public bool EhEdicao => Id.HasValue && Id.Value > 0;

        public string TituloPagina => "Editar Projeto";
    }
}
