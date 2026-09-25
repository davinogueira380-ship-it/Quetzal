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
            var respostaAmbiente = await _api.GetAsync<AmbienteApiModelo>(
                $"api/Ambiente/{ambienteId}");

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
                    ProjetoCId = p.ProjetoCId,
                    FotosSelecionadas = p.FotosSelecionadas
                        .OrderBy(f => f.Ordem)
                        .Select(f => new PortfolioFotoViewModel
                        {
                            ProjetoCFotoId = f.ProjetoCFotoId,
                            // Foto = f.Foto, foi trocado pelo codigo abaixo Kelly 24-09
                            Foto = PrepararImagemParaExibicao(f.Foto),
                            Ordem = f.Ordem
                        })
                        .ToList(),
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
        private static string PrepararImagemParaExibicao(string? foto) // metodo adicionado Kelly 24-09
        {
            if (string.IsNullOrWhiteSpace(foto))
                return string.Empty;

            // Já é uma imagem Base64 pronta para o navegador.
            if (foto.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
                return foto;

            // É um caminho/URL de imagem já existente, por exemplo:
            // /uploads/projetos/foto.jpg
            if (foto.StartsWith("/") ||
                foto.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                foto.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return foto;
            }

            // Caso contrário, tratamos como Base64 puro vindo da Desktop.
            // Detecta o tipo mais comum pelo início do Base64.
            string tipoImagem;

            if (foto.StartsWith("iVBOR", StringComparison.Ordinal))
                tipoImagem = "image/png";
            else if (foto.StartsWith("UklGR", StringComparison.Ordinal))
                tipoImagem = "image/webp";
            else if (foto.StartsWith("Qk", StringComparison.Ordinal))
                tipoImagem = "image/bmp";
            else
                tipoImagem = "image/jpeg";

            return $"data:{tipoImagem};base64,{foto}";
        }
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
            public int? ProjetoCId { get; set; }
            public List<PortfolioFotoApiModelo> FotosSelecionadas { get; set; } = new();
            public bool Ativo { get; set; }
            public DateTime DataCadastro { get; set; }
        }

        public class PortfolioFotoApiModelo
        {
            public int ProjetoCFotoId { get; set; }
            public string Foto { get; set; } = string.Empty;
            public int Ordem { get; set; }
        }
    }
}
