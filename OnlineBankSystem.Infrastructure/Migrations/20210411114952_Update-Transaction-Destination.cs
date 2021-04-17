using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineBankSystem.Infrastructure.Migrations
{
    public partial class UpdateTransactionDestination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Transaction");
        }
    }
}
