using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Admin/Projetos (listagem) e /Admin/Projetos/Detalhes/{id}
    // Mapeia de: ProjetoCDto (via ProjetoCController.ProjetoCApiModelo)
    public class ProjetoCViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome do Projeto")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Imagem")]
        public string? ImagemUpload { get; set; }

        // Dados do cliente dono do projeto — somente leitura, vindos do
        // ProjetoCDto.ClienteNome
        [Display(Name = "Cliente")]
        public string ClienteNome { get; set; } = string.Empty;

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
