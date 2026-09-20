using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;

namespace Quetzal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operador")]
    public class ProjetosCController : Controller
    {
        private readonly ApiCliente _api;
        private readonly IWebHostEnvironment _ambiente;

        public ProjetosCController(ApiCliente api, IWebHostEnvironment ambiente)
        {
            _api = api;
            _ambiente = ambiente;
        }

        // GET: /Admin/ProjetoC
        // Nota: a rota é "todas", diferente do Portfolio que usa "todos"
        public async Task<IActionResult> Index()
        {
            var resposta = await _api.GetAsync<List<ProjetoCApiModelo>>("api/ProjetoC/todas");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                TempData["MensagemErro"] = resposta.Mensagem;
                return View(new List<ProjetoCViewModel>());
            }

            var viewModel = resposta.Dados.Select(p => new ProjetoCViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                ImagemUpload = p.ImagemUpload,
                ClienteNome = string.IsNullOrWhiteSpace(p.UsuarioNome) ? "(sem nome cadastrado)" : p.UsuarioNome,
                AmbientesNomes = p.Ambientes.Select(a => a.Nome).ToList(),
                Ativo = p.Ativo,
                DataCadastro = p.DataCadastro
            }).ToList();

            return View(viewModel);
        }

        // GET: /Admin/ProjetoC/Editar/5
        // Sem action "Criar" de propósito -- o projeto já nasce vinculado
        // ao cliente em outro fluxo (fora do admin)
        public async Task<IActionResult> Editar(int id)
        {
            var resposta = await _api.GetAsync<ProjetoCApiModelo>($"api/ProjetoC/{id}");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                TempData["MensagemErro"] = "Projeto não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            var dados = resposta.Dados;
            var viewModel = new ProjetoCEdicaoViewModel
            {
                Id = dados.Id,
                Nome = dados.Nome,
                Descricao = dados.Descricao,
                ImagemAtualUrl = dados.ImagemUpload,
                ClienteNome = string.IsNullOrWhiteSpace(dados.UsuarioNome) ? "(sem nome cadastrado)" : dados.UsuarioNome,
                AmbientesSelecionadosIds = dados.AmbientesIds
            };

            await PreencherCheckboxesAmbientes(viewModel);
            return View(viewModel);
        }

        // POST: /Admin/ProjetoC/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ProjetoCEdicaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherCheckboxesAmbientes(viewModel);
                return View(viewModel);
            }

            var caminhoImagem = viewModel.ImagemAtualUrl ?? string.Empty;
            if (viewModel.ImagemArquivo != null)
            {
                caminhoImagem = await SalvarImagemAsync(viewModel.ImagemArquivo, "projetos") ?? caminhoImagem;
            }

            var dto = new AtualizarProjetoCApiModelo
            {
                Id = id,
                Nome = viewModel.Nome,
                Descricao = viewModel.Descricao,
                ImagemUpload = caminhoImagem,
                AmbientesIds = viewModel.AmbientesSelecionadosIds
            };

            var resposta = await _api.PutAsync<ProjetoCApiModelo, AtualizarProjetoCApiModelo>(
                $"api/ProjetoC/{id}/atualizar", dto);

            if (!resposta.Sucesso)
            {
                AdicionarErrosDaApi(resposta.Erros, resposta.Mensagem);
                await PreencherCheckboxesAmbientes(viewModel);
                return View(viewModel);
            }

            TempData["MensagemSucesso"] = "Projeto atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/ProjetoC/Desativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desativar(int id)
        {
            var resposta = await _api.DeleteAsync<object>($"api/ProjetoC/{id}/desativar");

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Projeto desativado." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/ProjetoC/Reativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reativar(int id)
        {
            var resposta = await _api.PutAsync<object, object>($"api/ProjetoC/{id}/reativar", new { });

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Projeto reativado." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/ProjetoC/ExcluirPermanente/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirPermanente(int id)
        {
            var resposta = await _api.DeleteAsync<object>($"api/ProjetoC/{id}/permanente");

            TempData[resposta.Sucesso ? "MensagemSucesso" : "MensagemErro"] =
                resposta.Sucesso ? "Projeto excluído permanentemente." : resposta.Mensagem;

            return RedirectToAction(nameof(Index));
        }

        // Busca os ambientes ativos na API e marca quais já pertencem a este projeto
        private async Task PreencherCheckboxesAmbientes(ProjetoCEdicaoViewModel viewModel)
        {
            var resposta = await _api.GetAsync<List<AmbienteApiModelo>>("api/Ambiente");

            if (resposta.Sucesso && resposta.Dados != null)
            {
                viewModel.AmbientesDisponiveis = resposta.Dados
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Nome,
                        Selected = viewModel.AmbientesSelecionadosIds.Contains(a.Id)
                    })
                    .ToList();
            }
        }

        // Salva o arquivo em wwwroot/uploads/projetos/ e devolve o caminho
        // relativo -- a API espera ImagemUpload como string, não multipart
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

            return $"/uploads/{pasta}/{nomeArquivo}";
        }

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

        // ── Modelos auxiliares para mapear a comunicação com a API ──

        // -> corresponde a ProjetoCDto na API
        public class ProjetoCApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public string UsuarioId { get; set; } = string.Empty;
            public string? UsuarioNome { get; set; }
            public List<int> AmbientesIds { get; set; } = new();
            public List<AmbienteApiModelo> Ambientes { get; set; } = new();
            public bool Ativo { get; set; }
            public DateTime DataCadastro { get; set; }
        }

        // -> corresponde a AtualizarProjetoCDto na API
        public class AtualizarProjetoCApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public string ImagemUpload { get; set; } = string.Empty;
            public List<int> AmbientesIds { get; set; } = new();
        }

        // -> corresponde a AmbienteDto na API (usado só para os checkboxes)
        public class AmbienteApiModelo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
        }
    }
}
