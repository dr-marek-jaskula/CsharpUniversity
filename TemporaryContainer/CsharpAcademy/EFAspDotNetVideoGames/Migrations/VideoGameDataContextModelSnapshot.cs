﻿// <auto-generated />
using System;
using EFAspDotNetVideoGames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFAspDotNetVideoGames.Migrations
{
    [DbContext(typeof(VideoGameDataContext))]
    partial class VideoGameDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("EFAspDotNetVideoGames.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("GenreID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("PersonalRating")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GenreID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("EFAspDotNetVideoGames.GameGenre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("ID");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("EFAspDotNetVideoGames.Game", b =>
                {
                    b.HasOne("EFAspDotNetVideoGames.GameGenre", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreID");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("EFAspDotNetVideoGames.GameGenre", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
