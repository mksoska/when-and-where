using Microsoft.EntityFrameworkCore;
using WhenAndWhereDAL.Models;

namespace WhenAndWhereDAL.Data;

public class WhenAndWhereDBContext : DbContext
{
    private const string DatabaseName = "WhenAndWhereDB";
    private const string ConnectionString = "";

    public DbSet<Address> Address { get; set; }
    public DbSet<Meetup> Meetup { get; set; }
    public DbSet<Option> Option { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<UserMeetup> UserMeetup { get; set; }
    public DbSet<UserOption> UserOption { get; set; }
    public DbSet<UserRole> UserRole { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(ConnectionString)
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMeetup>()
            .HasKey(userMeetup => new {userMeetup.MeetupId, userMeetup.UserId});

        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.User)
            .WithMany(user => user.JoinedMeetups)
            .HasForeignKey(um => um.UserId);
        
        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.Meetup)
            .WithMany(meetup => meetup.JoinedUsers)
            .HasForeignKey(um => um.MeetupId);

        modelBuilder.Entity<Meetup>()
            .HasOne(meetup => meetup.Owner)
            .WithMany(user => user.OwnedMeetups)
            .HasForeignKey(meetup => meetup.OwnerId);

        modelBuilder.Entity<UserOption>()
            .HasKey(userOption => new { userOption.OptionId, userOption.UserId });

        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.User)
            .WithMany(user => user.VotedOptions)
            .HasForeignKey(userOption => userOption.UserId);


        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.Option)
            .WithMany(option => option.UserOptions)
            .HasForeignKey(userOption => userOption.OptionId);

        modelBuilder.Entity<Option>()
            .HasOne(option => option.User)
            .WithMany(user => user.CreatedOptions)
            .HasForeignKey(option => option.UserId);
        
        modelBuilder.Entity<Option>()
            .HasOne(option => option.Meetup)
            .WithMany(meetup => meetup.Options)
            .HasForeignKey(option => option.MeetupId);

        modelBuilder.Entity<Address>()
            .HasOne(address => address.Option);

        modelBuilder.Entity<UserRole>()
            .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(um => um.User)
            .WithMany(user => user.AssignedRoles)
            .HasForeignKey(um => um.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(um => um.Role)
            .WithMany(meetup => meetup.AssignedUsers)
            .HasForeignKey(um => um.RoleId);

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}
