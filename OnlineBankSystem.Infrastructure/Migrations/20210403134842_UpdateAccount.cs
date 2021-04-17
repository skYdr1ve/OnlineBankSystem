using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBankSystem.Infrastructure.Migrations
{
    public partial class UpdateAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Account_Id",
                table: "Card");

            migrationBuilder.CreateIndex(
                name: "IX_Card_AccountId",
                table: "Card",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Account_AccountId",
                table: "Card",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Account_AccountId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_AccountId",
                table: "Card");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Account_Id",
                table: "Card",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
