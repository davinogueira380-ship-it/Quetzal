namespace Quetzal.UI.ViewModels
{
    public class DashboardEstatisticasViewModel
    {

        // Contadores gerais por entidade
        public int TotalAmbientes { get; set; }
        public int AmbientesAtivos { get; set; }
        public int AmbientesDesativados { get; set; }

        public int TotalUsuarios { get; set; }
        public int TotalAdministradores { get; set; }
        public int TotalOperadores { get; set; }
        public int TotalClientes { get; set; }

        public int TotalPortfolios { get; set; }
        public int PortfoliosAtivos { get; set; }
        public int PortfoliosDesativados { get; set; }

        public int TotalProjetosC { get; set; }
        public int ProjetosCAtivos { get; set; }

        // Dados para gráficos simples
        public List<GraficoItemViewModel> AmbientesPorPortfolio { get; set; } = new();
        public List<GraficoItemViewModel> PortfoliosPorAmbiente { get; set; } = new();
        public List<GraficoItemViewModel> ProjetosCPorAmiente { get; set; } = new();

        // Séries/labels para gráficos de tempo (ex.: novas inscrições por mês)
        public List<string> Labels { get; set; } = new();
        public List<GraficoItemViewModel> Series { get; set; } = new();

        // Listas úteis para cards / tabelas (recém-criados / top)
        public List<GraficoItemViewModel> UltimosAmbientes { get; set; } = new();
        public List<GraficoItemViewModel> UltimosUsuarios { get; set; } = new();
        public List<GraficoItemViewModel> UltimosPortfolios { get; set; } = new();

    }

    // Item simples para gráficos (label + valor numérico)
    public class GraficoItemViewModel
    {
        public string Label { get; set; } = string.Empty;
        public int Value { get; set; }
        public string? Color { get; set; }
    }

}