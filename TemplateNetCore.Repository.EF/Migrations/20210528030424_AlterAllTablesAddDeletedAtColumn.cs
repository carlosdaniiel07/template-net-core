using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemplateNetCore.Repository.EF.Migrations
{
    public partial class AlterAllTablesAddDeletedAtColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "user",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "transaction",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "user");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "transaction");
        }
    }
}
