using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Areas.Admin.Controllers
{

    // CRUD completo de itens do Portfólio (vitrine pública do site).
    // Index -> Criar (GET/POST) -> Editar (GET/POST) -> Desativar/Reativar/ExcluirPermanente
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PortfolioController : Controller
    {
        private readonly ApiCliente _api;
        private readonly IWebHostEnvironment _ambiente;

        public PortfolioController(ApiCliente api, IWebHostEnvironment ambiente)
        {
            _api = api;
            _ambiente = ambiente;
        }

        // GET: /Admin/Portfolio
        public async Task<IActionResult> Index()
        {
            var resposta = await _api.GetAsync<List<PortfolioApiModelo>>("api/Portfolio/todos");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                TempData["MensagemErro"] = resposta.Mensagem;
                return View(new List<PortfolioViewModel>());
            }

            var viewModel = resposta.Dados.Select(p => new PortfolioViewModel
            {
                Id = p.Id,
                NomeProjeto = p.NomeProjeto,
                Descricao = p.Descricao,
                ImagemUpload = p.ImagemUpload,
                AmbienteNome = p.AmbienteNome,
                Ativo = p.Ativo,
                DataCadastro = p.DataCadastro
            }).ToList();

            return View(viewModel);
        }

        // GET: /Admin/Portfolio/Criar
        public async Task<IActionResult> Criar()
        {
            var viewModel = new PortfolioEdicaoViewModel();
            await PreencherDropdownAmbientes(viewModel);
            return View(viewModel);
        }

        // POST: /Admin/Portfolio/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(PortfolioEdicaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherDropdownAmbientes(viewModel);
                return View(viewModel);
            }

            var caminhoImagem = await SalvarImagemAsync(viewModel.ImagemArquivo, "portfolio");

            var dto = new CriarPortfolioApiModelo
            {
                NomeProjeto = viewModel.NomeProjeto,
                Descricao = viewModel.Descricao,
                ImagemUpload = caminhoImagem ?? string.Empty,
                AmbienteId = viewModel.AmbienteId
            };

            var resposta = await _api.PostAsync<PortfolioApiModelo, CriarPortfolioApiModelo>("api/Portfolio", dto);

            if (!resposta.Sucesso)
            {
                AdicionarErrosDaApi(resposta.Erros, resposta.Mensagem);
                await PreencherDropdownAmbientes(viewModel);
                return View(viewModel);
            }

            TempData["MensagemSucesso"] = "Projeto adicionado ao portfólio com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Portfolio/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var resposta = await _api.GetAsync<PortfolioApiModelo>($"api/Portfolio/{id}");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                TempData["MensagemErro"] = "Projeto não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var dados = resposta.Dados;
            var viewModel = new PortfolioEdicaoViewModel
            {
                Id = dados.Id,
                NomeProjeto = dados.NomeProjeto,
                Descricao = dados.Descricao,
                ImagemAtualUrl = dados.ImagemUpload,
                AmbienteId = dados.AmbienteId
            };

            await PreencherDropdownAmbientes(viewModel);
            return View(viewModel);
        }

        // POST: /Admin/Portfolio/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, PortfolioEdicaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherDropdownAmbientes(viewModel);
                return View(viewModel);
            }

            // Se o admin não trocou a imagem, mantém a que já estava salva.
            // Só sobrescreve se um novo arquivo foi enviado no formulário.
            var caminhoImagem = viewModel.ImagemAtualUrl ?? string.Empty;
            if (viewModel.ImagemArquivo != null)
            {
                caminhoImagem = await SalvarImagemAsync(viewModel.ImagemArquivo, "portfolio") ?? caminhoImagem;
            }

            var dto = new AtualizarPortfolioApiModelo
            {
                Id = id,
                NomeProjeto = viewModel.NomeProjeto,
                Descricao = viewModel.Descricao,
                ImagemUpload = caminhoImagem,
                AmbienteId = viewModel.AmbienteId
            };

            var resposta = await _api.PutAsync<PortfolioApiModelo, AtualizarPortfolioApiModelo>(
                $"api/Portfolio/{id}/atualizar", dto);

            if (!resposta.Sucesso)
            {
                AdicionarErrosDaApi(resposta.Erros, resposta.Mensagem);
                await PreencherDropdownAmbientes(viewModel);
                return View(viewModel);
            }

            TempData["MensagemSucesso"] = "Projeto atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Portfolio/Desativar/5
        // Soft delete -- o registro continua no banco, só sai da vitrine pública
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desativar(int id)
        {
            var resposta = await _api.DeleteAsync<object>($"api/Portfolio/{id}/desativar");

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Projeto desativado." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Portfolio/Reativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reativar(int id)
        {
            var resposta = await _api.PutAsync<object, object>($"api/Portfolio/{id}/reativar", new { });

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Projeto reativado." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Portfolio/ExcluirPermanente/5
        // Hard delete -- remove de vez do banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirPermanente(int id)
        {
            var resposta = await _api.DeleteAsync<object>($"api/Portfolio/{id}/permanente");

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Projeto excluído permanentemente." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // Busca os ambientes ativos na API e monta a lista de opções do dropdown
        private async Task PreencherDropdownAmbientes(PortfolioEdicaoViewModel viewModel)
        {
            var resposta = await _api.GetAsync<List<AmbienteApiModelo>>("api/Ambiente");

            if (resposta.Sucesso && resposta.Dados != null)
            {
                viewModel.Ambientes = resposta.Dados
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Nome,
                        Selected = a.Id == viewModel.AmbienteId
                    })
                    .ToList();
            }
        }

        // Salva o arquivo enviado em wwwroot/uploads/{pasta}/ com um nome único,
        // e devolve o caminho relativo para guardar no banco (via API).
        //
        // OBS: a API espera ImagemUpload como string simples (um caminho),
        // não como upload multipart -- por isso o arquivo é salvo aqui na UI,
        // não enviado para a API.
        private async Task<string?> SalvarImagemAsync(IFormFile? arquivo, string pasta)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return null;
            }

            var extensao = Path.GetExtension(arquivo.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var pastaFisica = Path.Combine(_ambiente.WebRootPath, "uploads", pasta);

            Directory.CreateDirectory(pastaFisica);

            var caminhoFisico = Path.Combine(pastaFisica, nomeArquivo);
            using (var stream = new FileStream(caminhoFisico, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Caminho relativo, o que fica salvo no banco via API
            return $"/uploads/{pasta}/{nomeArquivo}";
        }

        // Traduz os erros vindos da API para o ModelState, para aparecerem
        // junto com os campos do formulário via asp-validation-summary
        private void AdicionarErrosDaApi(string[]? erros, string mensagemGeral)
        {
            if (erros != null && erros.Length > 0)
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

        // -> corresponde a PortfolioDto na API
        public class PortfolioApiModelo
        {
            public int Id { get; set; }
            public string NomeProjeto { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public int AmbienteId { get; set; }
            public string AmbienteNome { get; set; } = string.Empty;
            public bool Ativo { get; set; }
            public DateTime DataCadastro { get; set; }
        }

        // -> corresponde a CriarPortfolioDto na API
        public class CriarPortfolioApiModelo
        {
            public string NomeProjeto { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public int AmbienteId { get; set; }
        }

        // -> corresponde a AtualizarPortfolioDto na API
        public class AtualizarPortfolioApiModelo : CriarPortfolioApiModelo
        {
            public int Id { get; set; }
        }

        // -> corresponde a AmbienteDto na API (usado só para o dropdown)
        public class AmbienteApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
        }
    }
}



