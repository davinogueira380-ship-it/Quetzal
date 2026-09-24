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

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Ambiente> Ambientes { get; set; }
        public DbSet<ProjetoC> ProjetoC { get; set; }
        public DbSet<ProjetoCFoto> ProjetoCFotos { get; set; }

        // Relacionamento entre Portfolio e as fotos escolhidas do ProjetoC
        public DbSet<PortfolioFoto> PortfolioFotos { get; set; }

        public DbSet<ApplicationUser> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ============================================================
            // PORTFOLIO
            // ============================================================

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

                entidade.HasOne(p => p.Ambiente)
                    .WithMany(a => a.Portfolios)
                    .HasForeignKey(p => p.AmbienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entidade.HasOne(p => p.ProjetoC)
                    .WithMany()
                    .HasForeignKey(p => p.ProjetoCId)
                    .OnDelete(DeleteBehavior.Restrict);

                entidade.HasMany(p => p.Fotos)
                    .WithOne(f => f.Portfolio)
                    .HasForeignKey(f => f.PortfolioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ============================================================
            // PORTFOLIO FOTO
            // ============================================================

            builder.Entity<PortfolioFoto>(entidade =>
            {
                entidade.ToTable("PortfolioFotos");

                entidade.HasKey(pf => pf.Id);

                entidade.HasOne(pf => pf.Portfolio)
                    .WithMany(p => p.Fotos)
                    .HasForeignKey(pf => pf.PortfolioId)
                    .OnDelete(DeleteBehavior.Cascade);

                entidade.HasOne(pf => pf.ProjetoCFoto)
                    .WithMany()
                    .HasForeignKey(pf => pf.ProjetoCFotoId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Impede que a mesma foto seja adicionada duas vezes
                // ao mesmo Portfolio.
                entidade.HasIndex(pf => new
                {
                    pf.PortfolioId,
                    pf.ProjetoCFotoId
                })
                .IsUnique();
            });

            // ============================================================
            // AMBIENTE
            // ============================================================

            builder.Entity<Ambiente>(entidade =>
            {
                entidade.ToTable("Ambientes");

                entidade.Property(a => a.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // ============================================================
            // PROJETO C
            // ============================================================

            builder.Entity<ProjetoC>(entidade =>
            {
                entidade.ToTable("ProjetosC");

                entidade.Property(p => p.NomeProjeto)
                    .IsRequired()
                    .HasMaxLength(200);

                entidade.Property(p => p.Descricao)
                    .IsRequired();

                entidade.HasMany(p => p.Fotos)
                    .WithOne(f => f.ProjetoC)
                    .HasForeignKey(f => f.ProjetoCId)
                    .OnDelete(DeleteBehavior.Cascade);

                entidade.HasOne(p => p.Usuario)
                    .WithMany(u => u.ProjetosC)
                    .HasForeignKey(p => p.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================================
            // PROJETO C FOTO
            // ============================================================

            builder.Entity<ProjetoCFoto>(entidade =>
            {
                entidade.ToTable("ProjetoCFotos");

                entidade.HasKey(f => f.Id);

                entidade.Property(f => f.Foto)
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                entidade.Property(f => f.Ordem)
                    .IsRequired();
            });

            // ============================================================
            // USUARIO
            // ============================================================

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

            // ============================================================
            // TABELAS DO IDENTITY
            // ============================================================

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