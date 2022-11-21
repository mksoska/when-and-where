using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WhenAndWhere.DAL;

public class WhenAndWhereContextFactory : IDesignTimeDbContextFactory<WhenAndWhereDBContext>
{
    public WhenAndWhereDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WhenAndWhereDBContext>();
        optionsBuilder.UseSqlite("Data Source=WhenAndWhere.sqlite;Cache=Shared")
            .UseLazyLoadingProxies();

        return new WhenAndWhereDBContext(optionsBuilder.Options);
    }
}