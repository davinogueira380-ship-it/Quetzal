using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Areas.Cliente.Controllers
{
    // O Cliente e o Usuario só enxergam esta única tela -- nenhum CRUD, só leitura do
    // estado atual do projeto que a Admin vinculou a ele.
    [Area("Cliente")]
    [Authorize(Roles = "Cliente,Usuario")]
    public class MeuProjetoController : Controller
    {
        private readonly ApiCliente _api;

        public MeuProjetoController(ApiCliente api)
        {
            _api = api;
        }

        // GET: /Cliente/MeuProjeto
        public async Task<IActionResult> Index()
        {
            var resposta = await _api.GetAsync<ProjetoCApiModelo>("api/ProjetoC/meu");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                // Cliente ainda não tem nenhum projeto vinculado pela Admin --
                // não é um erro, é um estado válido de "aguardando vínculo"
                return View("SemProjeto");
            }

            var dados = resposta.Dados;
            var viewModel = new MeuProjetoViewModel
            {
                Id = dados.Id,
                Nome = dados.Nome,
                Descricao = dados.Descricao,
                // A API devolve a lista de fotos ordenada; a capa é a primeira.
                ImagemUpload = dados.Fotos.FirstOrDefault(),
                Ativo = dados.Ativo
            };

            return View(viewModel);
        }

        // ── Modelos auxiliares para mapear a comunicação com a API ──

        // -> corresponde a ProjetoCDto na API
        public class ProjetoCApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public List<string> Fotos { get; set; } = new();
            public bool Ativo { get; set; }
        }

        
    }
}
