using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;

namespace Quetzal.Infrastructure.Dados
{
    public class QuetzalContexto : IdentityDbContext<ApplicationUser>
    {
        public QuetzalContexto(DbContextOptions<QuetzalContexto> options) : base(options)
        {
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Ambiente> Ambientes { get; set; }
        public DbSet<ProjetoC> ProjetoC { get; set; }
        public DbSet<ApplicationUser> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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

            builder.Entity<Ambiente>(entidade =>
            {
                entidade.ToTable("Ambientes");

                entidade.Property(a => a.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                // Não mapear PortfolioId aqui (não existe como FK em Ambiente no seu modelo atual)
            });

            builder.Entity<ProjetoC>(entidade =>
            {
                entidade.ToTable("ProjetosC");

                entidade.Property(p => p.NomeProjeto)
                    .IsRequired()
                    .HasMaxLength(200);

                entidade.Property(p => p.Descricao)
                    .IsRequired();

                entidade.HasOne(p => p.Usuario)
                    .WithMany(u => u.ProjetosC)
                    .HasForeignKey(p => p.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configura many-to-many entre Ambiente e ProjetoC
            //builder.Entity<Ambiente>()
            //    .HasMany(a => a.ProjetosC)
            //    .WithMany(p => p.Ambientes)
            //    .UsingEntity(join => join.ToTable("AmbienteProjetoC"));

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

            // Renomear tabelas do Identity
            builder.Entity<ApplicationUser>().ToTable("Identidade_Usuarios");
            builder.Entity<IdentityRole>().ToTable("Identidade_Perfis");
            builder.Entity<IdentityUserRole<string>>().ToTable("Identidade_UsuarioPerfis");
            builder.Entity<IdentityUserClaim<string>>().ToTable("Identidade_UsuarioClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("Identidade_UsuarioLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("Identidade_PerfilClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("Identidade_UsuarioTokens");
        }
    }
}