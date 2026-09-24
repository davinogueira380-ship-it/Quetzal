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
    //[Authorize(Roles = "Admin,Operador")]
    public class PortfolioController : Controller
    {
        private readonly ApiCliente _api;

        public PortfolioController(ApiCliente api)
        {
            _api = api;
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
                ProjetoCId = p.ProjetoCId,
                FotosSelecionadas = p.FotosSelecionadas
                    .OrderBy(f => f.Ordem)
                    .Select(f => new PortfolioFotoViewModel
                    {
                        ProjetoCFotoId = f.ProjetoCFotoId,
                        Foto = f.Foto,
                        Ordem = f.Ordem
                    })
                    .ToList(),
                Ativo = p.Ativo,
                DataCadastro = p.DataCadastro
            })
                .OrderByDescending(p => p.Ativo)
                .ThenBy(p => p.NomeProjeto, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return View(viewModel);
        }

        // GET: /Admin/Portfolio/Criar
        public async Task<IActionResult> Criar()
        {
            var viewModel = new PortfolioEdicaoViewModel();

            await PreencherDropdownAmbientes(viewModel);
            await PreencherProjetosComFotos(viewModel);

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
                await PreencherProjetosComFotos(viewModel);
                return View(viewModel);
            }

            var dto = new CriarPortfolioApiModelo
            {
                NomeProjeto = viewModel.NomeProjeto,
                Descricao = viewModel.Descricao,
                AmbienteId = viewModel.AmbienteId,
                ProjetoCId = viewModel.ProjetoCId,
                ProjetoCFotosIds = viewModel.ProjetoCFotosIds
            };

            var resposta = await _api.PostAsync<PortfolioApiModelo, CriarPortfolioApiModelo>(
                "api/Portfolio",
                dto);

            if (!resposta.Sucesso)
            {
                AdicionarErrosDaApi(resposta.Erros, resposta.Mensagem);
                await PreencherDropdownAmbientes(viewModel);
                await PreencherProjetosComFotos(viewModel);
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
                AmbienteId = dados.AmbienteId,
                ProjetoCId = dados.ProjetoCId,
                ProjetoCFotosIds = dados.ProjetoCFotosIds.ToList()
            };

            await PreencherDropdownAmbientes(viewModel);
            await PreencherProjetosComFotos(viewModel);
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
                await PreencherProjetosComFotos(viewModel);
                return View(viewModel);
            }

            var dto = new AtualizarPortfolioApiModelo
            {
                Id = id,
                NomeProjeto = viewModel.NomeProjeto,
                Descricao = viewModel.Descricao,
                AmbienteId = viewModel.AmbienteId,
                ProjetoCId = viewModel.ProjetoCId,
                ProjetoCFotosIds = viewModel.ProjetoCFotosIds
            };

            var resposta = await _api.PutAsync<PortfolioApiModelo, AtualizarPortfolioApiModelo>(
                $"api/Portfolio/{id}/atualizar",
                dto);

            if (!resposta.Sucesso)
            {
                AdicionarErrosDaApi(resposta.Erros, resposta.Mensagem);
                await PreencherDropdownAmbientes(viewModel);
                await PreencherProjetosComFotos(viewModel);
                return View(viewModel);
            }

            TempData["MensagemSucesso"] = "Projeto atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Portfolio/Desativar/5
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
            var resposta = await _api.PutAsync<object, object>(
                $"api/Portfolio/{id}/reativar",
                new { });

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Projeto reativado." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Portfolio/ExcluirPermanente/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirPermanente(int id)
        {
            var resposta = await _api.DeleteAsync<object>(
                $"api/Portfolio/{id}/permanente");

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso
                    ? "Projeto excluído permanentemente."
                    : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        private async Task PreencherDropdownAmbientes(PortfolioEdicaoViewModel viewModel)
        {
            var resposta = await _api.GetAsync<List<AmbienteApiModelo>>("api/Ambiente");

            if (resposta.Sucesso && resposta.Dados != null)
            {
                viewModel.AmbientesDisponiveis = resposta.Dados
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Nome,
                        Selected = a.Id == viewModel.AmbienteId
                    })
                    .ToList();
            }
        }

        private async Task PreencherProjetosComFotos(
            PortfolioEdicaoViewModel viewModel)
        {
            var resposta = await _api.GetAsync<List<ProjetoCApiModelo>>(
                "api/ProjetoC");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                return;
            }

            viewModel.ProjetosDisponiveis = resposta.Dados
                .Where(p => p.FotosDetalhadas.Any())
                .OrderBy(p => p.Nome, StringComparer.OrdinalIgnoreCase)
                .Select(p => new ProjetoCPortfolioViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Fotos = p.FotosDetalhadas
                        .OrderBy(f => f.Ordem)
                        .Select(f => new ProjetoCFotoPortfolioViewModel
                        {
                            Id = f.Id,
                            Foto = f.Foto,
                            Ordem = f.Ordem
                        })
                        .ToList()
                })
                .ToList();
        }

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

        // -> corresponde a PortfolioDto na API
        public class PortfolioApiModelo
        {
            public int Id { get; set; }
            public string NomeProjeto { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public int AmbienteId { get; set; }
            public string AmbienteNome { get; set; } = string.Empty;
            public int? ProjetoCId { get; set; }
            public string ProjetoCNome { get; set; } = string.Empty;
            public List<int> ProjetoCFotosIds { get; set; } = new();
            public List<PortfolioFotoApiModelo> FotosSelecionadas { get; set; } = new();
            public bool Ativo { get; set; }
            public DateTime DataCadastro { get; set; }
            public DateTime? DataAtualizacao { get; set; }
            public DateTime? DataExclusao { get; set; }
        }

        public class PortfolioFotoApiModelo
        {
            public int ProjetoCFotoId { get; set; }
            public string Foto { get; set; } = string.Empty;
            public int Ordem { get; set; }
        }

        public class CriarPortfolioApiModelo
        {
            public string NomeProjeto { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public int AmbienteId { get; set; }
            public int? ProjetoCId { get; set; }
            public List<int> ProjetoCFotosIds { get; set; } = new();
        }

        public class AtualizarPortfolioApiModelo : CriarPortfolioApiModelo
        {
            public int Id { get; set; }
        }

        public class AmbienteApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
        }

        public class ProjetoCApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string? UsuarioId { get; set; }
            public string? UsuarioNome { get; set; }
            public List<ProjetoCFotoApiModelo> FotosDetalhadas { get; set; } = new();
        }

        public class ProjetoCFotoApiModelo
        {
            public int Id { get; set; }
            public string Foto { get; set; } = string.Empty;
            public int Ordem { get; set; }
        }
    }
}
