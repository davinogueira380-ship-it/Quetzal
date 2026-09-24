using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Admin/Portfolio e telas públicas de Portfolio.
    public class PortfolioViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        // Compatibilidade com registros antigos.
        [Display(Name = "Imagem")]
        public string? ImagemUpload { get; set; }

        [Display(Name = "Ambiente")]
        public string AmbienteNome { get; set; } = string.Empty;

        public int? ProjetoCId { get; set; }

        public List<PortfolioFotoViewModel> FotosSelecionadas { get; set; } = new();

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Cadastrado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        public string StatusTexto => Ativo ? "Ativo" : "Inativo";

        public string StatusCssClass => Ativo ? "badge bg-success" : "badge bg-secondary";
    }

    public class PortfolioFotoViewModel
    {
        public int ProjetoCFotoId { get; set; }

        public string Foto { get; set; } = string.Empty;

        public int Ordem { get; set; }
    }
}
