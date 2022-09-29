using Microsoft.EntityFrameworkCore;

namespace PizzaShopDAL.Data;

public class WhenAndWhereDBContext : DbContext
{
    private const string DatabaseName = "When&Where";

    private const string ConnectionString =
        $"Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database={DatabaseName};Trusted_Connection=True;";

    // TODO define DbSets

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(ConnectionString)
            .UseLazyLoadingProxies();
    }

    // https://docs.microsoft.com/en-us/ef/core/modeling/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO setup constraints

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}