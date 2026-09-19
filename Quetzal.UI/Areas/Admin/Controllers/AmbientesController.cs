//Chama API usando HttpClient (server-to-server) e passa o resultado como JSON inline para a View.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;



namespace Quetzal.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operador")]
    public class AmbientesController : Controller
    {
        private readonly ApiCliente _api;
        public AmbientesController(ApiCliente api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var resposta = await _api.GetAsync<IEnumerable<AmbienteViewModel>>("/api/Ambientes/todos");
            return View(resposta.Dados ?? new List<AmbienteViewModel>());
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View(new AmbienteEdicaoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AmbienteEdicaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dto = new
            {
                model.Nome,
                model.Descricao
            };
            var resposta = await _api.PostAsync<AmbienteViewModel, object>("api/Ambientes", dto);

            if (resposta.Sucesso)
            {
                TempData["Sucesso"] = "Ambiente criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", resposta.Mensagem ?? "Erro desconhecido.");
            return View(model);
        }


        //Só admin e operador faz mudanças

        //[Area("Admin")]
        //[Authorize(Roles = "Admin,Operador")]
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var resposta = await _api.GetAsync<AmbienteViewModel>($"/api/Ambientes/{id}");
            if (!resposta.Sucesso || resposta.Dados == null)
            {
                TempData["Erro"] = "Ambiente não encontrado.";
                return RedirectToAction(nameof(Index));
            }
            var a = resposta.Dados;
            var model = new AmbienteEdicaoViewModel
            {
                Id = a.Id,
                Nome = a.Nome,
                Descricao = a.Descricao,
                ImagemAtualUrl = a.ImagemAtualUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(AmbienteEdicaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dto = new
            {
                model.Nome,
                model.Descricao,
                model.ImagemAtualUrl
            };

            var resposta = await _api.PutAsync<AmbienteViewModel, object>($"/api/Ambientes/{model.Id}", dto);

            if (resposta.Sucesso)
            {
                TempData["Sucesso"] = "Ambiente atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", resposta.Mensagem ?? "Erro desconhecido.");
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desativar(AmbienteEdicaoViewModel model)
        {
            var resposta = await _api.DeleteAsync<object>($"/api/Ambientes/{model.Id}/desativar");
            if (resposta.Sucesso)
            {
                TempData["Sucesso"] = "Ambiente inativado com sucesso.";
            }
            else
            {
                TempData["Erro"] = resposta.Mensagem ?? "Erro desconhecido.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reativar(int id)
        {
            var resposta = await _api.PutAsync<object, object>($"/api/Ambientes/{id}/reativar", new { });
            if (resposta.Sucesso)
            {
                TempData["Sucesso"] = "Ambiente reativado com sucesso.";
            }
            else
            {
                TempData["Erro"] = resposta.Mensagem ?? "Erro desconhecido.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExcluirPermanente(int id)
        {
            var resposta = await _api.DeleteAsync<object>($"/api/Ambientes/{id}/permanente");
            if (resposta.Sucesso)
            {
                TempData["Sucesso"] = "Ambiente excluído permanentemente.";
            }
            else
            {
                TempData["Erro"] = resposta.Mensagem ?? "Erro desconhecido";
            }
            return RedirectToAction(nameof(Index));


        }
    }
}
