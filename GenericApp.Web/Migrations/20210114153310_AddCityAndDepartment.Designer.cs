﻿// <auto-generated />
using System;
using GenericApp.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GenericApp.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210114153310_AddCityAndDepartment")]
    partial class AddCityAndDepartment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GenericApp.Web.Data.Entities.CityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Name", "DepartmentId")
                        .IsUnique()
                        .HasFilter("[DepartmentId] IS NOT NULL");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("GenericApp.Web.Data.Entities.CountryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FlagImageUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("GenericApp.Web.Data.Entities.DepartmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name", "CountryId")
                        .IsUnique()
                        .HasFilter("[CountryId] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("GenericApp.Web.Data.Entities.TeamEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId");

                    b.Property<string>("LogoImageUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name", "CountryId")
                        .IsUnique()
                        .HasFilter("[CountryId] IS NOT NULL");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("GenericApp.Web.Data.Entities.CityEntity", b =>
                {
                    b.HasOne("GenericApp.Web.Data.Entities.DepartmentEntity", "Department")
                        .WithMany("Cities")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GenericApp.Web.Data.Entities.DepartmentEntity", b =>
                {
                    b.HasOne("GenericApp.Web.Data.Entities.CountryEntity", "Country")
                        .WithMany("Departments")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GenericApp.Web.Data.Entities.TeamEntity", b =>
                {
                    b.HasOne("GenericApp.Web.Data.Entities.CountryEntity", "Country")
                        .WithMany("Teams")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
