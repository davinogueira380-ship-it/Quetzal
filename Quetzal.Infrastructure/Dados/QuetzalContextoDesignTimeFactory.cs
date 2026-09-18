using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Quetzal.Infrastructure.Dados
{
    public class QuetzalContextoDesignTimeFactory : IDesignTimeDbContextFactory<QuetzalContexto>
    {
        public QuetzalContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuetzalContexto>();

            // Use a mesma connection string do appsettings da API ou variável de ambiente usada em produção/dev
            // Exemplo (substitua pela sua connection string ou carregue de variável)
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=QuetzalDB;Trusted_Connection=True;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);

            return new QuetzalContexto(optionsBuilder.Options);
        }
    }
}