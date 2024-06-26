﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mountebank.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mountebank.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240512193235_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Mountebank.Data.Configurations.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("currencies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "RUB",
                            Rate = 1.0
                        },
                        new
                        {
                            Id = 2,
                            Name = "USD",
                            Rate = 73.441900000000004
                        },
                        new
                        {
                            Id = 3,
                            Name = "EUR",
                            Rate = 79.726399999999998
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
