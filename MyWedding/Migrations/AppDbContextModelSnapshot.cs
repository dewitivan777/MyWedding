﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWedding;

namespace MyWedding.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("MyWedding.Models.Guest", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.Property<string>("email");

                    b.Property<bool>("isattending");

                    b.Property<string>("mobile");

                    b.Property<string>("name")
                        .IsRequired();

                    b.Property<string>("surname")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("guests");
                });
#pragma warning restore 612, 618
        }
    }
}
