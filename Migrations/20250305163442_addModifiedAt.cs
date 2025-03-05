using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOTNETCoreWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class addModifiedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifieldAt",
                table: "TodoModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifieldAt",
                table: "TodoModel");
        }
    }
}
