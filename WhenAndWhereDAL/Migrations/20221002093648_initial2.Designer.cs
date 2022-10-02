﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaShopDAL.Data;

#nullable disable

namespace PizzaShopDAL.Migrations
{
    [DbContext(typeof(WhenAndWhereDBContext))]
    [Migration("20221002093648_initial2")]
    partial class initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("PizzaShopDAL.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OptionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OptionId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Meetup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Logo")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OptionFrom")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OptionTo")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Meetup");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MeetupId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MeetupId");

                    b.HasIndex("UserId");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Avatar")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.UserMeetup", b =>
                {
                    b.Property<int>("MeetupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateInvited")
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.HasKey("MeetupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMeetup");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.UserOption", b =>
                {
                    b.Property<int>("OptionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeVoted")
                        .HasColumnType("TEXT");

                    b.HasKey("OptionId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOption");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Admin", b =>
                {
                    b.HasBaseType("PizzaShopDAL.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Address", b =>
                {
                    b.HasOne("PizzaShopDAL.Models.Option", "Option")
                        .WithOne("Address")
                        .HasForeignKey("PizzaShopDAL.Models.Address", "OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Option");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Meetup", b =>
                {
                    b.HasOne("PizzaShopDAL.Models.User", "User")
                        .WithMany("CreatedMeetups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Option", b =>
                {
                    b.HasOne("PizzaShopDAL.Models.Meetup", "Meetup")
                        .WithMany("Options")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PizzaShopDAL.Models.User", "User")
                        .WithMany("Options")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meetup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.UserMeetup", b =>
                {
                    b.HasOne("PizzaShopDAL.Models.Meetup", "Meetup")
                        .WithMany("JoinnedUsers")
                        .HasForeignKey("MeetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PizzaShopDAL.Models.User", "User")
                        .WithMany("JoinnedMeetups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meetup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.UserOption", b =>
                {
                    b.HasOne("PizzaShopDAL.Models.Option", "Option")
                        .WithMany("UserOptions")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PizzaShopDAL.Models.User", "User")
                        .WithMany("UserOptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Option");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Meetup", b =>
                {
                    b.Navigation("JoinnedUsers");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.Option", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("UserOptions");
                });

            modelBuilder.Entity("PizzaShopDAL.Models.User", b =>
                {
                    b.Navigation("CreatedMeetups");

                    b.Navigation("JoinnedMeetups");

                    b.Navigation("Options");

                    b.Navigation("UserOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
