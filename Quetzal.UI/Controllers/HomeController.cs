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
            // Duas chamadas independentes — dispara as duas e espera juntas,
            // em vez de uma depois da outra. Corta o tempo de carga quase pela metade.
            var tarefaAmbientes = _api.GetAsync<List<AmbienteApiModelo>>("api/Ambiente");
            var tarefaCarrossel = _api.GetAsync<List<PortfolioApiModelo>>("api/Portfolio");

            await Task.WhenAll(tarefaAmbientes, tarefaCarrossel);

            var respostaAmbientes = await tarefaAmbientes;
            var respostaCarrossel = await tarefaCarrossel;

            var viewModel = new HomeViewModel
            {
                Ambientes = respostaAmbientes.Sucesso && respostaAmbientes.Dados != null
                    ? respostaAmbientes.Dados.Select(a => new AmbienteViewModel
                    {
                        Id = a.Id,
                        Nome = a.Nome,
                        Descricao = a.Descricao,
                        ImagemUpload = a.ImagemUpload,
                        Ativo = a.Ativo
                    }).ToList()
                    : new List<AmbienteViewModel>(),

                // Pega só os 8 mais recentes que tenham imagem.
                // O carrossel não precisa de tudo, e a Home fica leve.
                ImagensCarrossel = respostaCarrossel.Sucesso && respostaCarrossel.Dados != null
                    ? respostaCarrossel.Dados
                        .Where(p => !string.IsNullOrWhiteSpace(p.ImagemUpload))
                        .OrderByDescending(p => p.DataCadastro)
                        .Take(8)
                        .Select(p => new ImagemCarrosselViewModel
                        {
                            Url = p.ImagemUpload,
                            TextoAlternativo = p.NomeProjeto
                        })
                        .ToList()
                    : new List<ImagemCarrosselViewModel>()
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

        // -> corresponde a AmbienteDto na API
        public class AmbienteApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string? Descricao { get; set; }
            public bool Ativo { get; set; }
            public string? ImagemUpload { get; set; }
        }

        // -> corresponde a PortfolioDto na API — só o que o carrossel precisa
        public class PortfolioApiModelo
        {
            public int Id { get; set; }
            public string NomeProjeto { get; set; } = string.Empty;
            public string? ImagemUpload { get; set; }
            public DateTime DataCadastro { get; set; }
        }
    }
}
