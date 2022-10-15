using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhenAndWhereDAL.Migrations
{
    public partial class BasicDataSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "Email", "Name", "PhoneNumber", "Surname" },
                values: new object[] { 1, new byte[] { 171, 205, 239 }, "palenka@kde.je", "Jozef", "+421123456789", "Kovalcik" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "Email", "Name", "PhoneNumber", "Surname" },
                values: new object[] { 150, new byte[] { 254, 220, 186 }, "raz@vyrastiem.dufam", "Matus", "+421987654321", "Valkovic" });

            migrationBuilder.InsertData(
                table: "Meetup",
                columns: new[] { "Id", "Logo", "Name", "OptionsFrom", "OptionsTo", "OwnerId", "Type" },
                values: new object[] { 1, new byte[] { 0 }, "Bowling", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0 });

            migrationBuilder.InsertData(
                table: "Meetup",
                columns: new[] { "Id", "Logo", "Name", "OptionsFrom", "OptionsTo", "OwnerId", "Type" },
                values: new object[] { 2, new byte[] { 0 }, "Snem Tvrdosinskych Alkoholikov", new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 11, 23, 59, 59, 0, DateTimeKind.Unspecified), 150, 1 });

            migrationBuilder.InsertData(
                table: "UserMeetup",
                columns: new[] { "MeetupId", "UserId", "DateInvited", "State" },
                values: new object[] { 2, 1, new DateTime(2022, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "UserMeetup",
                columns: new[] { "MeetupId", "UserId", "DateInvited", "State" },
                values: new object[] { 2, 150, new DateTime(2022, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Meetup",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserMeetup",
                keyColumns: new[] { "MeetupId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "UserMeetup",
                keyColumns: new[] { "MeetupId", "UserId" },
                keyValues: new object[] { 2, 150 });

            migrationBuilder.DeleteData(
                table: "Meetup",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 150);
        }
    }
}
