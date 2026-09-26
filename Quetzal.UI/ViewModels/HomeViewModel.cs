namespace Quetzal.UI.ViewModels
{
    // A Home agora mostra duas coisas diferentes, então ela precisa de um
    // modelo próprio em vez de receber List<AmbienteViewModel> direto.
    public class HomeViewModel
    {
        public List<PortfolioViewModel> Portfolios { get; set; } = new();
        public List<ImagemCarrosselViewModel> ImagensCarrossel { get; set; } = new();

        // Nomes distintos de ambiente entre os portfólios exibidos, usados
        // nos links de filtro logo abaixo da seção #portfolio.
        public List<string> AmbientesFiltro { get; set; } = new();
    }

    public class ImagemCarrosselViewModel
    {
        public string Url { get; set; } = string.Empty;
        public string TextoAlternativo { get; set; } = string.Empty;
    }
}