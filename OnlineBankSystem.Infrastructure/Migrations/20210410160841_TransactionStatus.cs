using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBankSystem.Infrastructure.Migrations
{
    public partial class TransactionStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Transaction",
                newName: "StatusId");

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_StatusId",
                table: "Transaction",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TransactionStatus_StatusId",
                table: "Transaction",
                column: "StatusId",
                principalTable: "TransactionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TransactionStatus_StatusId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "TransactionStatus");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_StatusId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Transaction",
                newName: "Status");
        }
    }
}
