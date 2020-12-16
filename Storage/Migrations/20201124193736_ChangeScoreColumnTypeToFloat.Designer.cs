﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Storage;

namespace Storage.Migrations
{
    [DbContext(typeof(JudgeContext))]
    [Migration("20201124193736_ChangeScoreColumnTypeToFloat")]
    partial class ChangeScoreColumnTypeToFloat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("Storage.Tables.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Storage.Tables.CategoryRating", b =>
                {
                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("RatingId")
                        .HasColumnType("int");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.HasKey("CategoryId", "RatingId");

                    b.HasIndex("RatingId");

                    b.ToTable("CategoryRatings");
                });

            modelBuilder.Entity("Storage.Tables.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MasterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MasterId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Storage.Tables.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Storage.Tables.PlayerProfile", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("PlayerProfiles");
                });

            modelBuilder.Entity("Storage.Tables.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("PlayerProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("TotalScore")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PlayerProfileId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Storage.Tables.Summary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("Summaries");
                });

            modelBuilder.Entity("Storage.Tables.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Storage.Tables.Category", b =>
                {
                    b.HasOne("Storage.Tables.Game", "Game")
                        .WithMany("Categories")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Storage.Tables.CategoryRating", b =>
                {
                    b.HasOne("Storage.Tables.Category", "Category")
                        .WithMany("CategoryRatings")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Storage.Tables.Rating", "Rating")
                        .WithMany("CategoryRatings")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("Storage.Tables.Game", b =>
                {
                    b.HasOne("Storage.Tables.User", "Master")
                        .WithMany("OwnedGames")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Master");
                });

            modelBuilder.Entity("Storage.Tables.Item", b =>
                {
                    b.HasOne("Storage.Tables.Game", null)
                        .WithMany("Items")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Storage.Tables.PlayerProfile", b =>
                {
                    b.HasOne("Storage.Tables.Game", "Game")
                        .WithMany("PlayerProfiles")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Tables.User", "User")
                        .WithMany("PlayerProfiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Storage.Tables.Rating", b =>
                {
                    b.HasOne("Storage.Tables.Item", "Item")
                        .WithMany("Ratings")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Storage.Tables.PlayerProfile", "PlayerProfile")
                        .WithMany("Ratings")
                        .HasForeignKey("PlayerProfileId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Item");

                    b.Navigation("PlayerProfile");
                });

            modelBuilder.Entity("Storage.Tables.Summary", b =>
                {
                    b.HasOne("Storage.Tables.Game", "Game")
                        .WithOne("Summary")
                        .HasForeignKey("Storage.Tables.Summary", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Storage.Tables.Category", b =>
                {
                    b.Navigation("CategoryRatings");
                });

            modelBuilder.Entity("Storage.Tables.Game", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Items");

                    b.Navigation("PlayerProfiles");

                    b.Navigation("Summary");
                });

            modelBuilder.Entity("Storage.Tables.Item", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Storage.Tables.PlayerProfile", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Storage.Tables.Rating", b =>
                {
                    b.Navigation("CategoryRatings");
                });

            modelBuilder.Entity("Storage.Tables.User", b =>
                {
                    b.Navigation("OwnedGames");

                    b.Navigation("PlayerProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
