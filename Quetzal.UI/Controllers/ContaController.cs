using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quetzal.UI.Servicos;
using Quetzal.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quetzal.UI.Controllers
{
    public class ContaController : Controller
    {
        private readonly ApiCliente _apiCliente;

        public ContaController(ApiCliente apiCliente)
        {
            _apiCliente = apiCliente;
        }

        // GET: /Conta/Login
        [HttpGet]
        public IActionResult Login(string? retornoUrl = null)
        {
            // Se já está logado, não faz sentido mostrar a tela de login de novo
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new LoginViewModel { RetornoUrl = retornoUrl };
            return View(viewModel);
        }

        // POST: /Conta/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var loginDto = new LoginRequisicao
            {
                Email = viewModel.Email,
                Senha = viewModel.Senha
            };

            var resposta = await _apiCliente.PostAsync<LoginResposta, LoginRequisicao>("api/auth/login", loginDto);

            // Falha de credenciais ou erro de comunicação com a API.
            // Nunca detalhar se foi "email não existe" ou "senha errada" —
            // isso ajuda um atacante a descobrir quais e-mails estão cadastrados.
            if (!resposta.Sucesso || resposta.Dados == null)
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View(viewModel);
            }

            await AutenticarUsuarioAsync(resposta.Dados, viewModel.LembrarMe);

            // Só redireciona para RetornoUrl se for uma
            // URL local do próprio site.
            if (!string.IsNullOrEmpty(viewModel.RetornoUrl) && Url.IsLocalUrl(viewModel.RetornoUrl))
            {
                return Redirect(viewModel.RetornoUrl);
            }

            // Redireciona por perfil: Admin cai direto no Dashboard.
           
            if (resposta.Dados.Perfis.Contains("Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: /Conta/Registro
        [HttpGet]
        public IActionResult Registro()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new RegistroViewModel());
        }

        // POST: /Conta/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

           
            var registrarDto = new RegistrarRequisicao
            {
                NomeCompleto = viewModel.NomeCompleto,
                Email = viewModel.Email,
                Telefone = viewModel.Telefone,
                Senha = viewModel.Senha,
                ConfirmarSenha = viewModel.ConfirmarSenha
            };

            var resposta = await _apiCliente.PostAsync<UsuarioResposta, RegistrarRequisicao>("api/auth/registrar", registrarDto);

            if (!resposta.Sucesso)
            {
                // Erros de validação vindos da API (ex: "e-mail já cadastrado")
                if (resposta.Erros != null && resposta.Erros.Length > 0)
                {
                    foreach (var erro in resposta.Erros)
                    {
                        ModelState.AddModelError(string.Empty, erro);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, resposta.Mensagem);
                }

                return View(viewModel);
            }

            // Loga automaticamente após o cadastro, evitando pedir login logo em seguida
            var loginDto = new LoginRequisicao { Email = viewModel.Email, Senha = viewModel.Senha };
            var loginResposta = await _apiCliente.PostAsync<LoginResposta, LoginRequisicao>("api/auth/login", loginDto);

            if (loginResposta.Sucesso && loginResposta.Dados != null)
            {
                await AutenticarUsuarioAsync(loginResposta.Dados, lembrarMe: false);
                return RedirectToAction("Index", "Home");
            }

            // Se o auto-login falhar por algum motivo, manda para a tela de login manualmente
            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso! Faça login para continuar.";
            return RedirectToAction(nameof(Login));
        }

        // POST: /Conta/Sair
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sair()
        {
            // Encerra a sessão MVC (cookie de autenticação/Claims)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Remove o token JWT usado pelo ApiCliente
            Response.Cookies.Delete("quetzal_token");

            return RedirectToAction("Index", "Home");
        }

        // GET: /Conta/AcessoNegado
        [HttpGet]
        public IActionResult AcessoNegado()
        {
            return View();
        }

        // Monta os Claims a partir da resposta de login da API e realiza o
        // SignIn no esquema de Cookie do MVC, além de guardar o JWT puro
        // no cookie que o ApiCliente consome nas próximas requisições.
        private async Task AutenticarUsuarioAsync(LoginResposta dadosLogin, bool lembrarMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dadosLogin.NomeUsuario),
                new Claim(ClaimTypes.Email, dadosLogin.Email)
            };

            // Um Claim de Role por perfil, para [Authorize(Roles = "Admin")] funcionar
            foreach (var perfil in dadosLogin.Perfis)
            {
                claims.Add(new Claim(ClaimTypes.Role, perfil));
            }

            var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identidade);

            var propriedades = new AuthenticationProperties
            {
                IsPersistent = lembrarMe,
                ExpiresUtc = dadosLogin.Expiracao
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, propriedades);

            // Cookie separado, HttpOnly, só com o JWT cru — é o que o ApiCliente lê
            Response.Cookies.Append("quetzal_token", dadosLogin.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = dadosLogin.Expiracao
            });
        }

        // ── Modelos auxiliares para mapear a comunicação com a API ──
        
        // -> corresponde a LoginDto na API
        public class LoginRequisicao
        {
            public string Email { get; set; } = string.Empty;
            public string Senha { get; set; } = string.Empty;
        }

        // -> corresponde a LoginRespostaDto na API
        public class LoginResposta
        {
            public string Token { get; set; } = string.Empty;
            public DateTime Expiracao { get; set; }
            public string NomeUsuario { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public List<string> Perfis { get; set; } = new();
        }

        // -> corresponde a RegistrarUserDto na API
       
        public class RegistrarRequisicao
        {
            public string NomeCompleto { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Telefone { get; set; } = string.Empty;
            public string Senha { get; set; } = string.Empty;
            public string ConfirmarSenha { get; set; } = string.Empty;
        }

        // -> corresponde a UsuarioDto na API (resposta do endpoint de registro)
        public class UsuarioResposta
        {
            public string Id { get; set; } = string.Empty;
            public string NomeCompleto { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Telefone { get; set; } = string.Empty;
            public bool Ativo { get; set; }
            public DateTime DataCadastro { get; set; }
            public List<string> Perfis { get; set; } = new();
        }
    }
}