﻿// <auto-generated />
using System;
using CLDVPart1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CLDVPart1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250611200853_AddEventTypeIDToEvent")]
    partial class AddEventTypeIDToEvent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CLDVPart1.Models.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingID"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int>("VenueID")
                        .HasColumnType("int");

                    b.HasKey("BookingID");

                    b.HasIndex("EventID");

                    b.HasIndex("VenueID");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("CLDVPart1.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventTypeID")
                        .HasColumnType("int");

                    b.Property<int?>("VenueID")
                        .HasColumnType("int");

                    b.HasKey("EventID");

                    b.HasIndex("EventTypeID");

                    b.HasIndex("VenueID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("CLDVPart1.Models.EventType", b =>
                {
                    b.Property<int>("EventTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventTypeID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventTypeID");

                    b.ToTable("EventType");
                });

            modelBuilder.Entity("CLDVPart1.Models.Venue", b =>
                {
                    b.Property<int>("VenueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VenueID"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Image_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VenueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VenueID");

                    b.ToTable("Venue");
                });

            modelBuilder.Entity("CLDVPart1.Models.Booking", b =>
                {
                    b.HasOne("CLDVPart1.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CLDVPart1.Models.Venue", "Venue")
                        .WithMany()
                        .HasForeignKey("VenueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("CLDVPart1.Models.Event", b =>
                {
                    b.HasOne("CLDVPart1.Models.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeID");

                    b.HasOne("CLDVPart1.Models.Venue", "Venue")
                        .WithMany()
                        .HasForeignKey("VenueID");

                    b.Navigation("EventType");

                    b.Navigation("Venue");
                });
#pragma warning restore 612, 618
        }
    }
}
