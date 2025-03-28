﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Data;

#nullable disable

namespace ToDoList.Migrations
{
    [DbContext(typeof(ToDoListDBContext))]
    [Migration("20250308213018_TestDataMigration")]
    partial class TestDataMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ToDoList.Models.Taak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Taken");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDateTime = new DateTime(2025, 3, 8, 22, 30, 17, 914, DateTimeKind.Local).AddTicks(3158),
                            Description = "Dit is de beschrijving voor taak 1",
                            Title = "Test Taak1"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDateTime = new DateTime(2025, 3, 8, 22, 30, 17, 917, DateTimeKind.Local).AddTicks(301),
                            Description = "Dit is de beschrijving voor taak 2",
                            Title = "Test Taak2"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDateTime = new DateTime(2025, 3, 8, 22, 30, 17, 917, DateTimeKind.Local).AddTicks(321),
                            Description = "Dit is de beschrijving voor taak 3",
                            Title = "Test Taak3"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDateTime = new DateTime(2025, 3, 8, 22, 30, 17, 917, DateTimeKind.Local).AddTicks(325),
                            Description = "Je kunt het al raden dit is ook een bijscrijving, maar dan voor taak 4 :)",
                            Title = "Test Taak4"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
