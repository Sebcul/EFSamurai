﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EFSamuari.Data;

namespace EFSamuari.Data.Migrations
{
    [DbContext(typeof(SamuraiContext))]
    [Migration("20170508082355_MyFourthMigration")]
    partial class MyFourthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFSamurai.Domain.Samurai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Agility");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Strength");

                    b.HasKey("Id");

                    b.ToTable("Samurais");
                });
        }
    }
}
