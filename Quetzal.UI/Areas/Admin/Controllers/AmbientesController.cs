//Chama API usando HttpClient (server-to-server) e passa o resultado como JSON inline para a View.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;


// STHEFANNY Aqui ↓



namespace Quetzal.UI.Areas.Admin.Controllers
{
    public class AmbientesController : Controller
    {
        //[Area("Admin")]
        //[Authorize(Roles = "Admin,Operador")]

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
        [Area("Admin")]
        [Authorize(Roles = "Admin,Operador")]
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
                model.ImagemAtualUrl //Davi Alterou de ImagemUrl para ImagemAtualUrl
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

     



    }
}

//Voltar aqui STHEFANNY ↑