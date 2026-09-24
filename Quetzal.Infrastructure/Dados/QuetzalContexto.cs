using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;

namespace Quetzal.Infrastructure.Dados
{
    public class QuetzalContexto : IdentityDbContext<ApplicationUser>
    {
        public QuetzalContexto(DbContextOptions<QuetzalContexto> options)
            : base(options)
        {
        }

        // =========================
        // TABELAS DO SISTEMA
        // =========================

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Ambiente> Ambientes { get; set; }
        public DbSet<ProjetoC> ProjetoC { get; set; }

        // Fotos pertencentes aos projetos
        public DbSet<ProjetoCFoto> ProjetoCFotos { get; set; }

        public DbSet<ApplicationUser> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // =====================================================
            // PORTFOLIO
            // =====================================================

            builder.Entity<Portfolio>(entidade =>
            {
                entidade.ToTable("Portfolio");

                entidade.Property(p => p.NomeProjeto)
                    .IsRequired()
                    .HasMaxLength(200);

                entidade.Property(p => p.Descricao)
                    .IsRequired();

                entidade.Property(p => p.ImagemUpload)
                    .HasMaxLength(500);

                // Relacionamento 1:N: Portfolio -> Ambiente
                entidade.HasOne(p => p.Ambiente)
                    .WithMany(a => a.Portfolios)
                    .HasForeignKey(p => p.AmbienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // =====================================================
            // AMBIENTE
            // =====================================================

            builder.Entity<Ambiente>(entidade =>
            {
                entidade.ToTable("Ambientes");

                entidade.Property(a => a.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                // Não mapear PortfolioId aqui.
                // Atualmente Ambiente não possui essa FK.
            });


            // =====================================================
            // PROJETO DO CLIENTE
            // =====================================================

            builder.Entity<ProjetoC>(entidade =>
            {
                entidade.ToTable("ProjetosC");

                entidade.Property(p => p.NomeProjeto)
                    .IsRequired()
                    .HasMaxLength(200);

                entidade.Property(p => p.Descricao)
                    .IsRequired();

                // Relacionamento:
                //
                // ProjetoC 1 -------- N ProjetoCFoto
                //
                // Um projeto pode possuir várias fotos.
                entidade.HasMany(p => p.Fotos)
                    .WithOne(f => f.ProjetoC)
                    .HasForeignKey(f => f.ProjetoCId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relacionamento do projeto com o cliente/usuário
                entidade.HasOne(p => p.Usuario)
                    .WithMany(u => u.ProjetosC)
                    .HasForeignKey(p => p.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // =====================================================
            // FOTOS DO PROJETO
            // =====================================================

            builder.Entity<ProjetoCFoto>(entidade =>
            {
                entidade.ToTable("ProjetoCFotos");

                entidade.HasKey(f => f.Id);

                // Conteúdo/informação da foto
                entidade.Property(f => f.Foto)
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                // Ordem em que a foto será apresentada
                entidade.Property(f => f.Ordem)
                    .IsRequired();
            });


            // =====================================================
            // RELACIONAMENTO AMBIENTE / PROJETOC
            // =====================================================
            // Mantido comentado porque esse relacionamento
            // também estava comentado no seu código original.

            // builder.Entity<Ambiente>()
            //     .HasMany(a => a.ProjetosC)
            //     .WithMany(p => p.Ambientes)
            //     .UsingEntity(join =>
            //         join.ToTable("AmbienteProjetoC"));


            // =====================================================
            // USUÁRIOS
            // =====================================================

            builder.Entity<ApplicationUser>(entidade =>
            {
                entidade.Property(u => u.NomeCompleto)
                    .IsRequired()
                    .HasMaxLength(200);

                entidade.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entidade.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entidade.Property(u => u.Telefone)
                    .HasMaxLength(11);

                entidade.Property(u => u.SenhaHash)
                    .HasMaxLength(100);
            });


            // =====================================================
            // NOMES DAS TABELAS DO IDENTITY
            // =====================================================

            builder.Entity<ApplicationUser>()
                .ToTable("Identidade_Usuarios");

            builder.Entity<IdentityRole>()
                .ToTable("Identidade_Perfis");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("Identidade_UsuarioPerfis");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("Identidade_UsuarioClaims");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("Identidade_UsuarioLogins");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("Identidade_PerfilClaims");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("Identidade_UsuarioTokens");
        }
    }
}