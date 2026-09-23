using System.ComponentModel.DataAnnotations;

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

        // Somente leitura -- agora a API expõe UsuarioNome, não precisa
        // mais mostrar só o Id cru
        [Display(Name = "Cliente")]
        public string ClienteNome { get; set; } = string.Empty;

        public bool EhEdicao => Id.HasValue && Id.Value > 0;

        public string TituloPagina => "Editar Projeto";

         // CriarProjetoCDto no final desta resposta
        public string? UsuarioId { get; set; }
    }
}
