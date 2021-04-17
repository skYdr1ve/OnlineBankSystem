using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBankSystem.Infrastructure.Migrations
{
    public partial class CountryCurrencyCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Card_CardId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_CardId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Account");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "Transaction",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "FromCurrency",
                table: "Transaction",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToCurrency",
                table: "Transaction",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CountryCurrencyCode",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCurrencyCode", x => x.Number);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CurrencyId",
                table: "Account",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_CountryCurrencyCode_CurrencyId",
                table: "Account",
                column: "CurrencyId",
                principalTable: "CountryCurrencyCode",
                principalColumn: "Number");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Account_Id",
                table: "Card",
                column: "Id",
                principalTable: "Account",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_CountryCurrencyCode_CurrencyId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Account_Id",
                table: "Card");

            migrationBuilder.DropTable(
                name: "CountryCurrencyCode");

            migrationBuilder.DropIndex(
                name: "IX_Account_CurrencyId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "FromCurrency",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ToCurrency",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Account");

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Account_CardId",
                table: "Account",
                column: "CardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Card_CardId",
                table: "Account",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
