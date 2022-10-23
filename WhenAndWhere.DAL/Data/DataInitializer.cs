using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.DAL.Data;

public static class DataInitializer
{
    //Specifying IDs is mandatory if seeding db through OnModelCreating method
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new
        {
            Id = 1,
            Name = "Jozef",
            Surname = "Kovalcik",
            Email = "palenka@kde.je",
            PhoneNumber = "+421123456789",
            Avatar = new byte[] {0xAB, 0xCD, 0xEF}
        });

        modelBuilder.Entity<User>().HasData(new
        {
            Id = 150,
            Name = "Matus",
            Surname = "Valkovic",
            Email = "raz@vyrastiem.dufam",
            PhoneNumber = "+421987654321",
            Avatar = new byte[] {0xFE, 0xDC, 0xBA}
        });

        modelBuilder.Entity<Meetup>().HasData(new
        {
            Id = 1,
            Name = "Bowling",
            OptionsFrom = new DateTime(2022, 1, 1),
            OptionsTo = new DateTime(2022, 1, 1),
            Logo = new byte[] {0x00},
            Type = MeetupType.Sport,
            OwnerId = 1
        });

        modelBuilder.Entity<Meetup>().HasData(new
        {
            Id = 2,
            Name = "Snem Tvrdosinskych Alkoholikov",
            OptionsFrom = new DateTime(2022, 11, 11, 00, 00, 00),
            OptionsTo = new DateTime(2022, 11, 11, 23, 59, 59),
            Logo = new byte[] {0x00},
            Type = MeetupType.Drinking,
            OwnerId = 150
        });

        modelBuilder.Entity<UserMeetup>().HasData(new
        {
            FirstId = 1,
            SecondId = 2,
            State = StateEnum.Accepted,
            DateInvited = new DateTime(2022, 11, 11, 12, 00, 00)
        });
            
        modelBuilder.Entity<UserMeetup>().HasData(new
        {
            FirstId = 150,
            SecondId = 2,
            State = StateEnum.Accepted,
            DateInvited = new DateTime(2022, 11, 11, 12, 00, 00)
        });
    }
}