using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;
using System.Threading.Tasks;

namespace Quetzal.UI.Areas.Admin.Controllers
{
    // Chama a API internamente usando HttpClient (server-to-server) e passa o resultado como modelo
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operador")]
    public class DashboardController : Controller
    {
        private readonly ApiCliente _api;

        public DashboardController(ApiCliente api)
        {
            _api = api;
        }

        // Index: busca estatísticas no servidor e passa para a View
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resposta = await _api.GetAsync<DashboardEstatisticasViewModel>("/api/Estatisticas/dashboard");

            if (!resposta.Sucesso || resposta.Dados == null)
            {
                // Passa uma mensagem de erro para a View exibir
                ViewBag.ErroDashboard = resposta.Mensagem ?? "Não foi possível carregar as estatísticas.";
                return View(new DashboardEstatisticasViewModel());
            }

            // Passa o modelo preenchido para a View
            return View(resposta.Dados);
        }
    }
}