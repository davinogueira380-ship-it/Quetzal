namespace Quetzal.UI.ViewModels
{
    public class AmbienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
        public string? ImagemUrl { get; set; }

        public int TotalAmbientes { get; set; }
    }
}


// STHEFANNY Aqui ↑