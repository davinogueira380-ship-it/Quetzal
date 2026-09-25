namespace Quetzal.UI.ViewModels
{
    // A Home agora mostra duas coisas diferentes, então ela precisa de um
    // modelo próprio em vez de receber List<AmbienteViewModel> direto.
    public class HomeViewModel
    {
        public List<PortfolioViewModel> Portfolios { get; set; } = new();
        public List<ImagemCarrosselViewModel> ImagensCarrossel { get; set; } = new();
    }

    public class ImagemCarrosselViewModel
    {
        public string Url { get; set; } = string.Empty;
        public string TextoAlternativo { get; set; } = string.Empty;
    }
}