using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EFSamuari.Data;
using EFSamurai.Domain;
namespace EFSamuari.Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    partial class SamuraiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFSamurai.Domain.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BattleLogId");

                    b.Property<bool>("IsBrutal");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("BattleLogId");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("EFSamurai.Domain.BattleEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BattleLogId");

                    b.Property<string>("Description");

                    b.Property<string>("EventSummary");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("BattleLogId");

                    b.ToTable("BattleEvent");
                });

            modelBuilder.Entity("EFSamurai.Domain.BattleLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BattleLog");
                });

            modelBuilder.Entity("EFSamurai.Domain.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsFunny");

                    b.Property<int>("SamuraiId");

                    b.Property<string>("Text");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("EFSamurai.Domain.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Agility");

                    b.Property<int>("Haircut");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Strength");

                    b.HasKey("Id");

                    b.ToTable("Samurais");
                });

            modelBuilder.Entity("EFSamurai.Domain.SamuraiBattles", b =>
                {
                    b.Property<int>("SamuraiId");

                    b.Property<int>("BattleId");

                    b.HasKey("SamuraiId", "BattleId");

                    b.HasIndex("BattleId");

                    b.ToTable("SamuraiBattles");
                });

            modelBuilder.Entity("EFSamurai.Domain.SecretIdentity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RealName");

                    b.Property<int>("SamuraiId");

                    b.HasKey("Id");

                    b.HasIndex("SamuraiId")
                        .IsUnique();

                    b.ToTable("SecretIdentity");
                });

            modelBuilder.Entity("EFSamurai.Domain.Battle", b =>
                {
                    b.HasOne("EFSamurai.Domain.BattleLog", "BattleLog")
                        .WithMany()
                        .HasForeignKey("BattleLogId");
                });

            modelBuilder.Entity("EFSamurai.Domain.BattleEvent", b =>
                {
                    b.HasOne("EFSamurai.Domain.BattleLog")
                        .WithMany("Events")
                        .HasForeignKey("BattleLogId");
                });

            modelBuilder.Entity("EFSamurai.Domain.Quote", b =>
                {
                    b.HasOne("EFSamurai.Domain.Samurai", "Samurai")
                        .WithMany("Quotes")
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFSamurai.Domain.SamuraiBattles", b =>
                {
                    b.HasOne("EFSamurai.Domain.Battle", "Battle")
                        .WithMany("SamuraiBattles")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EFSamurai.Domain.Samurai", "Samurai")
                        .WithMany()
                        .HasForeignKey("SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFSamurai.Domain.SecretIdentity", b =>
                {
                    b.HasOne("EFSamurai.Domain.Samurai", "Samurai")
                        .WithOne("Alias")
                        .HasForeignKey("EFSamurai.Domain.SecretIdentity", "SamuraiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
