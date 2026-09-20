namespace Quetzal.UI.ViewModels
{
    // Tela pública: /Portfolio/Ambiente/{id}
    // Para onde o usuário vai ao clicar numa imagem da seção #ambientes na Home
    public class PortfolioPorAmbienteViewModel
    {
        public int AmbienteId { get; set; }
        public string AmbienteNome { get; set; } = string.Empty;
        public string? AmbienteDescricao { get; set; }
        public List<PortfolioViewModel> Itens { get; set; } = new();
    }
}
