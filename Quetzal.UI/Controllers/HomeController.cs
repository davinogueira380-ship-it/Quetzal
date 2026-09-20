using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Servicos;
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
            // Busca os ambientes ativos para a seção #ambientes.
            // Cada card leva para /Portfolio/Ambiente/{id}, com os projetos
            // publicados naquele ambiente específico.
            var resposta = await _api.GetAsync<List<AmbienteApiModelo>>("api/Ambiente");

            var ambientes = resposta.Sucesso && resposta.Dados != null
                ? resposta.Dados.Select(a => new AmbienteViewModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Descricao = a.Descricao,
                    ImagemUpload = a.ImagemUpload,
                    Ativo = a.Ativo
                }).ToList()
                : new List<AmbienteViewModel>();

            return View(ambientes);
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
    }
}
