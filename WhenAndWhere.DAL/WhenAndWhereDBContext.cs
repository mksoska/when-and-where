using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DAL.Data;
using WhenAndWhere.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WhenAndWhere.DAL;

public class WhenAndWhereDBContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
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
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserMeetup>()
            .HasKey(userMeetup => new {userMeetup.UserId, userMeetup.MeetupId});

        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.User)
            .WithMany(user => user.InvitedMeetups)
            .HasForeignKey(um => um.UserId);
        
        modelBuilder.Entity<UserMeetup>()
            .HasOne(um => um.Meetup)
            .WithMany(meetup => meetup.InvitedUsers)
            .HasForeignKey(um => um.MeetupId);

        modelBuilder.Entity<Meetup>()
            .HasOne(meetup => meetup.Owner)
            .WithMany(user => user.OwnedMeetups)
            .HasForeignKey(meetup => meetup.OwnerId);

        modelBuilder.Entity<UserOption>()
            .HasKey(userOption => new { userOption.UserId, userOption.OptionId });

        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.User)
            .WithMany(user => user.VotedOptions)
            .HasForeignKey(userOption => userOption.UserId);


        modelBuilder.Entity<UserOption>()
            .HasOne(userOption => userOption.Option)
            .WithMany(option => option.Voters)
            .HasForeignKey(userOption => userOption.OptionId);

        modelBuilder.Entity<Option>()
            .HasOne(option => option.Owner)
            .WithMany(user => user.CreatedOptions)
            .HasForeignKey(option => option.OwnerId);
        
        modelBuilder.Entity<Option>()
            .HasOne(option => option.Meetup)
            .WithMany(meetup => meetup.Options)
            .HasForeignKey(option => option.MeetupId);
        
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

        modelBuilder.Entity<Role>()
            .HasOne(role => role.Meetup)
            .WithMany(meetup => meetup.Roles)
            .HasForeignKey(meetup => meetup.MeetupId);

        //modelBuilder.Seed();
    }
}
