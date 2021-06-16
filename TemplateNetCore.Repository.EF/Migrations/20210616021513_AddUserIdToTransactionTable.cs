using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemplateNetCore.Repository.EF.Migrations
{
    public partial class AddUserIdToTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "transaction",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_transaction_user_id",
                table: "transaction",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_user",
                table: "transaction",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transaction_user",
                table: "transaction");

            migrationBuilder.DropIndex(
                name: "IX_transaction_user_id",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "transaction");
        }
    }
}
