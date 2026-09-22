using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Quetzal.Infrastructure.Dados
{
    public class QuetzalContextoDesignTimeFactory : IDesignTimeDbContextFactory<QuetzalContexto>
    {
        public QuetzalContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuetzalContexto>();
            var connectionString =
                "Server=(localdb)\\mssqllocaldb;Database=QuetzalDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer
                (connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly("Quetzal.Infrastructure"));

            return new QuetzalContexto(optionsBuilder.Options);
        }
    }
}