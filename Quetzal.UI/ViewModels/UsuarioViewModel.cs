using System.ComponentModel.DataAnnotations;

namespace Quetzal.UI.ViewModels
{
    // Tela: /Admin/Usuarios (listagem e detalhe)
    // Mapeia de: UsuarioDto (Quetzal.Application)
  
    public class UsuarioViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = string.Empty;

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Cadastrado em")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Perfis")]
        public List<string> Perfis { get; set; } = new();

        // Campos calculados só para exibição — evitam lógica dentro do .cshtml

        public string PerfisFormatados =>
            Perfis.Any() ? string.Join(", ", Perfis) : "Sem perfil";

        public bool EhAdmin =>
            Perfis.Contains("Admin", StringComparer.OrdinalIgnoreCase);

        public string StatusTexto => Ativo ? "Ativo" : "Inativo";

        public string StatusCssClass => Ativo ? "badge bg-success" : "badge bg-secondary";
    }
}
