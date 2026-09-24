using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Infraestrutura;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Areas.Admin.Controllers
{
    // CRUD de Ambientes (nome + imagem exibidos na Home e usados para
    // categorizar Portfólio/Projetos de Cliente).
    [Area("Admin")]
    //[Authorize(Roles = "Admin,Operador")]
    public class AmbienteController : Controller
    {
        private readonly ApiCliente _api;
        private readonly ServicoUpload _upload;   // ← trocou de _ambiente para _upload

        public AmbienteController(ApiCliente api, ServicoUpload upload)
        {
            _api = api;
            _upload = upload;
        }

        // GET: /Admin/Ambiente
        public async Task<IActionResult> Index()
        {
            var resposta = await _api.GetAsync<List<AmbienteApiModelo>>("api/Ambiente/todas");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                TempData["MensagemErro"] = resposta.Mensagem;
                return View(new List<AmbienteViewModel>());
            }

            var viewModel = resposta.Dados.Select(a => new AmbienteViewModel
            {
                Id = a.Id,
                Nome = a.Nome,
                Descricao = a.Descricao,
                ImagemUpload = a.ImagemUpload,
                Ativo = a.Ativo,
                TotalProjetos = a.TotalProjetos,
                DataCadastro = a.DataCastro
            })
                .OrderByDescending(a => a.Ativo)
                .ThenBy(a => a.Nome, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return View(viewModel);
        }

        // GET: /Admin/Ambiente/Criar
        public IActionResult Criar()
        {
            return View(new AmbienteEdicaoViewModel());
        }

        // POST: /Admin/Ambiente/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AmbienteEdicaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string? caminhoImagem = null;

            if (viewModel.ImagemArquivo != null)
            {
                var resultado = await _upload.SalvarImagemAsync(viewModel.ImagemArquivo, "ambientes");

                if (!resultado.Sucesso)
                {
                    ModelState.AddModelError(nameof(viewModel.ImagemArquivo), resultado.Erro!);
                    return View(viewModel);
                }

                caminhoImagem = resultado.CaminhoRelativo;
            }

            var dto = new CriarAmbienteApiModelo
            {
                Nome = viewModel.Nome,
                Descricao = viewModel.Descricao,
                ImagemUpload = caminhoImagem
            };

            var resposta = await _api.PostAsync<AmbienteApiModelo, CriarAmbienteApiModelo>("api/Ambiente", dto);

            if (!resposta.Sucesso)
            {
                AdicionarErrosDaApi(resposta.Erros, resposta.Mensagem);
                return View(viewModel);
            }

            TempData["MensagemSucesso"] = "Ambiente cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Ambiente/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var resposta = await _api.GetAsync<AmbienteApiModelo>($"api/Ambiente/{id}");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                TempData["MensagemErro"] = "Ambiente não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var dados = resposta.Dados;
            var viewModel = new AmbienteEdicaoViewModel
            {
                Id = dados.Id,
                Nome = dados.Nome,
                Descricao = dados.Descricao,
                ImagemAtualUrl = dados.ImagemUpload,
                Ativo = dados.Ativo
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AmbienteEdicaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var caminhoImagem = viewModel.ImagemAtualUrl;

            if (viewModel.ImagemArquivo != null)
            {
                var resultado = await _upload.SalvarImagemAsync(viewModel.ImagemArquivo, "ambientes");

                if (!resultado.Sucesso)
                {
                    ModelState.AddModelError(nameof(viewModel.ImagemArquivo), resultado.Erro!);
                    return View(viewModel);
                }

                caminhoImagem = resultado.CaminhoRelativo;
            }

            var dto = new CriarAmbienteApiModelo
            {
                Nome = viewModel.Nome,
                Descricao = viewModel.Descricao,
                ImagemUpload = caminhoImagem
            };

            var resposta = await _api.PutAsync<AmbienteApiModelo, CriarAmbienteApiModelo>(
                $"api/Ambiente/{id}/atualizar", dto);

            if (!resposta.Sucesso)
            {
                AdicionarErrosDaApi(resposta.Erros, resposta.Mensagem);
                return View(viewModel);
            }

            TempData["MensagemSucesso"] = "Ambiente atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Ambiente/Desativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desativar(int id)
        {
            var resposta = await _api.DeleteAsync<object>($"api/Ambiente/{id}/desativar");

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Ambiente desativado." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Ambiente/Reativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reativar(int id)
        {
            var resposta = await _api.PutAsync<object, object>($"api/Ambiente/{id}/reativar", new { });

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Ambiente reativado." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Ambiente/ExcluirPermanente/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirPermanente(int id)
        {
            var resposta = await _api.DeleteAsync<object>($"api/Ambiente/{id}/permanente");

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Ambiente excluído permanentemente." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        

        // era: string[]? erros  →  .Length
        // vira: List<string>? erros  →  .Count
        private void AdicionarErrosDaApi(List<string>? erros, string mensagemGeral)
        {
            if (erros != null && erros.Count > 0)
            {
                foreach (var erro in erros)
                {
                    ModelState.AddModelError(string.Empty, erro);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, mensagemGeral);
            }
        }

        // ── Modelos auxiliares para mapear a comunicação com a API ──
        // ATENCAO: a API grava o campo de data como "DataCastro" (com typo),
        // não "DataCadastro" -- o nome aqui precisa bater com o JSON exato.

        public class AmbienteApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string? Descricao { get; set; }
            public bool Ativo { get; set; }
            public DateTime DataCastro { get; set; }
            public int TotalProjetos { get; set; }
            public string? ImagemUpload { get; set; }
        }

        public class CriarAmbienteApiModelo
        {
            public string Nome { get; set; } = string.Empty;
            public string? Descricao { get; set; }
            public string? ImagemUpload { get; set; }
        }
    }
}
