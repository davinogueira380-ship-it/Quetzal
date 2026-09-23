using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Areas.Cliente.Controllers
{
    // ═══════════════════════════════════════════════════════════════════
    // ATENCAO -- BLOQUEADO ATE A API IMPLEMENTAR:
    //   GET api/ProjetoC/meu   (projeto do usuario logado)
    // ═══════════════════════════════════════════════════════════════════
    //
    // O Cliente só enxerga esta única tela -- nenhum CRUD, só leitura do
    // estado atual do projeto que a Admin vinculou a ele.
    [Area("Cliente")]
    //[Authorize(Roles = "Cliente")]
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
                ImagemUpload = dados.ImagemUpload,
                AmbientesNomes = dados.Ambientes.Select(a => a.Nome).ToList(),
                Ativo = dados.Ativo
            };

            return View(viewModel);
        }

        // ── Modelos auxiliares para mapear a comunicação com a API ──

        public class ProjetoCApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public List<AmbienteApiModelo> Ambientes { get; set; } = new();
            public bool Ativo { get; set; }
        }

        public class AmbienteApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
        }
    }
}
