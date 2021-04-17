using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBankSystem.Infrastructure.Migrations
{
    public partial class UpdateCardAndTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "Card",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardId",
                table: "Transaction",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Card_CardId",
                table: "Transaction",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Card_CardId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CardId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "Card");
        }
    }
}
