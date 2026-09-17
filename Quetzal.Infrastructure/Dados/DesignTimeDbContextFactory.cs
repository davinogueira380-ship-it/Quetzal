using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Quetzal.Infrastructure.Dados
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<QuetzalContexto>
    {
        public QuetzalContexto CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Ajustado para usar o nome presente em Quetzal.API\appsettings.json
            var connectionString = config.GetConnectionString("QuetzalDB")
                                   ?? Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");

            var optionsBuilder = new DbContextOptionsBuilder<QuetzalContexto>();
            // Altere para o provedor que você usa (UseSqlServer, UseNpgsql, etc.)
            optionsBuilder.UseSqlServer(connectionString);

            return new QuetzalContexto(optionsBuilder.Options);
        }
    }
}