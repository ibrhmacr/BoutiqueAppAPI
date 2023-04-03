using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;

namespace Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BoutiqueAppAPIDbContext>
{
    public BoutiqueAppAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<BoutiqueAppAPIDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(
            Configuration.ConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
    //Terminalden migration komutunu verebilmek icin bu calismanin yapilmasi gerekiyor
    //Package manager Konsol uzerinden gerek kalmiyor
}