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

        // Todas as fotos do projeto, já prontas para exibir no <img src>
        public List<string> Fotos { get; set; } = new();
        public bool Ativo { get; set; }
    }
}
