﻿// <auto-generated />
using System;
using AgendaApiLucianoSvaikaukas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgendaApiLucianoSvaikaukas.Migrations
{
    [DbContext(typeof(AgendaContext))]
    partial class AgendaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("AgendaApiLucianoSvaikaukas.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CelularNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("TelephoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CelularNumber = 341457896L,
                            Description = "Plomero",
                            Name = "Jaimito",
                            TelephoneNumber = 66L,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CelularNumber = 34156978L,
                            Description = "Papa",
                            Name = "Pepe",
                            TelephoneNumber = 422568L,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            CelularNumber = 11425789L,
                            Description = "Jefa",
                            Name = "Maria",
                            TelephoneNumber = 7656L,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("AgendaApiLucianoSvaikaukas.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Natacion"
                        });
                });

            modelBuilder.Entity("AgendaApiLucianoSvaikaukas.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "karenbailapiola@gmail.com",
                            LastName = "Lasot",
                            Name = "Karen",
                            Password = "Pa$$w0rd"
                        },
                        new
                        {
                            Id = 2,
                            Email = "elluismidetotoras@gmail.com",
                            LastName = "Gonzales",
                            Name = "Luis Gonzalez",
                            Password = "lamismadesiempre"
                        });
                });

            modelBuilder.Entity("ContactGroup", b =>
                {
                    b.Property<int>("ContactsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GroupsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ContactsId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("ContactGroup", (string)null);

                    b.HasData(
                        new
                        {
                            ContactsId = 1,
                            GroupsId = 1
                        },
                        new
                        {
                            ContactsId = 3,
                            GroupsId = 1
                        });
                });

            modelBuilder.Entity("AgendaApiLucianoSvaikaukas.Entities.Contact", b =>
                {
                    b.HasOne("AgendaApiLucianoSvaikaukas.Entities.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ContactGroup", b =>
                {
                    b.HasOne("AgendaApiLucianoSvaikaukas.Entities.Contact", null)
                        .WithMany()
                        .HasForeignKey("ContactsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgendaApiLucianoSvaikaukas.Entities.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AgendaApiLucianoSvaikaukas.Entities.User", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
