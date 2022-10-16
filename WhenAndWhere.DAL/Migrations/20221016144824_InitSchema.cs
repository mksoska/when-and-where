using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhenAndWhere.DAL.Migrations
{
    public partial class InitSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Avatar = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OptionsFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OptionsTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Logo = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetup_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<int>(type: "INTEGER", nullable: false),
                    MeetupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Meetup_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserMeetup",
                columns: table => new
                {
                    FirstId = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondId = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    DateInvited = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMeetup", x => new { x.FirstId, x.SecondId });
                    table.ForeignKey(
                        name: "FK_UserMeetup_Meetup_SecondId",
                        column: x => x.SecondId,
                        principalTable: "Meetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMeetup_User_FirstId",
                        column: x => x.FirstId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    FirstId = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.FirstId, x.SecondId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_SecondId",
                        column: x => x.SecondId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_FirstId",
                        column: x => x.FirstId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionId = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeetupId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Option_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Option_Meetup_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Option_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOption",
                columns: table => new
                {
                    FirstId = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeVoted = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOption", x => new { x.FirstId, x.SecondId });
                    table.ForeignKey(
                        name: "FK_UserOption_Option_SecondId",
                        column: x => x.SecondId,
                        principalTable: "Option",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOption_User_FirstId",
                        column: x => x.FirstId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "FirstId", "SecondId", "DateInvited", "State" },
                values: new object[] { 1, 2, new DateTime(2022, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "UserMeetup",
                columns: new[] { "FirstId", "SecondId", "DateInvited", "State" },
                values: new object[] { 150, 2, new DateTime(2022, 11, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Address_OptionId",
                table: "Address",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetup_OwnerId",
                table: "Meetup",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_AddressId",
                table: "Option",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_MeetupId",
                table: "Option",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_UserId",
                table: "Option",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_MeetupId",
                table: "Role",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMeetup_SecondId",
                table: "UserMeetup",
                column: "SecondId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOption_SecondId",
                table: "UserOption",
                column: "SecondId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_SecondId",
                table: "UserRole",
                column: "SecondId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Option_OptionId",
                table: "Address",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Option_OptionId",
                table: "Address");

            migrationBuilder.DropTable(
                name: "UserMeetup");

            migrationBuilder.DropTable(
                name: "UserOption");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Meetup");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
