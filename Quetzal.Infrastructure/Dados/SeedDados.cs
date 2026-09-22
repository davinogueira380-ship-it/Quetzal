
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
            // Recupera os serviços através do ServiceProvider recebido
            var context = serviceProvider.GetRequiredService<QuetzalContexto>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Garante que as migrations existentes sejam aplicadas
            await context.Database.MigrateAsync();

            // ================================================================
            // SEED DE IDENTITY
            // ================================================================

            await SeedIdentityAsync(userManager, roleManager);

            // ================================================================
            // SEED DE AMBIENTES
            // ================================================================

            var todosAmbientes = new List<Ambiente>
            {
                new Ambiente
                {
                    Nome = "Sala",
                    Descricao = "Ambiente destinado à sala de estar.",
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow
                },

                new Ambiente
                {
                    Nome = "Cozinha",
                    Descricao = "Ambiente destinado à preparação e refeições.",
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow
                },

                new Ambiente
                {
                    Nome = "Quarto",
                    Descricao = "Ambiente destinado ao descanso.",
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow
                },

                new Ambiente
                {
                    Nome = "Banheiro",
                    Descricao = "Ambiente destinado à higiene pessoal.",
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow
                },

                new Ambiente
                {
                    Nome = "Escritorio",
                    Descricao = "Ambiente destinado ao trabalho e estudos.",
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow
                },

                new Ambiente
                {
                    Nome = "Lavanderia",
                    Descricao = "Ambiente destinado aos serviços de lavanderia.",
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow
                }
            };

            var ambientesExistentes = context.Ambientes
                .Select(a => a.Nome)
                .ToHashSet();

            var novosAmbientes = todosAmbientes
                .Where(a => !ambientesExistentes.Contains(a.Nome))
                .ToList();

            if (novosAmbientes.Any())
            {
                context.Ambientes.AddRange(novosAmbientes);
                await context.SaveChangesAsync();
            }

            // ================================================================
            // SEED DE PORTFOLIOS
            // ================================================================

            if (!await context.Portfolios.AnyAsync())
            {
                var proSala = await context.Ambientes
                    .Where(a => a.Nome == "Sala")
                    .Select(a => a.Id)
                    .FirstAsync();

                var proCozinha = await context.Ambientes
                    .Where(a => a.Nome == "Cozinha")
                    .Select(a => a.Id)
                    .FirstAsync();

                var proQuarto = await context.Ambientes
                    .Where(a => a.Nome == "Quarto")
                    .Select(a => a.Id)
                    .FirstAsync();

                var proBanheiro = await context.Ambientes
                    .Where(a => a.Nome == "Banheiro")
                    .Select(a => a.Id)
                    .FirstAsync();

                var proEscritorio = await context.Ambientes
                    .Where(a => a.Nome == "Escritorio")
                    .Select(a => a.Id)
                    .FirstAsync();

                var proLavanderia = await context.Ambientes
                    .Where(a => a.Nome == "Lavanderia")
                    .Select(a => a.Id)
                    .FirstAsync();

                var portfolios = new List<Portfolio>
                {
                    new Portfolio
                    {
                        NomeProjeto = "Projeto Sala",
                        AmbienteId = proSala,
                        Descricao = "Projeto de Sala de Estar",
                        ImagemUpload = "https://images.unsplash.com/photo-1616628180680-1e3f5b8c9f1e",
                        Ativo = true,
                        DataCriacao = DateTime.UtcNow
                    },

                    new Portfolio
                    {
                        NomeProjeto = "Projeto Cozinha",
                        AmbienteId = proCozinha,
                        Descricao = "Projeto de Cozinha Moderna",
                        ImagemUpload = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
                        Ativo = true,
                        DataCriacao = DateTime.UtcNow
                    },

                    new Portfolio
                    {
                        NomeProjeto = "Projeto Quarto",
                        AmbienteId = proQuarto,
                        Descricao = "Projeto de Quarto Aconchegante",
                        ImagemUpload = "https://images.unsplash.com/photo-1616628180680-1e3f5b8c9f1e",
                        Ativo = true,
                        DataCriacao = DateTime.UtcNow
                    },

                    new Portfolio
                    {
                        NomeProjeto = "Projeto Banheiro",
                        AmbienteId = proBanheiro,
                        Descricao = "Projeto de Banheiro Moderno",
                        ImagemUpload = "https://images.unsplash.com/photo-1616628180680-1e3f5b8c9f1e",
                        Ativo = true,
                        DataCriacao = DateTime.UtcNow
                    },

                    new Portfolio
                    {
                        NomeProjeto = "Projeto Escritorio",
                        AmbienteId = proEscritorio,
                        Descricao = "Projeto de Escritório Moderno",
                        ImagemUpload = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
                        Ativo = true,
                        DataCriacao = DateTime.UtcNow
                    },

                    new Portfolio
                    {
                        NomeProjeto = "Projeto Lavanderia",
                        AmbienteId = proLavanderia,
                        Descricao = "Projeto de Lavanderia",
                        ImagemUpload = "https://images.unsplash.com/photo-1616628180680-1e3f5b8c9f1e",
                        Ativo = true,
                        DataCriacao = DateTime.UtcNow
                    }
                };

                context.Portfolios.AddRange(portfolios);
                await context.SaveChangesAsync();
            }
        }

        // ================================================================
        // SEED DE IDENTITY
        // ================================================================

        private static async Task SeedIdentityAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // ============================================================
            // ROLES
            // ============================================================

            var perfis = new[]
            {
                "Admin",
                "Usuario",
                "Cliente",
                "Administrador"
            };

            foreach (var perfil in perfis)
            {
                if (!await roleManager.RoleExistsAsync(perfil))
                {
                    var resultadoRole = await roleManager.CreateAsync(
                        new IdentityRole(perfil));

                    if (!resultadoRole.Succeeded)
                    {
                        var erros = string.Join(
                            ", ",
                            resultadoRole.Errors.Select(e => e.Description));

                        throw new Exception(
                            $"Erro ao criar o perfil '{perfil}': {erros}");
                    }
                }
            }

            // ============================================================
            // ADMIN
            // ============================================================

            var adminExistente = await userManager
                .FindByNameAsync("admin@Quetzal.com");

            if (adminExistente == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@Quetzal.com",
                    Email = "admin@Quetzal.com",
                    NomeCompleto = "Administrador Quetzal",
                    EmailConfirmed = true,
                    Ativo = true
                };

                var resultado = await userManager.CreateAsync(
                    admin,
                    "Quetzal@123");

                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        admin,
                        "Admin");
                }
                else
                {
                    var erros = string.Join(
                        ", ",
                        resultado.Errors.Select(e => e.Description));

                    throw new Exception(
                        $"Erro ao criar usuário administrador: {erros}");
                }
            }
            else if (!adminExistente.Ativo)
            {
                adminExistente.Ativo = true;

                await userManager.UpdateAsync(adminExistente);

                var token = await userManager
                    .GeneratePasswordResetTokenAsync(adminExistente);

                await userManager.ResetPasswordAsync(
                    adminExistente,
                    token,
                    "Quetzal@123");
            }

            // ============================================================
            // OPERADOR
            // ============================================================

            var operadorExistente = await userManager
                .FindByNameAsync("operador@Quetzal.com");

            if (operadorExistente == null)
            {
                var operador = new ApplicationUser
                {
                    UserName = "operador@Quetzal.com",
                    Email = "operador@Quetzal.com",
                    NomeCompleto = "Operador Quetzal",
                    EmailConfirmed = true,
                    Ativo = true
                };

                var resultado = await userManager.CreateAsync(
                    operador,
                    "Quetzal@123");

                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        operador,
                        "Usuario");
                }
                else
                {
                    var erros = string.Join(
                        ", ",
                        resultado.Errors.Select(e => e.Description));

                    throw new Exception(
                        $"Erro ao criar usuário operador: {erros}");
                }
            }
            else if (!operadorExistente.Ativo)
            {
                operadorExistente.Ativo = true;

                await userManager.UpdateAsync(operadorExistente);

                var token = await userManager
                    .GeneratePasswordResetTokenAsync(operadorExistente);

                await userManager.ResetPasswordAsync(
                    operadorExistente,
                    token,
                    "Quetzal@123");
            }

            // ============================================================
            // USUÁRIO
            // ============================================================

            var usuarioExistente = await userManager
                .FindByNameAsync("usuario@Quetzal.com");

            if (usuarioExistente == null)
            {
                var usuario = new ApplicationUser
                {
                    UserName = "usuario@Quetzal.com",
                    Email = "usuario@Quetzal.com",
                    NomeCompleto = "Usuário Padrão",
                    EmailConfirmed = true,
                    Ativo = true
                };

                var resultado = await userManager.CreateAsync(
                    usuario,
                    "Quetzal@123");

                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(
                        usuario,
                        "Usuario");
                }
                else
                {
                    var erros = string.Join(
                        ", ",
                        resultado.Errors.Select(e => e.Description));

                    throw new Exception(
                        $"Erro ao criar usuário padrão: {erros}");
                }
            }
            else if (!usuarioExistente.Ativo)
            {
                usuarioExistente.Ativo = true;

                await userManager.UpdateAsync(usuarioExistente);

                var token = await userManager
                    .GeneratePasswordResetTokenAsync(usuarioExistente);

                await userManager.ResetPasswordAsync(
                    usuarioExistente,
                    token,
                    "Quetzal@123");
            }
        }
    }
}

