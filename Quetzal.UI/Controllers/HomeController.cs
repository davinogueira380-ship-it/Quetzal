using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Quetzal.UI.Servicos;
using Quetzal.UI.Models;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiCliente _api;

        public HomeController(ApiCliente api)
        {
            _api = api;
        }

        // Landing page pública do site
        public async Task<IActionResult> Index()
        {
            var respostaPortfolio = await _api.GetAsync<List<PortfolioApiModelo>>("api/Portfolio");

            var portfoliosOrdenados = respostaPortfolio.Sucesso && respostaPortfolio.Dados != null
                ? respostaPortfolio.Dados.OrderByDescending(p => p.DataCadastro).ToList()
                : new List<PortfolioApiModelo>();

            var viewModel = new HomeViewModel
            {
                // Seção #portfolio da Home: mesma estrutura da página Portfolio/Ambiente,
                // com imagem e texto alternando de lado a cada projeto.
                Portfolios = portfoliosOrdenados.Select(p => new PortfolioViewModel
                {
                    Id = p.Id,
                    NomeProjeto = p.NomeProjeto,
                    Descricao = p.Descricao ?? string.Empty,
                    ImagemUpload = p.ImagemUpload,
                    DataCadastro = p.DataCadastro,
                    FotosSelecionadas = p.FotosSelecionadas
                        .OrderBy(f => f.Ordem)
                        .Select(f => new PortfolioFotoViewModel
                        {
                            ProjetoCFotoId = f.ProjetoCFotoId,
                            Foto = f.Foto,
                            Ordem = f.Ordem
                        })
                        .ToList()
                }).ToList(),

                // Pega até 8 fotos publicadas, ordenando primeiro pelos projetos mais recentes.
                ImagensCarrossel = portfoliosOrdenados
                    .SelectMany(p => p.FotosSelecionadas
                        .OrderBy(f => f.Ordem)
                        .Select(f => new ImagemCarrosselViewModel
                        {
                            Url = f.Foto,
                            TextoAlternativo = p.NomeProjeto
                        }))
                    .Take(8)
                    .ToList()
            };

            return View(viewModel);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contato(ContatoViewModel formulario)
        {
            if (!ModelState.IsValid)
            {
                // Guardamos os erros para reexibir depois do redirect.
                // (Alternativa: renderizar a Index direto, mas aí perdemos o PRG.)
                TempData["MensagemErroContato"] = "Confira os campos e tente novamente.";
                return RedirectToAction(nameof(Index));
            }

            var resposta = await _api.PostAsync<object, ContatoViewModel>("api/Contato", formulario);

            TempData[resposta.Sucesso ? "MensagemSucessoContato" : "MensagemErroContato"] =
                resposta.Sucesso
                    ? "Recebemos sua mensagem! Entraremos em contato em breve."
                    : "Não foi possível enviar agora. Tente pelo WhatsApp.";

            // Redirect (e não View) para o F5 não reenviar o formulário
            return RedirectToAction(nameof(Index));
        }

        //Add posteriormente, remover se ocorrer erros
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                // O TraceIdentifier é um código único daquela requisição.
                // Serve para o usuário te passar e você achar no log.
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }


        // Página estática "Sobre" o projeto/designer
        public IActionResult Sobre()
        {
            return View();
        }

        // -> corresponde a PortfolioDto na API
        public class PortfolioApiModelo
        {
            public int Id { get; set; }
            public string NomeProjeto { get; set; } = string.Empty;
            public string? Descricao { get; set; }
            public string? ImagemUpload { get; set; }
            public List<PortfolioFotoApiModelo> FotosSelecionadas { get; set; } = new();
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
