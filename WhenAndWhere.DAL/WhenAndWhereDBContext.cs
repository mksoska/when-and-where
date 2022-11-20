using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DAL.Data;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DAL;

public class WhenAndWhereDBContext : DbContext
{
    public DbSet<Address> Address { get; set; }
    public DbSet<Meetup> Meetup { get; set; }
    public DbSet<Option> Option { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<UserMeetup> UserMeetup { get; set; }
    public DbSet<UserOption> UserOption { get; set; }
    public DbSet<UserRole> UserRole { get; set; }

    public WhenAndWhereDBContext(DbContextOptions<WhenAndWhereDBContext> options) : base(options)
    {
    }

    public WhenAndWhereDBContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMeetup>()
            .HasKey(userMeetup => new {userMeetup.FirstId, userMeetup.SecondId});

        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.User)
            .WithMany(user => user.JoinedMeetups)
            .HasForeignKey(um => um.FirstId);
        
        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.Meetup)
            .WithMany(meetup => meetup.JoinedUsers)
            .HasForeignKey(um => um.SecondId);

        modelBuilder.Entity<Meetup>()
            .HasOne(meetup => meetup.Owner)
            .WithMany(user => user.OwnedMeetups)
            .HasForeignKey(meetup => meetup.OwnerId);

        modelBuilder.Entity<UserOption>()
            .HasKey(userOption => new { userOption.FirstId, userOption.SecondId });

        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.User)
            .WithMany(user => user.VotedOptions)
            .HasForeignKey(userOption => userOption.FirstId);


        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.Option)
            .WithMany(option => option.Voters)
            .HasForeignKey(userOption => userOption.SecondId);

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
            .HasKey(userRole => new { userRole.FirstId, userRole.SecondId });

        modelBuilder.Entity<UserRole>()
            .HasOne(um => um.User)
            .WithMany(user => user.AssignedRoles)
            .HasForeignKey(um => um.FirstId);

        modelBuilder.Entity<UserRole>()
            .HasOne(um => um.Role)
            .WithMany(meetup => meetup.AssignedUsers)
            .HasForeignKey(um => um.SecondId);

        modelBuilder.Entity<Role>()
            .HasOne(role => role.Meetup)
            .WithMany(meetup => meetup.Roles)
            .HasForeignKey(meetup => meetup.MeetupId);

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}
