﻿#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Core.Migrations;

public partial class IdentityKeyAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Users",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "(getdate())",
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AlterColumn<Guid>(
            name: "Id",
            table: "Users",
            type: "uniqueidentifier",
            nullable: false,
            defaultValueSql: "(newid())",
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Users",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldDefaultValueSql: "(getdate())");

        migrationBuilder.AlterColumn<Guid>(
            name: "Id",
            table: "Users",
            type: "uniqueidentifier",
            nullable: false,
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier",
            oldDefaultValueSql: "(newid())");
    }
}