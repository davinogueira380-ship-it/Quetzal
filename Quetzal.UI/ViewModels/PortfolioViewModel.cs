using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Admin/Portfolio (listagem)
    // Mapeia de: PortfolioDto (via PortfolioController.ApiModelo, ver PortfolioController.cs)
    public class PortfolioViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Imagem")]
        public string? ImagemUpload { get; set; }

        [Display(Name = "Ambiente")]
        public string AmbienteNome { get; set; } = string.Empty;

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Cadastrado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        // Campos calculados só para exibição

        public string StatusTexto => Ativo ? "Ativo" : "Inativo";

        public string StatusCssClass => Ativo ? "badge bg-success" : "badge bg-secondary";
    }
}
