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

        [Display(Name = "Imagem")]
        public string? ImagemUpload { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Projetos vinculados")]
        public int TotalProjetos { get; set; }

        [Display(Name = "Cadastrado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        // Campos calculados só para exibição

        public string StatusTexto => Ativo ? "Ativo" : "Inativo";

        public string StatusCssClass => Ativo ? "badge bg-success" : "badge bg-secondary";
    }
}
