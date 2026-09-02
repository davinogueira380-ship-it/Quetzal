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

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Ambiente> Ambientes { get; set; }

        // Aqui configuramos relacionamentos, restricoes e comportamentos usando Fluent API
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Projeto>(entidade =>
        //    {
        //        //Define o nome da tabela no banco de dados
        //        entidade.ToTable("Projetos");

        //        // Titulo e obrigatorio e tem limite de 200 caracteres
        //        entidade.Property(p => p.NomeProjeto)
        //            .IsRequired()
        //            .HasMaxLength(200);

        //        // Usuario e obrigatorio e tem limite de 200 caracteres
        //        entidade.Property(p => p.UsuarioId)
        //            .IsRequired()
        //            .HasMaxLength(200);

        //        // Descricao e obrigatoria
        //        entidade.Property(p => p.Descricao)
        //            .IsRequired();

        //        // ImagemUpload e obrigatoria
        //        entidade.Property(p => p.ImagemUpload)
        //            .HasMaxLength(500);

        //        // Configuracao do ambienteId como chave estrangeira para a tabela Ambientes
        //        entidade.HasOne(p => p.Ambiente)
        //            .WithMany(a => a.Projetos)
        //            .HasForeignKey(p => p.AmbienteId)
        //            .OnDelete(DeleteBehavior.Restrict);

        //    });



            //builder.Entity<Ambiente>(entidade =>
            //{
            //    entidade.ToTable("Categorias");

            //    // Nome do ambiente e obrigatorio e tem maximo de 100 caracteres
            //    entidade.Property(a => a.Nome)
            //        .IsRequired()
            //        .HasMaxLength(100);

            //    // Descricao e opcional
            //    entidade.Property(a => a.Descricao)
            //        .HasMaxLength(500);
            //});



            // ================================================================
            // RENOMEAR TABELAS DO IDENTITY PARA MELHOR ORGANIZACAO
            // ================================================================
          

            //builder.Entity<ApplicationUser>().ToTable("Identidade_Usuarios");
            //builder.Entity<IdentityRole>().ToTable("Identidade_Perfis");
            //builder.Entity<IdentityUserRole<string>>().ToTable("Identidade_UsuarioPerfis");
            //builder.Entity<IdentityUserClaim<string>>().ToTable("Identidade_UsuarioClaims");
            //builder.Entity<IdentityUserLogin<string>>().ToTable("Identidade_UsuarioLogins");
            //builder.Entity<IdentityRoleClaim<string>>().ToTable("Identidade_PerfilClaims");
            //builder.Entity<IdentityUserToken<string>>().ToTable("Identidade_UsuarioTokens");

            // ================================================================
            // APPLY SEED DATA
            // ================================================================
            // Os dados iniciais agora sao injetados nativamente via SQL na migration SeedData.



        }
    }
//}
