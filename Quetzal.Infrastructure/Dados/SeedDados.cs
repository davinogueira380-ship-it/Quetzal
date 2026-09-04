using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quetzal.Domain.Entidades;


namespace Quetzal.Infrastructure.Dados
{
    public static class SeedDados
    {
        public static async Task InicializarAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QuetzalContexto>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.Migrate();

            await SeedIdentityAsync(userManager, roleManager);

            var todosAmbientes = new List<Ambiente>
            {
                new Ambiente { Nome = "Sala" },
                new Ambiente { Nome = "Cozinha" },
                new Ambiente { Nome = "Quarto" }
            };
            var ambientesExistentes = context.Ambientes.Select(a => a.Nome).ToHashSet();
            var novosAmbientes = todosAmbientes.Where(a => !ambientesExistentes.Contains(a.Nome)).ToList();
            if (novosAmbientes.Any())
            {
                context.Ambientes.AddRange(novosAmbientes);
                context.SaveChanges();
            }




            if (!context.Portfolios.Any())
            {
                var proSala = context.Ambientes.First(a => a.Nome == "Sala").Id;
                var proCozinha = context.Ambientes.First(a => a.Nome == "Cozinha").Id;
                var proQuarto = context.Ambientes.First(a => a.Nome == "Quarto").Id;

                var portfolios = new List<Portfolio>
                {
                    new Portfolio
                    {
                        NomeProjeto = "Projeto Sala",
                        AmbienteId = proSala,
                        Descricao = "Projeto de Sala de Estar",
                        ImagemUpload = "https://images.unsplash.com/photo-1616628180680-1e3f5b8c9f1e",
                        Ativo = true
                    },

                    new Portfolio
                    {
                        NomeProjeto = "Projeto Cozinha",
                        AmbienteId = proCozinha,
                        Descricao = "Projeto de Cozinha Moderna",
                        ImagemUpload = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
                        Ativo = true
                    },

                    new Portfolio
                    {
                        NomeProjeto = "Projeto Quarto",
                        AmbienteId = proQuarto,
                        Descricao = "Projeto de Quarto Aconchegante",
                        ImagemUpload = "https://images.unsplash.com/photo-1616628180680-1e3f5b8c9f1e",
                        Ativo = true
                    }
                };

                context.Portfolios.AddRange(portfolios);
                context.SaveChanges();
            }
        }

        // METODO AUXILIAR: Cria Roles e Usuários padrão via Identity
        // Este método e idempotente: não cria duplicatas se já existirem.
        private static async Task SeedIdentityAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Criação de perfis
            var perfis = new[] { "Admin", "Usuario" };
            foreach (var perfil in perfis)
            {
                if (!await roleManager.RoleExistsAsync(perfil))
                    await roleManager.CreateAsync(new IdentityRole(perfil));

            }

            //Admin padrão
            var adminExistente = await userManager.FindByNameAsync("admin@Quetzal.com");
            if (adminExistente == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@Quetzal.com",
                    Email = "admin@Quetzal.com",
                    NomeCompleto = "Administrador Quetzal",
                    EmailConfirmed = true,
                    Ativo = true,
                };
                var resultado = await userManager.CreateAsync(admin, "Quetzal@123");
                if (resultado.Succeeded) 
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
            else if (!adminExistente.Ativo) // Se o admin já existe, mas está inativo, reativa e redefine a senha para um padão temporário
            {
                adminExistente.Ativo = true;
                await userManager.UpdateAsync(adminExistente);
                var token = await userManager.GeneratePasswordResetTokenAsync(adminExistente);
                await userManager.ResetPasswordAsync(adminExistente, token, "Quetzal@123");
            }

            //Operador existente
            var operadorExistente = await userManager.FindByNameAsync("operador@Quetzal.com");
            if (operadorExistente == null)
            {
                var operador = new ApplicationUser
                {
                    UserName = "operador@Quetzal.com",
                    Email = "usuario@Quetzal.com",
                    NomeCompleto = "Usuário Padrão",
                    EmailConfirmed = true,
                    Ativo = true,
                };
                var resultado = await userManager.CreateAsync(operador, "Quetzal@123");
                if (resultado.Succeeded)
                    await userManager.AddToRoleAsync(operador, "Operador");
            }
            else if (!operadorExistente.Ativo) // Se o operador já existe, mas está inativo, reativa e redefine a senha para um padão temporário
            {
                operadorExistente.Ativo = true;
                await userManager.UpdateAsync(operadorExistente);
                var token = await userManager.GeneratePasswordResetTokenAsync(operadorExistente);
                await userManager.ResetPasswordAsync(operadorExistente, token, "Quetzal@123");
            }

            //Usuário existente
            var usuarioExistente = await userManager.FindByNameAsync("usuario@Quetzal.com");
            if (usuarioExistente == null)
            {
                var usuario = new ApplicationUser
                {
                    UserName = "usuario@Quetzal.com",
                    Email = "usuario@Quetzal.com",
                    NomeCompleto = "Usuário Padrão",
                    EmailConfirmed = true,
                    Ativo = true,
                };
                var resultado = await userManager.CreateAsync(usuario, "Quetzal@123");
                if (resultado.Succeeded)
                    await userManager.AddToRoleAsync(usuario, "Usuario");
            }
            else if (!usuarioExistente.Ativo) // Se o usuário já existe, mas está inativo, reativa e redefine a senha para um padão temporário
            {
                usuarioExistente.Ativo = true;
                await userManager.UpdateAsync(usuarioExistente);
                var token = await userManager.GeneratePasswordResetTokenAsync(usuarioExistente);
                await userManager.ResetPasswordAsync(usuarioExistente, token, "Quetzal@123");
            }
        }
    }
}
