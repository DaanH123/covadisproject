﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using covadis.Api.Context;

#nullable disable

namespace covadis.Api.Migrations
{
    [DbContext(typeof(DbContextCovadis))]
    partial class DbContextCovadisModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("covadis.Api.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("Latitude")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("Longitude")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TripId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("covadis.Api.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("From")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TripId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Until")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.HasIndex("UserId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("covadis.Api.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("covadis.Api.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OdometerEnd")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OdometerStart")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReservationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("covadis.Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("covadis.Api.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("ManufacturedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Odometer")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LicensePlate")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("covadis.Api.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("covadis.Api.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("covadis.Api.Models.Address", b =>
                {
                    b.HasOne("covadis.Api.Models.Trip", null)
                        .WithMany("Addresses")
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("covadis.Api.Models.Reservation", b =>
                {
                    b.HasOne("covadis.Api.Models.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId");

                    b.HasOne("covadis.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("covadis.Api.Models.Vehicle", "Vehicle")
                        .WithMany("Reservations")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trip");

                    b.Navigation("User");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("covadis.Api.Models.Trip", b =>
                {
                    b.HasOne("covadis.Api.Models.Reservation", "Reservation")
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("covadis.Api.Models.Trip", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("covadis.Api.Models.Vehicle", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
