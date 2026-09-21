namespace Quetzal.UI.ViewModels
{
    // Tela: /Cliente/MeuProjeto (a única tela que o Cliente acessa)
    // Mostra o estado ATUAL do projeto
    public class MeuProjetoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string? ImagemUpload { get; set; }
        public List<string> AmbientesNomes { get; set; } = new();
        public bool Ativo { get; set; }

        public string AmbientesFormatados =>
            AmbientesNomes.Any() ? string.Join(", ", AmbientesNomes) : "Nenhum ambiente definido ainda";
    }
}
