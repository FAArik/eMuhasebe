using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeApi.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class balance_removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceAmount",
                table: "CashRegisters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BalanceAmount",
                table: "CashRegisters",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
