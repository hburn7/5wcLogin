﻿// <auto-generated />
using System;
using FiveWCLoginAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FiveWCLoginAPI.Migrations
{
    [DbContext(typeof(FiveWCDbContext))]
    partial class FiveWCDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FiveWCLoginAPI.AuthorizedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AuthorizedUsers");
                });

            modelBuilder.Entity("FiveWCLoginAPI.OsuRegistrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DiscordDisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DiscordID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OsuDisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OsuID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OsuJson")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Registrants");
                });
#pragma warning restore 612, 618
        }
    }
}
