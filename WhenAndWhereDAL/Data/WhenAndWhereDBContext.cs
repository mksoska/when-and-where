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
        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.User)
            .WithMany(user => user.JoinnedMeetups)
            .HasForeignKey(um => um.UserId);
        
        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.Meetup)
            .WithMany(meetup => meetup.JoinnedUsers)
            .HasForeignKey(um => um.MeetupId);

        modelBuilder.Entity<Meetup>()
            .HasOne(meetup => meetup.User)
            .WithMany(user => user.CreatedMeetups)
            .HasForeignKey(meetup => meetup.UserId);        
        
        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.User)
            .WithMany(user => user.UserOptions)
            .HasForeignKey(userOption => userOption.UserId);
        
        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.Option)
            .WithMany(option => option.UserOptions)
            .HasForeignKey(userOption => userOption.OptionId);

        modelBuilder.Entity<Option>()
            .HasOne(option => option.User)
            .WithMany(user => user.Options)
            .HasForeignKey(option => option.UserId);
        
        modelBuilder.Entity<Option>()
            .HasOne(option => option.Meetup)
            .WithMany(meetup => meetup.Options)
            .HasForeignKey(option => option.MeetupId);

        modelBuilder.Entity<Address>()
            .HasOne(address => address.Option);

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}