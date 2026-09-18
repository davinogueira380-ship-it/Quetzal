using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Admin/Ambientes (listagem) e exibição pública
    // Mapeia de: AmbienteDto (Quetzal.Application)
    public class AmbienteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public string? ImagemUrl { get; set; }
        public int TotalAmbientes { get; set; }

        // STHEFANNY Aqui ↑

        [Display(Name = "Imagem")]
        public string? ImagemUpload { get; set; }

        public string StatusTexto => Ativo ? "Ativo" : "Inativo";

        public string StatusCssClass => Ativo ? "badge bg-success" : "badge bg-secondary";
    }
}

