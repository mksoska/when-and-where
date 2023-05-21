﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WhenAndWhere.DAL;

public class WhenAndWhereContextFactory : IDesignTimeDbContextFactory<WhenAndWhereDBContext>
{
    public WhenAndWhereDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WhenAndWhereDBContext>();
        optionsBuilder.UseNpgsql("Server=hw2-postgresql.postgres.database.azure.com;Database=whenandwhere;Port=5432;User Id=postgres;Password=milakunisisnot120%hot;Ssl Mode=Require; Trust Server Certificate=true;")
            .UseLazyLoadingProxies();

        return new WhenAndWhereDBContext(optionsBuilder.Options);
    }
}