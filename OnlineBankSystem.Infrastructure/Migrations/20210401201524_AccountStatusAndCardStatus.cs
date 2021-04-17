using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBankSystem.Infrastructure.Migrations
{
    public partial class AccountStatusAndCardStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Card",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Account",
                newName: "StatusId");

            migrationBuilder.CreateTable(
                name: "AccountStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_StatusId",
                table: "Card",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_StatusId",
                table: "Account",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_AccountStatus_StatusId",
                table: "Account",
                column: "StatusId",
                principalTable: "AccountStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_CardStatus_StatusId",
                table: "Card",
                column: "StatusId",
                principalTable: "CardStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AccountStatus_StatusId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_CardStatus_StatusId",
                table: "Card");

            migrationBuilder.DropTable(
                name: "AccountStatus");

            migrationBuilder.DropTable(
                name: "CardStatus");

            migrationBuilder.DropIndex(
                name: "IX_Card_StatusId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Account_StatusId",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Card",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Account",
                newName: "Status");
        }
    }
}
