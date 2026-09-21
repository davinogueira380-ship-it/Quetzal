using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Controllers
{
    // Exibe os itens do portfólio filtrados por Ambiente, para onde o usuário é
    // levado ao clicar numa imagem da seção #ambientes na Home.
    public class PortfolioController : Controller
    {
        private readonly ApiCliente _api;

        public PortfolioController(ApiCliente api)
        {
            _api = api;
        }

        // GET: /Portfolio/Ambiente/5
        [HttpGet("Portfolio/Ambiente/{ambienteId:int}")]
        public async Task<IActionResult> Ambiente(int ambienteId)
        {
            var respostaAmbiente = await _api.GetAsync<AmbienteApiModelo>($"api/Ambiente/{ambienteId}");

            if (!respostaAmbiente.Sucesso || respostaAmbiente.Dados == null)
            {
                return NotFound();
            }

            var respostaPortfolio = await _api.GetAsync<List<PortfolioApiModelo>>(
                $"api/Portfolio/buscar?ambienteId={ambienteId}");

            var itens = respostaPortfolio.Sucesso && respostaPortfolio.Dados != null
                ? respostaPortfolio.Dados.Select(p => new PortfolioViewModel
                {
                    Id = p.Id,
                    NomeProjeto = p.NomeProjeto,
                    Descricao = p.Descricao,
                    ImagemUpload = p.ImagemUpload,
                    AmbienteNome = p.AmbienteNome,
                    Ativo = p.Ativo,
                    DataCadastro = p.DataCadastro
                }).ToList()
                : new List<PortfolioViewModel>();

            var viewModel = new PortfolioPorAmbienteViewModel
            {
                AmbienteId = respostaAmbiente.Dados.Id,
                AmbienteNome = respostaAmbiente.Dados.Nome,
                AmbienteDescricao = respostaAmbiente.Dados.Descricao,
                Itens = itens
            };

            return View(viewModel);
        }

        // ── Modelos auxiliares para mapear a comunicação com a API ──

        public class AmbienteApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string? Descricao { get; set; }
        }

        public class PortfolioApiModelo
        {
            public int Id { get; set; }
            public string NomeProjeto { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public string AmbienteNome { get; set; } = string.Empty;
            public bool Ativo { get; set; }
            public DateTime DataCadastro { get; set; }
        }
    }
}
