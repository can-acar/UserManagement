﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UserManagement.Core.Data;

namespace UserManagement.Core.Migrations;

[DbContext(typeof(UserManagementData))]
partial class UserManagementDataModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "7.0.9")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("UserManagement.API.Models.User", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("(newid())");

            b.Property<DateTime>("CreatedAt")
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("(getdate())");

            b.Property<string>("Email")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<bool>("IsActive")
                .HasColumnType("bit");

            b.Property<bool>("IsDeleted")
                .HasColumnType("bit");

            b.Property<bool>("IsLocked")
                .HasColumnType("bit");

            b.Property<string>("Password")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("datetime2");

            b.Property<string>("Username")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.HasKey("Id");

            b.ToTable("Users");
        });
#pragma warning restore 612, 618
    }
}