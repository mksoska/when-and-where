using Microsoft.EntityFrameworkCore;
using PizzaShopDAL.Models;

namespace PizzaShopDAL.Data;

public class WhenAndWhereDBContext : DbContext
{
    private const string DatabaseName = "When&Where";

    private const string ConnectionString =  // TODO change string
        $"Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database={DatabaseName};Trusted_Connection=True;";

    public DbSet<Address> Address { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Meetup> Meetup { get; set; }
    public DbSet<Option> Option { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserMeetup> UserMeetup { get; set; }
    public DbSet<UserOption> UserOption { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(ConnectionString)
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