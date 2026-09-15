using Microsoft.AspNetCore.Authentication.Cookies;

namespace Quetzal.UI;


public class Program
{

        public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona suporte a Controllers e Views (padrao MVC)
        builder.Services.AddControllersWithViews();

        // Permite acessar o HttpContext (necessario para pegar o cookie do JWT)
        builder.Services.AddHttpContextAccessor();

        // Configura a autenticacao via Cookie para a aplicacao Web
        // O MVC usa Cookie para manter sessao, o JWT fica salvo dentro do cookie
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Conta/Login";
                options.LogoutPath = "/Conta/Sair";
                options.AccessDeniedPath = "/Conta/AcessoNegado";
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
            });

        builder.Services.AddAuthorization();

        // Configura o HttpClient padrao para apontar para a API
        builder.Services.AddHttpClient("QuetzalAPI", client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["ApiConfiguracoes:UrlBase"] ?? "http://localhost:5277");
        })
            // Ignora validacao de certificado SSL apenas para ambiente de desenvolvimento local
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });

        // Registra os servicos customizados
        builder.Services.AddScoped<ApiCliente>();
        builder.Services.AddScoped<ServicoUpload>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        // Serve arquivos estaticos (css, js, imagens de upload)
        app.UseStaticFiles();

        app.UseRouting();

        // Autenticacao e Autorizacao
        app.UseAuthentication();
        app.UseAuthorization();

        // Mapeamento de rotas com Areas (Admin, Cliente) e padrao
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
